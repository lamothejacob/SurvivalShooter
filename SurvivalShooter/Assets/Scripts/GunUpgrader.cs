using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GunUpgrader : MonoBehaviour, IInteractable
{
    [SerializeField] AnimationCurve UpgradeCurve;
    [SerializeField] int costToUpgrade;
    [SerializeField] AudioClip upgradeSound;
    [SerializeField] AudioSource audioPlayer;

    bool isUpgrading;
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

        StartCoroutine(randomizeColor(g));
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

    IEnumerator randomizeColor(GameObject gun)
    {
        List<Material> materials = new List<Material>();

        foreach (Renderer r in gun.GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(r.materials);
        }
        
        Color colorOriginal = materials[0].color;
        //50 offset from full black and full white, random color that isn't too dark or too light
        Color colorNext = new Color(Random.Range(50f, 205f)/255f, Random.Range(50f, 205f)/255f, Random.Range(50f, 205f) / 255f);

        foreach (Material m in materials)
        {
            m.SetColor("_Color", new Color(m.color.r - colorOriginal.r + colorNext.r, m.color.g - colorOriginal.g + colorNext.g, m.color.b - colorOriginal.b + colorNext.b));
        }

        yield return null;
    }
}
