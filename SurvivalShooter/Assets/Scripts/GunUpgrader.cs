using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrader : MonoBehaviour, IInteractable
{
    [SerializeField] AnimationCurve UpgradeCurve;
    [SerializeField] int costToUpgrade;
    public void interact()
    {
        if (gameManager.instance.playerScript.getPoints() >= costToUpgrade)
        {
            float mult = UpgradeCurve.Evaluate(gameManager.instance.playerScript.getCurrentGun().level + 1);

            gameManager.instance.playerScript.upgradeCurrentGun(mult);

            gameManager.instance.playerScript.addPoints(-costToUpgrade);

            StartCoroutine(Upgrade());
        }
    }

    IEnumerator Upgrade()
    {
        gameManager.instance.playerScript.toggleShooting(true);

        GameObject g = gameManager.instance.displayScript.currentActive;
        g.transform.parent = transform;
        
        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.position = Vector3.Lerp(g.transform.position, transform.position, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }


        yield return new WaitForSeconds(.1f);

        g.transform.parent = Camera.main.transform;

        elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            g.transform.localPosition = Vector3.Lerp(g.transform.localPosition, gameManager.instance.playerScript.gunLocation, elapsedTime / 1f);
            g.transform.rotation = Quaternion.Lerp(g.transform.rotation, Camera.main.transform.rotation, elapsedTime / 1f);
            elapsedTime += Time.deltaTime;

            yield return null;
        }

        gameManager.instance.playerScript.toggleShooting(false);
    }
}
