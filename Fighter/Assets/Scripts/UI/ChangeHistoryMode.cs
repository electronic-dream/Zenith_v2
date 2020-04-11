using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHistoryMode : MonoBehaviour
{
    public enum HistoryType
    {
        BulgarianHistory,
        WorldHistory
    }

    public HistoryType historyTypes;
    public List<Questions> questions;

    GameObject questionsGO;

    public void Update()
    {
        DontDestroyOnLoad(this.gameObject);

        questionsGO = GameObject.Find("Questions");

        questionsGO.GetComponentsInChildren(true, questions);
    }

    private void LateUpdate()
    {
        if (historyTypes == HistoryType.WorldHistory)
        {
            foreach (var question in questions)
            {
                question.historyType = Questions.HistoryType.WorldHistory;
            }
        }
        else if (historyTypes == HistoryType.BulgarianHistory)
        {
            foreach (var question in questions)
            {
                question.historyType = Questions.HistoryType.BulgarianHistory;
            }
        }
    }

    public void SetBulgarianHistory()
    {
        historyTypes = HistoryType.BulgarianHistory;
    }

    public void SetWorldHistory()
    {
        historyTypes = HistoryType.WorldHistory;
    }
}
