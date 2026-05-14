using UnityEngine;

public class PickUp : MonoBehaviour
{
    private Inventory Inventory;
    public GameObject slotButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnCollisionEnter2D (Collision2D other)
    {
        if( other.gameObject.CompareTag("Player"))
        {
            for( int i = 0; i < Inventory.slots.Length; i++)
            {
                if(Inventory.isFull[i] == false)
                {
                    Inventory.isFull[i] = true;
                    Instantiate(slotButton, Inventory.slots[i].transform);
                    gameObject.SetActive(false);
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().PickUpGun(gameObject, i);
                    break;
                }
            }
        }
    }
}