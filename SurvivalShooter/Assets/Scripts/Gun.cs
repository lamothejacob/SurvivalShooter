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

    [Header("----- Mutable Stats -----")]
    public int damage;
    public int level = 0;

    int ammoInClip;
    int reserveAmmo;

    public void Load()
    {
        ammoInClip = clipSize;
        reserveAmmo = reserveAmmoMax;
        damage = baseDamage;
        level = 0;
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

        if(reserveAmmo >= amount)
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
}
