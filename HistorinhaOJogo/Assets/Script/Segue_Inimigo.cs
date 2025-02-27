// using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Segue_Inimigo : MonoBehaviour
{
    private Transform posPlayer; //posição jogador
    public GameObject player;
    private Animator anim;
    public float enemyVel; //velocidade com que o inimigo segue
    public float enemyRange; //range do inimigo
    public bool canWalk; //pode perseguir ou não
    public SpriteRenderer sprite;

    void Start()
    {
        canWalk = true;
        anim = gameObject.GetComponent<Animator>();
        posPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        Debug.Log(canWalk);

        if (player.transform.position.x > transform.position.x)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;

        }

        PlayerFollow();    
    }

    private void PlayerFollow()
    {
        float distance = Vector2.Distance(transform.position, posPlayer.position);

        if(canWalk)
        {
            anim.SetBool("moving",true);
            //faz o inimigo se mover na direção do player
            //PRECISA aplicar LINEAR DRAG no rigidbody do inimigo, pois isso evita que
            //a velocidade de repulsão da colisão entre o inimigo e o player seja maior que a
            //velocidade de atração, bugando o movimento do inimigo.
            transform.position = Vector2.MoveTowards(transform.position, posPlayer.position, enemyVel * Time.deltaTime);

        }
        if(distance < enemyRange)
        {
            this.GetComponent<Atk_paladinho>().AtkPlayer();
        }


    } 

}
