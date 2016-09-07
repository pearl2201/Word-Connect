using UnityEngine;
using System.Collections;

public class ScreenManager : MonoBehaviour
{

    public GameScreenManager gameScreenManager;
    public MenuScreenManager menuScreenManager;
    public LeaderboardScreenManager leaderboardScreenManager;
    public InfoScreenManager infoScreenManager;
    public NetworkManager networkManager;
    public bool isLockScreen;

    public void OpenLeaderboard(string js)
    {
        menuScreenManager.gameObject.SetActive(false);
        leaderboardScreenManager.gameObject.SetActive(true);
        leaderboardScreenManager.Setup(js);
        
        
        isLockScreen = false;
    }

    public void OpenInfo()
    {
        menuScreenManager.gameObject.SetActive(false);
        infoScreenManager.gameObject.SetActive(true);
        isLockScreen = false;
    }

    public void OpenGameScreen()
    {
        gameScreenManager.gameObject.SetActive(true);
        menuScreenManager.gameObject.SetActive(false);
    }

    public void OpenWelcomeScreen()
    {
        Debug.Log("open welcome screen");
        menuScreenManager.gameObject.SetActive(true);
        menuScreenManager.WelcomeUser(Config.user.Username);
        isLockScreen = false;
    }

    public void OpenRegisUserScreen()
    {
        menuScreenManager.gameObject.SetActive(true);
        menuScreenManager.OpenSignInPopup();
    }


    public void OpenStartScreen()
    {
        if (!menuScreenManager.gameObject.activeSelf)
            menuScreenManager.gameObject.SetActive(true);
        menuScreenManager.OpenStartPopup();
    }
}
