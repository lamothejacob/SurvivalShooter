using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class playerController : MonoBehaviour, IDamage, IPhysics, IdataPersistence {
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;
    [SerializeField] AudioSource aud;

    [Header("----- Player Stats -----")]
    [Range(1, 100)]
    [SerializeField] int HP = 100;
    [Range(1f, 5f)]
    [SerializeField] float playerSpeed = 2.0f;
    [Range(1f, 5f)]
    [SerializeField] float sprintMod = 3.0f;
    [Range(1f, 5f)]
    [SerializeField] float crouchMod = 3.0f;
    [Range(1f, 25f)]
    [SerializeField] float jumpHeight = 1.0f;
    [Range(-9f, -50f)]
    [SerializeField] float gravityValue = -9.81f;
    [Range(1, 3)]
    [SerializeField] int maxJumps = 1;
    [Range(1f, 10f)]
    [SerializeField] float interactDistance = 1.0f;
    [SerializeField] int pushBackResolve;

    [Header("----- Mechanics -----")]
    [SerializeField] int points = 0;
    [Range(0, 1)]
    [SerializeField] int currentGun = 0;
    [Range(0, 5)]
    [SerializeField] int grenadeAmount;
    [SerializeField] GameObject[] grenade;
    [SerializeField] GameObject aimOverlay;

    [Header("----- Starting Gun -----")]
    [SerializeField] Gun starterGun;

    [Header("----- Abilities -----")]
    [Header("Shield")]
    [SerializeField] int shieldHP;

    [Header("Dash")]
    [SerializeField] int dashNumMax;
    [SerializeField] float dashCoolDown;
    [SerializeField] float dashLength;
    [SerializeField] float dashSpeed;
    [SerializeField] int dashDamage;
    [SerializeField] int dashPushBack;

    private Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private int jumpedTimes = 0;
    private bool isShooting = false;
    private bool isInteracting = false;
    private int HPOrig;
    private Vector3 scaleOrig;
    private Vector3 pushBack;
    [SerializeField] List<Gun> gunInventory;
    bool stepsIsPlaying;
    bool isSprinting;
    bool isAiming;
    float originalFOV;
    bool hasItem;

    bool hasImpact;
    float jumpHeightOrig;

    //Shield Variables
    bool shieldActive;
    int shieldHPMax;
    float playerSpeedOrig;

    //Dash Variables
    bool dashActive;
    int dashNum;
    bool dashRecharging;

    //Ability Progression Variables
    bool shieldPurchased;
    bool shieldUpgraded;
    bool dashPurchased;
    bool dashUpgraded;

    //Goal Item Variables
    bool hasGoalItem;

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] jumpAudio;
    [Range(0f, 1f)][SerializeField] float jumpAudioVol;
    [SerializeField] AudioClip[] damageAudio;
    [Range(0f, 1f)][SerializeField] float damageAudioVol;
    [SerializeField] AudioClip[] stepsAudio;
    [Range(0f, 1f)][SerializeField] float stepsAudioVol;


    private void Start() {
        HPOrig = HP;
        shieldHPMax = shieldHP;
        //dashNum = dashNumMax;
        playerSpeedOrig = playerSpeed;
        scaleOrig = transform.localScale;
        originalFOV = Camera.main.fieldOfView;

        starterGun.Load();
        gunInventory.Add(starterGun);
        currentGun = gunInventory.Count - 1;
        gameManager.instance.displayScript.setCurrentGun(starterGun);
        gameManager.instance.hudScript.DisplayGunType();

        shieldHP = 0;
        jumpHeightOrig = jumpHeight;

        //PlayerPrefs
        points = PlayerPrefs.GetInt("Points");
    }

    void Update() {
        if (gameManager.instance.activeMenu == null) {
            Movement();

            if (((Input.GetButton("Shoot") && gunInventory[currentGun].automatic) || (Input.GetButtonDown("Shoot") && !gunInventory[currentGun].automatic)) && !isShooting) {
                StartCoroutine(Shoot());
            }

            if (Input.GetButton("Interact") && !isInteracting) {
                StartCoroutine(interact());
            }

            if (Input.GetButtonDown("Grenade") && grenadeAmount > 0) {
                throwGrenade();
            }

            if (Input.GetButtonDown("Shield") && shieldHP > 0) {
                ActivateShield();
            } 
            else if (shieldHP <= 0)
                gameManager.instance.shieldActiveImage.SetActive(false);

            if (Input.GetButtonDown("Dash") && dashNum > 0) {
                dashNum--;
                StartCoroutine(Dash());
            }

            if (dashNum < dashNumMax && !dashRecharging && dashPurchased)
                StartCoroutine(DashRecharge());

            SwitchWeapon();

            Reload();
        }

        Sprint();

        Crouch();

        AimDownSights();
    }

    void AimDownSights() {
        if (Input.GetButtonDown("Aim")) {
            GameObject gun = gameManager.instance.displayScript.currentActive;
            gun.transform.localPosition = getCurrentGun().aimOffset;
            Camera.main.fieldOfView /= 1.75f;
            isAiming = true;

            if (getCurrentGun().hasAimOverlay) {
                gun.SetActive(false);
                aimOverlay.SetActive(true);
            }
        } else if (Input.GetButtonUp("Aim")) {
            GameObject gun = gameManager.instance.displayScript.currentActive;
            gun.transform.localPosition = getCurrentGun().handOffset;
            Camera.main.fieldOfView = originalFOV;
            isAiming = false;

            if (getCurrentGun().hasAimOverlay) {
                gun.SetActive(true);
                aimOverlay.SetActive(false);
            }
        }
    }

    void Movement() {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer) {
            if (!stepsIsPlaying && move.normalized.magnitude > 0.5f)
                StartCoroutine(playSteps());

            if (playerVelocity.y < 0) {
                playerVelocity.y = 0f;
                jumpedTimes = 0;
            }
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
                (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps) {
            aud.PlayOneShot(jumpAudio[Random.Range(0, jumpAudio.Length)], jumpAudioVol);
            playerVelocity.y += jumpHeight;
            jumpedTimes++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move((playerVelocity + pushBack) * Time.deltaTime);
        pushBack = Vector3.Lerp(pushBack, Vector3.zero, pushBackResolve * Time.deltaTime);
    }

    public void takePushBack(Vector3 direc, int damage) {
        pushBack = direc;
        TakeDamage(damage);
    }

    void Sprint() {
        if (Input.GetButtonDown("Sprint")) {
            isSprinting = true;
            playerSpeed *= sprintMod;
        } else if (Input.GetButtonUp("Sprint") && isSprinting) {
            isSprinting = false;
            playerSpeed /= sprintMod;
        }
    }

    void Crouch() {
        if (Input.GetButtonDown("Crouch")) {
            Transform gts = gameManager.instance.displayScript.currentActive.transform;

            transform.localScale = new Vector3(scaleOrig.x, scaleOrig.y / 2, scaleOrig.z);
            gts.localScale = new Vector3(gts.localScale.x, gts.localScale.y * 2, gts.localScale.z);
            playerSpeed /= crouchMod;
        } else if (Input.GetButtonUp("Crouch")) {
            Transform gts = gameManager.instance.displayScript.currentActive.transform;

            transform.localScale = scaleOrig;
            playerSpeed *= crouchMod;
            gts.localScale = new Vector3(gts.localScale.x, gts.localScale.y / 2, gts.localScale.z);
        }
    }

    void SwitchWeapon() {
        if (Input.GetButtonDown("Switch") && gunInventory.Count > 1 && !isShooting) {
            currentGun = currentGun == 0 ? 1 : 0;
            gameManager.instance.displayScript.setCurrentGun(gunInventory[currentGun]);
            gameManager.instance.hudScript.DisplayGunType();
        }
    }

    void Reload() {
        if (Input.GetButtonDown("Reload") && !isShooting) {
            Gun gun = getCurrentGun();
            if (gun.getAmmoInClip() < gun.clipSize && gun.getReserveAmmo() > 0) {
                StartCoroutine(Reloading());
            }
        }
    }

    IEnumerator Reloading() {
        isShooting = true;
        GameObject gun = gameManager.instance.displayScript.currentActive;
        Vector3 startPosition = gun.transform.localPosition, endPosition = startPosition - Vector3.up * 2;

        float elapsedTime = 0f, totalTime = .1f;
        while (elapsedTime < totalTime) {
            gun.transform.localPosition = Vector3.Lerp(startPosition, endPosition, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gun.transform.localPosition = endPosition;

        getCurrentGun().reload();
        yield return new WaitForSeconds(getCurrentGun().reloadAudio.length - 0.1f);

        elapsedTime = 0f;
        totalTime = .1f;
        Vector3 gunLocation = getCurrentGun().handOffset;

        while (elapsedTime < totalTime) {
            gun.transform.localPosition = Vector3.Lerp(endPosition, gunLocation, elapsedTime / totalTime);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        gun.transform.localPosition = gunLocation;

        isShooting = false;
        gameManager.instance.hudScript.UpdateHUD();
    }

    IEnumerator Shoot() {
        if (gunInventory.Count == 0) {
            yield break;
        }

        if (gunInventory[currentGun].getAmmoInClip() == 0) {
            isShooting = true;
            gameManager.instance.audioScript.Play(getCurrentGun().clipEmptyAudio);
            yield return new WaitForSeconds(getCurrentGun().clipEmptyAudio.length * .75f);
            isShooting = false;
            yield break;
        }

        isShooting = true;

        Gun gun = gunInventory[currentGun];

        gun.removeAmmo(1);
        gameManager.instance.audioScript.Play(getCurrentGun().fireAudio);

        List<RaycastHit> raycastHits = gun.GetRayList();

        foreach (RaycastHit hit in raycastHits) {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null) {
                damageable.TakeDamage(gun.damage);
                points += 50;
            }

            Destroy(Instantiate(gun.hitEffect, hit.point, Quaternion.identity), 1);
        }

        gameManager.instance.displayScript.MuzzleFlash();

        StopCoroutine(Recoil());
        StartCoroutine(Recoil());

        yield return new WaitForSeconds(gun.fireRate);

        isShooting = false;
    }

    IEnumerator Recoil() {
        float recoil = gunInventory[currentGun].verticalRecoil;

        if (isAiming) {
            recoil /= 2;
        }

        float timeElapsed = 0f;
        while (timeElapsed < gunInventory[currentGun].fireRate) {
            gameManager.instance.cameraScript.AddRecoil(Mathf.Lerp(0, recoil, timeElapsed / gunInventory[currentGun].fireRate));
            timeElapsed += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator interact() {
        isInteracting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, interactDistance)) {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null) {
                interactable.interact();
            }
        }

        yield return new WaitForSeconds(0.5f);

        isInteracting = false;
    }

    public void AddHealth(int amount) {
        if (HP + amount > HPOrig)
        {
            HP = HPOrig;
        }
        else
            HP += amount;
    }

    public void TakeDamage(int damage) {
        if (shieldActive == false || shieldHP <= 0) {
            HP -= damage;
            aud.PlayOneShot(damageAudio[Random.Range(0, damageAudio.Length)], damageAudioVol);
            StartCoroutine(damageFlash());

            if (HP <= 0) {
                HP = HPOrig;
                gameManager.instance.loseState();
            }
        } 
        else 
        {
            shieldHP -= damage;
            if (shieldUpgraded)
            {
                if (HP + damage > HPOrig)
                {
                    HP = HPOrig;
                }
                else
                {
                    HP += damage;
                }
            }
        }
    }

    IEnumerator damageFlash() {
        gameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageFlash.SetActive(false);
    }

    public bool hasGun(Gun gun) {
        if (gun == null) return false;
        if (gunInventory.Count <= 0) return false;

        foreach (Gun g in gunInventory) {
            if (g.gunImage == gun.gunImage) {
                return true;
            }
        }

        return false;
    }

    public void addGun(Gun gun) {
        if (points < gun.cost) {
            return;
        }

        if (gunInventory.Count < 2) {
            gunInventory.Add(gun);
            currentGun = gunInventory.Count - 1;
        } else {
            gunInventory[currentGun] = gun;
        }

        points -= gun.cost;

        gameManager.instance.displayScript.setCurrentGun(gun);
        gameManager.instance.hudScript.DisplayGunType();
    }

    public void addAmmo(Gun gun, int ammoAmount, int cost) {
        if (points < cost || !hasGun(gun)) {
            return;
        }

        foreach (Gun g in gunInventory) {
            if (g.gunImage == gun.gunImage) {
                g.addAmmo(ammoAmount);
            }
        }

        points -= cost;
    }

    public void upgradeCurrentGun(float mult) {
        Gun gun = getCurrentGun();

        gun.level++;

        gun.damage = (int)(gun.damage * mult);
    }

    public Gun getCurrentGun() {
        if (gunInventory.Count == 0) {
            return null;
        }

        return gunInventory[currentGun];
    }

    void throwGrenade() {
        Vector3 throwPos = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);

        if (!hasImpact)
            Instantiate(grenade[0], throwPos, transform.rotation);
        else if (hasImpact)
            Instantiate(grenade[1], throwPos, transform.rotation);
        grenadeAmount--;
    }

    public void toggleImpactGrenade(bool toggle)
    {
        hasImpact = toggle;
    }

    #region ------------- Player Abilities -------------

    IEnumerator Dash() {
        isSprinting = false;
        playerSpeed /= sprintMod;

        dashActive = true;
        playerSpeed = dashSpeed;

        yield return new WaitForSeconds(dashLength);

        playerSpeed = playerSpeedOrig;
        dashActive = false;

        if (Input.GetButton("Sprint"))
        {
            isSprinting = true;
            playerSpeed *= sprintMod;
        }
    }

    IEnumerator DashRecharge() {
        dashRecharging = true;

        yield return new WaitForSeconds(dashCoolDown);

        dashNum++;
        dashRecharging = false;
    }

    public void BuyDash()
    {
        dashPurchased = true;
        dashNum = dashNumMax;
    }

    public bool isDashPurchased()
    {
        return dashPurchased;
    }

    public bool isDashUpgraded()
    {
        return dashUpgraded;
    }

    public void UpgradeDash()
    {
        dashUpgraded = true;
    }

    public void BuyShield()
    {
        shieldPurchased = true;
    }

    void ActivateShield()
    {
        if (shieldPurchased)
        {
            shieldActive = !shieldActive;
            gameManager.instance.shieldActiveImage.SetActive(shieldActive);
        }
    }

    public bool isShieldPurchased()
    {
        return shieldPurchased;
    }

    public bool isShieldUpgraded()
    {
        return shieldUpgraded;
    }

    public void UpgradeShield()
    {
        shieldUpgraded = true;
    }
    #endregion

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Goal Item")) {
            Destroy(other.gameObject);
            hasGoalItem = true;
        }
    }

    public int getHP() {
        return HP;
    }

    public int getPoints() {
        return points;
    }

    public int getGrenadeAmount() {
        return grenadeAmount;
    }

    public void addGrenade(int amount) {
        grenadeAmount += amount;
    }

    public void addPoints(int amount) {
        points += amount;
    }

    public void subPoints(int amount)
    {
        points -= amount;
    }

    public void toggleShooting(bool change) {
        isShooting = change;
    }

    public bool getShootingState() {
        return isShooting;
    }

    public int getShieldHP() {
        if (shieldPurchased)
            return shieldHP;
        else
            return 0;
    }

    public int getShieldHPMax() {
        return shieldHPMax;
    }

    public bool getDashState() {
        return dashActive;
    }

    public int getDashPushBack() {
        return dashPushBack;
    }

    public int getDashDamage() {
        return dashDamage;
    }

    public int getDashNumCurrent() {
        return dashNum;
    }

    public Vector3 getVelocity() {
        return move;
    }

    public void addShield(int amount) {
        if (shieldHP + amount > shieldHPMax)
            shieldHP = shieldHPMax;
        else
            shieldHP += amount;
    }

    public void LowGravity(float amount)
    {
        jumpHeight = amount;
    }

    public void ResetGravity()
    {
        jumpHeight = jumpHeightOrig;
    }

    IEnumerator playSteps() {
        stepsIsPlaying = true;
        aud.PlayOneShot(stepsAudio[Random.Range(0, stepsAudio.Length)], stepsAudioVol);

        if (!isSprinting)
            yield return new WaitForSeconds(0.4f);
        else
            yield return new WaitForSeconds(0.2f);

        stepsIsPlaying = false;
    }

    public void LoadData(GameData data)
    {

        this.points = data.points;
        this.dashPurchased = data.dashPurchased;
        this.dashUpgraded = data.dashUpgraded;
        this.shieldPurchased = data.shieldPurchased;
        this.shieldUpgraded = data.ShieldUpgraded;
    }

    public void SaveData(ref GameData data)
    {
        data.points = this.points;
        data.dashPurchased = this.dashPurchased;
        data.dashUpgraded = this.dashUpgraded;
        data.shieldPurchased = this.shieldPurchased;
        data.ShieldUpgraded = this.shieldUpgraded;
        data.guns = this.gunInventory;
    }
}
