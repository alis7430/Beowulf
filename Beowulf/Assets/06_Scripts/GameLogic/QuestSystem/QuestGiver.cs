using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestGiver : MonoBehaviour
{
    public Quest[] quests;

    private PlayerController pc;

    public GameObject questWindow;
    public TMP_Text titleText;
    public TMP_Text descriptionText;
    public TMP_Text experienceText;
    public TMP_Text goldText;

    public Image rewardItemIcon;

    public int questNumber;

    private NPCState npcState;

    private DialogueTrigger dialogueTrigger;


    private void Start()
    {
        pc = FindObjectOfType<PlayerController>();
        questWindow = GameObject.FindGameObjectWithTag("QuestWindow");

        titleText = questWindow.transform.GetChild(10).GetComponent<TMP_Text>();
        descriptionText = questWindow.transform.GetChild(11).GetComponent<TMP_Text>();
        experienceText = questWindow.transform.GetChild(12).GetComponent<TMP_Text>();
        goldText = questWindow.transform.GetChild(13).GetComponent<TMP_Text>();
        rewardItemIcon = questWindow.transform.GetChild(14).GetComponent<Image>();

        questNumber = 0;
        questWindow.SetActive(false);

        dialogueTrigger = this.transform.GetComponent<DialogueTrigger>();
        npcState = this.transform.GetComponent<NPCState>();
    }

    public void OpenQuestWindow()
    {
        //진행중인 퀘스트가 있다면 완료시까지 퀘스트 윈도우를 열 필요가 없다.
        if (npcState.GetState() == NPC_STATE.QUEST_PROGRESSING)
            return;

        for (int i = 0; i < quests.Length; i++)
        {
            // Giver가 가지고있는 퀘스트 중 클리어 하지않은 퀘스트가 있다면
            if (quests[i].isdone == false)
            {
                questWindow.SetActive(true);

                questWindow.GetComponent<RectTransform>().localPosition = Vector3.zero;

                titleText.text = quests[i].title;
                descriptionText.text = quests[i].description;
                experienceText.text = quests[i].experienceReward.ToString();
                goldText.text = quests[i].goldReward.ToString();

                if (quests[i].itemReward)
                    rewardItemIcon.sprite = quests[i].itemReward.icon;

                questNumber = i;
                QuestManager.instance.SetQuestGiver(this);
                UIManager.Instance.questWindowEnabled = true;
                break;
            }
        }
    }

    public void AcceptQuest()
    {
        //Debug.Log("퀘스트 수락");
        UIManager.Instance.questWindowEnabled = false;

        questWindow.SetActive(false);
        quests[questNumber].isActive = true;
        quests[questNumber].questGiver = this;

        
        QuestManager.instance.AddCurrentQuest(quests[questNumber]);
        QuestManager.instance.ClearQuestGiver();
    }

    public void RefuseQuest()
    {
        //Debug.Log("퀘스트 취소");
        UIManager.Instance.questWindowEnabled = false;
        questWindow.SetActive(false);
        QuestManager.instance.ClearQuestGiver();
    }

    public void NextDialogue()
    {
        if (dialogueTrigger != null)
            dialogueTrigger.NextDialogueNumber();
    }
    
    public void SetState(NPC_STATE state)
    {
        if(npcState != null)
            npcState.SetState(state);
    }
}
