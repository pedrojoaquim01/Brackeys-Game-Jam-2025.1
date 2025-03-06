using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Atk_paladinho : MonoBehaviour
{
    public GameObject inimigo;
    public int dano = 25;
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

    }

    public void AtkPlayer()
    {
         StartCoroutine(Ataque());    
    }

    IEnumerator Ataque()
    {
        inimigo.GetComponent<Segue_Inimigo>().canWalk = false;
        //Debug.Log("Parou de andar");
        anim.SetBool("atk",true); //inicia anim de atk
        yield return new WaitForSeconds(1.10f);
        espada.SetActive(true); //espadada
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("atk", false);
        espada.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        inimigo.GetComponent<Segue_Inimigo>().canWalk = true;

    }
}