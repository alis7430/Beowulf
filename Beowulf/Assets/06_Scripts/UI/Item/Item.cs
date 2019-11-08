using UnityEngine;

public enum ItemType
{
    EQUIPMENT,
    USEABLE,
    INGREDIENT,
    QUEST_ITEM,

}


public class Item : MonoBehaviour
{
    public int ID;

    public ItemType Type;
    public EquipType equipType;
    public string description;

    public Sprite icon;

    public bool pickedUp;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    [HideInInspector]
    public bool equipped;
    
    [HideInInspector]
    public GameObject weaponManager;
    [HideInInspector]
    public EquipManager equipManager;
    [HideInInspector]
    private PlayerController pc;


    public Slot hasThisSlot;

    public int Health;
    public int stamina;
    public int Damage;
    public int strength;
    public int defense;
    public int dexterity;
    public int intelligence;

    public void Start()
    {
        weaponManager = GameObject.FindWithTag("WeaponManager") as GameObject;
        equipManager = GameObject.FindObjectOfType<EquipManager>();
        pc = GameObject.FindObjectOfType<PlayerController>();
    }

    public void ItemUsage()
    {
        switch (Type)
        {
            case ItemType.EQUIPMENT:
                EquipItem();
                break;
            case ItemType.USEABLE:
                break;
            case ItemType.INGREDIENT:
                break;
            case ItemType.QUEST_ITEM:
                break;
            default:
                break;
        }
    }

    public void EquipItem()
    {
        if(equipType == EquipType.WEAPON)
        {
            if (!pc.GetWeapon())
                EquipWeapon();
            else
                SwapWeapon();
        }
        else
        {

        }
    }
    public void EquipWeapon()
    {
        this.transform.parent = weaponManager.transform;
        this.transform.localPosition = positionOffset;
        this.transform.localEulerAngles = rotationOffset;

        UIManager.Instance.WeaponImageUpdate(icon);
        
        equipped = true;

        hasThisSlot.checkBox.SetActive(true);

        AddItemStat2PlayerStatus();
        EventManager.Instance.PostNotification(EVENT_TYPE.UPDATE_UI, this, null);
    }
    public void SwapWeapon()
    {
        GameObject equipedObj = weaponManager.transform.GetChild(0).gameObject;
        Item equipedItem = equipedObj.GetComponent<Item>();

        if (equipedItem.ID == this.ID)
            return;

        equipedItem.RealeaseWeapon();

        EquipWeapon();

        if (pc.is_Player_Armed)
            this.gameObject.SetActive(true);
    }
    public void RealeaseWeapon()
    {
        this.transform.parent = hasThisSlot.transform;
        RemoveItemStat2PlayerStatus();

        UIManager.Instance.WeaponImageSetDefalut();

        equipped = false;

        this.gameObject.SetActive(false);
        hasThisSlot.checkBox.SetActive(false);
    }
    public void AddItemStat2PlayerStatus()
    {
        pc.MAXHEALTH += Health;
        pc.HEALTH += Health;
        pc.DAMAGE += Damage;
        pc.STRENGTH += strength;
        pc.DEFENSE += defense;
        pc.DEXTERITY += dexterity;
        pc.INTELLIGENCE += intelligence;
    }
    public void RemoveItemStat2PlayerStatus()
    {
        pc.MAXHEALTH -= Health;
        pc.HEALTH -= Health;
        pc.DAMAGE -= Damage;
        pc.STRENGTH -= strength;
        pc.DEFENSE -= defense;
        pc.DEXTERITY -= dexterity;
        pc.INTELLIGENCE -= intelligence;

    }
}
