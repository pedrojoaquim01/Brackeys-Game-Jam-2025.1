using System;
using UnityEngine;

namespace Assets.Script.DialogueSystem
{
    public class DialogueContainer : MonoBehaviour {

        [field:SerializeField] public Dialogue[] Dialogs { get; private set; }
        
        public void Play(){
            DialogueManager.Play(Dialogs);
        }
    }
}