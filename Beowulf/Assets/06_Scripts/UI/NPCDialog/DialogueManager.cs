using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public GameObject Window;
    public TMP_Text NameText;
    public TMP_Text DialogueText;
    public Animator ani;

    public Queue<string> sentences;

    private PlayerController pc;

    public DialogueTrigger currentDialogueTrigger;

    public bool is_running;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        is_running = false;

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;
    }

    public void StartDialogue(Dialogue dialogue, DialogueTrigger dialogueTrigger)
    {
        is_running = true;
        ani.SetBool("IsOpen", true);
        pc.next_step = PlayerController.STEP.CONVERSATION;

        currentDialogueTrigger = dialogueTrigger;

        NameText.text = dialogue.name;
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    void EndDialogue()
    {
        is_running = false;
        ani.SetBool("IsOpen", false);
        pc.next_step = PlayerController.STEP.IDLE;

        if(currentDialogueTrigger.hasQuest)
        {
            currentDialogueTrigger.OpenQuest();
        }

        currentDialogueTrigger = null;
    }

    IEnumerator TypeSentence(string sentence)
    {
        DialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            DialogueText.text += letter;
            yield return null;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(is_running)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(1))
            {
                DisplayNextSentence();
            }
        }
    }
}
