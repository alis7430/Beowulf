using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------
// Scripts\AI\Aspect.cs
//
// AI 캐릭터가 무언가 발견했을 때 구분하게 해주는 목록 지정 클래스
// 열거체로만 이루어져 있다. 컴포넌트 방식으로 추가해서 aspectName을 설정하십시오.
//-----------------------------------------------------------
public class Aspect : MonoBehaviour
{
    public enum aspect
    {
        PLAYER,
        ENEMY
    }
    public aspect aspectName;
}
