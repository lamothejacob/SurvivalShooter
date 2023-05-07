using UnityEngine;

#pragma warning disable CS0660
#pragma warning disable CS0661

public class Gun : MonoBehaviour
{
    [SerializeField] Texture2D gunImage;

    [Header("----- Weapon Stats -----")]
    [SerializeField] int cost;
    [SerializeField] int damage;
    [SerializeField] int shootDist;
    [SerializeField] float fireRate;
    [SerializeField] int reserveAmmo;
    [SerializeField] int clipSize;

    int ammoInClip;

    private void Start()
    {
        ammoInClip = clipSize;
    }

    public int getCost()
    {
        return cost;
    }
    public int getClipSize()
    {
        return clipSize;
    }
    public int getDamage()
    {
        return damage;
    }
    public int getShootDist()
    {
        return shootDist;
    }
    public float getFireRate()
    {
        return fireRate;
    }
    public Texture2D get2DTexture()
    {
        return gunImage;
    }

    public void addAmmo(int ammo)
    {
        reserveAmmo += ammo;
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

    public static bool operator ==(Gun g1, Gun g2)
    {
        return g1.get2DTexture() == g2.get2DTexture();
    }

    public static bool operator !=(Gun g1, Gun g2)
    {
        return g1.get2DTexture() != g2.get2DTexture();
    }
}
