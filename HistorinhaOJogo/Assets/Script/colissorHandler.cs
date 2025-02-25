using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class colissorHandler : MonoBehaviour
{
    public UnityEvent Collider;
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.PLAYER_TAG)
        {
            Collider.Invoke();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("saiu");
    }


}
