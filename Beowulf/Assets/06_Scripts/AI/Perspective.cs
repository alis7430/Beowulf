using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//-----------------------------------------------------------
// Scripts\AI\Perspective.cs
//
// Sense를 상속받아 만든 커스텀 시각 센서 입니다.
// 시각 센서는 찾고 있는 특정 종류가 필드 내의 보이는 위치에 있는지,
// 일정 거리 내에 존재하는지 감지합니다. 
// 2개의 가상 메소드 Initialize와 UpdateSense를 정의하고 있습니다.
//-----------------------------------------------------------
public class Perspective : Sense
{
    #region variables
    //----------------------------------------------------------- 
    [Range(0, 180)]
    public int FieldOfView = 45;
    [Range(0, 100)]
    public int ViewDistance = 10;

    private Transform playerTransform;
    private Vector3 rayDirection;

    public LayerMask targetMask;
    public LayerMask obstacleMask;

    Ray ray;

    bool is_player_visible = false;
    //-----------------------------------------------------------
    #endregion

    #region methods
    //-----------------------------------------------------------
    //상속받은 Initialize 재정의
    protected override void Initialize()
    {
        //플레이어 위치 찾기
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //-----------------------------------------------------------
    //상속받은 UpdateSense 재정의
    protected override void UpdateSense()
    {
        elapsedTime += Time.deltaTime;

        // 감시 주기 내에 있다면 시각 감시를 수행한다.
        if (elapsedTime >= detectionRate)
        {
            DetectAspect();
        }
    }

    //-----------------------------------------------------------
    public bool IsPlayerVisible()
    {
        return is_player_visible;
    }

    //-----------------------------------------------------------
    //인공지능 캐릭터를 위한 시각 감시
    void DetectAspect()
    {
        is_player_visible = false;

        RaycastHit hit;

        //현재 위치에서 플레이어 위치로의 방향
        rayDirection = playerTransform.position - transform.position;

        //인공지능 캐릭터의 전방 벡터 그리고 플레이어와 인공지능 간의 방향 벡터 사이의 각도를 검사한다.
        if((Vector3.Angle(rayDirection, transform.forward)) < FieldOfView)
        {
            //플레이어가 필드 내 보이는 영역에 있는지 검사한다.
            if (Physics.Raycast(transform.position, rayDirection.normalized, out hit, ViewDistance))
            {
                Aspect aspect = hit.collider.GetComponent<Aspect>();

                if (aspect != null)
                {
                    //찾는 종류인지 검사한다
                    if (aspect.aspectName == Aspect.aspect.PLAYER)
                    {
                        is_player_visible = true;
                    }
                }
            }
        }
    }
    //-----------------------------------------------------------
    //인공지능 캐릭터의 시야를 화면에 표시한다
    //테스트 플레이에만 사용할 것
    private void OnDrawGizmos()
    {
        if (!isDebug || playerTransform == null) return;
        Debug.DrawLine(transform.position, playerTransform.position, Color.red);

        Vector3 frontRayPoint = transform.position + (transform.forward * ViewDistance);

        //대략적인 시야 시각화
        Vector3 leftRayPoint = transform.position + TransDegreeToZAxis(-FieldOfView) * ViewDistance;
        Vector3 rightRayPoint = transform.position + TransDegreeToZAxis(FieldOfView) * ViewDistance;

        Debug.DrawLine(transform.position, frontRayPoint, Color.green);
        Debug.DrawLine(transform.position, leftRayPoint, Color.green);
        Debug.DrawLine(transform.position, rightRayPoint, Color.green);
    }
    //-----------------------------------------------------------
    // Degree 각을 radian으로 변환
    private Vector3 TransDegreeToZAxis(float angleInDegrees)
    {
        float radian = (angleInDegrees + transform.eulerAngles.y) * Mathf.Deg2Rad;

        return new Vector3(Mathf.Sin(radian), 0f, Mathf.Cos(radian));
    }
    //-----------------------------------------------------------

    #endregion
}
