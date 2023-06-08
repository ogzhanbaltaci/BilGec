using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckpointController : MonoBehaviour
{
   Animator checkpointAnimator;
   [SerializeField] TextMeshProUGUI checkpointText;

    void Start()
    {
        checkpointAnimator = GetComponent<Animator>();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player" )
        {
            checkpointAnimator.SetBool("cpActive", true);
            checkpointText.text = "Kontrol Noktası Aktif Edildi";

        }
    }
}
