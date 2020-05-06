using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ItemDescription : MonoBehaviour
{
    static public ItemDescription instance;

    public TMP_Text Name;
    public TMP_Text Description;
    public TMP_Text EquipStatus;
    
    public bool is_running;

    private Camera UIcamera;

    private void Start()
    {
        UIcamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
        instance = this;
        is_running = false;
        this.gameObject.SetActive(false);
    }
    private void Update()
    {
        if(is_running)
        {
            Vector3 pos = UIcamera.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector3(pos.x, pos.y);
        }
    }

    public void DataInput(string name, string description, params int[] args)
    {
        Name.text = name;
        Description.text = description;

        for(int i = 0; i<args.Length; i++)
        {
            EquipStatus.text += args[i].ToString() + "\r\n";
        }
    }

    public void Clear()
    {
        Name.text = "";
        Description.text = "";
        EquipStatus.text = "";
    }
}
