using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_inimigo : MonoBehaviour
{
    //int lado = -1;
    private Rigidbody2D _InimRigidbody;
    private SpriteRenderer _inimSprite;
    //
    private Transform playerPosicao;

 
    private Animator anim;
    //
    public bool agir;
    public float velocidade;
    public float velocidadeMod = 1f;
    public GameObject player;

    public float vida = 100;
    public Constants.EnemyGroup EnemyGroup = Constants.EnemyGroup.DEFAULT;

    // Start is called before the first frame update
    void Start()
    {
        _InimRigidbody = GetComponent<Rigidbody2D>();
        _inimSprite = GetComponent<SpriteRenderer>();
        playerPosicao = GameObject.FindGameObjectWithTag("Player").transform;    
       // anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        

         if (agir)
         {
            SeguirPlayer();
        //     _InimRigidbody.velocity = new Vector2( lado * (velocidade * velocidadeMod), _InimRigidbody.velocity.y);
        //     //anim.SetBool("moving",true);
            
         }

        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }
    // void OnTriggerEnter2D(Collider2D col)
    // {
    //     if (col.gameObject.tag == "Chao" || col.gameObject.tag == "Inimigo" )   
    //     {
    //        lado = lado * -1;
    //        _inimSprite.flipX = !_inimSprite.flipX;
    //     }
    // }
    void OnCollisionEnter2D(Collision2D col)
    {   
        if (col.gameObject.tag == "Chao")
        {
           agir = true;
        }
    }
    void OnCollisionExit2D(Collision2D col)
    {   
        if (col.gameObject.tag == "Chao")
        {
           agir = false;
        }
    }

    private void SeguirPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position,playerPosicao.position,velocidade * Time.deltaTime);


    }

}

    