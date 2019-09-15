using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------
// Scripts\AI\Wander.cs
//
// 몬스터가 지정범위 내에서 자유롭게 돌아다니게 하는 클레스
// 이 클래스를 컴포넌트로 부착할 경우 지정 범위를 돌아다닙니다.
//-----------------------------------------------------------
public class Wander : MonoBehaviour
{
    #region variables
    //-----------------------------------------------------------
    private Vector3 targetPosition;

    private float movementSpeed = 2.0f;
    private float rotationSpeed = 2.0f;
    private float minX, maxX, minZ, maxZ;
    #endregion

    #region methods
    //-----------------------------------------------------------
    private void Start()
    {
        minX = -15.0f;
        maxX = 15.0f;

        minZ = -15.0f;
        maxZ = 15.0f;

        //돌아다닐 지점 가져오기
        GetNextPosition();
    }

    //-----------------------------------------------------------
    private void Update()
    {
        //목표지점에 있는지 검사
        if (Vector3.Distance(targetPosition, transform.position) <= 5.0f)
            GetNextPosition();

        //목적지를 향하는 회전 값 설정
        Quaternion tarRot = Quaternion.LookRotation(targetPosition - transform.position);

        //회전과 변환 없데이트
        transform.rotation = Quaternion.Slerp(transform.rotation, tarRot, rotationSpeed * Time.deltaTime);

        transform.Translate(new Vector3(0, 0, movementSpeed * Time.deltaTime));
    }

    //-----------------------------------------------------------
    private void GetNextPosition()
    {
        targetPosition = new Vector3(Random.Range(minX, maxX), 1.0f,
            Random.Range(minZ, maxZ));
    }
    #endregion
}
