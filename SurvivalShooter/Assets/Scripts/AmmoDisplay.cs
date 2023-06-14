using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoDisplay : MonoBehaviour
{
    Gun currentGun;
    [SerializeField] TextMeshPro tmp;

    int ammoPercent = 100;
    float ammoRatio = 1f;
    int clipSize;
    int clipCurrent;

    // Start is called before the first frame update
    void Start()
    {
        currentGun = gameManager.instance.playerScript.getCurrentGun();
        clipSize = currentGun.clipSize;
        ammoUpdate();

        tmp = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if(currentGun.getAmmoInClip() != clipCurrent) {
            ammoUpdate();
            tmp.SetText(ammoPercent.ToString() + "%");
        }
    }

    void ammoUpdate() {
        clipCurrent = currentGun.getAmmoInClip();
        ammoRatio = (float)clipCurrent / (float)clipSize;
        ammoPercent = (int)(ammoRatio * 100f);
    }
}
