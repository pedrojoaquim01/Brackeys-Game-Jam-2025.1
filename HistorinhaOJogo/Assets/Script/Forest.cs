using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public GameObject player;
    public GameObject gnomo;
    public GameObject druida;
    public GameObject golem;
    public GameObject paredeInvisivel;
    public GameOverScreen gameOverScreen;
    public GameObject tpIn;
    public GameObject tpOut;


    public DialogueContainer beginning;
    public DialogueContainer teleportBack;
    public DialogueContainer cenario2;
    public DialogueContainer startBossBattle;
    public DialogueContainer startBossBattleFase2;
    public DialogueContainer playerWin;
    public DialogueContainer playerLose;

    public int etapaForest = 0;

    private void Start() {
        player.GetComponent<Movimento>().podeMover = false;
        gnomo.SetActive(false);
        druida.SetActive(false);
        golem.SetActive(false);
        DialogueManager.OnDialogueEnd += BeginningEnd;
        beginning.Play();
    }


    public void TransitionScenes()
    {
        if(etapaForest == 2)
        {
            StartCenario2();
        }
        else if(etapaForest == 3)
        {
            StartCenario3();
        }

    }

    private void BeginningEnd()
    {
        //transportar o jogador para a saída da floresta
        DialogueManager.OnDialogueEnd -= BeginningEnd;
        DialogueManager.OnDialogueEnd += TeleportBackEnd;
        player.transform.position = tpOut.transform.position;
        etapaForest = 1;
        teleportBack.Play();
    }

    private void TeleportBackEnd()
    {
        //jogador é teletransportado para a entrada novamente. a floresta tem os gnomos como inimigos comuns agora.
        DialogueManager.OnDialogueEnd -= TeleportBackEnd;
        player.transform.position = tpIn.transform.position;
        gnomo.SetActive(true);
        player.GetComponent<Movimento>().podeMover = true;
        etapaForest = 2;
        //StartCenario2();
    }

    private void StartCenario2()
    {
        DialogueManager.OnDialogueEnd += EndCenario2;
        druida.SetActive(true);
        cenario2.Play();
    }

    private void EndCenario2()
    {
        //popa o druida
        DialogueManager.OnDialogueEnd -= EndCenario2;
        etapaForest = 3;
        //StartCenario3();
    }

    private void StartCenario3()
    {
        DialogueManager.OnDialogueEnd += StartBossBattle;
        golem.SetActive(true);
        startBossBattle.Play();
    }

    private void StartBossBattle()
    {
        //primeira fase da luta contra o Golem
        DialogueManager.OnDialogueEnd -= StartBossBattle;
        BossBattle2();
    }

    private void BossBattle2()
    {
        DialogueManager.OnDialogueEnd += StartBossBattleFase2;
        startBossBattleFase2.Play();
    }

    private void StartBossBattleFase2()
    {
        DialogueManager.OnDialogueEnd -= StartBossBattleFase2;
        //segunda fase da luta contra o Golem
    }
}
