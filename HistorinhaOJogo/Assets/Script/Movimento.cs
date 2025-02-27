using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject espada;
    [SerializeField] private GameObject escudo;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    private Rigidbody2D _playerRigidbody;
    public bool IsGrounded = true;
    public bool def = false;
    public bool podeMover = true;
    private void Start()
    {   
        //Debug.Log("pode mover");
        podeMover = true;
        //animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
        animator.SetBool("mov",false);
    }
    private void Update()
    {
        Debug.Log(podeMover);
        if (podeMover == true)
        {
            MovePlayer();
            if (Input.GetButtonDown("Jump") && IsGrounded)
            { 
                Jump();
                IsGrounded = false;
            }
            if (Input.GetKeyDown(KeyCode.J) || Input.GetKeyDown(KeyCode.Z))
            { 
                Ataque();
                animator.Play("Ataque");
                Ataque();
            }
        }
        animator.SetBool("cair",!IsGrounded);
    }
    private void FixedUpdate()
    {
        _playerRigidbody.velocity = Vector2.ClampMagnitude(_playerRigidbody.velocity, 30);

    }
    private void MovePlayer()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        
        if (horizontalInput != 0)
        {
            animator.SetBool("mov",true);
        }
        else {animator.SetBool("mov",false); }

        if (horizontalInput == -1)
        {
            sprite.flipX = true;
        }
        else if (horizontalInput == 1)
        {
            sprite.flipX = false;
        }

        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);

        if (horizontalInput != 0)
        {
            espada.transform.position = new Vector2(this.transform.localPosition.x + horizontalInput * 1.6f,espada.transform.position.y);
            escudo.transform.position = new Vector2(this.transform.localPosition.x + horizontalInput,espada.transform.position.y);
        }
    }
    private void Jump() => _playerRigidbody.AddForce( new Vector2( 0, jumpPower),  ForceMode2D.Impulse);

    private void Ataque()
    {
        espada.gameObject.SetActive(true);
    }

    void OnCollisionEnter2D(Collision2D col)
    {   
        
        if (col.gameObject.tag == "Chao")
        {
            IsGrounded = true;
        }

        Collider2D myCollider = col.GetContact(0).collider;

        if (myCollider.gameObject.tag == "Alvo")   
        {
            if (_playerRigidbody.velocity.y < 25)
            {
                _playerRigidbody.AddForce( new Vector2( 0, jumpPower),  ForceMode2D.Impulse);
            }
           col.gameObject.GetComponent<Mov_inimigo>().vida -= 100;
        }

        //if (col.gameObject.tag == "")
    }

}
