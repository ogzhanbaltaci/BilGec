using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    static GameManager instance;
    public List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField] Canvas deathCanvas;
    [SerializeField] Canvas winCanvas;
    void Awake()
    {
        winCanvas.gameObject.SetActive(false);
        deathCanvas.gameObject.SetActive(false);
        Debug.Log("girdi manager awake");
        ManageSingelton();
    }
    void Start()
    {
        //deathCanvas = GameObject.FindGameObjectWithTag("DeathCanvas");
        //deathCanvas.enabled = false;
        //winCanvas.enabled = false;
    }
    public void DeathCanvasEnabled()
    {
        //deathCanvas.enabled = true;
        deathCanvas.gameObject.SetActive(true);
    }
    public void WinCanvasEnabled()
    {
        winCanvas.gameObject.SetActive(true);
        //winCanvas.enabled = true;
    }
    
    void TakeLife()
    {
        playerHealth.health -= 10;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(playerHealth.health);
        SceneManager.LoadScene(currentSceneIndex);
        
        //livesText.text = playerLives.ToString();
    }

    public void ResetGameManager()
    {
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        //SceneManager.LoadScene(0);
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
