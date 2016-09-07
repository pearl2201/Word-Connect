using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BtnQuestion : MonoBehaviour
{

    public bool isSelect;
    public string text;
    [SerializeField]
    private tk2dTextMesh txtText;
    [SerializeField]
    private GameScreenManager gamescreenManager;
    [SerializeField]
    private tk2dSprite bg;

    public void SetText(string text)
    {
        this.text = text;
        this.txtText.text = text;
    }

    public void Click()
    {
        gamescreenManager.CheckClickBtnQuestion(this);
    }

    public void SetSelect()
    {
        isSelect = true;
        bg.color = new Color(0.4f, 0.4f, 0.4f, 1);
    }

    public void SetUnSelect()
    {
        isSelect = false;
        bg.color = Color.white;
    }
}

