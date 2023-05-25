using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    int currentSceneIndex;
    int nextSceneIndex;
    bool isTriggered = true;
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(isTriggered == true){
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if(other.tag == "Player" && currentSceneIndex + 1 != SceneManager.sceneCountInBuildSettings){    
                StartCoroutine(LoadNextLevel());
                isTriggered = false;
            }
            /*else{ 
                FindObjectOfType<GameSession>().gameFinished = true;
                FindObjectOfType<GameSession>().TotalScore();
                FindObjectOfType<GameSession>().winCanvas.gameObject.SetActive(true);
            
                isTriggered = false;

            }*/
        }
        
    }
    IEnumerator LoadNextLevel()
    {
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = currentSceneIndex + 1;

        //FindObjectOfType<ScenePersist>().ResetScenePersist();
        SceneManager.LoadScene(nextSceneIndex);
        
    }
    
}
