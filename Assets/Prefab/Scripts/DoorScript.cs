using UnityEngine;

public class DoorScript : MonoBehaviour
{
    [SerializeField] private GameObject doorOpen;
    [SerializeField] private GameObject doorClose;
    [SerializeField] private BoxCollider2D box;
    private bool isOpen;

    private void DoorOpen()
    {
        Debug.Log("DoorOpen");
        isOpen = true;
        doorOpen.SetActive(true);
        doorClose.SetActive(false);
        box.enabled = false;
    }
    
    private void DoorClose()
    {
        Debug.Log("DoorClose");
        isOpen = false;
        doorOpen.SetActive(false);
        doorClose.SetActive(true);
        box.enabled = true;
    }

    public void DoorManager()
    {
        if (isOpen == false)
        {
            DoorOpen();
            
        }
        
        else if(isOpen)
        {
            DoorClose();
        }
    }
}
