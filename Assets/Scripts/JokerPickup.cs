using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class JokerPickup : MonoBehaviour
{
    bool wasCollected;
    Quiz quiz;
    AudioPlayer audioPlayer;
    [SerializeField] bool isTwoXDamageButton;
    [SerializeField] bool isFiftyFiftyButton;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.tag == "Player" && !wasCollected && isTwoXDamageButton && quiz.twoXDamageButton.GetComponent<Button>().interactable == false)
        {
            wasCollected = true;
            quiz.twoXDamageButton.GetComponent<Button>().interactable = true;
            audioPlayer.PlayItemPickupClip();
            Destroy(gameObject);
        }   
        else if(other.tag == "Player" && !wasCollected && isFiftyFiftyButton && quiz.fiftyfiftyButton.GetComponent<Button>().interactable == false)
        {
            wasCollected = true;
            quiz.fiftyfiftyButton.GetComponent<Button>().interactable = true;
            audioPlayer.PlayItemPickupClip();
            Destroy(gameObject);
        }
    }
}
