using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log($"Player reached {nextLevel}");
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(nextLevel));
        }
    }
}
