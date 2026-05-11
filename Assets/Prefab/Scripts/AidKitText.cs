using System;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AidKitText : MonoBehaviour
{
    public TextMeshProUGUI aidKit;
    private int aidKitValue = 0;

    private void Start()
    {
        AIdTextNull();
        
    }
    private void AIdTextNull()
    {
        aidKit.text = "0";
    }
    
    public void AidText(int x)
    {
        aidKit.text = x + "";
    }
    
    public void AidKitGet(int x)
    {
        aidKitValue += x;
        AidText(aidKitValue);
    }
    
}
