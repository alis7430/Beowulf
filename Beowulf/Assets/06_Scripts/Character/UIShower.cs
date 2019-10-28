using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//-----------------------------------------------------------
// Scripts\Character\UIShower.cs
//
// 머리위에 이름과 체력바를 보이게하는 스크립트(월드좌표 UI)
// 1. 메인카메라의 월드-뷰포트 변환으로 좌표를 저장합니다.
// 2. 저장한 좌표를 뷰포트-월드 변환으로 UI카메라에 표시합니다.
// 3. BaseCharacter를 가진 오브젝트만이 사용 가능합니다.
//-----------------------------------------------------------

public class UIShower : MonoBehaviour
{
    //-----------------------------------------------------------
    #region Variables

    public Canvas canvas;       // UI를 표시할 캔버스 객체
    public TMP_Text Name;       // 이름을 표시할 텍스트 객체
    public Slider slider;       // 체력을 표시할 슬라이더

    public Camera mainCamera;   // 메인카메라
    public Camera UIcamera;     // UI카메라
        
    public Transform target;    // UI가 표시될 오브젝트 위치

    private BaseCharacter baseCharacter;

    #endregion
    // Start is called before the first frame update
    void Start()
    {
        canvas = transform.GetChild(2).GetComponent<Canvas>();
        baseCharacter = this.GetComponent<BaseCharacter>();
        mainCamera = Camera.main;
        UIcamera = GameObject.FindGameObjectWithTag("UICamera").GetComponent<Camera>();

        Name.text = baseCharacter.NAME;
        canvas.worldCamera = UIcamera;
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
