using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    QuestionSO currentQuestion;
    
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    [SerializeField] Sprite wrongAnswerSprite;
    Image buttonColor;
    Image buttonImage;
    Health health;
    public bool isActive;
    public PlayerMovement playerMovement;
    Quiz quiz;
    public bool inQuiz;
    void Awake()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
    }
    void Start()
    {
        timer = FindObjectOfType<Timer>();
        quiz = FindObjectOfType<Quiz>();
        health = FindObjectOfType<Health>();
        
    }
    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
        if(timer.loadNextQuestion)
        {
            hasAnsweredEarly = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion)
        {
            DisplayAnswer(-1);
            SetButtonState(false);              
        }
    }
    public void OnAnswerSelected(int index)
    {  
        hasAnsweredEarly = true;
        DisplayAnswer(index);     
        SetButtonState(false);
        timer.CancelTimer();
        //scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }
    void DisplayAnswer(int index)
    {
       if(index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct";
            health = playerMovement.enemyHealth;
            health.DealDamage();
            Debug.Log(playerMovement.enemyHealth.health);
            buttonImage = answerButtons[index].GetComponent<Image>(); 
            buttonImage.sprite = correctAnswerSprite;  
            
                
            //scoreKeeper.IncrementCorrectAnswers();            
            //audioSource.clip = trueAnswer;
            //audioSource.Play();       
        }
        else {
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            health = playerMovement.playerHealth;
            health.DealDamage();
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            if(hasAnsweredEarly){
                buttonImage = answerButtons[index].GetComponent<Image>(); 
                buttonImage.sprite = wrongAnswerSprite;
                //audioSource.clip = wrongAnswer;
                //audioSource.Play();
            }
            //scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
        }
    }
    void GetNextQuestion()
    {   
        if(health.health == 0)
            {
                quiz.gameObject.SetActive(false);
                isActive = false;
            }
        if(questions.Count > 0)
        {
            
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
            //scoreKeeper.IncrementQuestionsSeen();
        }    
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0,questions.Count);
        currentQuestion = questions[index];

        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }  
    }
   void DisplayQuestion()
   {
        //audioSource.clip = newQuestion;
        //audioSource.Play();
        questionText.text = currentQuestion.GetQuestion();

        for(int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
   }
   void SetButtonState(bool state)
   {
        for (int i = 0; i<answerButtons.Length;i++)
        {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
   }
   void SetDefaultButtonSprites()
   {
        for(int i = 0; i<answerButtons.Length; i++)
        {
            buttonImage = answerButtons[i].GetComponent<Image>(); 
            buttonColor = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
            buttonColor.color= Color.white;
        }
   }
}