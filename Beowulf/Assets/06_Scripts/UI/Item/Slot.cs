using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    public GameObject item;

    public bool empty;

    public Transform slotIconGo;
    public Sprite icon;

    private void Start()
    {
        slotIconGo = transform.GetChild(0);
    }
    public void UpdateSlot()
    {
        slotIconGo.GetComponent<Image>().sprite = icon;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        UseItem();
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }
}
