using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.Tilemaps;
public class PlayerMobileMovement : MonoBehaviour
{
    private float inputDirectionHorizantal;
    private float inputDirectionVertical;
    public float runSpeed;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float climbSpeed = 5f;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);
    [SerializeField] CompositeCollider2D platformsCompCollider;
    [SerializeField] TilemapCollider2D hazardsTilemapCollider;
    Vector2 moveInput;
    public Animator myAnimator;
    CapsuleCollider2D myBodyCollider;
    BoxCollider2D myFeetCollider;
    bool isGrounded;
    float gravityScaleAtStart;
    public Quiz quiz;
    [SerializeField] public Health playerHealth;
    private Vector3 respawnPoint;
    bool isTriggered;
    AudioPlayer audioPlayer;
    private Rigidbody2D myRigidbody;
    SpriteRenderer mySpriteRenderer;
    public bool isFalling;
    int animLayer = 0;
    void Awake() 
    {
        quiz = FindObjectOfType<Quiz>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        hazardsTilemapCollider = FindObjectOfType<TilemapCollider2D>();
        platformsCompCollider = FindObjectOfType<CompositeCollider2D>();
    }
    void Start()
    {
        playerHealth = GetComponent<Health>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        respawnPoint = transform.position;
        gravityScaleAtStart = myRigidbody.gravityScale;
        quiz.gameObject.SetActive(false);
    }
    void Update()
    {
        CheckInput();
        ClimbLadder();
        if(quiz.isActive != true && isPlaying(myAnimator, "isHurt"))
            runSpeed = 7f;
    }
    private void FixedUpdate() 
    {
        Movement();
    }
    private void CheckInput()
    {
        inputDirectionHorizantal = CrossPlatformInputManager.GetAxis("Horizontal");
        inputDirectionVertical = CrossPlatformInputManager.GetAxis("Vertical");
        if(quiz.isActive == true)
        { 
            runSpeed = 0f;
        }
        if(inputDirectionHorizantal > 0)
        {
            mySpriteRenderer.flipX = false;
        }
        else if(inputDirectionHorizantal < 0)
        {
            mySpriteRenderer.flipX = true;
        }
        if(CrossPlatformInputManager.GetButtonDown("Jump")
            && isGrounded == true 
            && quiz.isActive != true
            && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))
            && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))
            && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Hazards")))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpSpeed);
            audioPlayer.PlayJumpClip();
            myAnimator.SetBool("isJumping", true);
        }
    }
    private void Movement()
    {
        myRigidbody.velocity = new Vector2(runSpeed * inputDirectionHorizantal, myRigidbody.velocity.y);
        bool playerHasHorizantalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("isRunning", playerHasHorizantalSpeed);
    }
    void ClimbLadder()
    {
        if(!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")) || quiz.isActive == true){
            myRigidbody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("isClimbing", false);
            return;
        }
        Vector2 climbVelocity = new Vector2 (myRigidbody.velocity.x, inputDirectionVertical * climbSpeed);
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
            runSpeed = 7f;
            transform.position = respawnPoint;
            isFalling = false;
        }
        else if(other.tag == "Checkpoint")
        {
            respawnPoint = transform.position;
        }
        else if(other.tag == "Hazards")
        {
            if(isTriggered == false)
            {
                isFalling = true;
                myAnimator.SetBool("isHurt", true);
                playerHealth.TakeDamage(10);
                runSpeed = 0f;
                myAnimator.SetBool("isJumping", false);
                myRigidbody.velocity = deathKick;
                hazardsTilemapCollider.enabled = false;
                platformsCompCollider.isTrigger = true;
                isTriggered = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D Collider)
    {
        if (Collider.gameObject.CompareTag("Ground") && !myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            isGrounded = false;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        isTriggered = false;
    }
    bool isPlaying(Animator anim, string stateName)
    {
        if (anim.GetCurrentAnimatorStateInfo(animLayer).IsName(stateName) &&
                anim.GetCurrentAnimatorStateInfo(animLayer).normalizedTime < 1.0f)
            return true;
        else
            return false;
    }

}
