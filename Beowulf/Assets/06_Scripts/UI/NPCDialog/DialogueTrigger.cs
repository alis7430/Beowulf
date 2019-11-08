using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Animator ani;
    public Dialogue[] dialogue;

    public int dialogueNumber;
    public bool hasQuest;

    public QuestGiver questGiver;

    private void Start()
    {
        dialogueNumber = 0;

        if (this.transform.GetComponent<QuestGiver>() != null)
        {
            questGiver = this.transform.GetComponent<QuestGiver>();
            hasQuest = true;
        }
        else
        {
            questGiver = null;
            hasQuest = false;
        }
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue[dialogueNumber], this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ani.SetBool("IsPlayerNear", true);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                TriggerDialogue();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            ani.SetBool("IsPlayerNear", false);
        }
    }
    public void NextDialogueNumber()
    {
        if(dialogue.Length > dialogueNumber)
            dialogueNumber++;
    }

    public void OpenQuest()
    {
        questGiver.OpenQuestWindow();
    }
}
