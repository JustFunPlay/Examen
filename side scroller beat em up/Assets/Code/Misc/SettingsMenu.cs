using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    public GameObject MenuUI;
    public GameObject CharacterSelectUI;
    public GameObject SettingsUI;
    public GameObject creditsUI;

    public AudioSource ButtonClick;

    public AudioMixer AudioMixer;

    Resolution[] resolutions;
    public TMPro.TMP_Dropdown ResolutionsDropdown;

    //post processing wat niet werkt
    //public Slider brightnessSlider;
    //public PostProcessProfile brightness;
    //public PostProcessLayer layer;
    //public AutoExposure exposure;

    void Start()
    {
        //brightness.TryGetSettings(out exposure);

        resolutions = Screen.resolutions;

        ResolutionsDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;       //dit calculeerd alle unity resoluties, geen idee hoe en wat precies lmao
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionsDropdown.AddOptions(options);
        ResolutionsDropdown.value = currentResolutionIndex;
        ResolutionsDropdown.RefreshShownValue();
        QualitySettings.SetQualityLevel(2);
    }

    public void SetResolution(int resolutionIndex)
    {
        ButtonClick.Play();
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setFullScreen(bool isFullScreen)
    {
        ButtonClick.Play();
        Screen.fullScreen = isFullScreen;
    }

    //Graphics Quality//

    public void low(bool quality)
    {
        if (quality)
        {
            ButtonClick.Play();
            QualitySettings.SetQualityLevel(0);
        }
    }

    public void medium(bool quality)
    {
        if (quality)
        {
            ButtonClick.Play();
            QualitySettings.SetQualityLevel(2);
        }
    }

    public void high(bool quality)
    {
        if (quality)
        {
            ButtonClick.Play();
            QualitySettings.SetQualityLevel(3);
        }
    }

    public void ultra(bool quality)
    {
        if (quality)
        {
            ButtonClick.Play();
            QualitySettings.SetQualityLevel(5);
        }
    }

    public void setQuality(int qualityIndex)
    {
        ButtonClick.Play();
        QualitySettings.SetQualityLevel(qualityIndex);
    }


    //VOLUME SLIDERS//
    public void SetMasterVolume(float volume)
    {
        AudioMixer.SetFloat("masterVolume", Mathf.Log10(volume) * 20);
    }
    public void SetSoundVolume(float volume)
    {
        AudioMixer.SetFloat("soundVolume", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        AudioMixer.SetFloat("musicVolume", Mathf.Log10(volume) * 20);
    }

    //BUTTONS//
    public void GoToLevelSelect()
    {
        ButtonClick.Play();
        MenuUI.SetActive(false);
        CharacterSelectUI.SetActive(true);
        SettingsUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void GoToSettings()
    {
        ButtonClick.Play();
        MenuUI.SetActive(false);
        CharacterSelectUI.SetActive(false);
        SettingsUI.SetActive(true);
        creditsUI.SetActive(false);
    }

    public void GoToCredits()
    {
        ButtonClick.Play();
        MenuUI.SetActive(false);
        CharacterSelectUI.SetActive(false);
        SettingsUI.SetActive(false);
        creditsUI.SetActive(true);
    }

    public void QuitGame()
    {
        ButtonClick.Play();
        Application.Quit();
    }

    public void ReturnToMenu()
    {
        ButtonClick.Play();
        MenuUI.SetActive(true);
        CharacterSelectUI.SetActive(false);
        SettingsUI.SetActive(false);
        creditsUI.SetActive(false);
    }

    public void LoadLevel1()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("franswildit");
       // FindObjectOfType<AudioManagerScript>().Play("DefenceSetupMusic");
    }

    public void LoadLevel2()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("SunsetLevel");
       // FindObjectOfType<AudioManagerScript>().Play("DefenceSetupMusic");
    }

    public void LoadLevel3()
    {
        ButtonClick.Play();
        SceneManager.LoadScene("Catacombs");
    }

    //SETTINGS//



    // public void AdjustBrightness(float value)
    //   {
    //if(value != 0)
    //     {
    //exposure.keyValue.value = value;
    //   }
    // else
    //    {
    //exposure.keyValue.value = 0.5f;
    //   }

    //}
}
