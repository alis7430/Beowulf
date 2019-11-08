using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    #region C# properties
    //-----------------------------------------------------------
    // 인스턴스에 접근하기 위한 프로퍼티
    public static UIManager Instance
    {
        get
        {
            // 인스턴스가 없는 경우에 접근하려 하면 인스턴스를 할당해준다.
            if (!_instance)
            {
                _instance = FindObjectOfType(typeof(UIManager)) as UIManager;

                if (_instance == null)
                    Debug.Log("no Singleton obj");
            }
            return _instance;
        }
    }
    #endregion

    private static UIManager _instance;

    private PlayerController pc;

    //-----------------------------------------------------------
    //PlayerBaseInfo
    //-----------------------------------------------------------

    public Image Healthbar;
    public Image Stamina;
    public TMP_Text LevelText;
    //-----------------------------------------------------------
    //PlayerSkillInfo
    //-----------------------------------------------------------

    public Image EquipedWeapon;
    

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

    public Image PlayerEquipedWeapon;

    //-----------------------------------------------------------
    //Inventory
    //-----------------------------------------------------------
    public TMP_Text goldText;
    //-----------------------------------------------------------
    // UI Windows
    //-----------------------------------------------------------
    public GameObject inventory;
    public GameObject playerInfo;
    public GameObject questListWindow;

    [HideInInspector]
    public bool inventoryEnabled;
    [HideInInspector]
    public bool infoEnabled;
    [HideInInspector]
    public bool questListEnabled;
    [HideInInspector]
    public bool questWindowEnabled;

    public Sprite defaultImage;

    //-----------------------------------------------------------
    private void Awake()
    {
        // 인스턴스가 존재하지 않는 경우, 이 객체를 인스턴스로 만든다.
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // 씬이 전환되더라도 선언되었던 인스턴스가 파괴되지 않는다.
        }
        // 인스턴스가 존재하는 경우 새로생기는 인스턴스를 삭제한다.
        else
            DestroyImmediate(gameObject);
    }
    //-----------------------------------------------------------
    // Start is called before the first frame update
    void Start()
    {
        Transform status = GameObject.FindGameObjectWithTag("Status").transform;

        StatusInfo_HP = status.GetChild(0).GetComponent<TMP_Text>();
        StatusInfo_SP = status.GetChild(1).GetComponent<TMP_Text>();
        StatusInfo_ATK = status.GetChild(2).GetComponent<TMP_Text>();
        StatusInfo_DEF = status.GetChild(3).GetComponent<TMP_Text>();
        StatusInfo_STR = status.GetChild(4).GetComponent<TMP_Text>();
        StatusInfo_DEX = status.GetChild(5).GetComponent<TMP_Text>();
        StatusInfo_INT = status.GetChild(6).GetComponent<TMP_Text>();

        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_HIT, OnEvent);
        EventManager.Instance.AddListener(EVENT_TYPE.UPDATE_UI, OnEvent);

        inventory.GetComponent<RectTransform>().localPosition = Vector3.zero;
        playerInfo.GetComponent<RectTransform>().localPosition = Vector3.zero;
        questListWindow.GetComponent<RectTransform>().localPosition = Vector3.zero;

        EquipedWeapon.sprite = defaultImage;
        PlayerEquipedWeapon.sprite = defaultImage;
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
            questListEnabled = !questListEnabled;
        }
        if (questListEnabled)
            questListWindow.SetActive(true);
        else
            questListWindow.SetActive(false);

        //켰을 때 커서
        if (infoEnabled || inventoryEnabled || questListEnabled || questWindowEnabled)
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
        ExpAndLevelUpdate();
        goldText.text = Inventory.gold.ToString();
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
    private void ExpAndLevelUpdate()
    {
        LevelText.text = LevelManager.instance.LEVEL.ToString();
    }

    public void WeaponImageUpdate(Sprite icon)
    {
        EquipedWeapon.sprite = icon;
        PlayerEquipedWeapon.sprite = icon;
    }
    public void WeaponImageSetDefalut()
    {
        EquipedWeapon.sprite = defaultImage;
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
        if (questListEnabled)
            questListEnabled = false;
    }
}
