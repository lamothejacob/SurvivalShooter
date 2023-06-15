using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class gunDisplay : MonoBehaviour
{
    public GameObject currentActive; //The Current Gun Object the player is using
    GameObject muzzleEffect;
    bool isFlashing;

    /// <summary>
    /// Creates the gun object and attaches it to the main camera
    /// </summary>
    /// <param name="gun">The gun type to attach to the player</param>
    public void setCurrentGun(Gun gun)
    {
        //Destroy current gun object
        if(currentActive != null)
        {
            currentActive.SetActive(false);
            Destroy(currentActive);
        }

        //Instantiate new gun
        currentActive = Instantiate(gun.gunObject, Camera.main.transform.position, Camera.main.transform.rotation);
        SetGunColor(currentActive, gun.color);

        //Adjust scale to 10x
        //currentActive.transform.localScale = Vector3.one * 10f;

        //Put gun and child meshes into gun layer
        SetLayer(currentActive.transform);

        //Attach to the camera
        currentActive.transform.parent = Camera.main.transform;
        currentActive.SetActive(true);

        //Adjust position
        currentActive.transform.localPosition = gun.handOffset;

        //Instantiate Muzzle Effect
        muzzleEffect = Instantiate(gun.muzzleEffect, Camera.main.transform.position, Camera.main.transform.rotation);
        muzzleEffect.layer = 6;
        muzzleEffect.transform.parent = currentActive.transform;
        muzzleEffect.SetActive(false);
        muzzleEffect.transform.localPosition = gun.muzzleOffset;
    }

    void SetLayer(Transform go) {
        foreach(Transform child in go) {
            SetLayer(child);
        }

        go.gameObject.layer = 6;
    }

    /// <summary>
    /// Adjusts the color of the gun object, taking into account the color difference between the gun's materials.
    /// </summary>
    /// <param name="gun">The Gun object to adjust</param>
    /// <param name="c">The color to adjust to</param>
    public void SetGunColor(GameObject gun, Color c)
    {
        List<Material> materials = new List<Material>();

        foreach (Renderer r in gun.GetComponentsInChildren<Renderer>())
        {
            materials.AddRange(r.materials);
        }

        Color colorOriginal = materials[0].color;

        foreach (Material m in materials)
        {
            m.SetColor("_Color", new Color(m.color.r - colorOriginal.r + c.r, m.color.g - colorOriginal.g + c.g, m.color.b - colorOriginal.b + c.b));
        }

        gameManager.instance.playerScript.getCurrentGun().color = materials[0].color;
    }

    public void MuzzleFlash()
    {
        if (!isFlashing)
        {
            StartCoroutine(MakeMuzzleEffect());
        }
    }

    IEnumerator MakeMuzzleEffect()
    {
        isFlashing = true;
        muzzleEffect.SetActive(true);

        if (muzzleEffect.GetComponentInChildren<ParticleSystem>() == null)
        {
            yield return new WaitForSeconds(.1f);
        }
        else
        {
            foreach (ParticleSystem p in muzzleEffect.GetComponentsInChildren<ParticleSystem>())
            {
                p.time = 0;
                p.Play();

                yield return null;
            }

            yield return new WaitForSeconds(muzzleEffect.GetComponentInChildren<ParticleSystem>().main.duration);
        }

        isFlashing = false;
        muzzleEffect.SetActive(false);
    }
}
