using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyUIShower : MonoBehaviour
{
    public TMP_Text Name;       // 이름을 표시할 텍스트 객체
    public Slider slider;       // 체력을 표시할 슬라이더

    public Camera mainCamera;   // 메인카메라
    public Camera UIcamera;     // UI카메라

    public Transform target;    // UI가 표시될 오브젝트 위치

    private BaseCharacter baseCharacter;
    // Start is called before the first frame update
    void Start()
    {
        baseCharacter = this.GetComponent<BaseCharacter>();
        mainCamera = Camera.main;
        UIcamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 UIPosition = mainCamera.WorldToViewportPoint(target.position);
        Vector3 screenPos = UIcamera.ViewportToWorldPoint(UIPosition);

        slider.transform.position = screenPos;
        Name.transform.position = screenPos + new Vector3(0, 0.3f, 0);

        slider.value = (float)baseCharacter.HEALTH / (float)baseCharacter.MAXHEALTH;
    }
}
