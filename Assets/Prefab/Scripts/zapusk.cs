using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class zapusk : MonoBehaviour
{

    public GameObject Bot1;
    public GameObject Bot2;
    public GameObject Bot3;
    public GameObject GunMka;
    public GameObject Gun,GunS;

    private int i = 1;

    
    void Start()
    {
  
       
       // Instantiate(Bot1,new Vector2(8,0),quaternion.Euler(0,0,0));
       // Instantiate(Bot2,new Vector2(12,0),quaternion.Euler(0,0,0));
       // Instantiate(Bot3,new Vector2(16,0),quaternion.Euler(0,0,0));
       Instantiate(GunMka,new Vector2(1,0),quaternion.Euler(0,0,0));
       Instantiate(Gun,new Vector2(-3,0),quaternion.Euler(0,0,0));
       Instantiate(GunS,new Vector2(-1,0),quaternion.Euler(0,0,0));
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);

        if(Input.GetKeyDown(KeyCode.Q))
            SpawnBot();
        
    }

    void SpawnBot()
    {
        Instantiate(Bot1,new Vector2(10,0),quaternion.Euler(0,0,0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && i == 1)
        {
            i = 0;
            Instantiate(Bot1,new Vector2(8,0),quaternion.Euler(0,0,0));
            Instantiate(Bot2,new Vector2(12,0),quaternion.Euler(0,0,0));
            Instantiate(Bot3,new Vector2(16,0),quaternion.Euler(0,0,0));
        }
    }

}
