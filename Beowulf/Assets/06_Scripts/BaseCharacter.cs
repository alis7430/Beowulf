using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter
{
    #region Status value(캐릭터 스텟)

    private string name;        // 이름
    private string description; // 캐릭터 설명
    private float strength;     // 힘
    private float defense;      // 방어력
    private float dexterity;    // 민첩성
    private float intelligence; // 지능
    private float health;       // 체력

    public string NAME
    {
        get { return this.name; }
        set { this.name = value; }
    }
    public string DESCRIPTION
    {
        get { return this.description; }
        set { this.description = value; }
    }
    public float STRENGTH
    {
        get { return this.strength; }
        set { this.strength = value; }
    }
    public float DEFENSE
    {
        get { return this.defense; }
        set { this.defense = value; }
    }
    public float DEXTERITY
    {
        get { return this.dexterity; }
        set { this.dexterity = value; }
    }
    public float INTELLIGENCE
    {
        get { return this.intelligence; }
        set { this.intelligence = value; }
    }
    public float HEALTH
    {
        get { return this.health; }
        set { this.health = value; }
    }

    public BaseCharacter()
    {
        NAME = "no data";
        DESCRIPTION = "no description";
        STRENGTH = 0;
        DEFENSE = 0;
        DEXTERITY = 0;
        INTELLIGENCE = 0;
        HEALTH = 0;
    }


    #endregion

}
