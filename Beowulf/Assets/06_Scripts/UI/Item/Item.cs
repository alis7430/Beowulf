using UnityEngine;

public enum ItemType
{
    HELMET,
    WEAPON,
    CLOTHES_TOP,
    CLOTHES_BOTTOM,
    SHOES,
    GLOVE,
    EARRING,
    PENDANT,
    ARMLET,
    RING,

}


public class Item : MonoBehaviour
{
    public int ID;

    public ItemType Type;
    public string description;

    public Sprite icon;

    public bool pickedUp;

    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    [HideInInspector]
    public bool equipped;

    [HideInInspector]
    public GameObject weapon;
    [HideInInspector]
    public GameObject weaponManager;

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
    }

    public void ItemUsage()
    {
        if(Type == ItemType.WEAPON)
        {
            this.transform.parent = weaponManager.transform;
            this.transform.localPosition = positionOffset;
            this.transform.localEulerAngles = rotationOffset;

            this.gameObject.SetActive(true);
            equipped = true;
        }
    }
}
