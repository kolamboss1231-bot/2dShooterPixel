using UnityEngine;

[System.Serializable]public class Character : MonoBehaviour
{
    [SerializeField]protected float maxHp;
    [SerializeField]protected int speedLegs;
    
    protected bool lifePl=true;
    protected bool animMoves = false;
    
    protected Rigidbody2D rb;

    [System.NonSerialized]public bool faceRight = true;
}
