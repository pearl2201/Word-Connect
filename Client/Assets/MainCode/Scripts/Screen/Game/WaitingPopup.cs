using System;
using System.Collections.Generic;
using System.Linq;

using System.Text;
using UnityEngine;

public class WaitingPopup:MonoBehaviour
{
    private string[] waitingText = new string[4]
    {
        "Waiting","Waiting.","Waiting..","Waiting..."
    };

    private float time;
    [SerializeField]
    private tk2dTextMesh txtWaiting;
    private int level;
    void OnEnable()
    {
        time = 0;
        level = 0;
        txtWaiting.text = waitingText[0];
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time >1f)
        {
            level += 1;
            if (level == 4)
            {
                level = 0;

            }
            time = 0;
            txtWaiting.text = waitingText[level];
        }
    }
}

