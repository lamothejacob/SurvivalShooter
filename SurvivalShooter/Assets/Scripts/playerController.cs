using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("----- Components -----")]
    [SerializeField] CharacterController controller;

    [Header("----- Player Stats -----")]
    [Range(1, 100)]
    [SerializeField] int HP = 100;
    [Range(1f, 5f)]
    [SerializeField] float playerSpeed = 2.0f;
    [Range(1f, 5f)]
    [SerializeField] float sprintMod = 3.0f;
    [Range(1f, 25f)]
    [SerializeField] float jumpHeight = 1.0f;
    [Range(-10f, -50f)]
    [SerializeField] float gravityValue = -10f;
    [Range(1, 3)]
    [SerializeField] int maxJumps = 1;

    [Header("----- Weapon Stats -----")]
    [Range(2, 300)]
    [SerializeField] int shootDistance;
    [Range(0.1f, 3f)]
    [SerializeField] float shootRate;
    [Range(1, 10)]
    [SerializeField] int shootDamage;

    private Vector3 move;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private int jumpedTimes = 0;
    private bool isShooting = false;
    private int HPOrig;

    private void Start()
    {
        HPOrig = HP;
        //spawnPlayer();
    }

    void Update()
    {
        //if (gameManager.instance.activeMenu == null)
        //{
            Movement();

            if (Input.GetButton("Shoot") && !isShooting)
            {
                StartCoroutine(Shoot());
            }
        //}

        Sprint();
    }

    void Movement()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            jumpedTimes = 0;
        }

        move = (transform.right * Input.GetAxis("Horizontal")) +
                (transform.forward * Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && jumpedTimes < maxJumps)
        {
            playerVelocity.y += jumpHeight;
            jumpedTimes++;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }

    void Sprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            playerSpeed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            playerSpeed /= sprintMod;
        }
    }

    IEnumerator Shoot()
    {
        isShooting = true;

        RaycastHit hit;

        if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(0.5f, 0.5f)), out hit, shootDistance))
        {
            /**IDamage damageable = hit.collider.GetComponent<IDamage>();

            if (damageable != null)
            {
                damageable.takeDamage(shootDamage);
            }*/
        }

        yield return new WaitForSeconds(shootRate);

        isShooting = false;
    }

    public void AddHealth(int amount)
    {
        HP += amount;
    }

    public void takeDamage(int damage)
    {
        HP -= damage;

        if (HP <= 0)
        {
            HP = HPOrig;
            //gameManager.instance.loseState();
        }
    }

    public void spawnPlayer()
    {
        controller.enabled = false;
        //transform.position = gameManager.instance.playerSpawn.transform.position;
        controller.enabled = true;
    }
}
