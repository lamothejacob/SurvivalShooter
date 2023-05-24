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
    int ammoCost;

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
        else if (gun.reserveAmmoMax - gun.getReserveAmmo() > 0)
        {
<<<<<<< Updated upstream
            //Adds 5 clips of ammo if they do have the gun
            if (gameManager.instance.playerScript.getPoints() >= (gun.cost / 2))
            {
                gameManager.instance.playerScript.addPoints((-gun.cost / 2));
                gun.addAmmo(gun.reserveAmmoMax - gun.getReserveAmmo());
            }
=======
            //Refill Ammo, cost scales with gun upgrades
            gameManager.instance.playerScript.addAmmo(gun, gun.reserveAmmoMax - gun.getReserveAmmo(), (gun.level + 1) * gun.cost / 2);
>>>>>>> Stashed changes
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ammoCost = (gun.level + 1) * gun.cost / 2;

            if (gameManager.instance.playerScript.hasGun(gun))
            {
                interactText = "Press 'E' to refill Ammo\n Cost: " + ammoCost.ToString();
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
