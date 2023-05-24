using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunUpgrader : MonoBehaviour, IInteractable
{
    [SerializeField] AnimationCurve UpgradeCurve;
    [SerializeField] int costToUpgrade;
    [SerializeField] AudioSource audioPlayer;

    bool isUpgrading;

    /// <summary>
    /// Finds the audio source if one isn't provided
    /// </summary>
    void Start()
    {
        if(audioPlayer == null)
        {
            audioPlayer = GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Upgrades the gun for <b>costToUpgrade</b> based on the value returned by the <b>upgradeCurve</b> when the gun level is plugged in.
    /// </summary>
    public void interact()
    {
        if (gameManager.instance.playerScript.getPoints() >= costToUpgrade && !isUpgrading)
        {
            //Get damage multiplier from upgrade curve
            float mult = UpgradeCurve.Evaluate(gameManager.instance.playerScript.getCurrentGun().level + 1);

            //Increase the guns level and damage in the player script
            gameManager.instance.playerScript.upgradeCurrentGun(mult);

            //Decrement points by the cost to upgrade
            gameManager.instance.playerScript.addPoints(-costToUpgrade);

            //Refill gun's reserve ammo
            Gun g = gameManager.instance.playerScript.getCurrentGun();
            g.addAmmo(g.reserveAmmoMax - g.getReserveAmmo());

            //Play the upgrade animation
            StartCoroutine(Upgrade());
        }
    }

    /// <summary>
    /// Lerps the gun to the upgrade altar, changes the color, and lerps the gun back to the player.
    /// </summary>
    IEnumerator Upgrade()
    {
        isUpgrading = true;

        audioPlayer.Play();

        //Prevent the player from shooting/switching weapons/reloading while animation is playing
        gameManager.instance.playerScript.toggleShooting(true);

        //Get the gun object from the display script
        GameObject g = gameManager.instance.displayScript.currentActive;

        //Set the parent to the upgrade altar
        g.transform.parent = transform;

        //Change the gun's layer so it isn't drawn through walls
        g.layer = 1;

        foreach (Transform child in g.transform)
        {
            child.gameObject.layer = 1;
        }

        //Lerp the position and rotation to the gun altar
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.position = Vector3.Lerp(g.transform.position, transform.position, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //Change the guns color to a random color
        randomizeColor(g);

        //Wait so the audio clip can play
        yield return new WaitForSeconds(.9f);

        //Set the gun's parent back to the camera
        g.transform.parent = Camera.main.transform;

        //Lerp it back to the player
        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.localPosition = Vector3.Lerp(g.transform.localPosition, gameManager.instance.playerScript.gunLocation, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, Camera.main.transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        //Change the layer back to the gun layer so it is drawn above everything else
        g.layer = 6;

        foreach (Transform child in g.transform)
        {
            child.gameObject.layer = 6;
        }

        //Allow player to shoot/reload/swap weapons again
        gameManager.instance.playerScript.toggleShooting(false);
        isUpgrading = false;
    }

    /// <summary>
    /// Chooses a random color to change the gun to
    /// </summary>
    /// <param name="gun">The gun whose color is getting changed</param>
    void randomizeColor(GameObject gun)
    {
        //Use the display script to change the guns color to the random color
        gameManager.instance.displayScript.SetGunColor(gun, new Color(Random.Range(50f, 205f) / 255f, Random.Range(50f, 205f) / 255f, Random.Range(50f, 205f) / 255f));
    }
}
