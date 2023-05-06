using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- Player References -----")]
    public GameObject player;
    public playerController playerScript;

    [Header("----- UI Stuff -----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject loseMenu;

    public bool isPaused;
    float timeScaleOriginal;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        playerScript = player.GetComponent<playerController>();
        timeScaleOriginal = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && activeMenu == null || activeMenu == pauseMenu)
        {
            isPaused = !isPaused;
            pauseMenu.SetActive(isPaused);
            activeMenu = pauseMenu;

            pauseState();
        }
    }

    public void pauseState()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void unPausedState()
    {
        Time.timeScale = timeScaleOriginal;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        isPaused = false;
        activeMenu.SetActive(false);
        activeMenu = null;
    }

    public void loseState()
    {
        pauseState();
        activeMenu = loseMenu;
        activeMenu.SetActive(true);
    }
}
