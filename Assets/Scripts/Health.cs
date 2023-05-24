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
    public void HealthPickup(int healthForHealthPickup)
    {
        health += healthForHealthPickup;
    }
    public IEnumerator Die() 
    {
        if(quiz.playerController.playerHealth.health <= 0)
        {
            quiz.playerController.myAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.inQuiz = false;
        }
        else if(quiz.enemySeen.enemyHealth.health <= 0)
        {
            Debug.Log("girdi");
            //quiz.playerMovement.enemyAnimator.SetBool("isDead", true);
            quiz.enemySeen.enemyAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.playerController.runSpeed = 10f;
            quiz.inQuiz = false;
            
        }
    }
    
}
