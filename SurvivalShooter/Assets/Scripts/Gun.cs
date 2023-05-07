using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Texture2D gunImage;

    [Header("----- Weapon Stats -----")]
    [SerializeField] int cost;
    [SerializeField] int clipSize;
    [SerializeField] int damage;
    [SerializeField] int shootDist;
    [SerializeField] float fireRate;


    int getCost()
    {
        return cost;
    }
    int getClipSize()
    {
        return clipSize;
    }
    int getDamage()
    {
        return damage;
    }
    int getShootDist()
    {
        return shootDist;
    }
    float getFireRate()
    {
        return fireRate;
    }
    Texture2D get2DTexture()
    {
        return gunImage;
    }

}
