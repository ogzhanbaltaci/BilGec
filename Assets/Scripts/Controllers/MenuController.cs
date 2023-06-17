using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    [SerializeField] Canvas settingsCanvas;
    [SerializeField] Canvas mainMenuCanvas;
    public void OpenLevelsMenu()
    {
        SceneManager.LoadScene("LevelsMenu");
    }
    public void QuitGame()
    {
        Debug.Log("Quitting the game");
        Application.Quit();
    }
    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void OpenMainMenuFromPauseMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
        GameManager gameManager;
        gameManager = FindObjectOfType<GameManager>();
        gameManager.ResetGameManager();
    }
    public void OpenSettingsMenu()
    {
        mainMenuCanvas.gameObject.SetActive(false);
        settingsCanvas.gameObject.SetActive(true);
    }
}
