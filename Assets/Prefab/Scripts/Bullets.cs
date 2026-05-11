using System;
using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;

public class Bullets : MonoBehaviour
{
    [SerializeField]protected GameObject Gun;
    [SerializeField]protected float speedPiston;
    [SerializeField]protected float distPistDest;
    [SerializeField]protected LayerMask whatIsSolid;
    [SerializeField]protected float Damage;

    protected bool faceRight;
    protected Transform spawnPos;
    
    
    //faceRight = Gun.GetComponent<Guns>().faceRight;
    //Gun.GetComponent<Transform>();
    protected virtual void Awake()
    {
        
    }
    
    void Update()
    {
    }

    public virtual void GunPosition(Transform parent)
    {
        spawnPos = parent;
        GetSpawn();
    }

    protected virtual void GetSpawn()
    {
        faceRight = spawnPos.GetComponent<Character>().faceRight;
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        
    }

    
    // protected virtual void DestroyBullet()
    // {
    //     
    // }
}
