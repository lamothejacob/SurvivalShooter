using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseLight : MonoBehaviour
{
    [SerializeField] Light myLight;

    private float maxIntensity = 1f; 
    private float minIntensity = 0.10f;
    private float pulseSpeed = 1;
    private float targetIntensity; 
    private float currentIntensity;

    // Update is called once per frame
    void Update()
    {
        currentIntensity = Mathf.MoveTowards(myLight.intensity, targetIntensity, Time.deltaTime * pulseSpeed);
        if (currentIntensity >= maxIntensity)
        {
            currentIntensity = maxIntensity;
            targetIntensity = minIntensity;
        }
        else if (currentIntensity <= minIntensity)
        {
            currentIntensity = minIntensity;
            targetIntensity = maxIntensity;
        }
        myLight.intensity = currentIntensity;
    }
}
