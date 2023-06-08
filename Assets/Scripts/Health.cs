using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] bool isPlayer;
    [SerializeField] public int health = 50;
    Quiz quiz;
    [SerializeField] bool applyCameraShake;
    CameraShake cameraShake;
    GameManager gameManager;
    void Awake() 
    {
        quiz = FindObjectOfType<Quiz>(); 
        cameraShake = FindObjectOfType<CameraShake>();
        gameManager = FindObjectOfType<GameManager>();
        //deathCanvas.enabled = false;
    }
    public void DealDamage(bool twoXDamageAvailable)
    {
        DamageDealer damageDealer = GetComponent<DamageDealer>();
        if(damageDealer != null && twoXDamageAvailable == true)
        {
            TakeDamage(damageDealer.GetDamage()*2);
            ShakeCamera();
            //PlayHitEffect();
            //audioPlayer.PlayDamageTakenClip();
            //ShakeCamera();
        }
        else if(damageDealer != null)
        {
            TakeDamage(damageDealer.GetDamage());
            ShakeCamera();
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            StartCoroutine(Die());
            quiz.enemySeen.cinemachineBrain.enabled = true;
        }
    }
    public void HealthPickup(int healthForHealthPickup)
    {
        health += healthForHealthPickup;
    }
    void ShakeCamera()
    {
        if(cameraShake != null && applyCameraShake)
        {
            cameraShake.Play();
            Debug.Log("girdishake");
        }
    }
    public IEnumerator Die() 
    {
        if(quiz.playerController.playerHealth.health <= 0)
        {
            quiz.playerController.myAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.inQuiz = false;
            gameManager.DeathCanvasEnabled();
            //deathCanvas.enabled = true;
            
        }
        else if(quiz.enemySeen.enemyHealth.health <= 0)
        {
            quiz.enemySeen.enemyAnimator.SetBool("isDead", true);
            yield return new WaitForSecondsRealtime(0.8f);
            Destroy(gameObject);
            quiz.playerController.runSpeed = 10f;
            quiz.inQuiz = false;
            
        }
    }
    
}
