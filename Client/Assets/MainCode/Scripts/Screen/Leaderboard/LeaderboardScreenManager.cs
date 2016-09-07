using UnityEngine;
using System.Collections;

public class LeaderboardScreenManager : MonoBehaviour {

    public ScreenManager screenManager;

    [SerializeField]
    private ItemLeaderboard modelItem;

    [SerializeField]
    private tk2dUIScrollableArea scrollArea;

    private static float distance2Item;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Setup(string leaderboard)
    {

    }


    public void Close()
    {
        screenManager.OpenStartScreen();
    }
}
