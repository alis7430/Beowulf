using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler, 
    IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject item;
    public int itemCount;
    public bool empty;

    public Transform slotIconGo;
    public Sprite icon;

    [HideInInspector]
    public Camera UIcamera;
    [HideInInspector]
    public RectTransform rectTransform;

    private Vector3 originPos;
    public Sprite defaultIcon;

    [HideInInspector]
    public GameObject checkBox;

    private void Start()
    {
        slotIconGo = transform.GetChild(0);
        UIcamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        rectTransform = this.GetComponent<RectTransform>();
        originPos = rectTransform.localPosition;

    }
    private void Update()
    {
    }
    public void UpdateSlot()
    {
        slotIconGo.GetComponent<Image>().sprite = icon;
    }
    public void ClearSlot()
    {
        this.item = null;
        this.icon = defaultIcon;
        this.empty = true;
        checkBox.SetActive(false);

        UpdateSlot();
    }
    public void Additem(GameObject itemObj, int count)
    {
        Item item = itemObj.GetComponent<Item>();
        if (!item.equipped)
        {
            itemObj.transform.parent = this.transform;
            checkBox.SetActive(false);
        }
        else if (item.equipped)
            checkBox.SetActive(true);
        
        item.pickedUp = true;
        item.hasThisSlot = this;

        this.item = itemObj;
        this.icon = item.icon;
        //SetItemCount(count);
        this.empty = false;

        UpdateSlot();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            if (item != null && eventData.clickCount == 2)
            {
                UseItem();
            }
        }
    }

    public void UseItem()
    {
        item.GetComponent<Item>().ItemUsage();
    }

    public virtual void OnBeginDrag(PointerEventData eventData)
    {
        if(item != null)
        {
            DragSlot.instance.dragSlot = this;
            DragSlot.instance.DragSetImage(icon);

            Vector3 pos = UIcamera.ScreenToWorldPoint(eventData.position);
            DragSlot.instance.transform.position = new Vector3(pos.x, pos.y);
        }
    }

    public virtual void OnDrag(PointerEventData eventData)
    {
        if (item != null)
        {
            Vector3 pos = UIcamera.ScreenToWorldPoint(eventData.position);
            DragSlot.instance.transform.position = new Vector3(pos.x, pos.y);
        }
    }

    public virtual void OnEndDrag(PointerEventData eventData)
    {
        DragSlot.instance.SetColor(0);
        DragSlot.instance.dragSlot = null;
    }

    public virtual void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.instance.dragSlot != null)
            ChangeSlot();
    }
    protected virtual void ChangeSlot()
    {
        GameObject tempItem = item;
        int tempItemCount = itemCount;

        Additem(DragSlot.instance.dragSlot.item, DragSlot.instance.dragSlot.itemCount);

        if(tempItem != null)
        {
            DragSlot.instance.dragSlot.Additem(tempItem, tempItemCount);
        }
        else
        {
            DragSlot.instance.dragSlot.ClearSlot();
        }
    }
    virtual public void ChangeSlot(Slot other)
    {
        GameObject tempItem = item;
        int tempItemCount = itemCount;

        Additem(other.item, other.itemCount);
        other.Additem(tempItem, tempItemCount);
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item != null && ItemDescription.instance.is_running == false)
        {
            Item i = item.GetComponent<Item>();

            // 아이템 데이터를 집어넣음
            ItemDescription.instance.DataInput(item.name, i.description,
                i.Health, i.stamina, i.Damage, i.strength, i.defense, i.dexterity, i.intelligence);

            // 오브젝트를 액티브하고 실행중이라 알림
            ItemDescription.instance.gameObject.SetActive(true);
            ItemDescription.instance.is_running = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemDescription.instance.Clear();
        ItemDescription.instance.is_running = false;
        ItemDescription.instance.gameObject.SetActive(false);
    }
}
