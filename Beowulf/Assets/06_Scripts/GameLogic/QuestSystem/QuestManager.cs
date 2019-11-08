using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;

    //public List<Quest> questList = new List<Quest>();
    public Dictionary<int, Quest> currentQuest;

    private QuestGiver curQuestGiver;
    public QuestInfoSlot[] questInfoSlots;
    private int ContentSlotsNum;

    private GameObject infoSlotObj;

    private void Awake()
    {
        if(instance ==  null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }
    private void Start()
    {
        currentQuest = new Dictionary<int, Quest>();

        ContentSlotsNum = 20;

        GameObject QuestListContent = GameObject.FindGameObjectWithTag("QuestListContent") as GameObject;
    

        questInfoSlots = new QuestInfoSlot[ContentSlotsNum];
        for (int i = 0; i < ContentSlotsNum; i++)
        {
            questInfoSlots[i] = QuestListContent.transform.GetChild(i).GetComponent<QuestInfoSlot>();
        }

        EventManager.Instance.AddListener(EVENT_TYPE.GET_ITEM, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.ENEMY_KILLED, OnEvent);
    }

    private void Update()
    {

    }

    public void AddCurrentQuest(Quest quest)
    {
        if (quest != null)
        {
            currentQuest.Add(quest.ID, quest);
            AddQuestListContents(quest);
            CheckQuestStartEvent(quest.ID);
        }
    }

    public void AcceptQuest()
    {
        if (curQuestGiver != null)
            curQuestGiver.AcceptQuest();
    }
    public void RefuseQuest()
    {
        if (curQuestGiver != null)
            curQuestGiver.RefuseQuest();
    }
    public void SetQuestGiver(QuestGiver giver)
    {
        if(giver != null)
            curQuestGiver = giver;
    }
    public QuestGiver GetQuestGiver(int questID)
    {
        if (currentQuest.ContainsKey(questID))
            return currentQuest[questID].questGiver;
        else
            return null;
    }
    public void ClearQuestGiver()
    {
        curQuestGiver = null;
    }

    public void AddQuestListContents(Quest quest)
    {
        for(int i = 0; i < ContentSlotsNum; i++)
        {
            if (questInfoSlots[i].is_Using == false)
            {
                questInfoSlots[i].AddInfo(quest);
                return;
            }
        }
        //만약 꽉차있다면 슬롯을 더 생성한다.(오브젝트 풀링)
        StartCoroutine(InstanceNewQuestInfo(quest));
    }

    public void InstancingGoalItem(GameObject obj, Vector3 pos)
    {
        GameObject goalObj = GameObject.Instantiate(obj);
        goalObj.transform.position = pos;

    }
    public void CompleteQuest(Quest quest)
    {
        quest.Complete();
        GetQuestInfoSlotFromID(quest.ID).Complete();

        CheckQuestCompleteEvent(quest.ID);
        currentQuest.Remove(quest.ID);
    }

    public QuestInfoSlot GetQuestInfoSlotFromID(int questID)
    {
        for (int i = 0; i < ContentSlotsNum; i++)
        {
            if (questInfoSlots[i].QuestID == questID)
                return questInfoSlots[i];
        }
        return null;
    }

    //오브젝트풀링 방식으로 퀘스트 정보를 추가함
    private IEnumerator InstanceNewQuestInfo(Quest quest)
    {
        GameObject QuestListContent = GameObject.FindGameObjectWithTag("QuestListContent") as GameObject;

        for (int i = 0; i < 20; i++)
        {
            GameObject obj = GameObject.Instantiate(infoSlotObj);

            obj.transform.parent = QuestListContent.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
        }

        ContentSlotsNum += 20;

        yield return null;

        questInfoSlots = new QuestInfoSlot[ContentSlotsNum];
        for (int i = 0; i < ContentSlotsNum; i++)
        {
            questInfoSlots[i] = QuestListContent.transform.GetChild(i).GetComponent<QuestInfoSlot>();
        }
        yield return null;

        //못 넣은 퀘스트를 다시 추가한다.
        AddQuestListContents(quest);
    }
    private void CheckQuestStartEvent(int questId)
    {
        switch(questId)
        {
            case 101:
                Vector3 pos = new Vector3(36f, 59f, 40f);
                InstancingGoalItem(currentQuest[questId].goal.needItem.gameObject, pos);
                break;
            case 102:
                CompleteQuest(currentQuest[questId]);
                break;
            case 103:
                GameObject dummys = GameObject.Find("Dummys");
                dummys.transform.GetChild(0).gameObject.SetActive(true);
                dummys.transform.GetChild(1).gameObject.SetActive(true);
                dummys.transform.GetChild(2).gameObject.SetActive(true);
                break;
            case 104:
                currentQuest[questId].questGiver.NextDialogue();
                break;
            default:
                break;
        }
    }
    private void CheckQuestCompleteEvent(int questId)
    {
        switch(questId)
        {
            case 101:
                currentQuest[questId].questGiver.NextDialogue();
                break;
            case 102:
                currentQuest[questId].questGiver.NextDialogue();
                break;
            case 103:
                currentQuest[questId].questGiver.NextDialogue();
                break;
            case 104:
                break;
            default:
                break;
        }
    }

    // 아이템을 얻었을 때 퀘스트의 진행 상황을 확인
    private void CheckQuestItem(int itemID)
    {
        List<Quest> qList = new List<Quest>(currentQuest.Values);
        foreach(Quest quest in qList)             //수행중인 퀘스트 순회
        {
            if (quest != null)
            {
                if (quest.goal.goalType == QuestGoalType.GATHERING) //아이템을 얻는 퀘스트 일때
                {
                    if (quest.goal.needItem.ID == itemID)           //필요한 아이템인지 확인
                    {
                        quest.goal.ItemCollected();                 //맞으면 획득했다고 알림

                        //업데이트 사항을 반영
                        QuestInfoSlot slot = GetQuestInfoSlotFromID(quest.ID);
                        slot.UpdateInfo(quest.goal.currentAmount, quest.goal.requireAmount);

                        if (quest.goal.IsReached())                  //만약 요구치까지 획득하면
                        {
                            CompleteQuest(quest);                    //퀘스트 완료
                        }
                    }
                }
            }
        }
    }

    private void CheckKilledEnemy(int enemyID)
    {
        List<Quest> qList = new List<Quest>(currentQuest.Values);
        foreach (Quest quest in qList)             //수행중인 퀘스트 순회
        {
            if (quest != null)
            {
                if (quest.goal.goalType == QuestGoalType.KILL) //적을 죽이는 퀘스트일때
                {
                    if (quest.goal.enemy.ID == enemyID)           //죽인 적이 맞는지 확인
                    {
                        quest.goal.EnemyKilled();                 //맞으면 죽였다고 알림

                        //업데이트 사항을 반영
                        QuestInfoSlot slot = GetQuestInfoSlotFromID(quest.ID);
                        slot.UpdateInfo(quest.goal.currentAmount, quest.goal.requireAmount);

                        if (quest.goal.IsReached())                  //만약 요구치까지 획득하면
                        {
                            CompleteQuest(quest);                    //퀘스트 완료
                        }
                    }
                }
            }
        }
    }

    //-------------------------------------------------------
    //Called when events happen
    protected virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.GET_ITEM:
                CheckQuestItem((int)Param);
                break;
            case EVENT_TYPE.ENEMY_KILLED:
                CheckKilledEnemy((int)Param);
                break;
            default:
                break;
        }
    }
}
