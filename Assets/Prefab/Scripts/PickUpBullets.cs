using UnityEngine;

public class PickUpBullets : MonoBehaviour
{
    private Inventory Inventory;
    [SerializeField] private int ammoPlus;
    [SerializeField] private string nameGun;
    private bool stop = false;
    void Awake()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if( other.gameObject.name=="Player(Clone)")
        {
            for( int i = 0; i < Inventory.slots.Length; i++)
            {
                foreach (Transform child in transform)
                {
                    if(child.name == nameGun)
                    {
                        child.GetComponent<Guns>().AmmoPlus(ammoPlus);
                        Destroy(gameObject);
                        stop = true;
                        break;
                    }
                    if (child.name != nameGun)
                    {
                        break;
                    }
                }

                if (stop)
                {
                    break;
                }
            }
        }
    }
}
// if(Inventory.isFull[i] == false)
// {
//     Inventory.isFull[i] = true;
//     Instantiate(slotButton, Inventory.slots[i].transform);
//     gameObject.SetActive(false);
//     GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PickUpGun(gameObject, i);
//     break;
// }