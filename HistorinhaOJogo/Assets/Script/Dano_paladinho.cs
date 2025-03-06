using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dano_paladinho : MonoBehaviour
{
    [SerializeField] private GameObject paladinho;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {   
            int lado;
            Rigidbody2D rbInimigo = col.gameObject.GetComponent<Rigidbody2D>();
            if (col.gameObject.transform.position.x > paladinho.transform.position.x)
            {
                lado = 1;
            }
            else { lado = -1;}

            col.gameObject.GetComponent<Vida>().vida -= 20;
            //col.gameObject.GetComponent<Mov_inimigo>().agir = false;
            
            //col.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(10 * lado,6);
            rbInimigo.velocity = Vector2.zero;
            rbInimigo.AddForce(new Vector2(3 * lado * rbInimigo.mass,3 * Mathf.Max(rbInimigo.mass,1)),  ForceMode2D.Impulse);
        }

        this.gameObject.SetActive(false);
    }

}
