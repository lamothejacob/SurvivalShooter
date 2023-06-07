using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class SettingsMenu : MonoBehaviour
{
    [SerializeField] Slider audioSlider;
    [SerializeField] TMPro.TextMeshProUGUI audioLevel;
    [SerializeField] Slider musicSlider;
    [SerializeField] TMPro.TextMeshProUGUI musicLevel;
    [SerializeField] Slider sfxSlider;
    [SerializeField] TMPro.TextMeshProUGUI sfxLevel;
    [SerializeField] Slider cameraSensitivitySlider; 
    [SerializeField] TMPro.TextMeshProUGUI cameraSensitivity;
    [SerializeField] Toggle invertY;
    [SerializeField] Toggle displayHUD;

    public static bool bDisplayHUD;
    private bool bInvertY;
    private int audioLevelNum;
    private int musicLevelNum;
    private int sfxLevelNum;
    private int cameraSensNum;

    // Start is called before the first frame update
    void Start()
    {
        bDisplayHUD = true;
        bInvertY = false;
        audioLevelNum = (int)(audioSlider.value * 100);
        audioLevel.SetText(audioLevelNum.ToString());
        musicLevelNum = (int)(musicSlider.value * 100);
        musicLevel.SetText(musicLevelNum.ToString());
        sfxLevel.SetText(sfxSlider.value.ToString());
        cameraSensNum = (int)(cameraSensitivitySlider.value / 10);
        cameraSensitivity.SetText(cameraSensNum.ToString());

    }

    void Update()
    {
        audioLevelNum = (int)(audioSlider.value * 100);
        audioLevel.SetText(audioLevelNum.ToString());
        musicLevelNum = (int)(musicSlider.value * 100);
        musicLevel.SetText(musicLevelNum.ToString());
        sfxLevelNum = (int)(sfxSlider.value * 100);
        sfxLevel.SetText(sfxLevelNum.ToString());
        cameraSensNum = (int)(cameraSensitivitySlider.value / 10);
        cameraSensitivity.SetText(cameraSensNum.ToString());
    }        

    public float GetAudioLevel()
    {
        return audioSlider.value;
    }

    public float SetAudioLevel(Slider audio)
    {
        audioSlider.onValueChanged.AddListener((v) =>
        {
            audioLevelNum = (int)(audioSlider.value * 100);
            audioLevel.SetText(audioLevelNum.ToString());
            audioSlider.value = v;
        });
        

        return audio.value; 
    }

    public float GetMusicLevel()
    {
        return musicSlider.value;
    }

    public float SetMusicLevel(Slider audio)
    {
        musicSlider.onValueChanged.AddListener((v) =>
        {

            cameraSensNum = (int)(cameraSensitivitySlider.value / 100);
            cameraSensitivity.SetText(cameraSensNum.ToString());
            musicSlider.value = v;
        });


        return audio.value;
    }

    public float GetSFXLevel()
    {
        return sfxSlider.value;
    }

    public float SetsfxLevel(Slider audio)
    {
        musicSlider.onValueChanged.AddListener((v) =>
        {

            sfxLevelNum = (int)(sfxSlider.value * 100);
            sfxLevel.SetText(sfxLevelNum.ToString());
            sfxSlider.value = v;
        });


        return audio.value;
    }
    public float GetCameraSliderValue()
    {
        return cameraSensitivitySlider.value; 
    }

    public float SetCameraSensitivity(Slider camera) 
    {
        cameraSensitivitySlider.onValueChanged.AddListener((v) =>
        {
            cameraSensNum = (int)(v / 10);
            cameraSensitivity.SetText(cameraSensNum.ToString());
            cameraSensitivitySlider.value = v;
        });

        return cameraSensitivitySlider.value;
    }

    public void invertyYButtonClicked()
    {
        if (invertY.isOn)
            bInvertY = true; 
        else if(!invertY.isOn)
            bInvertY = false;

        // Add to the camera controller script to make the change possible using the bInvertY 

    }

    public void displayHUDButtonClicked() 
    {
        if (displayHUD.isOn)
            bDisplayHUD = true;
        else if (!displayHUD.isOn)
            bDisplayHUD = false;

        gameManager.instance.HUD.SetActive(bDisplayHUD);
        
    }

    public void SaveSettings()
    {
        SetCameraSensitivity(cameraSensitivitySlider);
        SetAudioLevel(audioSlider); 
        gameManager.instance.audioScript.SetMasterVolume(GetAudioLevel()); 
    }
}
