using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{
    static QuestionsManager instance;
    public List<QuestionSO> questions = new List<QuestionSO>();
    void Awake()
    {
        ManageSingleton();
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
    public void ResetQuestionsManager()
    {
        Destroy(gameObject);
    }
}
