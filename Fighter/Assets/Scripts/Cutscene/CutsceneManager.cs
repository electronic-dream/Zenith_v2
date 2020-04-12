using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour
{
    public Image[] frames;
    public GameObject nextFrameButton;
    public int currentFrameIndex = 0;

    private void Start()
    {
        for (int i = 1; i < frames.Length; i++)
        {
            frames[i].enabled = false;
        }
    }

    public void NextCutscene()
    {
        if(currentFrameIndex == 3)
        {
            frames[currentFrameIndex + 1].enabled = true;
            currentFrameIndex++;
            return;
        }

        if (currentFrameIndex == frames.Length - 1)
            return;

        frames[currentFrameIndex].enabled = false;
        frames[currentFrameIndex + 1].enabled = true;
     
        currentFrameIndex++;
    }

    private void Update()
    {
        if(currentFrameIndex >= frames.Length - 1)
        {
            nextFrameButton.SetActive(false);

            if(Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.KeypadEnter))
            {
                SceneManager.LoadScene("Menu");
            }
        }
    }
}
