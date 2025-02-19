using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    public class DialogueContainer : MonoBehaviour {

        [field:SerializeField] public Dialogue[] Dialogs { get; private set; }
        private DialogueManager manager;

        private void Start() {
            manager = FindAnyObjectByType<DialogueManager>();
        }

        public void Play(){
            manager.Play(Dialogs);
        }
    }
}