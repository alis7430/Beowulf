using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipType
{
    HELMET,
    ARMOR,
    PANTS,
    BOOTS,
    WEAPON,
    EARRING,
    PENDANT,
    RINGR,
    RINGL,
    ARMLET,
}
public class EquipManager : MonoBehaviour
{
    public GameObject PlayerInfo;

    public GameObject slotHolder;
    public GameObject[] slots;
    private int numOfSlots;

    public Dictionary<EquipType, GameObject> EquipList;

    // Start is called before the first frame update
    void Start()
    {
        numOfSlots = 10;
        EquipList = new Dictionary<EquipType, GameObject>();

        slots = new GameObject[numOfSlots];
        for(int i = 0; i < numOfSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slots[i].GetComponent<Slot>().item == null)
                slots[i].GetComponent<Slot>().empty = true;
            Debug.Log(slots[i].name);
        }

        EquipList.Add(EquipType.HELMET, slots[0]);
        EquipList.Add(EquipType.ARMOR, slots[1]);
        EquipList.Add(EquipType.PANTS, slots[2]);
        EquipList.Add(EquipType.BOOTS, slots[3]);
        EquipList.Add(EquipType.WEAPON, slots[4]);
        EquipList.Add(EquipType.EARRING, slots[5]);
        EquipList.Add(EquipType.PENDANT, slots[6]);
        EquipList.Add(EquipType.RINGR, slots[7]);
        EquipList.Add(EquipType.RINGL, slots[8]);
        EquipList.Add(EquipType.ARMLET, slots[9]);
    }

    public bool is_Equiped(EquipType type)
    {
        if (EquipList[type].GetComponent<Slot>().empty)
            return false;
        else
            return true;
    }
    

}
