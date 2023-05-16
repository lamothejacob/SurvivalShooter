using UnityEngine.Audio; 
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name; 
    public AudioClip clip;
    [Range(0f,100f)]public float volume; 
    [Range(.1f, 3.0f)]public float pitch;

    [HideInInspector] public AudioSource source; 

    void Start()
    {
        // add 
        volume = 50f;
        pitch = 1f; 
    }
}
