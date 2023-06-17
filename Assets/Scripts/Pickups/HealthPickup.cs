using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] int healthForHealthPickup = 20;
    Health playerHealth;
    GameObject playerGameObject;
    bool wasCollected = false;
    AudioPlayer audioPlayer;
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        playerGameObject = GameObject.FindGameObjectWithTag("Player");
        playerHealth = playerGameObject.GetComponent<Health>();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            playerHealth.HealthPickup(healthForHealthPickup);
            audioPlayer.PlayItemPickupClip();
            Destroy(gameObject);
        }   
    }
}
