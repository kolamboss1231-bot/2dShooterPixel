using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class pistonCreate : MonoBehaviour
{
    public GameObject piston;
    private GameObject pl;
    public float speedPiston;
    public float distPistDest;
    public LayerMask whatIsSolid;
    public int Damage;
    void Awake()
    {
        pl=GameObject.FindGameObjectWithTag("Player");
        pl.gameObject.GetComponent<Transform>();
        piston.gameObject.GetComponent<Transform>();
        piston.gameObject.GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        RaycastHit2D Hitinfo=Physics2D.Raycast(transform.position, transform.up, distPistDest,whatIsSolid);
        if(Hitinfo.collider != null)
        {
            if(Hitinfo.collider.CompareTag("Enemy"))
            {
          //     Hitinfo.collider.GetComponent<EnemyBotMka>().TakeDamage(Damage);
            }
            Destroy(gameObject);
        }

       
        if(piston.transform.position.x > pl.transform.position.x)
            piston.transform.Translate(Vector2.right*speedPiston*Time.deltaTime);
       
        else if (pl.transform.position.x > piston.transform.position.x)
         piston.transform.Translate(Vector2.left*speedPiston*Time.deltaTime);
       

        if (Vector2.Distance(piston.transform.position, pl.transform.position) > distPistDest)
        {
            Destroy(piston);
        }
    }
}
