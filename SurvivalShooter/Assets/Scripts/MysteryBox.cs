using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MysteryBox : MonoBehaviour, IInteractable
{
    [Header("----- Game Objects -----")]
    [SerializeField] GameObject open;
    [SerializeField] GameObject closed;

    // Start is called before the first frame update
    void Start()
    {
        open.SetActive(false); 
        closed.SetActive(true);
    }

    public void interact()
    {
        open.SetActive(!open.activeSelf);
        closed.SetActive(!closed.activeSelf);
    }
}
