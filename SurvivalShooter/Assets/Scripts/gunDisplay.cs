using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class gunDisplay : MonoBehaviour
{
    public GameObject currentActive;

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

        //Adjust scale to 10x
        currentActive.transform.localScale = Vector3.one * 10f;

        //Put gun and child meshes into gun layer
        currentActive.layer = 6;

        foreach (Transform child in currentActive.transform)
        {
            child.gameObject.layer = 6;
        }

        //Attach to the camera
        currentActive.transform.parent = Camera.main.transform;
        currentActive.SetActive(true);

        //Adjust position
        currentActive.transform.localPosition = gameManager.instance.playerScript.gunLocation;
    }
}
