using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D myRigidbody;
    BoxCollider2D enemyFootCollider;
    [SerializeField] public Slider enemyHealthBar;
    public TextMeshProUGUI enemyHealthText;
    public Quiz quiz;
    
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
    }
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        enemyFootCollider = GetComponent<BoxCollider2D>();
        enemyHealthBar.gameObject.SetActive(false);
        enemyHealthText.gameObject.SetActive(false);
    }
    void Update()
    {
        myRigidbody.velocity = new Vector2 (moveSpeed, 0f) ;
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
        enemyHealthBar.transform.localScale = new Vector2 (-0.01f, 0.02f);
        enemyHealthText.transform.localScale = new Vector2 (-(Mathf.Sign(enemyHealthText.transform.localScale.x)), 1f);
    }
    public void EnemyIdle()
    {
        moveSpeed = 0f;
        quiz.enemySeen.enemyAnimator.SetBool("isIdling", true);
    }
}
