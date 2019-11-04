using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    private PlayerController pc;

    //-----------------------------------------------------------
    //PlayerBaseInfo
    //-----------------------------------------------------------

    public Image Healthbar;
    public Image Stamina;

    //-----------------------------------------------------------
    //PlayerInfo
    //-----------------------------------------------------------
    public TMP_Text StatusInfo_HP;
    public TMP_Text StatusInfo_SP;
    public TMP_Text StatusInfo_ATK;
    public TMP_Text StatusInfo_DEF;
    public TMP_Text StatusInfo_STR;
    public TMP_Text StatusInfo_DEX;
    public TMP_Text StatusInfo_INT;
    //-----------------------------------------------------------
    // UI Windows
    //-----------------------------------------------------------
    public GameObject inventory;
    public GameObject playerInfo;
    public GameObject questWindow;

    [HideInInspector]
    public bool inventoryEnabled;
    [HideInInspector]
    public bool infoEnabled;
    [HideInInspector]
    public bool questEnabled;
    //-----------------------------------------------------------

    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_HIT, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.UPDATE_UI, OnEvent);


        EventManager.Instance.PostNotification(EVENT_TYPE.UPDATE_UI, this, null);

        inventory.GetComponent<RectTransform>().localPosition = Vector3.zero;
        playerInfo.GetComponent<RectTransform>().localPosition = Vector3.zero;
        questWindow.GetComponent<RectTransform>().localPosition = Vector3.zero;
    }

    protected virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.PLAYER_HIT:
                OnHealthBarChange();
                break;
            case EVENT_TYPE.UPDATE_UI:
                OnUpdateUI();
                break;
            default:
                break;
        }
    }
    private void Update()
    {
        //인벤토리는 I키로 열 수 있다.
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryEnabled = !inventoryEnabled;
        }
        if (inventoryEnabled)
            inventory.SetActive(true);
        else
            inventory.SetActive(false);

        //플레이어 정보는 P키로 열 수 있다.
        if (Input.GetKeyDown(KeyCode.P))
        {
            infoEnabled = !infoEnabled;
        }
        if (infoEnabled)
            playerInfo.SetActive(true);
        else
            playerInfo.SetActive(false);

        //퀘스트 창은 Q키로 열 수 있다.
        if (Input.GetKeyDown(KeyCode.Q))
        {
            questEnabled = !questEnabled;
        }
        if (questEnabled)
            questWindow.SetActive(true);
        else
            questWindow.SetActive(false);

        //켰을 때 커서
        if (infoEnabled || inventoryEnabled || questEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
    private void OnHealthBarChange()
    {
        if (pc.MAXHEALTH == 0)
            return;

        Healthbar.fillAmount = (float)pc.HEALTH / (float)pc.MAXHEALTH;
    }

    private void OnUpdateUI()
    {
        PlayerStatusUpdate();
    }

    private void PlayerStatusUpdate()
    {
        StatusInfo_HP.text = pc.HEALTH.ToString() + " / " + pc.MAXHEALTH.ToString();
        StatusInfo_SP.text = pc.STAMINA.ToString() + " / " + pc.MAXSTAMINA.ToString();
        StatusInfo_ATK.text = pc.DAMAGE.ToString();
        StatusInfo_DEF.text = pc.DEFENSE.ToString();
        StatusInfo_STR.text = pc.STRENGTH.ToString();
        StatusInfo_DEX.text = pc.DEXTERITY.ToString();
        StatusInfo_INT.text = pc.DEXTERITY.ToString();
    }

    public void CloseInventory()
    {
        if (inventoryEnabled)
            inventoryEnabled = false;
    }

    public void ClosePlayerInfo()
    {
        if (infoEnabled)
            infoEnabled = false;
    }
    
    public void CloseQuestWindow()
    {
        if (questEnabled)
            questEnabled = false;
    }
}
