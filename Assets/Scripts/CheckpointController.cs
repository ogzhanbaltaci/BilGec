using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
   Animator checkpointAnimator;
    void Start()
    {
        checkpointAnimator = GetComponent<Animator>();
    }

    
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            checkpointAnimator.SetBool("cpActive", true);
        }
    }
}
