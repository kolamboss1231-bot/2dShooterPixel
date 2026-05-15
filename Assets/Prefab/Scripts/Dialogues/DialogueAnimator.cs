using System;
using UnityEngine;

public class DialogueAnimator : MonoBehaviour
{
    public static Action startDialogue;
    
    public Dialogue dialogue;
    public Animator startAnim;
    public DialogueManager dm;

    private bool startD;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && startD == false)
        {
            startAnim.SetBool("Open", true);
            
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            startAnim.SetBool("Open", false);
            dm.EndDial();
        }
    }
    public void StartDialogue(Dialogue dial)
    {
        dm.StartDialogue(dial);
    }

    public void StartDialogueOnOPlayer()
    {
        if (startD == false)
        {
            StartDialogue(dialogue);
            startD = true;
        }
       
    }
}
