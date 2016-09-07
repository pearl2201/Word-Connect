using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
public class StartPopup : MonoBehaviour
{
    public ScreenManager screenManager;
    void OnEnable()
    {
        Config.gameState = GameState.start;
    }

    public void ClickStart()
    {
        if (!screenManager.isLockScreen)
        {
            screenManager.isLockScreen = true;
            screenManager.OpenGameScreen();
        }
    }

    public void ClickTurnSound()
    {

    }

    public void ClickLeaderboard()
    {
        if (!screenManager.isLockScreen)
        {
            Debug.Log("call leaderboard");
            screenManager.isLockScreen = true;
            NetworkManager.Instance.RequestLeaderboard();
        }
       

    }

    public void ClickInfo()
    {
        if (!screenManager.isLockScreen)
        {
            screenManager.isLockScreen = true;
            screenManager.OpenInfo();
        }
    }
}

