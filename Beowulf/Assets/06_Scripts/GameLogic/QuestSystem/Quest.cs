using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Quest
{
    public int ID;
    public bool isActive;

    public string title;
    [TextArea(3,10)]
    public string description;

    public int experienceReward;
    public int goldReward;

    public Item itemReward;
    public QuestGoal goal;
    
    public bool isdone = false;

    public QuestGiver questGiver;

    public void Complete()
    {
        isActive = false;
        isdone = true;
        Debug.Log(title + " was completed!");
        GetReward();
    }

    public void GetReward()
    {
        LevelManager.instance.AddExperience(experienceReward);
        Inventory.gold += goldReward;
        EventManager.Instance.PostNotification(EVENT_TYPE.UPDATE_UI, questGiver, null);
    }

    public Quest()
    {
        ID = -1;
        isActive = false;
        title = "NONE";
        description = "NONE";
        experienceReward = 0;
        goldReward = 0;

        itemReward = null;
        questGiver = null;
        goal = new QuestGoal();
    }
}
