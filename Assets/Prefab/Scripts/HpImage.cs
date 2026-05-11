using System;
using UnityEngine;
using UnityEngine.UI;

public class HpImage : MonoBehaviour
{
    [SerializeField]private GameObject player;
    private float hpPlayer;
    
    [SerializeField]private Image bar;

    private void Start()
    {
        hpPlayer = player.GetComponent<Player>().hpPl;

    }

    private void Update()
    {
        bar.fillAmount = hpPlayer / 200;
    }

    public void TakeHP(float hpPl)
    {
        hpPlayer = hpPl;

    }
    
}
