using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class WallBuy : MonoBehaviour, IInteractable
{
    //[SerializeField] Gun gun;
    [SerializeField] Renderer m_Renderer;
    [SerializeField] bool scaleByHalf;

    [SerializeField] Texture2D gunPhoto; //Will be private once gun class is implemented
    float width, height;
    Texture gunTexture;

    void Start()
    {
        //Gets the 2D texture from the Gun class
        //gunPhoto = gun.get2DTexture();

        //Adjusts the width and height
        width = gunPhoto.width;
        height = gunPhoto.height;
        width /= height;
        width *= .1f;
        height = .1f;

        //Converts 2D texture to 3D texture
        gunTexture = gunPhoto;

        //Applies the texture to the wall buy object
        m_Renderer.material.mainTexture = gunPhoto;

        //Scales the wall buy object based on width and height of texture
        transform.localScale = new Vector3(width, 0.1f, height);

        //Reduces the scaling in half
        if (scaleByHalf)
        {
            transform.localScale *= .5f;
        }
    }

    public void interact()
    {
        //gameManager.instance.playerScript.AddGun(gun, gun.getCost());
    }
}
