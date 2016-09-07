using UnityEngine;
using System.Collections;

public class Config
{
    public static string URL_SERVER = "ws://127.0.0.1:4568/socket.io/?EIO=4&transport=websocket";
    public static bool IS_AUTO_CONNECT = false;
    public static bool isSoundOn = true;
    private static string ANDROID_ID = "";
    public static User user = null;
    public static GameState gameState;
    public static bool isLogin;
    public static string GetAndroidID()
    {
        if (ANDROID_ID.Equals(""))
        {
          
            if (Prefs.Instance.GetAndroidID().Equals(""))
            {
                Debug.Log("prefs null");
                if (Application.isMobilePlatform)
                {
                    AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                    AndroidJavaObject currentActivity = up.GetStatic<AndroidJavaObject>("currentActivity");
                    AndroidJavaObject contentResolver = currentActivity.Call<AndroidJavaObject>("getContentResolver");
                    AndroidJavaClass secure = new AndroidJavaClass("android.provider.Settings$Secure");
                    string tmpStr = secure.CallStatic<string>("getString", contentResolver, "android_id");
                    Prefs.Instance.SetAndroidId(tmpStr);
                    ANDROID_ID = tmpStr;
                }else
                {
                    Debug.Log("desktop");
                    ANDROID_ID = Random.Range(1, 50000).ToString();
                    Prefs.Instance.SetAndroidId(ANDROID_ID);
                }
                
            }
            else
            {
                string tmpStr = Prefs.Instance.GetAndroidID();
                ANDROID_ID = tmpStr;
            }
        }

        return ANDROID_ID;
    }

}


public enum GameState
{
    connect,
    login,
    regis,
    leaderboard,
    welcome,
    start,
    waitnextQuestion,
    ingame,
}