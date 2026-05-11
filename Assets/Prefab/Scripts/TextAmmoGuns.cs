using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class TextAmmoGuns : MonoBehaviour
{
    //[SerializeField] 
    public TextMeshProUGUI ammo;

    private void Awake()
    {
        ammo.text = "0 / 0";
      //  gameObject.GetComponent<TextMesh>().text = ammo.text;
    }

    // private void Update()
    // {
    //     ammo.text = "asda";
    // }

    public void AmmoText(int ammoInMagazineGame, int maxAmmo)
    {
        ammo.text = ammoInMagazineGame + " / " + maxAmmo;
    }
    public void AmmoTextNull()
    {
        ammo.text = "X / X";
    }
}
