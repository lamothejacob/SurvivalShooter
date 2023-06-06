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
        bContinueGameMenuActive = true;
        continueGameMenu.SetActive(true); 
        mainMenu.SetActive(false);
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

}
