using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------
// Scripts\AI\Sense.cs
//
// 센서 시스템의 인터페이스로 다른 커스텀 센서는 이를 상속받아 구현합니다.
// 이 스크립트는 어떤 오브젝트에도 연결하지 마십시오.
// 기본 속성으로 감시 주기 및 감시 대상 정보를 포함하고 있습니다. 
// 2개의 가상 메소드 Initialize와 UpdateSense를 정의하고 있습니다.
//-----------------------------------------------------------
public class Sense : MonoBehaviour
{
    #region variables
    //-----------------------------------------------------------
    public bool isDebug = true;
    public Aspect.aspect aspect = Aspect.aspect.ENEMY;
    public float detectionRate = 1.0f;

    protected float elapsedTime = 0.0f;
    //-----------------------------------------------------------
    #endregion

    #region methods
    //-----------------------------------------------------------
    protected virtual void Initialize() { }
    protected virtual void UpdateSense() { }

    //-----------------------------------------------------------
    private void Start()
    {
        elapsedTime = 0.0f;
        Initialize();
    }
    //-----------------------------------------------------------
    private void Update()
    {
        UpdateSense();
    }
    //-----------------------------------------------------------
    #endregion
}
