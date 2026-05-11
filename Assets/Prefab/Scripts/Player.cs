using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    [SerializeField]private GameObject[] Gun = new GameObject[3];

    [SerializeField]private GameObject[] slots;
    [SerializeField]private float h;

    protected Animator anim;


    [SerializeField]private Image hpImage;

    private float x, y;
    [NonSerialized]public float hpPl;
    private int inHandGun;
    private GameObject textAmmoUi;
    
    // private bool IsGrounded;
    // [SerializeField]private LayerMask plat;
    // [SerializeField]private float distGround;
    // [SerializeField]private Transform groundCheck;

    
    private void Awake()
    {   
        textAmmoUi = GameObject.Find("TextAmmo");
        hpPl=maxHp;
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inHandGun = 0;

    }
    
    private void Update() 
    {

        if (hpPl > 0)
        {
            lifePl = true;
            x = Input.GetAxis("Horizontal") * speedLegs * Time.deltaTime;
            rb.linearVelocity = transform.TransformDirection(new Vector2(x, rb.linearVelocity.y));

            if (x > 0 && faceRight == false)
                FaceSwap();
            else if (x < 0 && faceRight == true)
                FaceSwap();

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
                anim.SetBool("Moves", true);
            else
                anim.SetBool("Moves", false);
            
            // IsGrounded = Physics2D.OverlapCircle(groundCheck.position, distGround, plat);
            
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if (Gun[inHandGun] == true)
                {
                    Gun[inHandGun].SetActive(false);
                }

                inHandGun = 0;
                GunSpawn(inHandGun);
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (Gun[inHandGun] == true)
                {
                    Gun[inHandGun].SetActive(false);
                }

                inHandGun = 1;
                GunSpawn(inHandGun);
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                if (Gun[inHandGun] == true)
                {
                    Gun[inHandGun].SetActive(false);
                }

                inHandGun = 2;
                GunSpawn(inHandGun);
            }

            if (Input.GetKeyDown(KeyCode.G) && Gun[inHandGun])
            {

                Gun[inHandGun].GetComponent<Animator>().enabled = false;
                Gun[inHandGun].GetComponent<Guns>().FaceGet(faceRight);
                Gun[inHandGun].GetComponent<Guns>().enabled = false;
                textAmmoUi.GetComponent<TextAmmoGuns>().AmmoTextNull();
                slots[inHandGun].GetComponent<Slot>().DropItem();
                Gun[inHandGun].transform.parent = null;
                Gun[inHandGun].GetComponent<BoxCollider2D>().enabled = true;
                Gun[inHandGun].GetComponent<Guns>().nullName();
                DropItem();
                Gun[inHandGun] = null;
            }
        }

        if(hpPl <= 0)
        {
            anim.SetBool("Death",true);
            GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyBot>().DeathPlayer();
            Destroy(Gun[inHandGun]);
        }
    }

    private void FaceSwap()
    {
        faceRight = !faceRight;
        Vector3 face = transform.localScale;
        face.x *= -1;
        transform.localScale = face;
    }

    private void OnTriggerStay2D(Collider2D other)
    { 
        if (other.gameObject.name=="Ladder")
        {
            y = Input.GetAxis("Vertical")*h*Time.deltaTime;
            rb.linearVelocity=transform.TransformDirection(new Vector2(rb.linearVelocity.x,y));
            if(Gun[inHandGun])
            {
                Gun[inHandGun].GetComponent<Guns>().LadderOff(y);
            }
        }  

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Ladder")
        {
            y = 0;
            Gun[inHandGun].GetComponent<Guns>().LadderOff(y);

       }
   }

    public void TakeDamage(float Damage)
    {
        hpPl -=Damage;
        hpImage.GetComponent<HpImage>().TakeHP(hpPl);
    }

    void GunSpawn(int inHandGun)
    {
        if(slots[inHandGun].transform.childCount > 0)
        { 
            Gun[inHandGun].SetActive(true);
            Gun[inHandGun].GetComponent<BoxCollider2D>().enabled = false;
            Gun[inHandGun].GetComponent<Animator>().enabled = true;
            Gun[inHandGun].GetComponent<Guns>().enabled = true;      
        }
    }

    public void PickUpGun(GameObject getGun, int i )
    {
        getGun.transform.parent = gameObject.transform;
        Gun[i] = getGun;
        if (inHandGun == i)
            GunSpawn(i);
    }

    private void DropItem()
    {
        if(faceRight)
        {
            Gun[inHandGun].transform.position= new Vector2(gameObject.transform.position.x + 1.5f,gameObject.transform.position.y);
        }
        else if ( faceRight == false)
        {
            Gun[inHandGun].transform.position= new Vector2(gameObject.transform.position.x - 1.5f,gameObject.transform.position.y);
        }
    }
}
