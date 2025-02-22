using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSegue : MonoBehaviour
{

    private Vector2 velocity;
    public float MovimentaX;
    public float MovimentaY;
    public GameObject player;

    public bool Limite;
    public Vector3 MaxCamera;
    public Vector3 MinCamera;

    
    public float offsetX;
    public float offsetY;


    // Use this for initialization
    void Start()
    {

        //player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, MovimentaX);
        float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, MovimentaY);

        transform.position = new Vector3(posX + offsetX, posY + offsetY, transform.position.z);

        if (Limite)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, MinCamera.x, MaxCamera.x),
                Mathf.Clamp(transform.position.y, MinCamera.y, MaxCamera.y),
                Mathf.Clamp(transform.position.z, MinCamera.z, MaxCamera.z));
        }

    }
}
