using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Player : Character
{
    public static Action DeathPlayer;
    
    public GameObject[] Gun = new GameObject[3];

    [SerializeField]private GameObject[] slots;
    [SerializeField]private float h;
    [SerializeField]private Image hpImage;
    [SerializeField] private float hpAidKit;
    
    
    
    [SerializeField] private GameObject f1Gren;
    private GameObject f1GrenGAme;
    
    [NonSerialized]public float hpPl;
    
   // private GameObject AidText;
    private GameObject f1Text;
    [SerializeField] private int maxF1 = 7;
    private GameObject aidText;
    [SerializeField] private int AidKutF1Plus = 1;
    [SerializeField] private int maxAidKit = 7;
    private int aidKit = 0;
    private int f1 = 3;

    private GameObject door;
    private bool doorTrigger;
    private GameObject dial;
    private bool dialTrigger;
    
    
    private float x, y;
    private int inHandGun;
    private GameObject textAmmoUi;
    private Animator anim;

    private void Awake()
    {   
        aidText = GameObject.Find("AidKitText");
        f1Text = GameObject.Find("F1Text");
        
        textAmmoUi = GameObject.Find("TextAmmo");
        hpPl=maxHp;
        rb=GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        inHandGun = 0;
        f1Text.GetComponent<HudText>().TextGet(f1);
    }
    

    private void Update() 
    {
        
        if (hpPl > 0)
        {
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
                Gun[inHandGun].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                slots[inHandGun].GetComponent<Slot>().DropItem();
                Gun[inHandGun].transform.parent = null;
                Gun[inHandGun].GetComponent<BoxCollider2D>().enabled = true;
                Gun[inHandGun].GetComponent<Guns>().nullName();
                DropItem();
                Gun[inHandGun] = null;
            }

            if (aidKit > 0 && Input.GetKeyDown(KeyCode.H))
            {
                HpPlus(hpAidKit);
            }
        }

        if (Input.GetKeyDown(KeyCode.B) && f1 > 0)
        {
            SpawnGranade();
            f1Text.GetComponent<HudText>().TextGet(-1);
            f1 -= 1;
        }
        
        if (Input.GetKeyDown(KeyCode.E) && dialTrigger )
        {
            dial.GetComponent<DialogueAnimator>().StartDialogueOnOPlayer();
        }
        
        if (Input.GetKeyDown(KeyCode.E) && doorTrigger )
        {
            door.GetComponent<DoorScript>().DoorManager();   
        }
        
        if(hpPl <= 0)
        {
            DeathPlayer?.Invoke();
            anim.SetBool("Death",true);
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
        
        if (other.gameObject.name == "Ladder")
        {
            y = Input.GetAxis("Vertical") * h * Time.deltaTime;
            rb.linearVelocity = transform.TransformDirection(new Vector2(rb.linearVelocity.x, y));
            if (Gun[inHandGun])
            {
                Gun[inHandGun].GetComponent<Guns>().LadderOff(y);
            }
        }

        if (other.name == "AidKit(Clone)")
        {
            if (aidKit < maxAidKit)
            {
                aidText.GetComponent<HudText>().TextGet(AidKutF1Plus);
                aidKit += 1;
                Destroy(other.gameObject);
            }
        }

        if (other.name == "F1 Drop(Clone)")
        {
            if (f1 < maxF1)
            {
                f1Text.GetComponent<HudText>().TextGet(AidKutF1Plus);
                f1 += 1;
                Destroy(other.gameObject);
            }
        }

        // if (other.gameObject.CompareTag("Door") )
        // {
        //     Debug.Log("Door");
        //     if (Input.GetKeyDown(KeyCode.E))
        //     {
        //         other.gameObject.GetComponent<DoorScript>().DoorManager();   
        //     }
        // }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "DialoguePerson")
        {
            dial = other.gameObject;
            dialTrigger = true;
        }

        if (other.gameObject.CompareTag("Door"))
        {
            door = other.gameObject;
            doorTrigger = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Ladder")
        {
            y = 0;
            Gun[inHandGun].GetComponent<Guns>().LadderOff(y); 
        }

        if (other.name == "DialoguePerson")
        {
            dialTrigger = false;
            dial = null;
        }
        
        if (other.gameObject.CompareTag("Door"))
        {
            door = null;
            doorTrigger = false;
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
            Gun[inHandGun].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
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

    public void HpPlus(float hp)
    {
        aidKit -= 1;
        hpPl += hp;
        if (hpPl > maxHp)
        {
            hpPl = maxHp;
        }
        hpImage.GetComponent<HpImage>().TakeHP(hpPl);
        aidText.GetComponent<HudText>().TextGet(-1);
    }



    private void SpawnGranade()
    {
        if (faceRight)
        {
            f1GrenGAme = Instantiate(f1Gren, new Vector2(transform.position.x + .5382f, transform.position.y + 0.033f),
                Quaternion.Euler(0, 0, 0));
            f1GrenGAme.GetComponent<Rigidbody2D>().AddForce(new Vector2(7f,6f), ForceMode2D.Impulse);
        }

        if (faceRight == false)
        {
            f1GrenGAme = Instantiate(f1Gren, new Vector2(transform.position.x - .5382f, transform.position.y + 0.033f),
                Quaternion.Euler(0, 0, 0));
            f1GrenGAme.GetComponent<Rigidbody2D>().AddForce(new Vector2(-7f,6f), ForceMode2D.Impulse);
        }
        
    }
}
