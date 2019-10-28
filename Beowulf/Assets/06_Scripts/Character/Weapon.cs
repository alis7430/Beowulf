using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------------------------
// Scripts\Character\Weapon.cs
// BaseCharacter의 무기에 부착하는 스크립트입니다.
// 스크립트 부착시 Trigger 이벤트가 일어나면 baseCharacter 스크립트의 함수를 호출합니다.
//-----------------------------------------------------------


public class Weapon : MonoBehaviour
{
    //-----------------------------------------------------------
    BaseCharacter bc;

    //-----------------------------------------------------------
    void Start()
    {
        bc = transform.parent.parent.GetComponent<BaseCharacter>();
    }
    //-----------------------------------------------------------
    // Update is called once per frame
    void Update()
    {
        
    }
    //-----------------------------------------------------------
    private void OnTriggerEnter(Collider other)
    {
        bc.CollisionDetected(this, other);
    }
    //-----------------------------------------------------------
}
