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

    GameObject prevMenu;
 
    // ******************************
    // MAIN MENU BUTTONS
    // ******************************
    public void playMain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.instance.unPausedState();
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
        //gameManager.instance.mainMenu.SetActive(true);
        //gameManager.instance.activeMenu = gameManager.instance.mainMenu;
        SceneManager.LoadScene(0); 
        
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
        gameManager.instance.audioScript.Play("ButtonClicked");
    }

    public void pauseSettings()
    {
        prevMenu = gameManager.instance.activeMenu;
        gameManager.instance.activeMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsPause;
        gameManager.instance.activeMenu.SetActive(true);
        
    }

    public void Restart()
    {
        gameManager.instance.audioScript.Play("ButtonClicked");
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.pauseMenu.SetActive(false);  
    }

    public void returnToMainMenu()
    {
        if (gameManager.instance.activeMenu != null)
        {
            gameManager.instance.activeMenu.SetActive(false);
        }

        //gameManager.instance.activeMenu = gameManager.instance.mainMenu;
        //gameManager.instance.activeMenu.SetActive(true);
        SceneManager.LoadScene(0); 
        gameManager.instance.audioScript.Play("MainTheme");
        gameManager.instance.audioScript.Stop("CombatMusic");
        gameManager.instance.audioScript.Play("ButtonClicked");
        gameManager.instance.unPausedState();
    }

    // ******************************
    // SETTINGS MENU BUTTONS
    // ******************************
    public void back()
    {
        gameManager.instance.activeMenu.SetActive(false);
        //gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
        gameManager.instance.activeMenu = prevMenu;
        gameManager.instance.activeMenu.SetActive(true);
        gameManager.instance.audioScript.Play("ButtonClicked");
    }

    // ******************************
    // YOU LOSE MENU BUTTONS
    // ******************************

    public void tryAgain()
    {
        gameManager.instance.audioScript.Play("ButtonClicked");
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.loseMenu.SetActive(false);

    }

    public void youLoseQuit()
    {
        gameManager.instance.audioScript.Play("ButtonClicked");
        gameManager.instance.loseMenu.SetActive(false); 
        SceneManager.LoadScene(0); 
    }

    public void goToNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        gameManager.instance.unPausedState();
    }
}
