using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
public class LevelSelector : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelText;
    public Button[] levelButtons;
    void Start()
    {
        levelText.text = level.ToString();
        int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        for (int i = 0; i < levelButtons.Length; i++)
        {
            if(i + 1 > levelReached)
                levelButtons[i].interactable = false;
        }
    }

    public void OpenScene(){
        SceneManager.LoadScene("Level " + level.ToString());
    }
}
