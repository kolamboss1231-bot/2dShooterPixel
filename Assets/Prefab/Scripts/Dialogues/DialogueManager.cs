using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Animator animBox;
    public Animator animStart;

    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dial)
    {
        animBox.SetBool("Open", true);
        animStart.SetBool("Open", false);
        
        nameText.text = dial.name;
        sentences.Clear();

        foreach (string sentence in dial.sentences)
        {
            sentences.Enqueue(sentence);
        }
        NextSentence();
    }

    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDial();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine((OutputNextText(sentence)));
    }

    IEnumerator OutputNextText(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDial()
    {
        animBox.SetBool("Open", false);
    }
}
