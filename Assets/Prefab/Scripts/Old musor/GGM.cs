using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GGM : MonoBehaviour {
    public float s,h;
  [SerializeField]private float timeToRestart;
    public GameObject piston;
    public SpriteRenderer player;
    private Rigidbody2D rb;
    public bool faceRigth=true;
    public Transform spawnPiston;
    public GameObject shot;
    SpriteRenderer shotS;
    public Animator _animWalk;
    public Animator _GunMka;
    Animator shotA;
    private float x;
    private float y;
    [SerializeField]private float riteFire;
    private float _delayRiteFire;
    private bool shotoOrNoShot;
    public int hpPlayer;
    public GameObject shotAudio;

    private void Awake()
    {   
        shotoOrNoShot=true;
        _delayRiteFire = riteFire;
        _GunMka.GetComponent<Animator>();
        _GunMka.SetBool("Moves",false);
        rb=GetComponent<Rigidbody2D>();
        _animWalk.GetComponent<Animator>();
        shotS=shot.GetComponent<SpriteRenderer>();
        shotA=shot.GetComponent<Animator>(); 
        _animWalk.SetBool("Moves",false);           
    }
    private void FixedUpdate() {
        
    {
        if (hpPlayer > 0)
        {
            x = Input.GetAxis("Horizontal")*s*Time.fixedDeltaTime;
            rb.linearVelocity=transform.TransformDirection(new Vector2(x,rb.linearVelocity.y));

            if(x>0 && faceRigth==false)
                Flip();
            else if(x<0 && faceRigth==true)
                Flip();

            if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)){
                _GunMka.SetBool("Moves",true);
                _animWalk.SetBool("Moves",true);
                shotA.SetBool("Moves",true);
            }
                 
            else {
                _GunMka.SetBool("Moves",false);  
                _animWalk.SetBool("Moves",false);
                shotA.SetBool("Moves",false);
            }
                
            if(Input.GetKey(KeyCode.F) && shotoOrNoShot==true){
                shotA.gameObject.SetActive(true);
            }

            if(_delayRiteFire > 0)
                    _delayRiteFire -=Time.fixedDeltaTime;

            if (Input.GetKey(KeyCode.F)&& _delayRiteFire<=0 && shotoOrNoShot==true)
            {  
                Instantiate(shotAudio,player.transform.position,quaternion.Euler(0,0,0));
                shotSpawn();
                Debug.Log(UnityEngine.Random.Range(-10f,10f));
                _delayRiteFire=riteFire;
                shotA.gameObject.SetActive(true);
            }
            else {
                shotA.gameObject.SetActive(false);
                }
        }

        if(hpPlayer<0){
            _animWalk.SetBool("Death",true);
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<BOT>().DeathPlayer(hpPlayer);
            timeToRestart -= Time.deltaTime;
        }

        if( timeToRestart <=0f)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);

    }
    }
        private void shotSpawn(){
            if(faceRigth)
                Instantiate(piston,new Vector2(spawnPiston.position.x+0.481f, spawnPiston.position.y+0.063f),quaternion.Euler(0,0,RandomRazbros()));
            else if(faceRigth==false) Instantiate(piston,new Vector2(spawnPiston.position.x-0.481f, spawnPiston.position.y+0.063f),quaternion.Euler(0,0, RandomRazbros()));
    }

    void OnTriggerStay2D(Collider2D other)
    {  
        if(other.gameObject.name=="swal")
        {
            y = Input.GetAxis("Vertical")*h*Time.deltaTime;
            rb.linearVelocity=transform.TransformDirection(new Vector2(rb.linearVelocity.x,y));
        }  

        if(other.gameObject.name=="swal" && y!=0)
        {
                    shotoOrNoShot=false;        
                    // Debug.Log("shot of"); 
        }

        if(other.gameObject.name=="swal" && y==0)
        {
            shotoOrNoShot=true;
            //Debug.Log("shot on");
        }
   }
   
   public void TakeDamage(int Damage)
    {
        hpPlayer -=Damage;
        Debug.Log("hpplayer"+hpPlayer);
    }
    
    private void Flip()
    {
        faceRigth = !faceRigth;
        Vector3 face = transform.localScale;
        face.x *= -1;
        transform.localScale = face;
    }

    private float RandomRazbros(){
        return UnityEngine.Random.Range(-0.17f,0.17f);
        }
}