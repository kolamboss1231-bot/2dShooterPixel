using UnityEngine;

public class BulletsEnemy : Bullets
{   
    void Update()
    {
        
        if(faceRight)
        {
            gameObject.transform.Translate(Vector2.right*speedPiston*Time.deltaTime);
        }
           
        if (faceRight==false)
        {
            gameObject.transform.Translate(Vector2.left*speedPiston*Time.deltaTime);
        }     
        
        if (Vector2.Distance(gameObject.transform.position, spawnPos.transform.position) > distPistDest)
        {
            Destroy(gameObject);
        }
    }
    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("PlayerLayer"))
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                Destroy(gameObject);
            }
            
            if (other.gameObject.tag != "Player")
            {
                Destroy(gameObject);
            }
            
        }
    }
}
        