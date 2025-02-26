using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida_inimigo : MonoBehaviour
{

    //
    private Animator anim;
    //

    public float vida = 100;
    public Constants.EnemyGroup EnemyGroup = Constants.EnemyGroup.DEFAULT;

    // Start is called before the first frame update
    void Start()
    {
        // _InimRigidbody = GetComponent<Rigidbody2D>();
        // _inimSprite = GetComponent<SpriteRenderer>();
        // anim = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if(vida <= 0)
        {
            Destroy(gameObject);
        }
    }


}
