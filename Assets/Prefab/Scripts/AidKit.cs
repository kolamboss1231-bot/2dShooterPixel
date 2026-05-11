using System;
using UnityEngine;

public class AidKit : MonoBehaviour
{
   private GameObject AidText;
   [SerializeField] private int AidKutPlus = 1;
   [SerializeField] private float timeDestroy;

   private void Start()
   {
      AidText = GameObject.Find("AidKitText");
      Destroy(gameObject,timeDestroy);
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.CompareTag("Player"))
      {
         AidText.GetComponent<AidKitText>().AidKitGet(AidKutPlus);
         other.gameObject.GetComponent<Player>().AidkitGet(AidKutPlus);
         Destroy(gameObject);
      }
   }
}
