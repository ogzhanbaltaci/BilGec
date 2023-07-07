using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    int currentSceneIndex;
    int nextSceneIndex;
    bool isTriggered = true;
    public int levelToUnlock = 2;
    LevelGemController levelGemController;
    Animator portalAnimator;
    TextMeshProUGUI textMeshProUGUI;
    void Awake()
    {
        levelGemController = FindAnyObjectByType<LevelGemController>();
        portalAnimator = GetComponent<Animator>();
        textMeshProUGUI = GetComponentInChildren<TextMeshProUGUI>();
    }
    void Update()
    {
        if(levelGemController.collectedGemCount == levelGemController.totalGemCount)
        {
            portalAnimator.SetBool("isActive", true);
            textMeshProUGUI.text = "";
        }
    }
    void OnTriggerEnter2D(Collider2D other) 
    {
        if(isTriggered == true){
            currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            if(other.tag == "Player" 
                && levelGemController.collectedGemCount == levelGemController.totalGemCount){    
                int levelReached = PlayerPrefs.GetInt("levelReached", 1);
        
                if(levelReached < levelToUnlock)
                    PlayerPrefs.SetInt("levelReached", levelToUnlock);
                
                FindObjectOfType<GameManager>().WinCanvasEnabled();
            }
            else
            { 
                textMeshProUGUI.text = "Portalı açmak için önce gerekli olan bütün elmasları toplamalısın!";
            }
        }
    }   
}
