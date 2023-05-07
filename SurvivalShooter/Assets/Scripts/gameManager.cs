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
        displayScript = player.GetComponent<gunDisplay>();
        playerScript = player.GetComponent<playerController>();
        enemySpawner = GameObject.FindGameObjectWithTag("EnemySpawner");
        enemySpawnerScript = enemySpawner.GetComponent<EnemySpawner>();
        timeScaleOriginal = Time.timeScale;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(enemySpawnerScript.EnemiesLeft());
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
