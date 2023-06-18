using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] GameObject mainMenu; 
    [SerializeField] GameObject settingsMenu;
    [SerializeField] GameObject continueGameMenu;

    [Header("=== Settings Menus ===")]
    [SerializeField] GameObject generalSettingsPanel;
    [SerializeField] GameObject controlsSettingsPanel;
    [SerializeField] GameObject aboutSettingsPanel; 

    private bool bSettingsMenuActive; 
    private bool bContinueGameMenuActive;

    float TimeScaleOriginal;
    int currSave;


    private void Awake()
    {
        TimeScaleOriginal = Time.timeScale;
    }

    // Start is called before the first frame update
    void Start()
    {
        //gameManager.instance.pauseState();
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
        Time.timeScale = 0; 

    }

    public void NewGameClicked()
    {
        PlayerPrefs.SetInt("CurrentScene", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Cursor.visible = false;
        Time.timeScale = TimeScaleOriginal;
        
        // Cursor.lockState = CursorLockMode.Locked; 
    }

    public void SettingsMenuClicked()
    {
        bSettingsMenuActive = true;
        settingsMenu.SetActive(true);
        mainMenu.SetActive(false);
    }

    public void ContinueGameClicked() 
    {
        //bContinueGameMenuActive = true;
        //continueGameMenu.SetActive(true); 
        //mainMenu.SetActive(false);

        SceneManager.LoadScene(PlayerPrefs.GetInt("CurrentScene"));
        Cursor.visible = false;
        Time.timeScale = TimeScaleOriginal;
    }

    public void GeneralSettingsButtonClicked()
    {
        generalSettingsPanel.SetActive(true);
        controlsSettingsPanel.SetActive(false);
        aboutSettingsPanel.SetActive(false);
    }

    public void ControlsSettingsButtonClicked()
    {
        controlsSettingsPanel.SetActive(true);
        generalSettingsPanel.SetActive(false); 
        aboutSettingsPanel.SetActive(false);
    }

    public void AboutSettingsButtonClicked()
    {
        controlsSettingsPanel.SetActive(false);
        generalSettingsPanel.SetActive(false); 
        aboutSettingsPanel.SetActive(true);
    }

    public void LoadGameBackButtonClicked()
    {
        continueGameMenu.SetActive(false );
        mainMenu.SetActive(true);
    }

    public void SettingsBackButtonClicked()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }

    public void EndlessButtonClicked()
    {
        SceneManager.LoadScene("Main");
        Cursor.visible = false;
        Time.timeScale = TimeScaleOriginal;
    }

    public void SetScreenRes()
    {
        // get the name of the button clicked
        string index = UnityEngine.EventSystems.EventSystem.current.currentSelectedGameObject.name;

        switch(index)
        {
            case "0":
                Screen.SetResolution(1920, 1080, true);
                break;
            case "1":
                Screen.SetResolution(2560, 1080, true);
                break;
            case "2":
                Screen.SetResolution(3440, 1440, true);
                break;
            case "3":
                Screen.SetResolution(3840, 1600, true);
                break;
            case "4":
                Screen.SetResolution(3840, 1080, true);
                break;
            case "5":
                Screen.SetResolution(5120, 1440, true);
                break;
        }
    }

}
