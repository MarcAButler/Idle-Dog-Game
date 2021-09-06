using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject menuPanel;
    bool menuShowing = false;

    

    // Either takes down or brings up the menu
    public void switchMenuState()
    {
        

        // Flips the menu on or off
        if (menuShowing)
        {
            menuShowing = !menuShowing;
            //Debug.Log("Menu Not Showing");
        }
        else
        {
            menuShowing = true;
            //Debug.Log("Menu Showing");
        }

        menuPanel.SetActive(menuShowing);
    }

    // Detects if an EscapeKey was pressed
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //Debug.Log("SPACE PRESSED");
            switchMenuState();
        }

    }

    // Opens the Settings Tab
    public void Settings()
    {
        Debug.Log("OPENING SETTINGS (CREATE SETTINGS TAB)");

    }

    public void Quit()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }
}
