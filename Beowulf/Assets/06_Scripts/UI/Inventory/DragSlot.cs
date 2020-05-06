using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    static public DragSlot instance;

    public Slot dragSlot;
    [SerializeField]
    private Image itemImage;

    private void Start()
    {
        instance = this;
    }

    public void DragSetImage(Sprite _itemSprite)
    {
        itemImage.sprite = _itemSprite;
        SetColor(1);
    }

    public void SetColor(float _alpha)
    {
        Color color = itemImage.color;
        color.a = _alpha;
        itemImage.color = color;
    }
}
