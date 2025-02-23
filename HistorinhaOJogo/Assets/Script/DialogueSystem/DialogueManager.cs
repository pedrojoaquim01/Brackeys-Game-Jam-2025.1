using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour txtPanelRight;
        [SerializeField] private TextMeshProUGUI txtNameRight;
        [SerializeField] private TextMeshProUGUI txtTextRight;
        [SerializeField] private MonoBehaviour txtPanelLeft;
        [SerializeField] private TextMeshProUGUI txtNameLeft;
        [SerializeField] private TextMeshProUGUI txtTextLeft;
        private IReadOnlyList<Dialogue> currentDialogue;
        private int index;
        private PanelGroup currentPanel => panels[currentDialogue[index].PanelSide];
        private bool textEnded => currentPanel.txtText.text == currentDialogue[index].Text;
        public bool IsPlaying => currentDialogue is not null;
        private Dictionary<Dialogue.DialoguePanelSide, PanelGroup> panels;

        public static DialogueManager Instance
        {
            get {
                instance = instance != null ? instance : FindAnyObjectByType<DialogueManager>(FindObjectsInactive.Include);
                return instance;
            }
        }
        private static DialogueManager instance;

        public delegate void DialogueStart();
        public static event DialogueStart OnDialogueStart;
        public delegate void DialogueEnd();
        public static event DialogueEnd OnDialogueEnd;
        public delegate void DialogueChanged(int index, Dialogue dialogue);
        public static event DialogueChanged OnDialogueChanged;

        private void Start() {
            gameObject.SetActive(false);
            panels = new(){
                {Dialogue.DialoguePanelSide.LEFT, new(txtNameLeft, txtTextLeft, txtPanelLeft)},
                {Dialogue.DialoguePanelSide.RIGHT, new(txtNameRight, txtTextRight, txtPanelRight)}
            };
        }

        private void Update() {
            HandleInput();
        }

        private void HandleInput()
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
                if (textEnded)
                    ShowNext();
                else
                {
                    StopAllCoroutines();
                    currentPanel.txtText.text = currentDialogue[index].Text;
                }
        }

        public static void Play(IReadOnlyList<Dialogue> dialogues, int startFrom = 0) => Instance.Play_Internal(dialogues, startFrom);

        private void Play_Internal(IReadOnlyList<Dialogue> dialogues, int startFrom)
        {
            if (IsPlaying)
                return;
            OnDialogueStart?.Invoke();
            currentDialogue = dialogues;
            index = startFrom;
            gameObject.SetActive(true);
            StartCoroutine(ShowDialogue(dialogues[index]));
        }

        public static void RequestEnd() => Instance.RequestEnd_Internal();
        public void RequestEnd_Internal()
        {
            if (!IsPlaying) 
                return;
            End();
        }

        private IEnumerator ShowDialogue(Dialogue d)
        {
            var panelData = currentPanel.Show();
            panelData.txtName.text = d.Name;
            panelData.txtText.text = string.Empty;
            OnDialogueChanged?.Invoke(index, d);
            foreach (var c in d.Text.ToCharArray())
            {
                panelData.txtText.text += c;
                if (textEnded && d.IsAutoAdvance)
                    Invoke(nameof(ShowNext), d.AutoAvanceTime);
                yield return new WaitForSeconds(d.SpeechSpeed);
            };
        }

        private void ShowNext()
        {
            if (index >= currentDialogue?.Count - 1)
            {
                End();
                return;
            }
            currentPanel.Hide();
            StartCoroutine(ShowDialogue(currentDialogue[++index]));
        }

        private void End()
        {
            gameObject.SetActive(false);
            currentDialogue = null;
            OnDialogueEnd?.Invoke();
        }

        private class PanelGroup
        {
            public PanelGroup(TextMeshProUGUI txtName, TextMeshProUGUI txtText, MonoBehaviour panel)
            {
                this.txtName = txtName;
                this.txtText = txtText;
                this.panel = panel;
                Hide();
            }

            public TextMeshProUGUI txtName { get; internal set; }
            public TextMeshProUGUI txtText { get; internal set; }
            public MonoBehaviour panel { get; internal set; }

            internal PanelGroup Hide()
            {
                panel.gameObject.SetActive(false);
                return this;
            }

            internal PanelGroup Show()
            {
                panel.gameObject.SetActive(true);
                return this;
            }
        }
    }
}
