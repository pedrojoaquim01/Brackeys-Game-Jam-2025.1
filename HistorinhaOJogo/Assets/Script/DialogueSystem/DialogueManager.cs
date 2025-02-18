using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI txtName;
        [SerializeField] private TextMeshProUGUI txtText;
        private IReadOnlyList<Dialogue> currentDialogue;
        private int index;
        private bool textEnded => txtText.text == currentDialogue[index].Text;

        public bool IsPlaying => currentDialogue is not null;

        public delegate void DialogueStart();
        public static event DialogueStart OnDialogueStart;
        public delegate void DialogueEnd();
        public static event DialogueEnd OnDialogueEnd;
        public delegate void DialogueChanged(int index, Dialogue dialogue);
        public static event DialogueChanged OnDialogueChanged;

        private void Start() {
            gameObject.SetActive(false);
        }

        private void Update() {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0))
                if (textEnded)
                    ShowNext();
                else
                {
                    StopAllCoroutines();
                    txtText.text = currentDialogue[index].Text;
                }
        }

        public void Play(IReadOnlyList<Dialogue> dialogues, int startFrom = 0)
        {
            if (IsPlaying)
                return;
            OnDialogueStart?.Invoke();
            currentDialogue = dialogues;
            index = startFrom;
            gameObject.SetActive(true);
            StartCoroutine(ShowDialogue(dialogues[index]));
        }

        private IEnumerator ShowDialogue(Dialogue d)
        {
            txtName.text = d.Name;
            txtText.text = string.Empty;
            OnDialogueChanged?.Invoke(index, d);
            foreach (var c in d.Text.ToCharArray())
            {
                txtText.text += c;
                if (textEnded && d.IsAutoAdvance)
                    Invoke(nameof(ShowNext), d.AutoAvanceTime);
                yield return new WaitForSeconds(d.SpeechSpeed);
            };
        }

        private void ShowNext()
        {
            if (index >= currentDialogue.Count - 1)
            {
                End();
                return;
            }
            index++;
            StartCoroutine(ShowDialogue(currentDialogue[index]));
        }

        private void End()
        {
            gameObject.SetActive(false);
            currentDialogue = null;
            OnDialogueEnd?.Invoke();
        }
    }
}
