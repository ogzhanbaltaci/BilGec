using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    public static bool gameIsPaused = false;
    PauseMenuController pauseMenuCanvas;
    void Awake()
    {
        pauseMenuCanvas = FindObjectOfType<PauseMenuController>();
    }
    private void Start() 
    {
        pauseMenuCanvas.gameObject.SetActive(false);
    }

    public void Resume()
    {
        pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1f;
        gameIsPaused = false;
    }
    
    void Pause()
    {
        Time.timeScale = 0f;
        gameIsPaused = true;
    }
    public void OpenPauseMenu()
    {
        pauseMenuCanvas.gameObject.SetActive(true);
        Pause();
    }
    

}
