using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class KeyValuePair
{
    public Texture2D key;
    public GameObject val;
}

public class gunDisplay : MonoBehaviour
{
    [Header("----- Gun Objects -----")]
    [SerializeField] List<KeyValuePair> gunPairList = new List<KeyValuePair>();

    //Unity inspector doesn't display dictionaries
    Dictionary<Texture2D, GameObject> gunObjectPairs = new Dictionary<Texture2D, GameObject>();
    GameObject currentActive;

    private void Start()
    {
        //Initialize dictionary
        foreach(KeyValuePair kvp in gunPairList)
        {
            gunObjectPairs.Add(kvp.key, kvp.val);
            Debug.Log(gunObjectPairs[kvp.key]);
        }
    }

    public void setCurrentGun(Gun gun)
    {
        if (!gunObjectPairs.ContainsKey(gun.get2DTexture()))
        {
            throw new System.Exception("Attempted to display non-existent gun on player. gunDisplay.cs at line 14.");
            return;
        }

        if(currentActive != null)
        {
            currentActive.SetActive(false);
        }

        currentActive = gunObjectPairs[gun.get2DTexture()];
        currentActive.SetActive(true);
    }
}
