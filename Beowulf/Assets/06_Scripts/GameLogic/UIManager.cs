using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIManager : MonoBehaviour
{
    private PlayerController pc;

    public Image Healthbar;
    public Image Stamina;


    // Start is called before the first frame update
    void Start()
    {
        pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>() as PlayerController;
        EventManager.Instance.AddListener(EVENT_TYPE.PLAYER_HIT, OnEvent);
    }

    protected virtual void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.PLAYER_HIT:
                OnHealthBarChange();
                break;
            default:
                break;
        }
    }

    private void OnHealthBarChange()
    {
        if (pc.MAXHEALTH == 0)
            return;

        Healthbar.fillAmount = (float)pc.HEALTH / (float)pc.MAXHEALTH;
    }
}
