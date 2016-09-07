using UnityEngine;
using System.Collections;

public class Prefs
{

    private static Prefs _instance;

    public static Prefs Instance
    {

        get
        {
            if (_instance == null)
            {
                _instance = new Prefs();

            }
            return _instance;
        }
    }

    public static string KEY_SOUND = "key_sound";
    public static string KEY_BESTSCORE = "key_best_score";
    public static string KEY_LASTSCORE = "key_last_score";
    public static string KEY_COUNT_PLAY = "key_count_play";
    public static string KEY_ANDROIDID = "key_androidid";
    public Prefs()
    {

       
    }

    public string GetAndroidID()
    {
        return PlayerPrefs.GetString(KEY_ANDROIDID);
    }

    public void SetAndroidId(string key)
    {
        Debug.Log("set android id: " + key);
        PlayerPrefs.SetString(KEY_ANDROIDID, key);
    }

    public void SetSound(bool on)
    {
        if (on)
        {
            PlayerPrefs.SetInt(KEY_SOUND, 1);
        }
        else
        {
            PlayerPrefs.SetInt(KEY_SOUND, 0);
        }

    }
    public bool isSoundOn()
    {
        return PlayerPrefs.GetInt(KEY_SOUND, 1) == 1;
    }
}
