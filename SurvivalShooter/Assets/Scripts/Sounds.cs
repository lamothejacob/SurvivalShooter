using UnityEngine.Audio; 
using UnityEngine;

[System.Serializable]
public class Sounds
{
    public string name; 
    public AudioClip clip;
    public AudioMixerGroup mixer;
    [Range(0f,1f)]public float volume = 1; 
    [Range(.1f, 3.0f)]public float pitch;
    public bool loop;

    public AudioSource source; 

    void Start()
    {
        // add 
        volume = 50f;
        pitch = 1f; 
    }
}
