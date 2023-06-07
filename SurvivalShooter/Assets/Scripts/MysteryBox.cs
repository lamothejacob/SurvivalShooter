using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class MysteryBox : MonoBehaviour, IInteractable
{
    [Header("----- Game Objects -----")]
    [SerializeField] GameObject open;
    [SerializeField] GameObject closed;

    [Header("----- Audio Source -----")]
    [SerializeField] AudioSource audioPlayer;

    [Header("----- Guns -----")]
    [SerializeField] Gun[] guns;

    [Header("----- Cost -----")]
    [SerializeField] int cost = 750;

    GameObject currentGun;
    int chosen;
    bool isCycling;

    // Start is called before the first frame update
    void Start()
    {
        open.SetActive(false); 
        closed.SetActive(true);

        if(audioPlayer == null)
        {
            audioPlayer = GetComponent<AudioSource>();
        }
    }

    public void interact()
    {
        if(closed.activeSelf && gameManager.instance.playerScript.getPoints() >= cost)
        {
            gameManager.instance.playerScript.addPoints(-cost);
            ToggleState();
            StartCoroutine(CreateGun());
        }else if (!isCycling && !gameManager.instance.playerScript.getShootingState() && open.activeSelf)
        {
            StopAllCoroutines();

            if (!gameManager.instance.playerScript.hasGun(guns[chosen]))
            {
                int tempCost = guns[chosen].cost;
                //Adds the gun to the players inventory if they do not have the gun
                guns[chosen].Load();
                guns[chosen].cost = 0;
                gameManager.instance.playerScript.addGun(guns[chosen]);
                guns[chosen].cost = tempCost;
            }
            else if (guns[chosen].reserveAmmoMax - guns[chosen].getReserveAmmo() > 0)
            {
                //Refill Ammo
                gameManager.instance.playerScript.addAmmo(guns[chosen], guns[chosen].reserveAmmoMax - guns[chosen].getReserveAmmo(), 0);
            }

            Destroy(currentGun); 
            currentGun = null;

            ToggleState();
        }
    }

    void ToggleState()
    {
        open.SetActive(!open.activeSelf);
        closed.SetActive(!closed.activeSelf);
    }

    IEnumerator CreateGun()
    {
        isCycling = true;
        audioPlayer.Play();

        int i = Random.Range(0, guns.Length);
        currentGun = Instantiate(guns[i].gunObject, transform.position + new Vector3(0,.5f,0), transform.rotation);
        currentGun.transform.localScale *= 10f;

        float elapsedTime = 0f;
        while (elapsedTime < audioPlayer.clip.length - .4f)
        {
            if(i >= guns.Length - 1) {
                i = 0;
            }
            else
            {
                i++;
            }

            Destroy(currentGun);
            currentGun = Instantiate(guns[i].gunObject, transform.position + new Vector3(0, .5f, 0), transform.rotation);
            currentGun.transform.localScale *= 10f;

            yield return new WaitForSeconds(.25f);
            elapsedTime += Time.deltaTime + .25f;
        }

        chosen = i;
        isCycling = false;

        StartCoroutine(ClearBox());
    }

    IEnumerator ClearBox()
    {
        yield return new WaitForSeconds(8f);
        Destroy(currentGun);
        currentGun = null;

        ToggleState();
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if(closed.activeSelf)
            {
                gameManager.instance.interactText.text = "Press 'E' to Buy\n Cost: " + cost.ToString();
            }else if (!isCycling)
            {
                if (!gameManager.instance.playerScript.hasGun(guns[chosen]))
                {
                    gameManager.instance.interactText.text = "Press 'E' to Take " + guns[chosen].name;
                }
                else
                {
                    gameManager.instance.interactText.text = "Press 'E' for ammo";
                }
            }
            else
            {
                gameManager.instance.interactText.text = null;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.interactText.text = null;
        }
    }
}
