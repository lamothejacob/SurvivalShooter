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
    // ******************************
    // MAIN MENU BUTTONS
    // ******************************
    public void playMain()
    {
        gameManager.instance.activeMenu = null;
        gameManager.instance.mainMenu.SetActive(false);
        gameManager.instance.activeMenu = null;
        gameManager.instance.HUD.SetActive(true);  
            
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
        gameManager.instance.isPaused = !gameManager.instance.isPaused;
        gameManager.instance.activeMenu = null;

    }

    public void pauseSettings()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsPause;
        gameManager.instance.activeMenu.SetActive(true);  
    }

    public void Restart()
    {
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.pauseMenu.SetActive(false);
    }

    public void returnToMainMenu()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.mainMenu;
        gameManager.instance.activeMenu.SetActive(true);
    }

    // ******************************
    // SETTINGS MENU BUTTONS
    // ******************************


    public void adjustAudio()
    {

    }

    public void adjustCameraSensitivity()
    {

    }

    public void invertYButton()
    {

    }

    public void HUDVisible()
    {
        if(bHUDVisible)
        {
            
        }
        else if(!bHUDVisible)
        {
            gameManager.instance.HUD.SetActive(false);
        }
    }
    public void saveSettings()
    {

    }

    public void back()
    {
        gameManager.instance.activeMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.pauseMenu;
        gameManager.instance.activeMenu.SetActive(true);
    }

    // ******************************
    // YOU LOSE MENU BUTTONS
    // ******************************

    public void tryAgain()
    {
        
        gameManager.instance.unPausedState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.loseMenu.SetActive(false);

    }

    public void youLoseQuit()
    {
        gameManager.instance.loseMenu.SetActive(false);
        gameManager.instance.mainMenu.SetActive(true);
        gameManager.instance.activeMenu = gameManager.instance.mainMenu; 
    }
}
