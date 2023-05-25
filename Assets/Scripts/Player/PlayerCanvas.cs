using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerCanvas : MonoBehaviour
{
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI healthText;
    [SerializeField] Health playerHealth;
    void Update()
    {
        healthBar.value = playerHealth.health;
        healthText.GetComponent<TextMeshProUGUI>().text = "CAN : " + playerHealth.health.ToString();
    }
}
