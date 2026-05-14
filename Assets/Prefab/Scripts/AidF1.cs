using System;
using UnityEngine;

public class AidF1 : MonoBehaviour
{
   private GameObject Text;
   
   [SerializeField] private float timeDestroy;

   private void Start()
   {
      Destroy(gameObject,timeDestroy);
   }

   
}
