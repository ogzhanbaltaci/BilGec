using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] public float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    Vector2 moveInput;
    Rigidbody2D myRigidbody;
    public Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isGrounded;
    //bool playerHasHorizantalSpeed;
    float gravityScaleAtStart;
    public Quiz quiz;
    public Health enemyHealth;
    public Health playerHealth;
    public EnemyMovement enemyMovement;
    public Animator enemyAnimator;
    public Slider enemyHealthBar;
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
        playerHealth = GetComponent<Health>();
        gravityScaleAtStart = myRigidbody.gravityScale;
        quiz.gameObject.SetActive(false);
    }

    void Update()
    {
        Run();
        FlipSprite();
        ClimbLadder();
        Die();
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
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.gameObject.GetComponent<Health>();
            enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyAnimator = other.gameObject.GetComponent<Animator>();
            enemyHealthBar = other.gameObject.GetComponentInChildren<Slider>();
            enemyHealthBar.value = enemyHealth.health;
            quiz.isActive = true;
            quiz.gameObject.SetActive(true);
            quiz.inQuiz = true;
            Debug.Log(quiz.inQuiz);
            if(quiz.inQuiz == true)
                enemyMovement.gameObject.GetComponent<EnemyMovement>().EnemyIdle();
        }
    }
    void OnCollisionExit2D(Collision2D Collider)
    {
        if (Collider.gameObject.CompareTag("Ground") && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = false;
        }
    }
    void Die()
    {
       if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards"))) 
       {
            myAnimator.SetTrigger("isHurt");
            myRigidbody.velocity = deathKick;
            //FindObjectOfType<GameSession>().ProcessPlayerDeath();
       }
    } 
}
