using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunctions : MonoBehaviour
{
    private bool bInvertY;
    private bool bHUDVisible;

    private bool audioLevel;
    private bool cameraSensitivity;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buttonClicked; 
 
    // ******************************
    // MAIN MENU BUTTONS
    // ******************************
    public void playMain()
    {
        
        Restart();
        AudioManager.instance.Play("ButtonClicked");
        /**
        gameManager.instance.activeMenu = null;
        gameManager.instance.mainMenu.SetActive(false);
        gameManager.instance.activeMenu = null;
        gameManager.instance.HUD.SetActive(true);  
        **/
    }

    public void settingsMain()
    {
        
        gameManager.instance.activeMenu = gameManager.instance.settingsMain;
        gameManager.instance.activeMenu.SetActive(true);
        gameManager.instance.mainMenu.SetActive(false);
        

    }

    public void backMain()
    {
        
        gameManager.instance.settingsMain.SetActive(false);
        gameManager.instance.mainMenu.SetActive(true);
        gameManager.instance.activeMenu = gameManager.instance.mainMenu;
        AudioManager.instance.Play("ButtonClicked");
    }

    public void quit()
    {
        Application.Quit();
    }

    // ******************************
    // PAUSE MENU BUTTONS
    // ******************************

    public void resume()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.unPausedState();
        AudioManager.instance.Play("ButtonClicked");

    }

    public void pauseSettings()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsPause;
        gameManager.instance.activeMenu.SetActive(true);
        AudioManager.instance.Play("ButtonClicked");
    }

    public void Restart()
    {
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.pauseMenu.SetActive(false);
        //AudioManager.instance.Play("CombatMusic");
        audioSource.PlayOneShot(buttonClicked, 1); 
    }

    public void returnToMainMenu()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.mainMenu;
        gameManager.instance.activeMenu.SetActive(true);
        //AudioManager.instance.Play("MainTheme"); 
        AudioManager.instance.Play("ButtonClicked");
    }

    // ******************************
    // SETTINGS MENU BUTTONS
    // ******************************
    public void back()
    {
        gameManager.instance.activeMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
        gameManager.instance.activeMenu.SetActive(true);
        AudioManager.instance.Play("ButtonClicked");
    }

    // ******************************
    // YOU LOSE MENU BUTTONS
    // ******************************

    public void tryAgain()
    {
        AudioManager.instance.Play("ButtonClicked");
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.loseMenu.SetActive(false);

    }

    public void youLoseQuit()
    {
        AudioManager.instance.Play("ButtonClicked");
        gameManager.instance.loseMenu.SetActive(false);
        gameManager.instance.mainMenu.SetActive(true);
        gameManager.instance.activeMenu = gameManager.instance.mainMenu; 
    }
}
