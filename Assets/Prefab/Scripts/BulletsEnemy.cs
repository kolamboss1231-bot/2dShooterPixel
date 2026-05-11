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
        if (other.gameObject.layer == LayerMask.NameToLayer("Solid"))
        {
            if (other.gameObject.tag == "Player")
            {
                other.gameObject.GetComponent<Player>().TakeDamage(Damage);
                Destroy(gameObject);
            }
            
            if (other.gameObject.tag == "Enemy")
            {
                if (faceRight)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x + 0.5f, gameObject.transform.position.y);
                }

                if (faceRight == false)
                {
                    gameObject.transform.position = new Vector2(gameObject.transform.position.x - 0.5f, gameObject.transform.position.y);
                }
            }

            if (other.gameObject.tag != "Player" && other.gameObject.tag != "Enemy")
            {
                Destroy(gameObject);
            }
            
        }
    }
}
        