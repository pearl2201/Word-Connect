using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
public class WelcomeUser : MonoBehaviour
{


    void OnEnable()
    {
        Config.gameState = GameState.welcome;
    }

    public MenuScreenManager menuScreenManager;

    public tk2dTextMesh txtWelcome;
    public void EndWellcomeUser()
    {
        menuScreenManager.OpenStartPopup();
        gameObject.SetActive(false);
    }

    public void ShowWelcomeUser(string name)
    {
        txtWelcome.text = "Welcome " + name + "!!!";
        Invoke("EndWellcomeUser", 2f);
    }

}

