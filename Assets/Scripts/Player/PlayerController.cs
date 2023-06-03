using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] CompositeCollider2D platformsCompCollider;
    [SerializeField] TilemapCollider2D hazardsTilemapCollider;
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    public Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isGrounded;
    //bool playerHasHorizantalSpeed;
    float gravityScaleAtStart;
    public Quiz quiz;
    [SerializeField] public Health playerHealth;
    private Vector3 respawnPoint;
    public GameObject fallDetector;

    void Awake() 
    {
        quiz = FindObjectOfType<Quiz>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
        gravityScaleAtStart = myRigidbody.gravityScale;
        Debug.Log("girdi");
        quiz.gameObject.SetActive(false);
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        //Die();
    }

    void OnMove(InputValue value)
    {
        if(quiz.isActive == true)
        { 
            runSpeed = 0f;
        }
        moveInput = value.Get<Vector2>();  
    }
    void OnJump(InputValue value)
    {
        if(quiz.isActive == true)
        { 
            return;
        }
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
            return;
        }
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            return;
        }
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))){
            return;
        }
        if(value.isPressed && isGrounded)
        {
            myRigidbody.velocity += new Vector2 (0f, jumpSpeed);
        }
        
        myAnimator.SetBool("isJumping", true);
        
    }
    void Run()
    {
        Vector2 playerVelocity = new Vector2 (moveInput.x * runSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;

        bool playerHasHorizantalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizantalSpeed);

    }
    void FlipSprite()
    {
        bool playerHasHorizantalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        
        if(playerHasHorizantalSpeed)
        {
            transform.localScale = new Vector2 (Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
        
    }
    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0;
        bool playerHasVerticalSpeed = Mathf.Abs(myRigidbody.velocity.y) > Mathf.Epsilon;
        myAnimator.SetBool("isClimbing", playerHasVerticalSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            myAnimator.SetBool("isJumping", false);
        }
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Ladder"))
        {
            myAnimator.SetBool("isClimbing", true);
            myAnimator.SetBool("isJumping", false);
        }
        else if(other.tag == "FallDetector")
        {
            platformsCompCollider.isTrigger = false;
            hazardsTilemapCollider.enabled = true;
            myAnimator.SetBool("isHurt", false);
            runSpeed = 10f;
            transform.position = respawnPoint;
        }
        else if(other.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if(other.tag == "Hazards")
        {
            myAnimator.SetBool("isHurt", true);
            playerHealth.health -= 10;
            runSpeed = 0f;
            myAnimator.SetBool("isJumping", false);
            myRigidbody.velocity = deathKick;
            hazardsTilemapCollider.enabled = false;
            platformsCompCollider.isTrigger = true;
        }
    }
    void OnCollisionExit2D(Collision2D Collider)
    {
        if (Collider.gameObject.CompareTag("Ground") && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = false;
        }
    }
    /*void Die()
    {
       if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) 
       {
            myAnimator.SetTrigger("isHurt");
            myRigidbody.velocity = deathKick;
            FindObjectOfType<GameSession>().ProcessPlayerDeath();
       }
    } */
}
