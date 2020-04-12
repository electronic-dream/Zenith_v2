using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    Resolution[] resolutions;

    public Dropdown resolutionDropdown;
    public ChangeHistoryMode historyMode;

    public Toggle[] historyToggles;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if(resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetHistoryTypeBG()
    {
        if (historyToggles[0].isOn)
            historyToggles[1].isOn = false;

        if (!historyToggles[0].isOn)
            historyToggles[1].isOn = true;

        historyMode.SetBulgarianHistory();
    }
    public void SetHistoryTypeWH()
    {
        if (historyToggles[1].isOn)
            historyToggles[0].isOn = false;

        if (!historyToggles[1].isOn)
            historyToggles[0].isOn = true;

        historyMode.SetWorldHistory();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution res = resolutions[resolutionIndex];

        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void NextLevel()
    {
        Health.askedQuestion = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
