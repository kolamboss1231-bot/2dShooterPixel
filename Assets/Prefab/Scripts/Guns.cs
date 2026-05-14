using System;
using UnityEngine;
using Unity.Mathematics;



public class Guns : MonoBehaviour
{
    [SerializeField]protected GameObject shotAudio;
    [SerializeField]protected GameObject bullet; 
    [SerializeField]protected int maxAmmo;
    [SerializeField]protected float downRazbros, upRazbros;
    [SerializeField]protected Animator animGun;
    
    [SerializeField]public float timeReload;
    [SerializeField]public GameObject audioReload;
    [SerializeField]public int ammoInMagazine;
    
    protected GameObject textAmmoUi;
    
    protected  GameObject bulletOb;
    
    protected float delayTimeReload;
    protected int ammoInMagazineGame;
    protected int maxAmmoGame;
    protected int x, y;
    protected bool ladderOff;
    protected float _delatRiteOfFire;
    public float riteOfFire;
    [NonSerialized]protected  bool faceRight = true;
    [NonSerialized]public Transform parent;
    protected Vector3 face;
    
    protected virtual void Start()
    {
        maxAmmoGame = maxAmmo;
        ladderOff=true;
        
        textAmmoUi = GameObject.Find("TextAmmo");
        ammoInMagazineGame = ammoInMagazine;
        
        face = transform.localScale;
        if (face.x < 0)
        {
            face.x *= -1;
        }

        _delatRiteOfFire = riteOfFire;
    }


    protected void Update()
    {
        if (gameObject.transform.parent.CompareTag("Player") && gameObject == true && parent != gameObject.transform.parent)
        {
            parent = gameObject.transform.parent;
            
            if (parent.GetComponent<Transform>().localScale.x < 0 || gameObject.transform.localScale.x < 0)
            {
                gameObject.transform.localScale = face;
            }
            gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            faceRight = parent.GetComponent<Character>().faceRight;
        }

        if (gameObject.transform.parent.CompareTag("Enemy") && parent != gameObject.transform.parent)
        {
            parent = gameObject.transform.parent;
        }
        
        if (parent == gameObject.transform.parent && parent.name == "Player")
        {
            textAmmoUi.GetComponent<TextAmmoGuns>().AmmoText(ammoInMagazineGame, maxAmmoGame);

            if (Input.GetKey(KeyCode.A))
                faceRight = false;
            if (Input.GetKey(KeyCode.D))
                faceRight = true;

            if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) && ladderOff)
            {
                animGun.SetBool("Moves", true);
            }
            else
            {
                animGun.SetBool("Moves", false);
            }

            if (delayTimeReload > 0)
                delayTimeReload -= Time.deltaTime;

            if (_delatRiteOfFire > 0)
                _delatRiteOfFire -= Time.deltaTime;
            
            if (ammoInMagazineGame >0)
            {
                if (Input.GetKey(KeyCode.F) && _delatRiteOfFire <= 0 && delayTimeReload <=0 && ladderOff && ammoInMagazineGame >0)
                {
                    shotSpawn(bullet, faceRight);
                    _delatRiteOfFire = riteOfFire;
                    ammoInMagazineGame -= 1;
                }
            } 
            
            if(maxAmmoGame>0)
            {
                if (ammoInMagazineGame == 0 && ladderOff ||
                    (ammoInMagazine > ammoInMagazineGame && Input.GetKey(KeyCode.R) && ladderOff))
                {
                    if (maxAmmoGame > ammoInMagazine)
                    {
                        maxAmmoGame -= (ammoInMagazine - ammoInMagazineGame);
                        ammoInMagazineGame = ammoInMagazine;
                    }
                    else if (maxAmmoGame < ammoInMagazine)
                    {
                        if (ammoInMagazineGame + maxAmmoGame == ammoInMagazine)
                        {
                            maxAmmoGame = 0;
                            ammoInMagazineGame = ammoInMagazine;
                        }
                        if (ammoInMagazineGame + maxAmmoGame > ammoInMagazine)
                        {
                            maxAmmoGame = maxAmmoGame + ammoInMagazineGame - ammoInMagazine;
                            ammoInMagazineGame = ammoInMagazine;
                        }
                        if (ammoInMagazineGame + maxAmmoGame < ammoInMagazine)
                        {
                            ammoInMagazineGame += maxAmmoGame;
                            maxAmmoGame = 0;
                        }
                    }

                    delayTimeReload = timeReload;
                    Instantiate(audioReload, transform.position, Quaternion.Euler(0, 0, 0));
                }
            }
        }
    }

    public void LadderOff( float y)
    {
        if (y !=0)
            ladderOff = false;
        if (y == 0)
            ladderOff = true;
    }
    
    public virtual void shotSpawn(GameObject bullet, bool faceRight)
    {
        if (faceRight)
        {
            bulletOb = Instantiate(bullet, new Vector2(transform.position.x + .3382f, transform.position.y + 0.033f),
                quaternion.Euler(0, 0, RandomRazbros()));
            bulletOb.GetComponent<Bullets>().GunPosition(parent);
        }

        if (faceRight == false)
        {
            bulletOb = Instantiate(bullet, new Vector2(transform.position.x - .382f, transform.position.y + 0.033f),
                quaternion.Euler(0, 0, RandomRazbros()));
            bulletOb.GetComponent<Bullets>().GunPosition(parent);
        }
        Instantiate(shotAudio, transform.position,quaternion.Euler(0,0,0));
    } 



    protected  float RandomRazbros()
        {
        return UnityEngine.Random.Range(downRazbros,upRazbros);
        }

    public void FaceGet(bool face)
    {
        faceRight = face;
    }


    public void nullName()
    {
        parent = null;
    }

    public void AmmoPlus(int plus)
    {
        maxAmmoGame += plus;
        if (maxAmmoGame > maxAmmo)
        {
            maxAmmoGame = maxAmmo;
        }
    }
}
