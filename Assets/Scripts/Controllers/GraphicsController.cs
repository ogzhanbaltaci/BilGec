using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
public class GraphicsController : MonoBehaviour
{
    [SerializeField] TMP_Dropdown tMP_Dropdown;
    void Start()
    {
        if(!PlayerPrefs.HasKey("qualityLevel"))
        {
            PlayerPrefs.SetInt("qualityLevel", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public void SetQuality (int qualityIndex)
    {
        Save(qualityIndex);
        QualitySettings.SetQualityLevel(qualityIndex); 
    }
    private void Save(int qualityIndex)
    {
        PlayerPrefs.SetInt("qualityLevel", qualityIndex);
    }
    private void Load()
    {
        tMP_Dropdown.value = PlayerPrefs.GetInt("qualityLevel");
    }
}
