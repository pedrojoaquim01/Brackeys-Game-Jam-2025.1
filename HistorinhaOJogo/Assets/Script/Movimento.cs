using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimento : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 5.0f;
    [SerializeField] private float jumpPower = 5.0f;
    [SerializeField] private GameObject espada;
    [SerializeField] private GameObject escudo;
    Animator animator;

    private Rigidbody2D _playerRigidbody;
    public bool IsGrounded = true;
    public bool def = false;
    private void Start()
    {   
        animator = GetComponent<Animator>();
        _playerRigidbody = GetComponent<Rigidbody2D>();
        if (_playerRigidbody == null)
        {
            Debug.LogError("Player is missing a Rigidbody2D component");
        }
    }
    private void Update()
    {
        MovePlayer();

        if (Input.GetButtonDown("Jump") && IsGrounded)
        { 
            Jump();
            IsGrounded = false;
        }

        _playerRigidbody.velocity = Vector2.ClampMagnitude(_playerRigidbody.velocity, 20);
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
            this.GetComponent<SpriteRenderer>().flipX = true;
            
        }
        else if (horizontalInput == 1)
        {
            this.GetComponent<SpriteRenderer>().flipX = false;
        }

        _playerRigidbody.velocity = new Vector2(horizontalInput * playerSpeed, _playerRigidbody.velocity.y);

        if (horizontalInput != 0)
        {
            espada.transform.position = new Vector2(this.transform.localPosition.x + horizontalInput,espada.transform.position.y);
            escudo.transform.position = new Vector2(this.transform.localPosition.x + horizontalInput,espada.transform.position.y);
        }
    }
    private void Jump() => _playerRigidbody.AddForce( new Vector2( 0, jumpPower),  ForceMode2D.Impulse);

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Chao")
        {
            IsGrounded = true;
        }
    }
}
