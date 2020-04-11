using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine.SceneManagement;

public class Questions : MonoBehaviour
{
    public enum HistoryType
    {
        BulgarianHistory,
        WorldHistory
    }

    public HistoryType historyType;// = HistoryType.BulgarianHistory;

    public Health hp;
    public PlayerMovement pM;

    public string mainQuestionFolder;
    public string questionFolder;

    [HideInInspector]
    public string startQuestionFolder = "";

    string question = "";
    public Text questionText;

    string correctAnswer = "";

    string answer1Str = "";
    string answer2Str = "";
    string answer3Str = "";
    string answer4Str = "";

    public Text answer1;
    public Text answer2;
    public Text answer3;
    public Text answer4;

    public bool isLastQuestion = false;

    public Button[] buttons;

    List<string> answers = new List<string>();
    int mainRand = 0;

    void Awake()
    {
        string DocumentsPath = Application.dataPath;
        //Debug.Log(DocumentsPath);

        int num = Random.Range(1, 5);
        //string finalFilePath = $"{DocumentsPath}/Questions/{mainQuestionFolder}/{questionFolder}/Question{num}.txt";

        if (historyType == HistoryType.WorldHistory)
        {
            startQuestionFolder = "QuestionsWH";
        }
        else if (historyType == HistoryType.BulgarianHistory)
        {
            startQuestionFolder = "Questions";
        }

        string[] lines = File.ReadAllLines(DocumentsPath + "/" + startQuestionFolder + "/" + mainQuestionFolder + "/" + questionFolder + "/Question" + num + ".txt", Encoding.UTF8);

        //Debug.Log("GGames/Zenith_v2/Fighter/Assets/Questions/" + mainQuestionFolder + "/" + questionFolder + "/Question" + num + ".txt");

        ReadFile(lines);
        questionText.text = question;

        answers.Add(answer1Str);
        answers.Add(answer2Str);
        answers.Add(answer3Str);
        answers.Add(answer4Str);

        correctAnswer = answers.Last();
        //Debug.Log(correctAnswer + " " + answers.Last());

        mainRand = Random.Range(0, answers.Count);
        answer1.text = answers[mainRand];
        answers.RemoveAt(mainRand);

        mainRand = Random.Range(0, answers.Count);
        answer2.text = answers[mainRand];
        answers.RemoveAt(mainRand);

        mainRand = Random.Range(0, answers.Count);
        answer3.text = answers[mainRand];
        answers.RemoveAt(mainRand);

        mainRand = Random.Range(0, answers.Count);
        answer4.text = answers[mainRand];
        answers.RemoveAt(mainRand);

        Time.timeScale = 0f;
    }

    public bool isDeathQuestion;

    public void CheckIfRight(int answerNum)
    {
        if (correctAnswer.CompareTo(buttons[answerNum - 1].GetComponentInChildren<Text>().text) == 0)
        {
            Health.health++;
            hp.colliderToDisable.enabled = true;

            pM.isMoving = true;
            pM.isDashing = true;

            if (isLastQuestion)
            {
                //hp.immortal = true;
                pM.isDashing = true;
                hp.immortalTime = 5F;
                Continue();
            }
            if (isDeathQuestion)
            {
                hp.immortal = true;
            }

            Debug.Log("Correct!");
        }
        else
        {
            Debug.Log("Not Correct!");

            if (isLastQuestion)
            {
                Continue();
            }

            if (isDeathQuestion)
            {
                hp.immortal = false;
                SceneManager.LoadScene("Level1");
                Health.health = 1;
            }
        }
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        pM.isDashing = true;
    }

    public void ReadFile(string[] lines)
    {
        int i = 0;
        foreach (var item in lines)
        {
            if (i == 0)
            {
                question = item;
            }
            if (i == 1)
            {
                answer1Str = item;
            }
            if (i == 2)
            {
                answer2Str = item;
            }
            if (i == 3)
            {
                answer3Str = item;
            }
            if (i == 4)
            {
                answer4Str = item;
            }

            i++;
        }
    }
}
