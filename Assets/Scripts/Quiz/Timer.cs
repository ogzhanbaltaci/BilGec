using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    public bool loadNextQuestion;
    public bool isAnsweringQuestion;
    public float fillFraction;
    float timerValue;
    bool isPlayed;
    public AudioSource audioSource;
    void Awake()
    {
        
    }
    void Update()
    {
        UpdateTimer();
    }
    public void CancelTimer()
    {
        timerValue = 0;
    }
    void UpdateTimer()
    {
        timerValue -= Time.deltaTime;
        if(isAnsweringQuestion)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
                if(timerValue <= 5 && isPlayed == false)
                {
                    isPlayed = true;
                    audioSource.Play();
                }
            }
            else
            {
                isAnsweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;               
            }
        }
        else
        {
             if(timerValue > 0)
            {
                audioSource.Stop();
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            else
            {
                isPlayed = false;
                isAnsweringQuestion = true;
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }
    }
}
