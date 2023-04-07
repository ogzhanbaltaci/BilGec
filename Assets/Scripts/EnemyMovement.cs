using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D enemyFootCollider;
    Animator enemyAnimator;
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyFootCollider = GetComponent<BoxCollider2D>();
        enemyAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f) ;
        /*if(FindObjectOfType<Question>().isActive == true)
        {
            moveSpeed = 0f;
            enemyAnimator.SetBool("isIdleing", true);
        }*/
    }
    void OnTriggerExit2D(Collider2D other) 
    {
        if(!enemyFootCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();
        }
        
    }
    void FlipEnemyFacing()
    {
        transform.localScale = new Vector2 (-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
    public void EnemyIdle()
    {
        moveSpeed = 0f;
        enemyAnimator.SetBool("isIdling", true);
    }
}
