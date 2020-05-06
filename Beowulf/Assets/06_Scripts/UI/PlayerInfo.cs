using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInfo : MonoBehaviour
{
    //PlayerInfo의 능력치 리스트의 부모트랜스폼
    public Transform status;

    //장착한 아이템 이미지
    private Image EquipedWeapon;

    //플레이어 능력치 텍스트
    private TMP_Text StatusInfo_HP;
    private TMP_Text StatusInfo_SP;
    private TMP_Text StatusInfo_ATK;
    private TMP_Text StatusInfo_DEF;
    private TMP_Text StatusInfo_STR;
    private TMP_Text StatusInfo_DEX;
    private TMP_Text StatusInfo_INT;

    

    private PlayerController pc;

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;

        EventManager.Instance.AddListener(EVENT_TYPE.EQUIP_ITEM, OnEvent);

        StatusInfo_HP = status.GetChild(0).GetComponent<TMP_Text>();
        StatusInfo_SP = status.GetChild(1).GetComponent<TMP_Text>();
        StatusInfo_ATK = status.GetChild(2).GetComponent<TMP_Text>();
        StatusInfo_DEF = status.GetChild(3).GetComponent<TMP_Text>();
        StatusInfo_STR = status.GetChild(4).GetComponent<TMP_Text>();
        StatusInfo_DEX = status.GetChild(5).GetComponent<TMP_Text>();
        StatusInfo_INT = status.GetChild(6).GetComponent<TMP_Text>();

        PlayerStatusUpdate();
    }

    //param은 Item클래스로 받아야 한다.
    protected virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.EQUIP_ITEM:
                break;
            default:
                break;
        }
    }

    public void PlayerStatusUpdate()
    {
        StatusInfo_HP.text = pc.HEALTH.ToString() + " / " + pc.MAXHEALTH.ToString();
        StatusInfo_SP.text = pc.STAMINA.ToString() + " / " + pc.MAXSTAMINA.ToString();
        StatusInfo_ATK.text = pc.DAMAGE.ToString();
        StatusInfo_DEF.text = pc.DEFENSE.ToString();
        StatusInfo_STR.text = pc.STRENGTH.ToString();
        StatusInfo_DEX.text = pc.DEXTERITY.ToString();
        StatusInfo_INT.text = pc.DEXTERITY.ToString();
    }

    private void updateItemSlot(Item item)
    {

    }
}
