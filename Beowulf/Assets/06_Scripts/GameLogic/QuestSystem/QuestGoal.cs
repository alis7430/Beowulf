using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestGoalType
{
    NONE,
    KILL,
    GATHERING,

}
[System.Serializable]
public class QuestGoal
{
    public QuestGoalType goalType;

    public int requireAmount;
    public int currentAmount;

    public Item needItem;
    public BaseCharacter enemy;     //수정 요구

    public bool IsReached()
    {
        return (currentAmount >= requireAmount);
    }

    public void EnemyKilled()
    {
        if(goalType == QuestGoalType.KILL)
            currentAmount++;
    }
    public void ItemCollected()
    {
        if (goalType == QuestGoalType.GATHERING)
            currentAmount++;
    }
    public QuestGoal()
    {
        goalType = QuestGoalType.NONE;
        requireAmount = 0;
        currentAmount = 0;
        needItem = null;
    }

}
