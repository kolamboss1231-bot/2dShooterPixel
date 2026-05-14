using UnityEngine;

public class BulletsPlayer  : Bullets
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
        if (other.gameObject.layer == LayerMask.NameToLayer("EnemyLayer"))
        {
            if (other.gameObject.tag == "Enemy")
            {
                other.gameObject.GetComponent<EnemyBot>().TakeDamage(Damage);
            }

            Destroy(gameObject);
        }
    }
}
        

