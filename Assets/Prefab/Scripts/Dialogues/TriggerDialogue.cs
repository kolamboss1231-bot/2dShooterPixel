using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager dm;

    public void StartDialogue(Dialogue dial)
    {
        dm.StartDialogue(dial);
    }
}
