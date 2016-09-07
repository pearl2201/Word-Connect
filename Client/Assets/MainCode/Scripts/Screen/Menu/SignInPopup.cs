using UnityEngine;
using System.Collections;

public class SignInPopup : MonoBehaviour
{


    public bool isKeyBoardOpen;
    [SerializeField]
    private tk2dTextMesh txtCurrUsernameRegis;
    private string usernameRegis;
    public ScreenManager screenManager;
    void OnEnable()
    {
        Config.gameState = GameState.regis;
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        UpdateKeyBoard();
    }

    public void UpdateKeyBoard()
    {

        TouchScreenKeyboard keyboard = null;


        if (!isKeyBoardOpen)
        {
            keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.ASCIICapable);
            isKeyBoardOpen = true;
        }

        if (keyboard.done)
        {
            
            SetUserName(keyboard.text);
           
            isKeyBoardOpen = false;
        }
        else
        {
            SetUserName(keyboard.text);
        }

    }

    public void ClickSignIn()
    {
        screenManager.isLockScreen = true;
        NetworkManager.Instance.Regis(usernameRegis);
    }
    public void SetUserName(string username)
    {
        usernameRegis = username;
        txtCurrUsernameRegis.text = username;
    }
}
