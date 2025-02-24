using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    public class DialogueManager : MonoBehaviour
    {
        public PanelGroup leftGroup;
        public PanelGroup rightGroup;
        private IReadOnlyList<Dialogue> currentDialogue;
        private int index;
        private PanelGroup currentPanel;
        private bool textEnded => currentPanel.txtText.text == currentDialogue[index].Text;
        public bool IsPlaying => currentDialogue is not null;

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

        private void Awake() {
            gameObject.SetActive(false);
        }

        // void Start()
        // {
        //     leftGroup.Hide();
        //     rightGroup.Hide();
        // }

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
            
            rightGroup.Hide();
            leftGroup.Hide();
            gameObject.SetActive(true);
            OnDialogueStart?.Invoke();
            currentDialogue = dialogues;
            index = startFrom;
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
            Debug.Log("DialogueManager ShowDialogue called.");
            currentPanel = d.PanelSide == Dialogue.DialoguePanelSide.RIGHT ? rightGroup.Show() : leftGroup.Show();
            currentPanel.txtName.text = d.Name;
            currentPanel.txtText.text = string.Empty;
            OnDialogueChanged?.Invoke(index, d);
            foreach (var c in d.Text.ToCharArray())
            {
                currentPanel.txtText.text += c;
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
            rightGroup.Hide();
            leftGroup.Hide();
            currentDialogue = null;
            gameObject.SetActive(false);
            OnDialogueEnd?.Invoke();
        }
        [Serializable]
        public class PanelGroup
        {
            [SerializeField] public Dialogue.DialoguePanelSide panelSide;// { get; private set; }        
            [SerializeField] public MonoBehaviour panel;// { get; private set; }
            [SerializeField] public TextMeshProUGUI txtName;// { get; private set; }
            [SerializeField] public TextMeshProUGUI txtText;// { get; private set; }

            public PanelGroup(){}
            public PanelGroup(MonoBehaviour panel, TextMeshProUGUI txtName, TextMeshProUGUI txtText)
            {
                this.panel = panel;
                this.txtName = txtName;
                this.txtText = txtText;
            }

            public PanelGroup Hide()
            {
                panel.gameObject.SetActive(false);
                return this;
            }

            public PanelGroup Show()
            {
                panel.gameObject.SetActive(true);
                return this;
            }
        }
    }
}
