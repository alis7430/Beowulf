using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int gold;
    public GameObject inventory;

    private int numOfSlots;
    private int enabledSlots;
    private GameObject[] slots;

    public GameObject slotHolder;

    private void Start()
    {
        numOfSlots = 42;
        slots = new GameObject[numOfSlots];
        for(int i = 0; i < numOfSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

            if (slots[i].GetComponent<Slot>().item == null)
                slots[i].GetComponent<Slot>().empty = true;
        }
    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            
            if(!item.pickedUp)
                AddItem(itemPickedUp, item);
        }
    }

    void AddItem(GameObject itemObject, Item item)
    {
        for(int i = 0; i< numOfSlots; i++)
        {
            if(slots[i].GetComponent<Slot>().empty)
            {
                //아이템을 슬롯에 추가합니다.
                itemObject.GetComponent<Item>().pickedUp = true;

                slots[i].GetComponent<Slot>().item = itemObject;
                slots[i].GetComponent<Slot>().icon = item.icon;

                itemObject.transform.parent = slots[i].transform;
                itemObject.SetActive(false);

                slots[i].GetComponent<Slot>().UpdateSlot();
                slots[i].GetComponent<Slot>().empty = false;

                return;
            }
        }
    }
}
