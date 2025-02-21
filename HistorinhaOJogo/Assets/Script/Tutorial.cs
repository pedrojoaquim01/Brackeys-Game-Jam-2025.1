using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public DialogueContainer beginning;

    private void Start() {
        beginning.Play();
    }
}
