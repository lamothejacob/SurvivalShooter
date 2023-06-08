using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CrateBuy : MonoBehaviour
{
    enum itemSelection
    {
        pistol, 
        machineGun, 
        shotgun, 
        sniper, 
        uzi, 
        rocketLauncher, 
        flameThrower, 
        grenade, 
        shield, 
        dash, 
        ammo
    }

    itemSelection userSelection;

    [Header("=== Menu Components ===")]
    [SerializeField] GameObject CratePrefab; 
    [SerializeField] GameObject CrateBuyMenu; 
    [SerializeField] TextMeshProUGUI ItemType_Text;
    [SerializeField] TextMeshProUGUI CurrentPoints;
    [SerializeField] Image itemImage;
    [SerializeField] GameObject NotEnoughPointsText; 

    [Header("=== Item Buttons ===")]
    [SerializeField] Button pistolButton;
    [SerializeField] Button machineGunButton;
    [SerializeField] Button shotgunButton;
    [SerializeField] Button sniperButton;
    [SerializeField] Button uziButton;
    [SerializeField] Button rocketLauncherButton;
    [SerializeField] Button flameThrowerButton;
    [SerializeField] Button grenadeButton;
    [SerializeField] Button shieldButton;
    [SerializeField] Button dashButton;
    [SerializeField] Button ammoButton; 


    [Header("=== Item Components ===")]
    [SerializeField] TextMeshProUGUI pistolLabel;
    [SerializeField] TextMeshProUGUI machineGunLabel;
    [SerializeField] TextMeshProUGUI shotgunLabel;
    [SerializeField] TextMeshProUGUI sniperLabel;
    [SerializeField] TextMeshProUGUI uziLabel;
    [SerializeField] TextMeshProUGUI rocketLauncherLabel;
    [SerializeField] TextMeshProUGUI flameThrowerLabel;
    [SerializeField] TextMeshProUGUI grenadeLabel;
    [SerializeField] TextMeshProUGUI shieldLabel;
    [SerializeField] TextMeshProUGUI dashLabel;
    [SerializeField] TextMeshProUGUI ammoLabel; 
    [SerializeField] int grenadeCost;
    [SerializeField] int shieldCost;
    [SerializeField] int dashCost;
    [SerializeField] int ammoCost;
    int pistolCost;
    int machineGunCost; 
    int shotgunCost;
    int sniperCost;
    int uziCost; 
    int rocketCost;
    int flameCost;

    [Header("=== Item Image ===")]
    [SerializeField] Sprite[] itemImages; 

    [Header("=== Stats ===")]
    [SerializeField] Slider damageSlider;
    [SerializeField] Slider fireRateSlider;
    [SerializeField] Slider rangeSlider;
    [SerializeField] Slider ammoCapacitySlider;

    [Header("=== Item Handouts ===")]
    [SerializeField] Gun pistol;
    [SerializeField] Gun machineGun;
    [SerializeField] Gun shotgun;
    [SerializeField] Gun sniper;
    [SerializeField] Gun uzi;
    [SerializeField] Gun rocket;
    [SerializeField] Gun flame;
    

    private int playerPoints;

    // Start is called before the first frame update
    void Start()
    {
        CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());

        playerPoints = gameManager.instance.playerScript.getPoints();
        pistolCost = 100; 
        machineGunCost = 1000;
        shotgunCost = 500;
        sniperCost = 1500; 
        uziCost = 1000;
        rocketCost = 2000;
        flameCost = 1250;

        pistolLabel.SetText("Pistol - " + pistolCost); 
        machineGunLabel.SetText("Machine Gun - " + machineGunCost);
        shotgunLabel.SetText("Shotgun - " +  shotgunCost);
        sniperLabel.SetText("Sniper  - " + sniperCost);
        uziLabel.SetText("Uzi - " +  uziCost); 
        rocketLauncherLabel.SetText("Rocket Launcher - " +  rocketCost);
        flameThrowerLabel.SetText("Flame Thrower - " +  flameCost);
        grenadeLabel.SetText("3 Grenades - " +  grenadeCost);
        shieldLabel.SetText("1 Shield - " + shieldCost);
        dashLabel.SetText("2 Dash - " +  dashCost); 
        ammoLabel.SetText("50 Rounds - " + ammoCost);


        pistolButtonClicked(); 


    }

    // Update is called once per frame
    void Update()
    {
        CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString()); 
    }

    void UpdatePlayerPoints(int cost)
    {
        CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
    }

    public void pistolButtonClicked()
    { 
        userSelection = itemSelection.pistol;

        // set the Item Image
        itemImage.sprite = itemImages[0]; 

        damageSlider.value = 0.1f;
        fireRateSlider.value = 0.8f;
        rangeSlider.value = 0.5f;
        ammoCapacitySlider.value = 0.8f;

        ItemType_Text.SetText("Pistol"); 
    }

    public void machineGunButtonClicked()
    {
        userSelection = itemSelection.machineGun;

        // set the Item Image
        itemImage.sprite = itemImages[1];

        damageSlider.value = 0.2f;
        fireRateSlider.value = 0.6f;
        rangeSlider.value = 0.5f;
        ammoCapacitySlider.value = 0.6f;

        ItemType_Text.SetText("Machine Gun");
    }

    public void shotgunButtonClicked()
    {
        userSelection = itemSelection.shotgun;

        // set the Item Image
        itemImage.sprite= itemImages[2];

        damageSlider.value = 0.5f;
        fireRateSlider.value = 0.4f;
        rangeSlider.value = 0.25f;
        ammoCapacitySlider.value = 0.22f;

        ItemType_Text.SetText("Shotgun"); 
    }

    public void sniperButtonClicked()
    {
        userSelection = itemSelection.sniper;

        // set the Item Image
        itemImage.sprite = itemImages[3]; 

        damageSlider.value = 1f;
        fireRateSlider.value = 0.4f;
        rangeSlider.value = 1f;
        ammoCapacitySlider.value = 0.15f;

        ItemType_Text.SetText("Sniper");
    }

    public void uziButtonClicked()
    {
        userSelection = itemSelection.uzi;

        // set the Item Image
        itemImage.sprite = itemImages[4];

        damageSlider.value = 0.15f;
        fireRateSlider.value = 0.85f;
        rangeSlider.value = .5f;
        ammoCapacitySlider.value = 0.4f;

        ItemType_Text.SetText("Uzi"); 
    }

    public void rocketLancherButtonClicked()
    {
        userSelection = itemSelection.rocketLauncher;

        // set the Item Image
        itemImage.sprite = itemImages[5]; 

        damageSlider.value = 1f;
        fireRateSlider.value = 0.1f;
        rangeSlider.value = .75f;
        ammoCapacitySlider.value = 0.02f;

        ItemType_Text.SetText("Rocket Launcher");
    }

    public void flameThrowerButtonClicked()
    {
        userSelection = itemSelection.flameThrower;

        // set the Item Image
        itemImage.sprite= itemImages[6];

        damageSlider.value = .75f;      //currently this says zero in the gun prefab?
        fireRateSlider.value = 1f;
        rangeSlider.value = .1f;
        ammoCapacitySlider.value = 1f;

        ItemType_Text.SetText("Flame Thrower");
    }

    public void grenadeButtonClicked()
    {
        userSelection = itemSelection.grenade;

        // set the Item Image
        itemImage.sprite = itemImages[7]; 

        damageSlider.value = .2f;      //currently this says zero in the gun prefab?
        fireRateSlider.value = .2f;
        rangeSlider.value = .125f;
        ammoCapacitySlider.value = 0.2f;

        ItemType_Text.SetText("Grenade");
    }

    public void shieldButtonClicked()
    {
        userSelection = itemSelection.shield;

        // set the Item Image
        itemImage.sprite = itemImages[8];

        damageSlider.value = 1f;      //currently this says zero in the gun prefab?
        fireRateSlider.value = 1f;
        rangeSlider.value = 1f;
        ammoCapacitySlider.value = 1f;

        ItemType_Text.SetText("Shield");
    }

    public void dashButtonClicked()
    {
        userSelection = itemSelection.dash;

        // set the Item Image
        itemImage.sprite = itemImages[9]; 

        damageSlider.value = 0.1f;      
        fireRateSlider.value = .01f;
        rangeSlider.value = 0.1f;
        ammoCapacitySlider.value = 0.2f;

        ItemType_Text.SetText("Dash Ability");
    }

    public void ammoButtonClicked()
    {
        userSelection = itemSelection.ammo;

        // set the Item Image; 
        itemImage.sprite= itemImages[10];

        damageSlider.value = 1f;
        fireRateSlider.value = 1f;
        rangeSlider.value = 1f;
        ammoCapacitySlider.value = 1f;

        ItemType_Text.SetText("50 Ammo Rounds");
    }

    public void purchaseItem()
    {
        switch(userSelection)
        {
            case itemSelection.pistol:
            {
               if (gameManager.instance.playerScript.getPoints() >= pistolCost)
               {
                  gameManager.instance.playerScript.addGun(pistol);
                  gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 100, 0);
                  playerPoints -= pistolCost; 
                  
               }
               else
                 StartCoroutine(notEnoughPoints());
               break;
            }
            case itemSelection.machineGun:
            {
               if(gameManager.instance.playerScript.getPoints() >= machineGunCost)
               {
                   gameManager.instance.playerScript.addGun(machineGun);
                    playerPoints -= machineGunCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                    gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 100, 0);
               }
               else
                 StartCoroutine(notEnoughPoints());
               break; 
            }
            case itemSelection.shotgun:
            {
                if (gameManager.instance.playerScript.getPoints() >= shotgunCost)
                {
                    gameManager.instance.playerScript.addGun(shotgun);
                        gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 100, 0);
                        playerPoints -= shotgunCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                        
                }
                else
                 StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.sniper:
            {
                if (gameManager.instance.playerScript.getPoints() >= sniperCost)
                {
                    gameManager.instance.playerScript.addGun(sniper);
                        gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 40, 0);
                        playerPoints -= sniperCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                  StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.uzi:
            {
                if (gameManager.instance.playerScript.getPoints() >= uziCost)
                { 
                    gameManager.instance.playerScript.addGun(uzi);
                        gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 100, 0);
                        playerPoints -= uziCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                  StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.rocketLauncher:
            {
                if (gameManager.instance.playerScript.getPoints() >= rocketCost)
                {
                    gameManager.instance.playerScript.addGun(rocket);
                        gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 5, 0);
                        playerPoints -= rocketCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                 StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.flameThrower:
            {
                if (gameManager.instance.playerScript.getPoints() >= sniperCost)
                {
                    gameManager.instance.playerScript.addGun(sniper);
                        gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 100, 0);
                        playerPoints -= sniperCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                 StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.grenade:
            {
                if(gameManager.instance.playerScript.getPoints() >= grenadeCost)
                {
                   gameManager.instance.playerScript.addGrenade(3);
                   playerPoints -= grenadeCost;
                   CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString()); 
                }
                else
                  StartCoroutine(notEnoughPoints());
                break; 
            }
            case itemSelection.shield:
            {
                if (gameManager.instance.playerScript.getPoints() >= shieldCost)
                {
                    gameManager.instance.playerScript.addShield(1);
                    playerPoints -= shieldCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                  StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.dash:
            {
                if (gameManager.instance.playerScript.getPoints() >= dashCost)
                {
                    gameManager.instance.playerScript.getDashNumCurrent();
                    playerPoints -= dashCost;
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                }
                else
                 StartCoroutine(notEnoughPoints());
                break;
            }
            case itemSelection.ammo:
            {
                if (gameManager.instance.playerScript.getPoints() >= ammoCost)
                {
                    gameManager.instance.playerScript.addAmmo(gameManager.instance.playerScript.getCurrentGun(), 50, ammoCost);
                    CurrentPoints.SetText(gameManager.instance.playerScript.getPoints().ToString());
                    
                }
                else
                 StartCoroutine(notEnoughPoints());
                break;
            }
             
        }
    }

    public void backButtonClicked()
    {  
        gameManager.instance.activeMenu = null;
        gameManager.instance.unPausedState();
        CrateBuyMenu.SetActive(false);

        Destroy(CratePrefab); 
    }

    IEnumerator notEnoughPoints()
    {
        NotEnoughPointsText.SetActive(true);
        yield return new WaitForSeconds(3);
        NotEnoughPointsText.SetActive(false); 
    }
}
