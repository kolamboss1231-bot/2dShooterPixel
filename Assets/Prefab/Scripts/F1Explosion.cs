using System;
using UnityEngine;

public class F1Explosion : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float lifeTime;
    private float delayLifeTime;

    private void Start()
    {
        delayLifeTime=lifeTime;
    }

    private void Update()
    {
        if (delayLifeTime > 0)
        {
            delayLifeTime -= Time.deltaTime;
        }

        if (delayLifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(damage);
        }

        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyBot>().TakeDamage(damage);
        }
    }
}

