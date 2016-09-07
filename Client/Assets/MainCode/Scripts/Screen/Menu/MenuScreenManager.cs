using UnityEngine;
using System.Collections;

public class MenuScreenManager : MonoBehaviour
{

    public SignInPopup signInPopup;
    public StartPopup startPopup;
    public WelcomeUser welcomeUserPopup;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void WelcomeUser(string username)
    {
        if (signInPopup.gameObject.activeSelf)
        {
            signInPopup.gameObject.SetActive(false);
        }
        welcomeUserPopup.gameObject.SetActive(true);
        welcomeUserPopup.ShowWelcomeUser(username);
    }

    public void OpenSignInPopup()
    {
        signInPopup.gameObject.SetActive(true);
    }


    public void OpenStartPopup()
    {
        startPopup.gameObject.SetActive(true);
    }
}
