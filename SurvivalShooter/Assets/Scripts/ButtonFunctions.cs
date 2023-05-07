using System.Collections;
using System.Collections.Generic;
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
        //gameManager.instance.unPausedState();
        gameManager.instance.activeMenu = gameManager.instance.HUD;
        gameManager.instance.HUD.SetActive(true);  
        //SceneManager.LoadScene(SceneManager.GetSceneByName()); 
        
    }

    public void settingsMain()
    {
        gameManager.instance.mainMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsMain; 
        gameManager.instance.activeMenu.SetActive(true);
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
        gameManager.instance.activeMenu = gameManager.instance.HUD;

    }

    public void pauseSettings()
    {
        gameManager.instance.pauseMenu.SetActive(false);
        gameManager.instance.activeMenu = gameManager.instance.settingsPause;
        gameManager.instance.activeMenu.SetActive(true);  
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
}
