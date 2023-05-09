using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [Header("------ HUD Components ------")]
    [SerializeField] TextMeshPro ammoCount;
    [SerializeField] TextMeshPro waveInfo;
    [SerializeField] TextMeshPro enemies;
    [SerializeField] TextMeshPro points;
    [SerializeField] Slider healthBar;   
    [SerializeField] Image gunType;
    [SerializeField] Image miniMap; 

    [Header("------ Ammo Icons ------")]
    [SerializeField] Image ammo100;
    [SerializeField] Image ammo90;
    [SerializeField] Image ammo80;
    [SerializeField] Image ammo70;
    [SerializeField] Image ammo60;
    [SerializeField] Image ammo50;
    [SerializeField] Image ammo40;
    [SerializeField] Image ammo30;
    [SerializeField] Image ammo20;
    [SerializeField] Image ammo10;
    [SerializeField] Image ammo0;


    private bool bHUDVisible;
    private int ammoLeft;
    private int clipSize; 
    private float enemiesKilled;
    private float enemiesLeft;
    private float pointScored;
    private float waveNumber;
    
    public 
    // Start is called before the first frame update
    void Start()
    {
        bHUDVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisplayAmmo()
    {
        Gun currentGun = gameManager.instance.playerScript.GetComponent<Gun>();
        ammoCount.text = currentGun.getAmmoInClip().ToString(); 
    }

    void DisplayGunType()
    {
        // set the gunType equal to the current gun.
        Gun currentGun = gameManager.instance.playerScript.GetComponent<Gun>();

    }

    void UpdateHealthBar()
    {
        // Update Health Bar
        int currentHP = gameManager.instance.playerScript.getHP();       
        healthBar.value = currentHP;
    }

    void UpdateWave()
    {
       WaveEditorSO waveNumber = gameManager.instance.enemySpawnerScript.GetCurrentWave();
       waveInfo.text = "Wave " + waveNumber.ToString();
    }

    void updatePoints()
    {
        // Add a get points method to the playerController
        points.text = gameManager.instance.playerScript.getPoints().ToString();
    }

    void DisplayAmmoIcons()
    {
        // Get the current guns clip size, divide it by the current ammo left. If the ammo is over 10, we will just use that
        // percentage to display the opacity of the bullets
        Gun currentGun = gameManager.instance.playerScript.GetComponent<Gun>();
        ammoLeft = currentGun.getAmmoInClip();
        clipSize = currentGun.getClipSize();

        float ammoLeftPercentage = ammoLeft / clipSize * 100; 

        if(clipSize > 10)
        {
            if (ammoLeftPercentage == 100)
            {
                ammo100.color.WithAlpha(100);
                ammo90.color.WithAlpha(100);
                ammo80.color.WithAlpha(100);
                ammo70.color.WithAlpha(100);
                ammo60.color.WithAlpha(100);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if (ammoLeftPercentage < 100 || ammoLeftPercentage >= 90)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(100);
                ammo80.color.WithAlpha(100);
                ammo70.color.WithAlpha(100);
                ammo60.color.WithAlpha(100);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if (ammoLeftPercentage < 90 || ammoLeftPercentage >= 80)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(100);
                ammo70.color.WithAlpha(100);
                ammo60.color.WithAlpha(100);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if (ammoLeftPercentage < 80 || ammoLeftPercentage >= 70)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);               
                ammo70.color.WithAlpha(100);
                ammo60.color.WithAlpha(100);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if (ammoLeftPercentage < 70 || ammoLeftPercentage >= 60)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(100);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 60 || ammoLeftPercentage >= 50)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(100);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 50 || ammoLeftPercentage >= 40)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(100);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 40 ||  ammoLeftPercentage >= 30)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(40);
                ammo30.color.WithAlpha(100);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 30 || ammoLeftPercentage >= 20)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(40);
                ammo30.color.WithAlpha(40);
                ammo20.color.WithAlpha(100);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 20 || ammoLeftPercentage >= 10)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(40);
                ammo30.color.WithAlpha(40);
                ammo20.color.WithAlpha(40);
                ammo10.color.WithAlpha(100);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage < 10 || ammoLeftPercentage >= 0)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(40);
                ammo30.color.WithAlpha(40);
                ammo20.color.WithAlpha(40);
                ammo10.color.WithAlpha(40);
                ammo0.color.WithAlpha(100);
            }
            else if(ammoLeftPercentage == 0)
            {
                ammo100.color.WithAlpha(40);
                ammo90.color.WithAlpha(40);
                ammo80.color.WithAlpha(40);
                ammo70.color.WithAlpha(40);
                ammo60.color.WithAlpha(40);
                ammo50.color.WithAlpha(40);
                ammo40.color.WithAlpha(40);
                ammo30.color.WithAlpha(40);
                ammo20.color.WithAlpha(40);
                ammo10.color.WithAlpha(40);
                ammo0.color.WithAlpha(40);
            }
        }


    }

    void DisplayKills()
    {
        // This will display how many enemies are left in the game.
        enemiesLeft = gameManager.instance.enemySpawnerScript.EnemiesLeft();
        enemies.text = enemiesLeft.ToString();
        
    }

    void UpdateHUD()
    {
        DisplayAmmo(); 
        DisplayAmmoIcons(); 
        DisplayKills();
        UpdateHealthBar();
        UpdateWave();
        updatePoints(); 
    }
}
