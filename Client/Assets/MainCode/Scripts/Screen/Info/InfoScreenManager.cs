using UnityEngine;
using System.Collections;

public class InfoScreenManager : MonoBehaviour {


    public ScreenManager screenManager;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Close()
    {
        screenManager.OpenStartScreen();
        gameObject.SetActive(false);
        
    }
}
