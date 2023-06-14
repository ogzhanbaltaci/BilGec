using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelGemPickup : MonoBehaviour
{
    bool wasCollected = false;
    AudioPlayer audioPlayer;
    LevelGemController levelGemController;
    void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
        levelGemController = FindObjectOfType<LevelGemController>();
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected)
        {
            levelGemController.collectedGemCount++;
            wasCollected = true;
            levelGemController.UpdateGemText();
            audioPlayer.PlayItemPickupClip();
            Destroy(gameObject);
        }   
    }
}
