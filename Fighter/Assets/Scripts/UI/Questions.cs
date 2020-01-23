using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.UI;

public class Questions : MonoBehaviour
{
    string filePath = "";
    var[] lines = new StreamReader(filePath, Encoding.GetEncoding("windows-1251"));
    string question = "";

    public void ReadFile(string[] lines, string question)
    {
        int i = 0;
        foreach (var item in lines)
        {
            if (i == 0)
            {
                question = item;
            }
            i++;
        }
    }
    
    void Start()
    {
       
        ReadFile(lines,question);

       
    }


}
