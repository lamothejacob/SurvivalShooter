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
    [SerializeField] TextMeshProUGUI grenadeAmount;
    [SerializeField] Slider healthBar;
    [SerializeField] Slider shieldBar;
    [SerializeField] Image gunType;
    [SerializeField] Image miniMap;
    [SerializeField] Image dashIcon;
    [SerializeField] TextMeshProUGUI dashText;

    [Header("------ Ammo Icons ------")]
    [SerializeField] Image[] ammoIcons = new Image[11];

    private bool bHUDVisible;
    private int ammoLeft;
    private int clipSize; 
    private float enemiesKilled;
    private float enemiesLeft;
    private float pointScored;
    private float waveNumber;

    private Color ammoColorTrans;
    private Color ammoColor;
    private Color ammoColorEmpty;

    // Start is called before the first frame update
    void Start()
    {
        gunType.preserveAspect = true;
        bHUDVisible = true;
        ammoColorTrans = new Color(1f, 1f, 1f, .4f);
        ammoColor = new Color(1f, 1f, 1f, 1f);
        ammoColorEmpty = new Color(1f, 1f, 1f, 0f);
        shieldBar.maxValue = gameManager.instance.playerScript.getShieldHPMax();
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
            ammoCount.SetText(Mathf.Min(999, gameManager.instance.playerScript.getCurrentGun().getReserveAmmo()).ToString());
            DisplayAmmoIcons();
        }
    }

    public void DisplayGunType()
    {
        // set the gunType equal to the current gun.
        Texture2D currentGun = gameManager.instance.playerScript.getCurrentGun().gunImage;

        Rect rect = new Rect(0, 0, currentGun.width, currentGun.height);
        gunType.sprite = Sprite.Create(currentGun, rect, new Vector2(0.5f, 0.5f));

        DisplayAmmo();
    }

    void UpdateHealthBar()
    {
        // Update Health Bar
        int currentHP = gameManager.instance.playerScript.getHP();       
        healthBar.value = currentHP;
        
    }

    void UpdateShieldBar()
    {
        int currentShieldHP = gameManager.instance.playerScript.getShieldHP();
        shieldBar.value = currentShieldHP;
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
        clipSize = currentGun.clipSize;

        float ammoLeftPercentage = (float)ammoLeft / (float)clipSize * 100f;

        if (clipSize >= 10)
        {
            for(int i = 0;  i < ammoIcons.Length; i++)
            {
                if (i != 10)
                {
                    //ammoIcons[i].color = ammoIcons[i].color.WithAlpha(ammoLeftPercentage >= (10 - i) * 10 ? 1f : .4f);

                    if (ammoLeftPercentage >= (10 - i) * 10)
                    {
                        ammoIcons[i].color = ammoColor;
                    }
                    else
                    {
                        ammoIcons[i].color = ammoColorTrans;
                    }
                }
                else
                {
                    //ammoIcons[i].color = ammoIcons[i].color.WithAlpha(ammoLeftPercentage > (10 - i) * 10 ? 1f : .4f);

                    if (ammoLeftPercentage > (10 - i) * 10)
                    {
                        ammoIcons[i].color = ammoColor;
                    }
                    else
                    {
                        ammoIcons[i].color = ammoColorTrans;
                    }
                }
            }
        }
        else
        {
            for(int i = 10; i >= 0; i--){
                if(i > clipSize - 1){
                    ammoIcons[i].color = ammoColorEmpty;
                }
                else if(clipSize - i > ammoLeft){
                    ammoIcons[i].color = ammoColorTrans;
                }
                else{
                    ammoIcons[i].color = ammoColor;
                }
            }
        }
    }

    void DisplayKills()
    {
        // This will display how many enemies are left in the game.
        enemiesLeft = gameManager.instance.enemySpawnerScript.EnemiesLeft();
        enemies.SetText(enemiesLeft.ToString());
        
    }

    void DisplayGrenadeAmount()
    {
        grenadeAmount.text = gameManager.instance.playerScript.getGrenadeAmount().ToString();
    }

    void DisplayDash()
    {
        int dashNumCurrent = gameManager.instance.playerScript.getDashNumCurrent();

        if (dashNumCurrent <= 0)
        {
            dashIcon.color = ammoColorTrans;
            dashText.color = ammoColorTrans;
        }
        else
        {
            dashIcon.color = ammoColor;
            dashText.color = ammoColor;
        }

        dashText.text = dashNumCurrent.ToString();
    }

    public void UpdateHUD()
    {
        DisplayAmmo(); 
        DisplayKills();
        UpdateHealthBar();
        UpdateShieldBar();
        UpdateWave();
        updatePoints();
        DisplayGrenadeAmount();
        DisplayDash();
    }
}
