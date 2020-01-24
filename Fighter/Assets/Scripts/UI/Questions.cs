using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;

public class Questions : MonoBehaviour
{
    string question = "";
    public Text questionText;

    bool isCorrect = false;

    string[] lines = File.ReadAllLines("Assets/Questions/History/FirstQuestionBegin.txt", Encoding.UTF8);

    string answer1Str = "";
    string answer2Str = "";
    string answer3Str = "";
    string answer4Str = "";

    public Text answer1;
    public Text answer2;
    public Text answer3;
    public Text answer4;

    string[] answers = new string[4];
    int firstRand = 0;
    int secondRand = 0;
    int thirdRand = 0;
    int fourthRand = 0;

    //int[] nums = new int[] { firstRand, secondRand, thirdRand };

    void Awake()
    {
        ReadFile(lines);
        questionText.text = question;

        answers[0] = answer1Str;
        answers[1] = answer2Str;
        answers[2] = answer3Str;
        answers[3] = answer4Str;

        var exclude = new HashSet<int>() { firstRand, secondRand, thirdRand };
        //var range = Enumerable.Range(1, 5).Where(i => !exclude.Contains(i));

        firstRand = Random.Range(1, 5 - exclude.Count);
        answer1.text = answers[firstRand];

        var scndExclude = new HashSet<int>() { firstRand, secondRand, thirdRand };
        secondRand = Random.Range(1, 5 - scndExclude.Count);
        answer2.text = answers[secondRand];

        var thrdExclude = new HashSet<int>() { firstRand, secondRand, thirdRand };
        thirdRand = Random.Range(1, 5 - thrdExclude.Count);
        answer3.text = answers[thirdRand];

        var fourthExlude = new HashSet<int>() { firstRand, secondRand, thirdRand };
        fourthRand = Random.Range(1, 5 - fourthExlude.Count);
        answer4.text = answers[fourthRand];
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
                isCorrect = false;
            }
            if (i == 2)
            {
                answer2Str = item;
                isCorrect = false;
            }
            if (i == 3)
            {
                answer3Str = item;
                isCorrect = false;
            }
            if (i == 4)
            {
                answer4Str = item;
                isCorrect = true;
            }

            i++;
        }
    }
}
