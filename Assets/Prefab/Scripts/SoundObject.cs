using UnityEngine;

public class SoundObject : MonoBehaviour
{

    [SerializeField] private float timeDestroy;
    void Start()
    {
        Destroy(gameObject,timeDestroy);
    }
}

