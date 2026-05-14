using UnityEngine;

public class GunShotGun : Guns
{
   public override void shotSpawn(GameObject bul,bool faceR)
   {
      if (faceR)
      {
             bulletOb = Instantiate(bul,new Vector2(transform.position.x + 0.5244f, transform.position.y + 0.0413f),Quaternion.Euler(0,0,downRazbros));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x + 0.5244f, transform.position.y + 0.0513f),Quaternion.Euler(0,0,downRazbros/2));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x + 0.5263f, transform.position.y + 0.075f),Quaternion.Euler(0,0,0));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x + 0.5244f, transform.position.y + 0.0882f),Quaternion.Euler(0,0,upRazbros/2));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x + 0.5244f, transform.position.y + 0.0982f),Quaternion.Euler(0,0,upRazbros));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             
      }      
      if(faceR == false)
      {
             bulletOb = Instantiate(bul,new Vector2(transform.position.x - 0.5244f, transform.position.y + 0.0413f),Quaternion.Euler(0,0,downRazbros));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x - 0.5244f, transform.position.y + 0.0513f),Quaternion.Euler(0,0,downRazbros/2));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x - 0.5263f, transform.position.y + 0.075f),Quaternion.Euler(0,0,0));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x - 0.5244f, transform.position.y + 0.0882f),Quaternion.Euler(0,0,upRazbros/2));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
             bulletOb = Instantiate(bul,new Vector2(transform.position.x - 0.5244f, transform.position.y + 0.0982f),Quaternion.Euler(0,0,upRazbros));
             bulletOb.GetComponent<Bullets>().GunPosition(parent);
      }
      Instantiate(shotAudio, transform.position,Quaternion.Euler(0,0,0));
   }
   
}
