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
        }

    }

    private void Start()
    {
        if(gameManager.instance.activeMenu == gameManager.instance.mainMenu)
        {
            //Play("MainTheme"); 
        }
        else if(gameManager.instance.activeMenu == null || gameManager.instance.activeMenu == gameManager.instance.HUD)
        {
            //Play("CombatMusic"); 
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Shoot") && !gameManager.instance.isPaused)
        {
            if(gameManager.instance.playerScript.getCurrentGun().getAmmoInClip() > 0)
            {
                Play("Shoot");
            }
              
        }
        else if(Input.GetButtonDown("Reload") && !gameManager.instance.isPaused)
        {
            if(gameManager.instance.playerScript.getCurrentGun().getReserveAmmo() > 0)
            {
                Play("Reload");
            }
             
        }
    }

    public void Play(string name)
    {
        Sounds s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();    
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
