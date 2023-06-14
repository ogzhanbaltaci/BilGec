using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
   // [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
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
    
    [Header("50:50Button")]
    public int another5050Chance;
    public Button fiftyfiftyButton; 
    //[SerializeField] TextMeshProUGUI icon5050;
    
    
    Health health;
    public bool isActive;
    public PlayerController playerController;
    Quiz quiz;
    public bool inQuiz;
    public Button twoXDamageButton;
    public bool twoXDamageAvailable;
    public EnemySeen enemySeen;
    static Quiz instance;
    public GameManager gameManager;
    bool displayTriggered;
    public AudioPlayer audioPlayer;
    public AudioSource audioSource;
    void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        enemySeen = FindObjectOfType<EnemySeen>();
        audioPlayer = FindObjectOfType<AudioPlayer>();
        audioSource = FindObjectOfType<AudioSource>();
        audioPlayer.PlayInGameMusicClip();
    }
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
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
        else if(!hasAnsweredEarly && !timer.isAnsweringQuestion && !displayTriggered)
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
        //audioSource.Stop();
        //audioPlayer.GetComponent<AudioSource>().Stop();
        //scoreText.text = "Score: " + scoreKeeper.CalculateScore() + "%";
    }
    void DisplayAnswer(int index)
    {
       if(index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Correct";
            health = enemySeen.enemyHealth;
            health.DealDamage(twoXDamageAvailable);
            audioPlayer.PlayEnemyHurtClip();
            buttonImage = answerButtons[index].GetComponent<Image>(); 
            buttonImage.sprite = correctAnswerSprite;    
        }
        else {
            displayTriggered = true;
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Sorry, the correct answer was;\n" + correctAnswer;
            health = playerController.playerHealth;
            health.DealDamage(twoXDamageAvailable);
            audioPlayer.PlayPlayerHurtClip();
            buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            if(hasAnsweredEarly){
                buttonImage = answerButtons[index].GetComponent<Image>(); 
                buttonImage.sprite = wrongAnswerSprite;
            }
        }
    }
    void GetNextQuestion()
    {   
        twoXDamageAvailable = false;
        displayTriggered = false;
        if(health.health <= 0)
            {
                quiz.gameObject.SetActive(false);
                isActive = false;
            }
        if(gameManager.questions.Count > 0)
        {
            SetButtonState(true);
            SetDefaultButtonSprites();
            GetRandomQuestion();
            DisplayQuestion();
        }    
    }
    void GetRandomQuestion()
    {
        int index = Random.Range(0,gameManager.questions.Count);
        currentQuestion = gameManager.questions[index];

        if(gameManager.questions.Contains(currentQuestion))
        {
            gameManager.questions.Remove(currentQuestion);
        }  
    }
   void DisplayQuestion()
   {
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
   public void On5050ButtonSelected()
    {
        Display5050Answer();
    }
   void Display5050Answer()
    {
        correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
        int index2 = 5;
        for(int i=0; i<2; )
        {
            int[] numbers = {0,1,2,3};     
            int index = Random.Range(0,numbers.Length);           
            if(index != correctAnswerIndex && index != index2)
            {
                Button button = answerButtons[index].GetComponent<Button>();
                buttonColor = answerButtons[index].GetComponent<Image>();
                button.interactable = false;
                buttonColor.color= Color.blue;
                index2 = index;
                i++;               
            }
        }
        fiftyfiftyButton.GetComponent<Button>().interactable = false;
        Another5050Chance();
    }
   void Another5050Chance()
    {
        another5050Chance=0;  
    }
    public void OnTwoXDamageButtonSelected()
    {
        twoXDamageAvailable = true;
        twoXDamageButton.GetComponent<Button>().interactable = false;
    }
}
