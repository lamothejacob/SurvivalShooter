using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using UnityEngine;

public class playerController : MonoBehaviour, IDamage, IPhysics
{
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
    [Range (0, 1)]
    [SerializeField] int currentGun = 0;
    [Range (0, 5)]
    [SerializeField] int grenadeAmount;
    [SerializeField] GameObject grenade;

    [Header("----- Starting Gun -----")]
    [SerializeField] Gun starterGun;
    public Vector3 gunLocation;

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

    [Header("----- Audio -----")]
    [SerializeField] AudioClip[] jumpAudio;
    [Range(0f, 1f)][SerializeField] float jumpAudioVol;
    [SerializeField] AudioClip[] damageAudio;
    [Range(0f, 1f)][SerializeField] float damageAudioVol;
    [SerializeField] AudioClip[] stepsAudio;
    [Range(0f, 1f)][SerializeField] float stepsAudioVol;


    private void Start()
    {
        HPOrig = HP;
        scaleOrig = transform.localScale;

        starterGun.Load();
        gunInventory.Add(starterGun);
        currentGun = gunInventory.Count - 1;
        gameManager.instance.displayScript.setCurrentGun(starterGun);
        gameManager.instance.hudScript.DisplayGunType();

    }

    void Update()
    {
        if (gameManager.instance.activeMenu == null)
        {
            Movement();

            if (((Input.GetButton("Shoot") && gunInventory[currentGun].automatic) || (Input.GetButtonDown("Shoot") && !gunInventory[currentGun].automatic)) && !isShooting)
            {
                StartCoroutine(Shoot());
            }

            if (Input.GetButton("Interact") && !isInteracting)
            {
                StartCoroutine(interact());
            }

            if (Input.GetButtonDown("Grenade") && grenadeAmount > 0)
            {
                throwGrenade();
            }

            SwitchWeapon();

            Reload();
        }

        Sprint();

        Crouch();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;

        if(groundedPlayer)
        {
            if(!stepsIsPlaying && move.normalized.magnitude > 0.5f)
                StartCoroutine(playSteps());

            if(playerVelocity.y < 0)
            {
                playerVelocity.y = 0f;
                jumpedTimes = 0;
            }
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
                (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps)
        {
            aud.PlayOneShot(jumpAudio[Random.Range(0, jumpAudio.Length)], jumpAudioVol);
            playerVelocity.y += jumpHeight;
            jumpedTimes++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move((playerVelocity + pushBack) * Time.deltaTime);
        pushBack = Vector3.Lerp(pushBack, Vector3.zero, pushBackResolve * Time.deltaTime);
    }

    public void takePushBack(Vector3 direc, int damage)
    {
        pushBack = direc;
        TakeDamage(damage);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            isSprinting = true;
            playerSpeed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            isSprinting = false;
            playerSpeed /= sprintMod;
        }
    }

    void Crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            Transform gts = gameManager.instance.displayScript.currentActive.transform;

            transform.localScale = new Vector3(scaleOrig.x, scaleOrig.y/2, scaleOrig.z);
            gts.localScale = new Vector3(gts.localScale.x, gts.localScale.y * 2, gts.localScale.z);
            playerSpeed /= crouchMod;
        }
        else if (Input.GetButtonUp("Crouch"))
        {
            Transform gts = gameManager.instance.displayScript.currentActive.transform;

            transform.localScale = scaleOrig;
            playerSpeed *= crouchMod;
            gts.localScale = new Vector3(gts.localScale.x, gts.localScale.y / 2, gts.localScale.z);
        }
    }

    void SwitchWeapon()
    {
        if (Input.GetButtonDown("Switch") && gunInventory.Count > 1 && !isShooting)
        {
            currentGun = currentGun == 0 ? 1 : 0;
            gameManager.instance.displayScript.setCurrentGun(gunInventory[currentGun]);
            gameManager.instance.hudScript.DisplayGunType();
        }
    }

    void Reload()
    {
        if (Input.GetButtonDown("Reload") && !isShooting)
        {
            gunInventory[currentGun].reload();
            gameManager.instance.hudScript.UpdateHUD();
        }
    }

    IEnumerator Shoot()
    {
        if(gunInventory.Count == 0 || gunInventory[currentGun].getAmmoInClip() == 0)
        {
            yield break;
        }

        isShooting = true;

        gunInventory[currentGun].removeAmmo(1);

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, gunInventory[currentGun].shootDist))
        {
            IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.TakeDamage(gunInventory[currentGun].damage);
                Debug.Log("Hit");
                points += 10;
            }

            Destroy(Instantiate(gunInventory[currentGun].hitEffect, hit.point, Quaternion.identity), 1);
        }

        yield return new WaitForSeconds(gunInventory[currentGun].fireRate);

        isShooting = false;
    }

    IEnumerator interact()
    {
        isInteracting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                interactable.interact();
            }
        }

        yield return new WaitForSeconds(0.5f);

        isInteracting = false;
    }

    public void AddHealth(int amount)
    {
        HP += amount;
    }

    public void TakeDamage(int damage)
    {
        HP -= damage;
        aud.PlayOneShot(damageAudio[Random.Range(0, damageAudio.Length)], damageAudioVol);
        StartCoroutine(damageFlash());

        if (HP <= 0)
        {
            HP = HPOrig;
            gameManager.instance.loseState();
        }
    }

    IEnumerator damageFlash()
    {
        gameManager.instance.playerDamageFlash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageFlash.SetActive(false);
    }

    public bool hasGun(Gun gun)
    {
        if(gun == null) return false;
        if(gunInventory.Count <=  0) return false;

        foreach (Gun g in gunInventory)
        {
            if (g.gunImage == gun.gunImage)
            {
                return true;
            }
        }

        return false;
    }

    public void addGun(Gun gun)
    {
        if(points < gun.cost)
        {
            return;
        }

        if(gunInventory.Count < 2)
        {
            gunInventory.Add(gun);
            currentGun = gunInventory.Count - 1;
        }
        else
        {
            gunInventory[currentGun] = gun;
        }

        points -= gun.cost;

        gameManager.instance.displayScript.setCurrentGun(gun);
        gameManager.instance.hudScript.DisplayGunType();
    }

    public void addAmmo(Gun gun, int ammoAmount, int cost)
    {
        if (points < cost || !hasGun(gun))
        {
            return;
        }

        foreach (Gun g in gunInventory)
        {
            if (g.gunImage == gun.gunImage)
            {
                g.addAmmo(ammoAmount);
            }
        }
        
        points -= cost;
    }

    public void upgradeCurrentGun(float mult)
    {
        Gun gun = getCurrentGun();

        gun.level++;

        gun.damage = (int)(gun.damage * mult);
    }

    public Gun getCurrentGun()
    {
        if(gunInventory.Count == 0)
        {
            return null;
        }

        return gunInventory[currentGun]; 
    }

    void throwGrenade()
    {
        Instantiate(grenade, transform.position, transform.rotation);
        grenadeAmount--;
    }

    public int getHP()
    {
        return HP; 
    }

    public int getPoints()
    {
        return points;
    }

    public int getGrenadeAmount()
    {
        return grenadeAmount;
    }

    public void addGrenade(int amount)
    {
        grenadeAmount += amount;
    }

    public void addPoints(int amount)
    {
        points += amount;
    }

    public void toggleShooting(bool change)
    {
        isShooting = change;
    }

    IEnumerator playSteps()
    {
        stepsIsPlaying = true;
        aud.PlayOneShot(stepsAudio[Random.Range(0, stepsAudio.Length)], stepsAudioVol);

        if (!isSprinting)
            yield return new WaitForSeconds(0.4f);
        else
            yield return new WaitForSeconds(0.2f);

        stepsIsPlaying = false;         
    }

   
}
