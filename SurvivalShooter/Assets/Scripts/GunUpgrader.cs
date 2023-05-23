using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunUpgrader : MonoBehaviour, IInteractable
{
    [SerializeField] AnimationCurve UpgradeCurve;
    [SerializeField] int costToUpgrade;
    [SerializeField] AudioClip upgradeSound;
    [SerializeField] AudioSource audioPlayer;

    bool isUpgrading;

    /// <summary>
    /// Finds the audio source if one isn't provided
    /// </summary>
    void Start()
    {
        if(audioPlayer == null)
        {
            audioPlayer = gameManager.instance.player.GetComponent<AudioSource>();
        }
    }

    /// <summary>
    /// Upgrades the gun for <b>costToUpgrade</b> based on the value returned by the <b>upgradeCurve</b> when the gun level is plugged in.
    /// </summary>
    public void interact()
    {
        if (gameManager.instance.playerScript.getPoints() >= costToUpgrade && !isUpgrading)
        {
            float mult = UpgradeCurve.Evaluate(gameManager.instance.playerScript.getCurrentGun().level + 1);

            gameManager.instance.playerScript.upgradeCurrentGun(mult);

            gameManager.instance.playerScript.addPoints(-costToUpgrade);

            StartCoroutine(Upgrade());
        }
    }

    /// <summary>
    /// Lerps the gun to the upgrade altar, changes the color, and lerps the gun back to the player.
    /// </summary>
    IEnumerator Upgrade()
    {
        isUpgrading = true;

        audioPlayer.PlayOneShot(upgradeSound);
        gameManager.instance.playerScript.toggleShooting(true);

        GameObject g = gameManager.instance.displayScript.currentActive;
        g.transform.parent = transform;

        g.layer = 1;

        foreach (Transform child in g.transform)
        {
            child.gameObject.layer = 1;
        }

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.position = Vector3.Lerp(g.transform.position, transform.position, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        randomizeColor(g);
        yield return new WaitForSeconds(.9f);

        g.transform.parent = Camera.main.transform;

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.localPosition = Vector3.Lerp(g.transform.localPosition, gameManager.instance.playerScript.gunLocation, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, Camera.main.transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        g.layer = 6;

        foreach (Transform child in g.transform)
        {
            child.gameObject.layer = 6;
        }

        gameManager.instance.playerScript.toggleShooting(false);
        isUpgrading = false;
    }

    void randomizeColor(GameObject gun)
    {
        gameManager.instance.displayScript.SetGunColor(gun, new Color(Random.Range(50f, 205f) / 255f, Random.Range(50f, 205f) / 255f, Random.Range(50f, 205f) / 255f));
    }
}
