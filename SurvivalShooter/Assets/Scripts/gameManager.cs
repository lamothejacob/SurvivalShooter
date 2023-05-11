using System.Collections;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- References -----")]
    public GameObject player;
    public GameObject enemySpawner;
    public EnemySpawner enemySpawnerScript;
    public playerController playerScript;
    public gunDisplay displayScript;
    public HUD hudScript;

    [Header("----- UI Stuff -----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject settingsMain;
    public GameObject settingsPause;  
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject HUD; 

    public bool isPaused;
    float timeScaleOriginal;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        displayScript = player.GetComponentInChildren<gunDisplay>();
        playerScript = player.GetComponent<playerController>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        hudScript = HUD.GetComponent<HUD>();
        timeScaleOriginal = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        
        if ((Input.GetKeyDown(KeyCode.Escape) && (activeMenu == HUD || activeMenu == null)) && !isPaused)
        {
            isPaused = !isPaused;
            activeMenu = pauseMenu;
            pauseMenu.SetActive(isPaused);
           
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

    public void updateGameGoal()
    {
        int currentWave = enemySpawnerScript.GetWaveNumber();

        if (currentWave > enemySpawnerScript.GetWaveAmount())
        {

        }
    }

    IEnumerator NewWin()
    {

        yield return new WaitForSeconds(2);

        activeMenu = winMenu;
        activeMenu.SetActive(true);
        pauseState();

    }
}
