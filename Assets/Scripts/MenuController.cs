using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void OpenLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }

    
}
