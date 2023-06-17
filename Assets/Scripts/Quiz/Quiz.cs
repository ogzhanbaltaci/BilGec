using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
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
    public Button fiftyfiftyButton; 
    
    Health health;
    public bool isActive;
    //public PlayerController playerController;
    public PlayerMobileMovement playerMobileMovement;
    Quiz quiz;
    public bool inQuiz;
    public Button twoXDamageButton;
    bool twoXDamageAvailable;
    public EnemySeen enemySeen;
    static Quiz instance;
    public GameManager gameManager;
    bool displayTriggered;
    public AudioPlayer audioPlayer;
    public AudioSource audioSource;
    bool displayingAnswer;
    void Awake()
    {
        playerMobileMovement = FindObjectOfType<PlayerMobileMovement>();
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
    }
    void DisplayAnswer(int index)
    {
       if(index == currentQuestion.GetCorrectAnswerIndex()){
            questionText.text = "Doğru!";
            health = enemySeen.enemyHealth;
            health.DealDamage(twoXDamageAvailable);
            audioPlayer.PlayEnemyHurtClip();
            buttonImage = answerButtons[index].GetComponent<Image>(); 
            buttonImage.sprite = correctAnswerSprite;   
            displayingAnswer = true;
        }
        else 
        {
            displayTriggered = true;
            displayingAnswer = true;
            correctAnswerIndex = currentQuestion.GetCorrectAnswerIndex();
            string correctAnswer = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = "Üzgünüm, doğru cevap şuydu;\n" + correctAnswer;
            health = playerMobileMovement.playerHealth;
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
        displayingAnswer = false;
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
        if(displayingAnswer != true)
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
    }
    public void OnTwoXDamageButtonSelected()
    {
        if(displayingAnswer != true)
        {
            twoXDamageAvailable = true;
            twoXDamageButton.GetComponent<Button>().interactable = false;
        }
    }
}
