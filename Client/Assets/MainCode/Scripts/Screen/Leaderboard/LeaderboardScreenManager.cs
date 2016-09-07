using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimpleJSON;
public class LeaderboardScreenManager : MonoBehaviour
{

    public ScreenManager screenManager;

    [SerializeField]
    private ItemLeaderboard modelItem;

    [SerializeField]
    private tk2dUIScrollableArea scrollArea;

    private static float distance2Item = -0.8f;
    private static float posStartScroll = -0.28f;

    [SerializeField]
    private Transform parentItemLeaderboard;

    public List<GameObject> listItemLeaderboard;
    // Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Setup(string leaderboard)
    {
      
        Vector3 localPos = new Vector3(0, posStartScroll, 0);
        int order = 0;
        leaderboard = leaderboard.Replace("\\\"", "\"");
       
       
        var data = JSON.Parse(leaderboard);
        
      
        var arr = data.AsArray;
        foreach (var tj in arr)
        {
          
            var j = JSON.Parse(tj.ToString());
            order += 1;
          
            GameObject go = Instantiate(modelItem.gameObject) as GameObject;
            go.transform.SetParent(parentItemLeaderboard);
      
            go.transform.localPosition = localPos;
       
            localPos.y += distance2Item;
         
            listItemLeaderboard.Add(go);
        
            go.GetComponent<ItemLeaderboard>().SetInfo(order,j["username"], j["score"].AsInt);
           
        }
        float length = -(localPos.y - posStartScroll);
        if (length<20)
        {
            length = 20;
        }
        scrollArea.ContentLength = length ;

    }


    public void Close()
    {
        screenManager.OpenStartScreen();
        gameObject.SetActive(false);
    }
}
