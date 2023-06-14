using System.Collections;
using TMPro;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [Header("----- References -----")]
    public GameObject player;
    public GameObject enemySpawner;
    public EnemySpawner enemySpawnerScript;
    public cameraController cameraScript;
    public playerController playerScript;
    public gunDisplay displayScript;
    public HUD hudScript;
    public AudioManager audioScript;
    public ButtonFunctions buttonsScript;

    [Header("----- UI Stuff -----")]
    public GameObject activeMenu;
    public GameObject pauseMenu;
    public GameObject mainMenu;
    public GameObject settingsMain;
    public GameObject settingsPause;  
    public GameObject loseMenu;
    public GameObject winMenu;
    public GameObject HUD;
    public GameObject playerDamageFlash;
    public GameObject shieldActiveImage;
    public TextMeshProUGUI interactText;
    public TextMeshProUGUI levelGoalText;

    public bool isPaused;
    float timeScaleOriginal;
    private int enemiesAlive;
    private int totalEnemiesSpawned;
    private int enemiesKilled;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        player = GameObject.FindGameObjectWithTag("Player");
        displayScript = player.GetComponentInChildren<gunDisplay>();
        playerScript = player.GetComponent<playerController>();
        cameraScript = player.GetComponentInChildren<cameraController>();
        hudScript = HUD.GetComponent<HUD>();
        audioScript = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        buttonsScript = gameObject.GetComponent<ButtonFunctions>();
        timeScaleOriginal = Time.timeScale;

        if (GameObject.FindGameObjectWithTag("EnemySpawner"))
        {
            enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
            enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        }
    }

    private void Start()
    {
        HUD.SetActive(true);
        //pauseState();
        //buttonsScript.returnToMainMenu();
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
        if (activeMenu != null)
        {
            activeMenu.SetActive(false);
        }
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
            StartCoroutine(NewWin(2));
        }
    }

    public void WinState(int timeUntilMenu)
    {
        StartCoroutine(NewWin(timeUntilMenu));
    }

    IEnumerator NewWin(int time)
    {

        yield return new WaitForSeconds(time);

        activeMenu = winMenu;
        activeMenu.SetActive(true);
        pauseState();

    }

    public void AddEnemiesAlive(int spawned)
    {
        enemiesAlive += spawned;
        totalEnemiesSpawned += spawned;
    }

    public void SubEnemiesAlive(int amount)
    {
        enemiesAlive -= amount;
        enemiesKilled += amount;
    }

    public int EnemiesAliveCurr()
    {
        return enemiesAlive;
    }

    public int GetEnemiesKilled()
    {
        return enemiesKilled;
    }
}
