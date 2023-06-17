using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas winCanvas;
    void Awake()
    {
        winCanvas.gameObject.SetActive(false);
        deathCanvas.gameObject.SetActive(false);
        ManageSingelton();
    }
    public void DeathCanvasEnabled()
    {
        deathCanvas.gameObject.SetActive(true);
    }
    public void WinCanvasEnabled()
    {
        winCanvas.gameObject.SetActive(true);
    }
    public void ResetGameManager()
    {
        Destroy(gameObject);
    }
    void ManageSingelton()
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
