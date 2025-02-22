using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public DialogueContainer beginning;
    public DialogueContainer battleTutorial;
    public DialogueContainer knightBattle1;
    public DialogueContainer knightBattle2;

    private void Start() {
        DialogueManager.OnDialogueEnd += BeginningEnd;
        beginning.Play();
    }

    private void BeginningEnd()
    {
        //ensina o tutorial de movimentação
        Debug.Log(nameof(BeginningEnd));
        DialogueManager.OnDialogueEnd -= BeginningEnd;
        DialogueManager.OnDialogueEnd += BattleTutorialEnd;
        battleTutorial.Play();
    }

    private void BattleTutorialEnd()
    {
        // popa um boneco de feno e ensina o tutorial de batalha
        Debug.Log(nameof(BattleTutorialEnd));
        DialogueManager.OnDialogueEnd -= BattleTutorialEnd;
        DialogueManager.OnDialogueEnd += KnightBattle1End;
        knightBattle1.Play();
    }

    private void KnightBattle1End()
    {
        //popa um cavaleiro ENORME
        Debug.Log(nameof(KnightBattle1End));
        DialogueManager.OnDialogueEnd -= KnightBattle1End;
        DialogueManager.OnDialogueEnd += KnightBattle2End;
        knightBattle2.Play();
    }

    private void KnightBattle2End()
    {
        Debug.Log(nameof(KnightBattle2End));
        DialogueManager.OnDialogueEnd -= KnightBattle2End;
    }
}
