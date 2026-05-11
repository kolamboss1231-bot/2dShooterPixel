using System.Data;
using UnityEngine;
using Unity.Mathematics;

public class IIbot : MonoBehaviour
{
    public Animator AnimBot;
    public float speed;
    public int posControl;
    public bool reverse;
    public float stopDistanse;
    Transform mainplayer;
    public Transform point;
    private bool chil=false;
    private bool angry=false;
    private bool goBack=false;
    public GameObject Bot;
    public GameObject Piston;
    [SerializeField]private int maxHealthBot;
    private int HpBot;  
    public GameObject MainPlayer;
    private int hpPlayer;
    public GameObject shotAudio;

    float distShoting;
    public SpriteRenderer SpBot;
    public BoxCollider2D BotBox;
    private float riteFire=0.3f;
    private float _delayRiteFire;
    
    void Awake()
    { 
        hpPlayer=MainPlayer.GetComponent<GGM>().hpPlayer;
        _delayRiteFire = riteFire;
        distShoting=Piston.GetComponent<pistonEnemy>().distPistDest;
        SpBot.GetComponent<SpriteRenderer>();
        mainplayer=GameObject.FindGameObjectWithTag("Player").transform;
        AnimBot.GetComponent<Animator>();
        HpBot=maxHealthBot;
        AnimBot.SetBool("Moves",true);
      //  SpBot.flipX=true;
        BotBox=Bot.GetComponent<BoxCollider2D>();
        
    }

    void Update()
    {
        if(HpBot<0){
            chil=false;
            angry=false;
            goBack=false;
            Bot.transform.position=new Vector2(Bot.transform.position.x,Bot.transform.position.y);
            AnimBot.SetBool("Death",true);
            BotBox.enabled=false;
            Destroy(Bot,10f);
        }
        if(_delayRiteFire > 0)
                _delayRiteFire -=Time.deltaTime;

        if(Vector2.Distance(transform.position, point.position) < posControl && angry ==false && HpBot>0)
        { 
            chil=true;
        }

        else if(hpPlayer>0)
            if((Vector2.Distance(transform.position, mainplayer.position) < stopDistanse) && HpBot>0)
            {
                chil=false;
                angry=true;
                goBack=false;         
             }
         else if(hpPlayer<0)
          if(Vector2.Distance(transform.position, mainplayer.position) > stopDistanse && angry==true && HpBot>0)
        {
            angry=false; goBack=true;
        }
        
        if(chil)
            Chil();
        else if(angry)
            Angry();
        else if(goBack)
            GoBack();
    }
    void Chil()
    {
        if(transform.position.x > point.position.x + posControl)
           {
             reverse=false;
           SpBot.flipX=true;
           }
        else if(transform.position.x < point.position.x - posControl)
           { reverse=true;
           SpBot.flipX=false;
           }
        if(reverse)
          {  transform.position=new Vector2(transform.position.x + speed*Time.deltaTime,transform.position.y);}
        else 
           {transform.position=new Vector2(transform.position.x - speed*Time.deltaTime,transform.position.y);}
        
    }

     void Angry()
    {
        if( hpPlayer < 0)
            angry=false;

        transform.position=Vector2.MoveTowards(transform.position, mainplayer.position, speed*Time.deltaTime);

        if(Vector2.Distance(mainplayer.position,transform.position) <distShoting-5 && _delayRiteFire<=0)
        {
            shotSpawn();
            _delayRiteFire=riteFire;
        }

        
    }

     void GoBack()
    {
        transform.position=Vector2.MoveTowards(transform.position,point.position, speed*Time.deltaTime);
        
    }
    public void TakeDamage(int Damage)
    {
        HpBot-=Damage;  
    }
  

    public void shotSpawn()
    {
        Instantiate(shotAudio,Bot.transform.position,quaternion.Euler(0,0,0));
        if(SpBot.flipX==true)
            Instantiate(Piston,new Vector2(transform.position.x-1.681f, transform.position.y+0.063f), quaternion.Euler(0,0,0));
        else if (SpBot.flipX==false)
        Instantiate(Piston,new Vector2(transform.position.x+-1.681f, transform.position.y+0.063f), quaternion.Euler(0,0,0));
    }
    public void DeathPlayer(bool lifePl)
    {
        angry=lifePl;
    }
}
