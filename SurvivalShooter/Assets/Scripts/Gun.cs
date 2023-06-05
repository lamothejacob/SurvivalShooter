using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Gun : ScriptableObject
{
    [Header("----- Weapon -----")]
    public Texture2D gunImage;
    public GameObject gunObject;
    public GameObject hitEffect;

    [Header("----- Immutable Stats -----")]
    public int cost;
    public int baseDamage;
    public int shootDist;
    public float fireRate;
    public int reserveAmmoMax;
    public int clipSize;
    public bool automatic = true;
    public Color baseColor;

    [Header("----- Shooting Stats -----")]
    [SerializeField][Range(1, 25)] int projectileAmount = 1;
    [Range(0, 45f)] public float verticalRecoil;
    [SerializeField][Range(0, 0.5f)] float verticalSpread;
    [SerializeField][Range(0, 0.5f)] float horizontalSpread;

    [Header("----- Mutable Stats -----")]
    public int damage;
    public int level = 0;
    public Color color;

    int ammoInClip;
    int reserveAmmo;

    public void Load()
    {
        ammoInClip = clipSize;
        reserveAmmo = reserveAmmoMax;
        damage = baseDamage;
        level = 0;
        color = baseColor;
    }

    public void addAmmo(int ammo)
    {
        reserveAmmo += ammo;
        if (reserveAmmo > reserveAmmoMax)
            reserveAmmo = reserveAmmoMax;
    }

    public void removeAmmo(int ammo)
    {
        ammoInClip -= ammo;
    }

    public int getAmmoInClip()
    {
        return ammoInClip;
    }

    public int getReserveAmmo()
    {
        return reserveAmmo;
    }

    public void reload()
    {
        if(ammoInClip == clipSize || reserveAmmo == 0)
        {
            return;
        }

        int amount = clipSize - ammoInClip;
        gameManager.instance.audioScript.Play("Reload");


        if (reserveAmmo >= amount)
        {
            reserveAmmo -= amount;
            ammoInClip += amount;
        }
        else
        {
            ammoInClip += reserveAmmo;
            reserveAmmo = 0;
        }
    }

    public List<RaycastHit> GetRayList()
    {
        List<RaycastHit> raycastHits = new List<RaycastHit>();

        for (int i = 0; i < projectileAmount; i++)
        {
            Vector2 spread = Random.insideUnitCircle;
            RaycastHit hit;

            if(Physics.Raycast(Camera.main.ViewportPointToRay(new Vector2(spread.x * horizontalSpread + 0.5f, spread.y * verticalSpread + 0.5f)), out hit, shootDist))
            {
                raycastHits.Add(hit);
            }
        }

        return raycastHits;
    }
}
