using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class pistonEnemy : MonoBehaviour
{
    public GameObject piston;
    private GameObject enemy;
    public float speedPiston;
    public float distPistDest;
    public LayerMask whatIsSolid;
    public int Damage;
    void Awake()
    {
        enemy=GameObject.FindGameObjectWithTag("Enemy");
        enemy.gameObject.GetComponent<Transform>();
        piston.gameObject.GetComponent<Transform>();
        piston.gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        RaycastHit2D Hitinfo=Physics2D.Raycast(transform.position,transform.up,distPistDest, whatIsSolid);
        if(Hitinfo.collider != null)
        {
            if(Hitinfo.collider.CompareTag("Player"))
            {
                Hitinfo.collider.GetComponent<Player>().TakeDamage(Damage);
                Destroy(gameObject);
             //   Debug.Log("Player damaged");
            }
            if(Hitinfo.collider.CompareTag("Enemy"))
                Destroy(gameObject);
        }

        if(piston.transform.position.x > enemy.transform.position.x && enemy.gameObject != null)
           piston.transform.Translate(Vector2.right*speedPiston*Time.deltaTime);
        else if (enemy.transform.position.x > piston.transform.position.x && enemy.gameObject != null)
            piston.transform.Translate(Vector2.left*speedPiston*Time.deltaTime);
        else if ( enemy.gameObject ==null)
                Destroy(gameObject);
                
        if (Vector2.Distance(piston.transform.position, enemy.transform.position) > distPistDest)
        {
            Destroy(piston);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.name=="Bot(Clone)")
            Destroy(piston);
    }
}