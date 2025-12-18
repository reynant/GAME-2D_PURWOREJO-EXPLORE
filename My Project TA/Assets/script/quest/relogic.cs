using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class QuestionUIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject panel;
    public Text questionText;
    public Text answerAText, answerBText, answerCText, answerDText;
    public Button answerAButton, answerBButton, answerCButton, answerDButton;
    public Button closeButton;
    public Text scoreText;

    [Header("Data Pertanyaan")]
    public List<Question> questions = new List<Question>();
    private int currentIndex = 0;
    private bool isAnswered = false;
    private int score = 0;

    [System.Serializable]
    public class Question
    {
        public string question;
        public string answerA;
        public string answerB;
        public string answerC;
        public string answerD;
        public char correctAnswer; 
    }

    void Start()
    {
        panel.SetActive(false);
        answerAButton.onClick.AddListener(() => CheckAnswer('A'));
        answerBButton.onClick.AddListener(() => CheckAnswer('B'));
        answerCButton.onClick.AddListener(() => CheckAnswer('C'));
        answerDButton.onClick.AddListener(() => CheckAnswer('D'));
        closeButton.onClick.AddListener(() => panel.SetActive(false));
        score = 0;
        scoreText.text = "Score: 0";
    }

    public void ShowQuestion()
    {
        if (questions.Count == 0 || currentIndex >= questions.Count) return;

        isAnswered = false;
        panel.SetActive(true);

        Question q = questions[currentIndex];
        questionText.text = q.question;
        answerAText.text = "A. " + q.answerA;
        answerBText.text = "B. " + q.answerB;
        answerCText.text = "C. " + q.answerC;
        answerDText.text = "D. " + q.answerD;
    }

    void CheckAnswer(char selected)
    {
        if (isAnswered) return;

        Question q = questions[currentIndex];
        if (selected == q.correctAnswer)
        {
            score += 10;
            scoreText.text = "Score: " + score;
        }

        isAnswered = true;
        Invoke("NextQuestion", 1f); // Delay 1 detik
    }

    void NextQuestion()
    {
        currentIndex++;
        if (currentIndex < questions.Count)
        {
            ShowQuestion();
        }
        else
        {
            panel.SetActive(false); // Tamat
        }
    }
}
