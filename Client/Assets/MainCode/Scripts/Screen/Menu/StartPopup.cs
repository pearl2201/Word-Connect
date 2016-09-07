using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using UnityEngine;
public class StartPopup : MonoBehaviour
{
    public ScreenManager screenManager;
    [SerializeField]
    private GameObject icSoundOn;
    [SerializeField]
    private GameObject icSoundOff;
    void OnEnable()
    {
        Config.gameState = GameState.start;
        if (Prefs.Instance.isSoundOn())
        {
            Config.isSoundOn = true;
            icSoundOff.SetActive(false);
            icSoundOn.SetActive(true);
            SoundManager.Instance.PlayMusic();
        }
        else
        {
            Config.isSoundOn = false;
            icSoundOff.SetActive(true);
            icSoundOn.SetActive(false);
        }
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
        Config.isSoundOn = !Config.isSoundOn;
        Prefs.Instance.SetSound(Config.isSoundOn);
        if (Config.isSoundOn)
        {
            icSoundOff.SetActive(false);
            icSoundOn.SetActive(true);
            SoundManager.Instance.PlayMusic();
        }
        else
        {
            icSoundOff.SetActive(true);
            icSoundOn.SetActive(false);
            SoundManager.Instance.StopMusic();
        }
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

