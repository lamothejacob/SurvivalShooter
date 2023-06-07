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


    // Start is called before the first frame update
    void Start()
    {
        bDisplayHUD = true;
        bInvertY = false;
        audioLevel.SetText(audioSlider.value.ToString());
        musicLevel.SetText(musicSlider.value.ToString());
        sfxLevel.SetText(sfxSlider.value.ToString());
        cameraSensitivity.SetText(cameraSensitivitySlider.value.ToString());
    }

    void Update()
    {
        audioLevel.SetText(audioSlider.value.ToString());
        musicLevel.SetText(musicSlider.value.ToString());
        sfxLevel.SetText(sfxSlider.value.ToString());
        cameraSensitivity.SetText(cameraSensitivitySlider.value.ToString());
    }        

    public float GetAudioLevel()
    {
        return audioSlider.value;
    }

    public float SetAudioLevel(Slider audio)
    {
        audioSlider.onValueChanged.AddListener((v) =>
        {
            
            audioLevel.SetText(v.ToString());
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

            musicLevel.SetText(v.ToString());
            musicSlider.value = v;
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
            cameraSensitivity.SetText(v.ToString());
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
