using UnityEngine.Audio;
using System; 
using UnityEngine;
using UnityEditor;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; 

    public Sounds[] sounds;
    private float masterVolume; 


    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sounds s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }

    }

    private void Start()
    {
        if(gameManager.instance.activeMenu == gameManager.instance.mainMenu)
        {
            Stop("CombatMusic");
            Play("MainTheme"); 
        }
        else if(gameManager.instance.activeMenu == null || gameManager.instance.activeMenu == gameManager.instance.HUD)
        {
            Stop("MainTheme");
            Play("CombatMusic");
        }

        
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return; 
        }
        if (!s.loop)
        {
            s.source.PlayOneShot(s.clip);
        }
        else
        {
            s.source.Play();
        }
    }

    public void Stop(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void SetMasterVolume(float newVolume)
    {
        foreach (Sounds s in sounds)
        {
            s.volume = newVolume;
            s.source.volume = newVolume / 100;
        }
    }
}
