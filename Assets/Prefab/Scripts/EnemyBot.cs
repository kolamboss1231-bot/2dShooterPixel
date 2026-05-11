using UnityEngine;
using Unity.Mathematics;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;

public class EnemyBot : Character
{
    [SerializeField]protected float distPatrol;
    [SerializeField]protected float angryDist;
    [SerializeField]protected float distShoting;
    [SerializeField]protected Transform pointPatrol;
    [SerializeField]protected Animator anim;
    [SerializeField]protected GameObject Gun;
    [SerializeField]protected GameObject bullet;
    [SerializeField]protected GameObject[] dropItem = new GameObject[2];
    
    
     protected float riteOfFire;
     protected float _delayRiteOfFire;
     protected int AmmoinMagazine;
     protected int AmmoInMagazineGame;
     protected float timeReload;
     protected float delayTimeReload;
     protected GameObject audioReload;


    protected Transform playerPos;
    protected BoxCollider2D botBox;
    //protected bool lifePl;
    protected bool chil=false;
    protected bool angry=false;
    protected bool goBack=false;
    protected bool reverse=false;
    protected bool spawn  ;

    private int r;
    protected void Awake()
    {
        timeReload = Gun.GetComponent<Guns>().timeReload;
        audioReload = Gun.GetComponent<Guns>().audioReload;
        AmmoinMagazine = Gun.GetComponent<Guns>().ammoInMagazine;
        AmmoInMagazineGame = AmmoinMagazine;
        // shotAudio = Gun.GetComponent<Guns>().shotAudio;
         riteOfFire= Gun.GetComponent<Guns>().riteOfFire;
        
        // maxAmmo= Gun.GetComponent<Guns>().maxAmmo;
        // downRazbros= Gun.GetComponent<Guns>().downRazbros;
        // upRazbros= Gun.GetComponent<Guns>().upRazbros;
        //Gun.GetComponent<Guns>().enabled = true;
        
        _delayRiteOfFire = riteOfFire;
        rb=GetComponent<Rigidbody2D>();

        botBox = gameObject.GetComponent<BoxCollider2D>();
        playerPos=GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    protected void Update()
    {

        if(maxHp<0)
        {
            chil=false;
            angry=false;
            goBack=false;
            transform.position=new Vector2(transform.position.x,transform.position.y);
            anim.SetBool("Death",true);
            Destroy(Gun);
            botBox.enabled=false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            if (spawn == false)
            {
                DropSpawn();
                spawn = true;
            }
            Destroy(gameObject,5f);
        }

        if(maxHp>0)
        {
             if (_delayRiteOfFire > 0)
                 _delayRiteOfFire -= Time.deltaTime;
             
             if (delayTimeReload > 0)
                 delayTimeReload -= Time.deltaTime;
             
             if (AmmoInMagazineGame == 0)
             {
                 AmmoInMagazineGame = AmmoinMagazine;
                 delayTimeReload = timeReload;
                 Instantiate(audioReload, transform.position,Quaternion.Euler(0,0,0));
             }
             
            if(Vector2.Distance(transform.position, pointPatrol.position)< distPatrol && angry == false)
            {
                chil = true; 
            }
            
        
            if (Vector2.Distance(transform.position, playerPos.position) < angryDist &&  lifePl)
            {
                angry = true;
                chil = false;
                goBack = false;
            }
            


            if( Vector2.Distance(transform.position, playerPos.position) > angryDist)
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

    public void DeathPlayer()
    {
        lifePl = false;
    }

    protected void Chil()
    {

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
            transform.position=new Vector2(transform.position.x - speedLegs*Time.deltaTime,transform.position.y);
        else if(reverse==false)
            transform.position=new Vector2(transform.position.x + speedLegs*Time.deltaTime,transform.position.y);

        if(reverse == true && faceRight == true)
            FaceSwap();

        if(reverse == false && faceRight == false)
            FaceSwap();
        
    }

    protected void Angry()
    {
        if((transform.position.x > playerPos.position.x && faceRight ==true) || (transform.position.x < playerPos.position.x && faceRight == false))
            FaceSwap();
            
        transform.position=Vector2.MoveTowards(transform.position, playerPos.position, speedLegs*Time.deltaTime);

        if(Vector2.Distance(playerPos.position,transform.position) <distShoting && _delayRiteOfFire<=0 && AmmoInMagazineGame > 0 && delayTimeReload <= 0)
        {
            Gun.GetComponent<Guns>().shotSpawn(bullet,faceRight);
             _delayRiteOfFire  = riteOfFire;
             AmmoInMagazineGame -= 1;
        }
    }

    protected void GoBack()
    {
        if((transform.position.x > pointPatrol.position.x && faceRight == true) || ( transform.position.x < pointPatrol.position.x && faceRight == false))
            FaceSwap();

        transform.position=Vector2.MoveTowards(transform.position, pointPatrol.position, speedLegs*Time.deltaTime);
    }

    protected void FaceSwap()
    {
        faceRight = !faceRight;
        Vector3 face = transform.localScale;
        face.x *= -1;
        transform.localScale = face;
    }

    public void TakeDamage(float Damage)
    {
        maxHp -= Damage;
    }

    private void DropSpawn()
    {
        r = Random.Range(0, 10);
        if (r >= 1 && r <= 4)
        {
            Instantiate(dropItem[0], transform.position, transform.rotation);
        }

        if (r >= 5 && r <= 6)
        {
            Instantiate(dropItem[1], transform.position, transform.rotation);
        }
    }
}

