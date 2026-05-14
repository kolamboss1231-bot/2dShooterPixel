using System;
using UnityEngine;

public class F1Grenade : MonoBehaviour
{
   
    [SerializeField] private float lifeTime;
    [SerializeField] private GameObject f1Explosion;
    [SerializeField] private GameObject F1ExplosionAudio;
    private float delayLifeTime;

    private void Start()
    {
        delayLifeTime =  lifeTime;
    }

    private void Update()
    {
        if (delayLifeTime > 0)
        {
            delayLifeTime -= Time.deltaTime;    
        }

        if (delayLifeTime < 0)
        {
            Explosion();
        }
    }

    private void Explosion()
    {
        Destroy(gameObject);
        Instantiate(f1Explosion, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
        Instantiate(F1ExplosionAudio, new Vector3(transform.position.x, transform.position.y), Quaternion.identity);
    }
}
