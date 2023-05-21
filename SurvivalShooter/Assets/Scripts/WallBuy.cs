using UnityEngine;

public class WallBuy : MonoBehaviour, IInteractable
{
    [SerializeField] Gun gun;
    [SerializeField] Renderer m_Renderer;
    [SerializeField] bool scaleByHalf;

    Texture2D gunPhoto;
    float width, height;
    Texture gunTexture;

    string interactText;

    void Start()
    {
        //Gets the 2D texture from the Gun class
        gunPhoto = gun.get2DTexture();

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
        if (!gameManager.instance.playerScript.hasGun(gun)) 
        {
            Gun newGun = Instantiate(gun);
            //Adds the gun to the players inventory if they do not have the gun
            gameManager.instance.playerScript.addGun(newGun);
        }
        else
        {
            //Adds 5 clips of ammo if they do have the gun
            gameManager.instance.playerScript.addAmmo(gun, gun.getReserveAmmo(), gun.getCost() / 2);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactText = "Press 'E' to Buy\n Cost: " + gun.getCost().ToString();
            gameManager.instance.interactText.text = interactText;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.interactText.text = null;
        }
    }
}
