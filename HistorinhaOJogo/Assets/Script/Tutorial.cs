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
    public GameOverScreen gameOverScreen;

    public DialogueContainer beginning;
    public DialogueContainer battleTutorial;
    public DialogueContainer knightBattle1;
    public DialogueContainer knightBattle2;
    public DialogueContainer playerWin;

    public int etapaTutorial = 0;

    private void Start() {
        //Debug.Log("Não pode mover");
        player.GetComponent<Movimento>().podeMover = false;
        Debug.Log("começou o diálogo de intro");
        DialogueManager.OnDialogueEnd += BeginningEnd;
        
        
        paladino.SetActive(false);
        beginning.Play();
        etapaTutorial = 0;
    }

    private void Update() 
    {
        if( espantalho.IsDestroyed() && etapaTutorial == 2)
        {
            //se eu bater no espantalho andando, essa linhha trava meu controle 
            //sobre o boneco, mas o boneco permanece no último estado de movimento
            // que é andando, o que gera o bug de sair correndo do nada. Isso também
            //faz ele mudar de cena antes de chamar o paladino o que gera o bug de
            // gameobject destroyed, but still trying to access (o paladino é destruido
            //quando muda de cena forçado pelo bug).
            player.GetComponent<Movimento>().podeMover = false;
            knightBattle1.Play();
        }
        if( paladino.IsDestroyed() && etapaTutorial == 4)
        {
            playerWin.Play();
            etapaTutorial = 5;
        }
        if (player.GetComponent<Vida>().vida <= 0)
        {
            gameOverScreen.GameOver();
            DialogueManager.RequestEnd();
            gameObject.SetActive(false);
        }
    
    }

    private void BeginningEnd()
    {
        //ensina o tutorial de movimentação
        Debug.Log("termina a intro, n trava o player");
        etapaTutorial = 1;
        //Debug.Log(nameof(BeginningEnd));
        DialogueManager.OnDialogueEnd -= BeginningEnd;
        DialogueManager.OnDialogueEnd += BattleTutorialEnd;
        Debug.Log("Boneco volta se mexer");
        player.GetComponent<Movimento>().podeMover = true;
        //battleTutorial.Play();
    }

    private void BattleTutorialEnd()
    {
        //boneco de feno e ensina o tutorial de batalha
        etapaTutorial = 2;
        //Debug.Log(nameof(BattleTutorialEnd));
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
        player.GetComponent<Movimento>().podeMover = true;
        paredeInvisivel.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.);
        if (col.gameObject.tag == "Player" && etapaTutorial == 1)   
        {
           battleTutorial.Play();
        }
    }
}
