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
        gunPhoto = gun.gunImage;

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
            //Adds the gun to the players inventory if they do not have the gun
            gun.Load();
            gameManager.instance.playerScript.addGun(gun);
        }
        else
        {
            //Adds 5 clips of ammo if they do have the gun
            gun.addAmmo(gun.reserveAmmoMax - gun.getReserveAmmo());
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gameManager.instance.playerScript.hasGun(gun))
            {
                interactText = "Press 'E' to refill Ammo\n Cost: " + (gun.cost / 2).ToString();
            }
            else
            {
                interactText = "Press 'E' to Buy\n Cost: " + gun.cost.ToString();
            }
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
