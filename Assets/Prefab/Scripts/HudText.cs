using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HudText : MonoBehaviour
{
    public TextMeshProUGUI Text;
    private int Value;

   // private void Awake()
    // {
    //     TextNull();
    //     
    // }
    private void TextNull()
    {
        Text.text = "0";
    }
    
    // public void HudF1AidText(int x)
    // {
    //     Text.text = x + "";
    // }
    
    public void TextGet(int x)
    {
        Value += x;
        Text.text = Value + "";
        //HudF1AidText(Value);
    }
    
}
