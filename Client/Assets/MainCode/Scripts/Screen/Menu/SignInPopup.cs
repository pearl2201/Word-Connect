using UnityEngine;
using System.Collections;

public class SignInPopup : MonoBehaviour
{


    public bool isKeyBoardOpen;
   
    [SerializeField]
    private tk2dUITextInput inpText;
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
       
    }

    public void UpdateKeyBoard()
    {

        usernameRegis = inpText.Text;
        

    }

    public void ClickSignIn()
    {
        screenManager.isLockScreen = true;
        NetworkManager.Instance.Regis(usernameRegis);
    }
   
}
