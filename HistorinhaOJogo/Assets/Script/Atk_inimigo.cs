using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Atk_inimigo : MonoBehaviour
{
    public int dano = 25;
    private int animLayer = 0;
    private int lado = 0;
    private Animator anim;
    public GameObject espada;

    public float timer = 0;

    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        espada.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= 4)
        {
            anim.SetBool("atk",true);
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        if (isPlaying(anim, "ataque"))
        {
            espada.SetActive(true);
            anim.SetBool("atk",false);
        }
        else
        {
            espada.SetActive(false);
        }
    }

    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")   
        {
           Rigidbody2D rbplayer = col.gameObject.GetComponent<Rigidbody2D>();
            if (col.gameObject.transform.position.x < this.transform.position.x)
            {
                lado = 1;
            }
            else { lado = -1;}

            col.gameObject.GetComponent<Vida>().vida -= dano;
            
            rbplayer.velocity = Vector2.zero;
            rbplayer.AddForce(new Vector2(40 * lado ,10),  ForceMode2D.Impulse);

        }
    }
}
