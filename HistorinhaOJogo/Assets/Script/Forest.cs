using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Script.DialogueSystem;
using UnityEngine;

public class Forest : MonoBehaviour
{
    public DialogueContainer beginning;
    public DialogueContainer teleportBack;
    public DialogueContainer cenario2;
    public DialogueContainer startBossBattle;
    public DialogueContainer startBossBattleFase2;
    public DialogueContainer playerWin;
    public DialogueContainer playerLose;

    private void Start() {
        DialogueManager.OnDialogueEnd += BeginningEnd;
        beginning.Play();
    }

    private void BeginningEnd()
    {
        //transportar o jogador para a saída da floresta
        DialogueManager.OnDialogueEnd -= BeginningEnd;
        DialogueManager.OnDialogueEnd += TeleportBackEnd;
        teleportBack.Play();
    }

    private void TeleportBackEnd()
    {
        //jogador é teletransportado para a entrada novamente. a floresta tem os gnomos como inimigos comuns agora.
        DialogueManager.OnDialogueEnd -= TeleportBackEnd;
        StartCenario2();
    }

    private void StartCenario2()
    {
        DialogueManager.OnDialogueEnd += EndCenario2;
        cenario2.Play();
    }

    private void EndCenario2()
    {
        //popa o druida
        DialogueManager.OnDialogueEnd -= EndCenario2;
        StartCenario3();
    }

    private void StartCenario3()
    {
        DialogueManager.OnDialogueEnd += StartBossBattle;
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
