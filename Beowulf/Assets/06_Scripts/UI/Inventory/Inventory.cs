using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static int gold;
    public GameObject inventory;

    private int numOfSlots;
    private int enabledSlots;
    private GameObject[] slots;

    public GameObject slotHolder;

    public GameObject checkBox;

    private void Start()
    {
        gold = 0;
        numOfSlots = 42;
        slots = new GameObject[numOfSlots];

        inventory = UIManager.Instance.inventory;
        slotHolder = GameObject.FindGameObjectWithTag("SlotHolder");

        for(int i = 0; i < numOfSlots; i++)
        {
            slots[i] = slotHolder.transform.GetChild(i).gameObject;

            GameObject box =  GameObject.Instantiate(checkBox);
            slots[i].GetComponent<Slot>().checkBox = box;
            slots[i].GetComponent<Slot>().checkBox.transform.parent = slots[i].transform;
            slots[i].GetComponent<Slot>().checkBox.transform.localPosition = new Vector3(30.0f, -30.0f);
            slots[i].GetComponent<Slot>().checkBox.transform.localScale = Vector3.one;
            slots[i].GetComponent<Slot>().checkBox.SetActive(false);

            if (slots[i].GetComponent<Slot>().item == null)
                slots[i].GetComponent<Slot>().empty = true;
        }
    }
   
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();
            //Debug.Log(itemPickedUp.name);
            if (!item.pickedUp)
            {
                AddItem(itemPickedUp, item);
                SoundManager.instance.PlaySFX("GetItem");
            }
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

                slots[i].GetComponent<Slot>().Additem(itemObject, 1);
                itemObject.SetActive(false);

                EventManager.Instance.PostNotification(EVENT_TYPE.GET_ITEM, this, item.ID);
                return;
            }
        }
    }
}
