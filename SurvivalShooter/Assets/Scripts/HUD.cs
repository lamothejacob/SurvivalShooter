using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        // ammoCount = gameManager.instance.    DONT THINK ANYONE IS KEEPING TRACK OF THIS AS OF RIGHT NOW. 
    }

    void DispalyGunType()
    {
        // set the gunType equal to the current gun. 
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
        //clipSize = gameManager.instance.playerScript.getCurrentGun().
        
    }

    void DisplayKills()
    {
        // This will display how many enemies are left in the game. I don't know if we want to display the kill count, or enemies left
        enemiesLeft = gameManager.instance.enemySpawnerScript.EnemiesLeft();
        enemies.text = enemiesLeft.ToString();
        
    }
}
