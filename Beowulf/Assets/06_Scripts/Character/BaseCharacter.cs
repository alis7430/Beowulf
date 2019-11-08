using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------
//Scripts\Character\BaseCharacter.cs
//
// 캐릭터 스탯 데이터를 저장하는 클래스
// 이벤트를 사용하기 위해 OnEvent를 상속할 수 있다.
//-----------------------------------------------------------
public class BaseCharacter : MonoBehaviour
{
    public int ID;

    #region Status value(캐릭터 스텟)
    //-----------------------------------------------------------
    [SerializeField]
    private string name;        // 이름
    [SerializeField]
    private string description; // 캐릭터 설명
    [SerializeField]
    private int health;       // 체력
    [SerializeField]
    private int maxHealth;    // 최대 체력
    [SerializeField]
    private int stamina;      // 스태미너
    [SerializeField]          
    private int maxStamina;   // 최대 스태미너
    [SerializeField]
    private int damage;       // 데미지
    [SerializeField]
    private int strength;     // 힘
    [SerializeField]
    private int defense;      // 방어력
    [SerializeField]
    private int dexterity;    // 민첩성
    [SerializeField]
    private int intelligence; // 지능

    //-----------------------------------------------------------
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
    public int DAMAGE
    {
        get { return this.damage; }
        set { this.damage = value; }
    }
    public int STRENGTH
    {
        get { return this.strength; }
        set { this.strength = value; }
    }
    public int DEFENSE
    {
        get { return this.defense; }
        set { this.defense = value; }
    }
    public int DEXTERITY
    {
        get { return this.dexterity; }
        set { this.dexterity = value; }
    }
    public int INTELLIGENCE
    {
        get { return this.intelligence; }
        set { this.intelligence = value; }
    }
    public int STAMINA
    {
        get { return this.stamina; }
        set
        {
            if (value <= 0)
                this.stamina = 0;
            else
                this.stamina = value;
        }
    }
    public int MAXSTAMINA
    {
        get { return this.maxStamina; }
        set
        {
            if (value <= 0)
                this.maxStamina = 1;
            else
                this.maxStamina = value;
        }
    }
    public int HEALTH
    {
        get { return this.health; }
        set
        {
            if (value <= 0)
                this.health = 0;
            else
                this.health = value;
        }
    }
    public int MAXHEALTH
    {
        get { return this.maxHealth; }
        set
        {
            if (value <= 0)
                this.maxHealth = 1;
            else
                this.maxHealth = value;
        }
    }
    //-----------------------------------------------------------
    #endregion

    //-----------------------------------------------------------
    // BaseCharacter에 데이터가 없으면 참조가 불가능하므로 생성자를 이용해 초기화 시켜준다.
    //public BaseCharacter()
    //{
    //    NAME = "no data";
    //    DESCRIPTION = "no description";

    //    HEALTH = 0;
    //    MAXHEALTH = 0;
    //    DAMAGE = 0;

    //    STRENGTH = 0;
    //    DEFENSE = 0;
    //    DEXTERITY = 0;
    //    INTELLIGENCE = 0;
    //}
    //-----------------------------------------------------------
    private void Start()
    {
        Initialize();
    }
    #region methods
    //-----------------------------------------------------------
    // Use this for initialization
    protected virtual void Initialize()
    {
        MAXHEALTH = HEALTH;
        EventManager.Instance.AddListener(EVENT_TYPE.DEAD, OnEvent);
    }
    //-------------------------------------------------------
    //Called when events happen
    protected virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.DEAD:
                //OnDead(Sender, (int)Param);
                break;
            default:
                break;
        }
    }

    //-------------------------------------------------------
    //데미지를 입었을 때 호출
    protected virtual void OnAttacked(object param)
    {
        int damage = (int)param;

        if (damage < 0)
            return;
        HEALTH -= damage;

        Debug.Log(string.Format("체력 : {0}", HEALTH));

        if (HEALTH <= 0)
        {
            OnDead();
        }
    }

    //-------------------------------------------------------
    //체력이 0이 되었을 때 호출
    protected virtual void OnDead()
    {

    }
    //-------------------------------------------------------
    //공격하는 클래스의 OnTriggerEnter에서 호출한다.
    public virtual void CollisionDetected(Weapon weapon, Collider other)
    {
    }
    //-------------------------------------------------------
    #endregion
}
