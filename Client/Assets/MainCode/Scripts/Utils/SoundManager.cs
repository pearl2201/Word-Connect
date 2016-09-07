using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour
{

    public AudioSource music;
    public AudioSource explo;
    public AudioSource ting;
    private bool isMusicPlay;
    private static SoundManager _instance;
    public static SoundManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<SoundManager>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else
        {
            if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
    }


    public void PlayMusic()
    {
        if (!isMusicPlay)
        {
            isMusicPlay = true;
            music.Play();
        }
       
    }

    public void StopMusic()
    {
        if (isMusicPlay)
        {
            isMusicPlay = false;
            music.Stop();
        }
    }

    public void PlayExplo()
    {
        if (Config.isSoundOn)
        {
            explo.Play();
        }
    }

    public void PlayTing()
    {
        if (Config.isSoundOn)
        {
            ting.Play();
        }
    }

}
