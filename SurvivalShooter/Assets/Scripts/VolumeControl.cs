using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class VolumeControl : MonoBehaviour
{
    [SerializeField] string volPrmtr = "MasterVolume";
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;
    private bool disableToggleEvent;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderChange);
        toggle.onValueChanged.AddListener(HandleToggle);
    }

    private void HandleToggle(bool enableSound)
    {
        if (disableToggleEvent)
        {
            return;
        }

        if (enableSound)
        {
            slider.value = slider.maxValue*0.8f;
        }
        else
        {
            slider.value = slider.minValue;
        }
    }

    private void HandleSliderChange(float value)
    {
        mixer.SetFloat(volPrmtr, MathF.Log10(value)*30f);
        disableToggleEvent = true;
        toggle.isOn = slider.value > slider.minValue;
        disableToggleEvent = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volPrmtr, slider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(volPrmtr, slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
