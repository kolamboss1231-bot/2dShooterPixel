using UnityEngine;

public class PickUpAmmo : MonoBehaviour
{
    private GameObject[] gunPlayer = new GameObject[3];
    [SerializeField] private int ammoDown;
    [SerializeField] private int ammoUp;
    [SerializeField] private float timeDestroy;
    [SerializeField] private string gunName;

    private int ammoPlus;
    private void Start()
    {
        AmmoRand(ammoDown, ammoUp);
        gunPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Gun;
        Destroy(gameObject, timeDestroy);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < gunPlayer.Length; i++)
            {
                if (gunPlayer[i])
                {
                    if (gunPlayer[i].name == gunName)
                    {
                        gunPlayer[i].GetComponent<Guns>().AmmoPlus(ammoPlus);
                        Destroy(gameObject);
                        break;
                    }
                }
            }
            
        }
    }

    private void AmmoRand(int ammoDown, int ammoUp)
    {
        ammoPlus = Random.Range(ammoDown, ammoUp);
    }
}
