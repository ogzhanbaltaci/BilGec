using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] public int health = 50;
    Quiz quiz;
    void Awake() 
    {
        quiz = FindObjectOfType<Quiz>(); 
    }
    public void DealDamage(bool twoXDamageAvailable)
    {
        DamageDealer damageDealer = GetComponent<DamageDealer>();
        if(damageDealer != null && twoXDamageAvailable == true)
        {
            TakeDamage(damageDealer.GetDamage()*2);
            //PlayHitEffect();
            //audioPlayer.PlayDamageTakenClip();
            //ShakeCamera();
            //damageDealer.Hit();
        }
        else if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
        }
    }
    void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Die());
        }
    }
    public IEnumerator Die() 
    {
        if(quiz.playerMovement.playerHealth.health <= 0)
        {
            quiz.playerMovement.myAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.inQuiz = false;
        }
        else if(quiz.playerMovement.enemyHealth.health <= 0)
        {
            Debug.Log("girdi");
            quiz.playerMovement.enemyAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.playerMovement.runSpeed = 10f;
            quiz.inQuiz = false;
        }
    }
    
}
