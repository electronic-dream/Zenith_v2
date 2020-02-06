using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Questions : MonoBehaviour
{
    public Health hp;
    public PlayerMovement pM;
    public string questionFolder;
    public string mainQuestionFolder;

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

    public Button[] buttons;

    List<string> answers = new List<string>();
    int mainRand = 0;

    //int[] nums = new int[] { firstRand, secondRand, thirdRand };

    void Awake()
    {
        int num = Random.Range(1, 5);
        string[] lines = File.ReadAllLines("Assets/Questions/" + mainQuestionFolder + "/" + questionFolder + "/Question" + num + ".txt", Encoding.UTF8);

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

    public void CheckIfRight(int answerNum)
    {
        if (correctAnswer.CompareTo(buttons[answerNum - 1].GetComponentInChildren<Text>().text) == 0)
        {
            hp.health++;
            hp.immortal = true;
            hp.colliderToDisable.enabled = true;

            pM.isMoving = true;
            pM.isDashing = true;

            Debug.Log("Correct!");
        }
        else
            Debug.Log("Not Correct!");
    }

    public void Continue()
    {
        Time.timeScale = 1f;
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
