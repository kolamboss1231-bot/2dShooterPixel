using UnityEngine;

public class Soundshot : MonoBehaviour
{
    private AudioSource shot;

    [SerializeField] private float timeDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // void Awake()
    // {
    // shot.GetComponent<AudioSource>();
    
    // }
    void Start()
    {

        Destroy(gameObject,timeDestroy);
    }
}
    // Update is called once per frame}
