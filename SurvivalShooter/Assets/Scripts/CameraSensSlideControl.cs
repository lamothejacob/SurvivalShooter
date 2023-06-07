using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraSensSlideControl : MonoBehaviour
{
    [SerializeField] string sensParameter = "CameraSensitivity";
    [SerializeField] Slider slider;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderChange);
    }

    private void HandleSliderChange(float val)
    {
        gameManager.instance.cameraScript.SetSensitivityHor((int)slider.value);
        gameManager.instance.cameraScript.SetSensitivityVer((int)slider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sensParameter, slider.value);
    }

    // Start is called before the first frame update
    void Start()
    {
        slider.value = PlayerPrefs.GetFloat(sensParameter, slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
