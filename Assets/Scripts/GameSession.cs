using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameSession : MonoBehaviour
{
    [SerializeField] Health playerHealth;
    static GameSession instance;
    public List<QuestionSO> questions = new List<QuestionSO>();
    void Start()
    {
        ManageSingelton();
    }
    public void ProcessPlayerDeath()
    {
        if(playerHealth.health > 10)
        {
            TakeLife();
        }
        else
        {
            //ongoingCanvas.gameObject.SetActive(false);
            //tryAgainCanvas.gameObject.SetActive(true);
        }
    }
    void TakeLife()
    {
        playerHealth.health -= 10;
        
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log(playerHealth.health);
        SceneManager.LoadScene(currentSceneIndex);
        
        //livesText.text = playerLives.ToString();
    }

    public void ResetGameSession()
    {
        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(0);
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
