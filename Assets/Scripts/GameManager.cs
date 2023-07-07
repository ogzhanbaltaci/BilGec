using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas winCanvas;
    [SerializeField] Canvas mobileControlsCanvas;
    void Awake()
    {
        winCanvas.gameObject.SetActive(false);
        deathCanvas.gameObject.SetActive(false);
        ManageSingleton();
    }
    public void DeathCanvasEnabled()
    {
        deathCanvas.gameObject.SetActive(true);
    }
    public void WinCanvasEnabled()
    {
        winCanvas.gameObject.SetActive(true);
    }
    public void MobileControlsCanvasEnabled()
    {
        mobileControlsCanvas.gameObject.SetActive(true);
    }
    public void WinCanvasDisabled()
    {
        winCanvas.gameObject.SetActive(false);
    }
    public void DeathCanvasDisabled()
    {
        deathCanvas.gameObject.SetActive(false);
    }
    public void MobileControlsCanvasDisabled()
    {
        mobileControlsCanvas.gameObject.SetActive(false);
    }
    public void ResetGameManager()
    {
        Destroy(gameObject);
    }
    void ManageSingleton()
    {
        if(instance != null)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    
}
