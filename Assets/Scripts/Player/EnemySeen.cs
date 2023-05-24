using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySeen : MonoBehaviour
{
    public Health enemyHealth;
    public EnemyMovement enemyMovement;
    public Animator enemyAnimator;
    Quiz quiz;
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
    }
    
    void Update()
    {
        if(enemyMovement != null)
        {
            enemyMovement.enemyHealthBar.value = enemyHealth.health;
            enemyMovement.enemyHealthText.text = "CAN : " + enemyHealth.health.ToString();
        }
            
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Enemy"))
        {
            enemyHealth = other.gameObject.GetComponent<Health>();
            enemyMovement = other.gameObject.GetComponent<EnemyMovement>();
            enemyAnimator = other.gameObject.GetComponent<Animator>();
            enemyMovement.enemyHealthBar.gameObject.SetActive(true);
            enemyMovement.enemyHealthText.gameObject.SetActive(true);
            enemyMovement.enemyHealthBar.maxValue = enemyHealth.health;
            quiz.isActive = true;
            quiz.gameObject.SetActive(true);
            quiz.inQuiz = true;
            Debug.Log(quiz.inQuiz);
            if(quiz.inQuiz == true)
                enemyMovement.gameObject.GetComponent<EnemyMovement>().EnemyIdle();
        }
    }
}
