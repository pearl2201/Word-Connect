using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ItemLeaderboard : MonoBehaviour
{
    [SerializeField]
    private tk2dTextMesh txtOrder;
    [SerializeField]
    private tk2dTextMesh txtUsername;
    [SerializeField]
    private tk2dTextMesh txtScore;

    public void SetInfo(int order, string username, int score)
    {
        txtOrder.text = "" + order;
        txtUsername.text = "" + username;
        txtScore.text = "" + score;
    }

}

