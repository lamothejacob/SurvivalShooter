using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class HUD : MonoBehaviour
{
    [Header("------ HUD Components ------")]
    [SerializeField] TextMeshProUGUI waveInfo;
    [SerializeField] TextMeshProUGUI enemies;
    [SerializeField] TextMeshProUGUI points;
    [SerializeField] TextMeshProUGUI ammoCount;
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
    
    // Start is called before the first frame update
    void Start()
    {
        bHUDVisible = true;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();  
    }

    void DisplayAmmo()
    {
        Gun tempGun = gameManager.instance.playerScript.getCurrentGun();

        if (tempGun == null)
        {
            ammoCount.SetText("0");
        }
        else
        {
            ammoCount.SetText(gameManager.instance.playerScript.getCurrentGun().getAmmoInClip().ToString());

        }
        
    }

    void DisplayGunType()
    {
        // set the gunType equal to the current gun.
        Gun currentGun = gameManager.instance.playerScript.getCurrentGun();

        gunType.material.mainTexture = currentGun.get2DTexture(); //Untested conversion, might not work properly
    }

    void UpdateHealthBar()
    {
        // Update Health Bar
        int currentHP = gameManager.instance.playerScript.getHP();       
        healthBar.value = currentHP;
        
    }

    void UpdateWave()
    {
       int waveNumber = gameManager.instance.enemySpawnerScript.GetWaveNumber();
       waveInfo.SetText("Wave " + waveNumber.ToString());
    }

    void updatePoints()
    {
        // Add a get points method to the playerController
        points.SetText(gameManager.instance.playerScript.getPoints().ToString());
    }

    void DisplayAmmoIcons()
    {
        // Get the current guns clip size, divide it by the current ammo left. If the ammo is over 10, we will just use that
        // percentage to display the opacity of the bullets
        Gun currentGun = gameManager.instance.playerScript.getCurrentGun();
        ammoLeft = currentGun.getAmmoInClip();
        clipSize = currentGun.getClipSize();

        float ammoLeftPercentage = ammoLeft / clipSize * 100; 

        if(clipSize > 10)
        {
            ammo100.color.WithAlpha(ammoLeftPercentage == 100 ? 100 : 40);
            ammo90.color.WithAlpha(ammoLeftPercentage >= 90 ? 100 : 40);
            ammo80.color.WithAlpha(ammoLeftPercentage >= 80 ? 100 : 40);
            ammo70.color.WithAlpha(ammoLeftPercentage >= 70 ? 100 : 40);
            ammo60.color.WithAlpha(ammoLeftPercentage >= 60 ? 100 : 40);
            ammo50.color.WithAlpha(ammoLeftPercentage >= 50 ? 100 : 40);
            ammo40.color.WithAlpha(ammoLeftPercentage >= 40 ? 100 : 40);
            ammo30.color.WithAlpha(ammoLeftPercentage >= 30 ? 100 : 40);
            ammo20.color.WithAlpha(ammoLeftPercentage >= 20 ? 100 : 40);
            ammo10.color.WithAlpha(ammoLeftPercentage >= 10 ? 100 : 40);
            ammo0.color.WithAlpha(ammoLeftPercentage > 0 ? 100 : 40);
        }


    }

    void DisplayKills()
    {
        // This will display how many enemies are left in the game.
        enemiesLeft = gameManager.instance.enemySpawnerScript.EnemiesLeft();
        enemies.SetText(enemiesLeft.ToString());
        
    }

    public void UpdateHUD()
    {
        DisplayAmmo(); 
        //DisplayAmmoIcons(); 
        DisplayKills();
        UpdateHealthBar();
        UpdateWave();
        updatePoints(); 
    }
}
