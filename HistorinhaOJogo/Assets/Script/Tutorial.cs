using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using Unity.VisualScripting;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject player;
    public GameObject espantalho;
    public GameObject paladino;
    public GameObject paredeInvisivel;

    public DialogueContainer beginning;
    public DialogueContainer battleTutorial;
    public DialogueContainer knightBattle1;
    public DialogueContainer knightBattle2;
    public DialogueContainer playerWin;

    public int etapaTutorial = 0;

    private void Start() {
        DialogueManager.OnDialogueEnd += BeginningEnd;
        player.GetComponent<Movimento>().SetPodeMover(false);
        paladino.SetActive(false);
        beginning.Play();
        etapaTutorial = 0;
    }

    private void Update() 
    {
        if( espantalho.IsDestroyed() && etapaTutorial == 2)
        {
            player.GetComponent<Movimento>().SetPodeMover(false);
            knightBattle1.Play();
        }
        if( paladino.IsDestroyed() && etapaTutorial == 4)
        {
            playerWin.Play();
            etapaTutorial = 5;
        }
    
    }

    private void BeginningEnd()
    {
        //ensina o tutorial de movimentação
        etapaTutorial = 1;
        Debug.Log(nameof(BeginningEnd));
        DialogueManager.OnDialogueEnd -= BeginningEnd;
        DialogueManager.OnDialogueEnd += BattleTutorialEnd;
        player.GetComponent<Movimento>().SetPodeMover(true);
        //battleTutorial.Play();
    }

    private void BattleTutorialEnd()
    {
        // popa um boneco de feno e ensina o tutorial de batalha
        etapaTutorial = 2;
        Debug.Log(nameof(BattleTutorialEnd));
        DialogueManager.OnDialogueEnd -= BattleTutorialEnd;
        DialogueManager.OnDialogueEnd += KnightBattle1End;
    }

    private void KnightBattle1End()
    {
        //popa um cavaleiro ENORME
        etapaTutorial = 3;
        paladino.SetActive(true);
        Debug.Log(nameof(KnightBattle1End));
        DialogueManager.OnDialogueEnd -= KnightBattle1End;
        DialogueManager.OnDialogueEnd += KnightBattle2End;
        knightBattle2.Play();
    }

    private void KnightBattle2End()
    {
        etapaTutorial = 4;
        Debug.Log(nameof(KnightBattle2End));
        DialogueManager.OnDialogueEnd -= KnightBattle2End;
        player.GetComponent<Movimento>().SetPodeMover(true);
        paredeInvisivel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player" && etapaTutorial == 1)   
        {
           battleTutorial.Play();
        }
    }
}
