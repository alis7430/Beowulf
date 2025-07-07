using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public int LEVEL
    {
        get
        {
            return level;
        }
        set { level = value; }
    }
    public int EXPERIENCE
    {
        get
        {
            return currentExperience;
        }
        set { currentExperience = value; }
    }

    [Range(1, MAXLEVEL)]
    private int level;
    private int currentExperience;

    public int[] needExperience;

    public const int MAXLEVEL = 99;


    private void Start()
    {
        LEVEL = 1;
        needExperience = new int[MAXLEVEL];
        needExperience[0] = 500;
        for (int i = 1; i < MAXLEVEL; i++)
        {
            needExperience[i] = needExperience[i - 1] + 500;
        }
        instance = this;
    }
    
    public void AddExperience(int value)
    {
        EXPERIENCE += value;
        int curlevel = LEVEL;
        if(EXPERIENCE >= needExperience[curlevel - 1])
        {
            LevelUp();
        }
    }

    public void LevelUp()
    {
        LEVEL += 1;

        //능력치 상승
        //UpgradePlayerStatus
    }

}
