using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class LevelGemController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelGemText;
    public int totalGemCount;
    public int collectedGemCount;
    void Start() 
    {
        levelGemText = GetComponentInChildren<TextMeshProUGUI>();
        UpdateGemText();
        collectedGemCount = 0;
    }
    public void SetGemCountZero()
    {
        collectedGemCount = 0;
    }
    public void UpdateGemText()
    {
        levelGemText.text = collectedGemCount.ToString() + "/" + totalGemCount.ToString();
    }
}
