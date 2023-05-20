using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthForHealthPickup = 20;
    [SerializeField] Health playerHealth;
    bool wasCollected = false;
    void Awake()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            playerHealth.HealthPickup(healthForHealthPickup);
            //AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
            Destroy(gameObject);
        }   
    }
}
