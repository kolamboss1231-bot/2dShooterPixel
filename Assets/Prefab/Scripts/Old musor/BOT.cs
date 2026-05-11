using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using System.Collections;

public class BOT : MonoBehaviour
{
    public int maxhHpBot;
    private int hpBot;
    public float speed;
    public float RiteFire;
    private float _delayRiteFire;
    public float distPatrol;
    public float angryDist;
    public float distShoting;

    public Transform pointPatrol;
    private GameObject player;
    private Transform playerT;
    public GameObject pistonEnemy;
    public GameObject shotAudio;
    public Animator anim;
    private BoxCollider2D BoxBot;
    private Rigidbody2D rbBot;

    private bool faceRight;
    private bool chil=false;
    private bool angry=false;
    private bool goBack=false;
    private bool reverse;
    private int Lifepl;

    private Vector3 face;
  
    private void Awake()
    {
        Lifepl = GameObject.FindGameObjectWithTag("Player").GetComponent<GGM>().hpPlayer;
        player = GameObject.FindGameObjectWithTag("Player");
        _delayRiteFire = RiteFire;
        BoxBot=gameObject.GetComponent<BoxCollider2D>();
        rbBot=gameObject.GetComponent<Rigidbody2D>();
        faceRight=true;
        hpBot=maxhHpBot;
        playerT=GameObject.FindGameObjectWithTag("Player").transform;
        Debug.Log(Lifepl);
    }

    void Update()
    {
        if(hpBot<0){
            chil=false;
            angry=false;
            goBack=false;
            transform.position=new Vector2(transform.position.x,transform.position.y);
            anim.SetBool("Death",true);
            BoxBot.enabled=false;
            rbBot.constraints = RigidbodyConstraints2D.FreezeAll;
            Destroy(gameObject,5f);
        }

        if(hpBot>0){

            if(_delayRiteFire >0)
                _delayRiteFire -=Time.deltaTime;

            if(Vector2.Distance(transform.position, pointPatrol.position)< distPatrol && angry == false)
            {
             chil = true; 
            }
            
            if ( Lifepl > 0){
                if (Vector2.Distance(transform.position, playerT.position) < angryDist )
                {
                    angry = true;
                    chil = false;
                    goBack = false;
                // Debug.Log(chil);
                }
            }  
            else if(  Lifepl < 0)
                angry = false;

            if( Vector2.Distance(transform.position, playerT.position)>angryDist)
            {
                angry=false;
                goBack = true;
            }

            if(chil==true)
                Chil();
            else if(angry==true)
                Angry();
            else if(goBack==true)
                GoBack();

        }
    }
    private void Chil()
    {
        // Debug.Log("CHIL");
        if(transform.position.x > pointPatrol.position.x + distPatrol)
        {
            reverse=true;
        }
        else if(transform.position.x < pointPatrol.position.x - distPatrol)
        {
            reverse=false;
        }

        if((transform.position.x > pointPatrol.position.x + distPatrol && faceRight ==true) || (transform.position.x < pointPatrol.position.x - distPatrol && faceRight == false))
            FaceSwap();
            
        if (reverse)
            transform.position=new Vector2(transform.position.x - speed*Time.deltaTime,transform.position.y);
        else if(reverse==false)
            transform.position=new Vector2(transform.position.x + speed*Time.deltaTime,transform.position.y);

        if(reverse == true && faceRight == true)
            FaceSwap();

        if(reverse == false && faceRight == false)
            FaceSwap();
        
    }

    private void Angry()
    {

       // Debug.Log("ANGRYYYYY");
        if((transform.position.x > playerT.position.x && faceRight ==true) || (transform.position.x < playerT.position.x && faceRight == false))
            FaceSwap();
            
        transform.position=Vector2.MoveTowards(transform.position, playerT.position, speed*Time.deltaTime);
        if(Vector2.Distance(playerT.position,transform.position) <distShoting-5 && _delayRiteFire<=0)
        {
            shotSpawn();
            _delayRiteFire=RiteFire;
        }
    }

    private void GoBack()
    {
        if((transform.position.x > pointPatrol.position.x && faceRight == true) || ( transform.position.x < pointPatrol.position.x && faceRight == false))
            FaceSwap();

        transform.position=Vector2.MoveTowards(transform.position, pointPatrol.position, speed*Time.deltaTime);
    }
    
    private void FaceSwap()
    {
        faceRight = !faceRight;
        face = transform.localScale;
        face.x *= -1;
        transform.localScale= face;
    }

     

    private void shotSpawn()
    {
        Instantiate(shotAudio,gameObject.transform.position,quaternion.Euler(0,0,0));
        if(faceRight==true)
            Instantiate(pistonEnemy,new Vector2(transform.position.x+0.481f, transform.position.y+0.063f), quaternion.Euler(0,0,0));
        else if (faceRight==false)
        Instantiate(pistonEnemy,new Vector2(transform.position.x-0.481f, transform.position.y+0.063f), quaternion.Euler(0,0,0));
    }

    public void DeathPlayer(int hpPlayer){
        Lifepl = hpPlayer;
    }


    
}
