using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MyCustomKeyboard : MonoBehaviour {

    public OnScreenKeyboard oks;
    public Color32[] color;
    public Sprite[] spr;

	public void SetColor1()
    {
        oks.SetBackgroundColor(color[0]);
        oks.SetMainColor(color[1]);
        oks.SetSpecialColor(color[1]);
        oks.SetTextColor(color[2]);
        oks.SetMainSprite(spr[0]);
        oks.SetSpecialSprite(spr[0]);
    }

    public void SetColor2()
    {
        oks.SetBackgroundColor(color[0]);
        oks.SetMainColor(color[1]);
        oks.SetSpecialColor(color[3]);
        oks.SetTextColor(color[2]);
        oks.SetMainSprite(spr[0]);
        oks.SetSpecialSprite(spr[0]);
    }

    public void SetColor3()
    {
        oks.SetBackgroundColor(color[0]);
        oks.SetMainColor(color[1]);
        oks.SetSpecialColor(color[4]);
        oks.SetTextColor(color[2]);
        oks.SetMainSprite(spr[0]);
        oks.SetSpecialSprite(spr[0]);
    }

    public void SetColor4()
    {
        oks.SetBackgroundColor(color[5]);
        oks.SetMainColor(color[6]);
        oks.SetSpecialColor(color[7]);
        oks.SetTextColor(color[0]);
        oks.SetMainSprite(spr[1]);
        oks.SetSpecialSprite(spr[1]);
    }

    public void SwitchNumpad()
    {
        oks.ShowNumeric(!oks.showNumeric);
    }

}
