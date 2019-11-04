using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Panda;

//-----------------------------------------------------------
// Scripts\Character\EnemyBase.cs
//
// 적 캐릭터를 관리하는 클래스
// 1. Panda BT를 이용한 Behavior Tree로 동작
// 2. Perspective Sensor, Collider Sensor의 2가지를 이용하여 데이터를 받는다
// 3. BaseCharacter를 상속받아 이벤트를 사용
//-----------------------------------------------------------
public class EnemyBase : BaseCharacter
{
    #region variables
    //-----------------------------------------------------------
    // Enemy의 행동로직에 필요한 변수들
    [HideInInspector]
    private float elapsedTime;
    private float deadDelayTime;

    public Transform spawnTransform;            // Spawner 오브젝트으 트랜스폼
    public Transform targetTransform;           // Player의 트랜스폼 참조
    private Transform modelTransform;           // 해당 스크립트의 모델 트랜스폼

    private Animator ani;

    public Vector3 destination;                 // 이동하고자 하는 방향

    public float moveSpeed;                     // 움직임 속도
    public float rotateSpeed;                   // 회전 속도

    private Perspective perspective;            // 시각 센서
    
    private BoxCollider weaponCollider;

    private bool hasEnemy = false;              // (공격하고자 하는) 적이 있는가
    private bool isOutOfBounds = false;         // 제한된 이동범위를 벗어났는가
    private bool is_attacked = false;           // 공격을 받았는가
    #endregion

    #region methods
    //-----------------------------------------------------------
    // Use this for initialization
    protected override void Initialize()
    {
        base.Initialize();


        perspective = this.GetComponent<Perspective>();

        targetTransform = GameObject.FindGameObjectWithTag("Player").transform;
        modelTransform = transform.GetChild(0);
        ani = modelTransform.GetComponent<Animator>();
        
        weaponCollider = transform.GetChild(0).GetChild(3).GetComponent<BoxCollider>() as BoxCollider;
        weaponCollider.enabled = false;
    }

    //-----------------------------------------------------------
    // Update per frame
    private void Update()
    {

    }

    //-------------------------------------------------------
    //Called when events happen
    protected override void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        base.OnEvent(Event_Type, Sender, Param);

        switch (Event_Type)
        {
            case EVENT_TYPE.NONE:
                break;
            case EVENT_TYPE.GAME_INIT:
                break;
            case EVENT_TYPE.GAME_END:
                break;
            case EVENT_TYPE.DEAD:
                break;
            case EVENT_TYPE.NUM_OF_EVENTS:
                break;
            default:
                break;
        }
    }
    // ----------------------------------------------------------
    // 공격하는 무기의 OnTriggerEnter에서 호출한다.
    public override void CollisionDetected(Weapon weapon, Collider other)
    {
        if (other.tag == "Player")
            other.gameObject.SendMessage("OnAttacked", DAMAGE);
    }
    //-------------------------------------------------------
    //Called when attacked by player
    protected override void OnAttacked(object param)
    {
        base.OnAttacked(param);

        if(!IsDead())
            ani.Play("Sword And Shield Impact");
        is_attacked = true;
    }
    //-------------------------------------------------------
    #endregion

    #region tasks
    //-------------------------------------------------------
    [Task]
    bool IsDead()
    {
        return HEALTH <= 0;
    }
    //-------------------------------------------------------
    [Task]
    bool IsAttacked()
    {
        return is_attacked;
    }
    //-------------------------------------------------------
    [Task]
    bool IsVisibleTarget()
    {
        return perspective.IsPlayerVisible();
    }

    //-------------------------------------------------------
    [Task]
    bool HasEnemy()
    {
        //수정필요
        return hasEnemy;
    }

    //-------------------------------------------------------
    [Task]
    bool IsOutOfBounds()
    {
        hasEnemy = false;

        //수정필요
        return isOutOfBounds;
    }
    //-------------------------------------------------------

    // Vector3.Distance로 공격 가능 범위인지 판단
    [Task]
    bool IsTargetInAttackRange()
    {
        //보이지 않으면 공격범위로 두지 않는다.
        if (!IsVisibleTarget()) return false;

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("d = {0}", Vector3.Distance(transform.position,
                targetTransform.position));

        return Vector3.Distance(transform.position, targetTransform.position) < 1.5f;
    }

    //-------------------------------------------------------
    [Task]
    void LookAtDestination()
    {
        Vector3 targetDelta = destination - this.transform.position;
        Vector3 targetDir = targetDelta.normalized;

        if (targetDelta.magnitude > 0.2f)
        {
            Quaternion targetRot = Quaternion.LookRotation(destination - transform.position);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRot,
               rotateSpeed * Time.deltaTime);

            if (Vector3.Angle(targetDir, this.transform.forward) < 0.05f)
            {
                Task.current.Succeed();
            }
        }
        else
        {
            Task.current.Succeed();
        }

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("angle = {0}", Vector3.Angle(targetDir,
                this.transform.forward));

    }
    //-------------------------------------------------------
    [Task]
    void IsTargetThreaten(float duration)
    {

        if (Task.current.isStarting)
        {
            elapsedTime = -Time.deltaTime;
        }

        ani.Play("Talking");

        elapsedTime += Time.deltaTime;

        var t = duration - elapsedTime;

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", t);

        if (t <= 0)
        {
            hasEnemy = true;
            Task.current.Succeed();
        }
    }

    //-------------------------------------------------------
    [Task]
    public bool SetDestination(Vector3 p)
    {
        destination = p + new Vector3(0, 1.0f, 0);

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("{0}, {1}, {2}",
                destination.x, destination.y, destination.z);

        return true;
    }

    //-------------------------------------------------------
    [Task]
    bool SetDestination()
    {
        return true;
    }

    //-------------------------------------------------------
    [Task]
    bool SetRandomDestination()
    {
        Vector3 dst = new Vector3(Random.Range(-10.0f, 10.0f), 0, Random.Range(-10.0f, 10.0f));
        SetDestination(dst);

        return true;
    }

    //-------------------------------------------------------
    [Task]
    bool SetDestination_Enemy()
    {
        if (targetTransform != null)
        {
            SetDestination(targetTransform.position + new Vector3(0, -1.0f, 0));
            return true;
        }
        else
            return false;
    }
    //-------------------------------------------------------
    [Task]
    void SetDestination_ChaseTarget()
    {
        if (targetTransform != null)
        {
            SetDestination(targetTransform.position + new Vector3(0, -1.0f, 0));
            float dist = Vector3.Distance(destination, transform.position);

            if (dist < 1.0f)
                Task.current.Succeed();
        }
        else
            Task.current.Fail();
    }

    //-------------------------------------------------------
    [Task]
    void MoveToDestination()
    {
        Vector3 targetDelta = destination - this.transform.position;
        Vector3 targetDir = targetDelta.normalized;

        float dist = Vector3.Distance(destination, transform.position);

        if (dist > 1.0f)
        {
            if (hasEnemy)
            {
                moveSpeed = 1.5f;
                ani.Play("Slow Run");
            }
            else
            {
                moveSpeed = 1.0f;
                ani.Play("Sword And Shield Walk");
            }

            transform.Translate(new Vector3(0, 0, moveSpeed * Time.deltaTime));
        }
        else
        {
            Task.current.Succeed();
        }

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("{0}", dist);
    }

    //-------------------------------------------------------
    [Task]
    void Idle(float duration)
    {
        if (Task.current.isStarting)
        {
            elapsedTime = -Time.deltaTime;
        }

        ani.Play("Sword And Shield Idle");

        elapsedTime += Time.deltaTime;

        var t = duration - elapsedTime;
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", t);

        if (t <= 0)
        {
            Task.current.Succeed();
        }

    }

    //-------------------------------------------------------
    [Task]
    void AttackTarget(float AttackSpeed)
    {
        if (Task.current.isStarting)
        {
            elapsedTime = -Time.deltaTime;
        }

        ani.Play("Sword And Shield Slash");

        elapsedTime += Time.deltaTime;


        if (elapsedTime > 0.5f && elapsedTime < 0.7f)
            weaponCollider.enabled = true;
        if (elapsedTime > 0.7f)
            weaponCollider.enabled = false;


        var t = AttackSpeed - elapsedTime;

        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", t);

        if (t <= 0)
        {
            Task.current.Succeed();
        }
    }

    //-------------------------------------------------------
    [Task]
    void WaitIdle(float duration)
    {
        if (Task.current.isStarting)
        {
            elapsedTime = -Time.deltaTime;
        }

        ani.Play("Sword And Shield Wait");

        elapsedTime += Time.deltaTime;

        var t = duration - elapsedTime;
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", t);

        if (t <= 0)
        {
            Task.current.Succeed();
        }
    }
    //-------------------------------------------------------
    [Task]
    void AttackedImpact()
    {
        if (!ani.GetCurrentAnimatorStateInfo(0).IsName("Sword And Shield Impact"))
        {
            if (!hasEnemy)
                hasEnemy = true;

            is_attacked = false;
            Task.current.Succeed();
        }
    }
    //-------------------------------------------------------
    [Task]
    void DeadAndDestroy(float duration)
    {
        if (Task.current.isStarting)
        {
            deadDelayTime = -Time.deltaTime;
            ani.Play("Sword And Shield Death");
        }

        deadDelayTime += Time.deltaTime;

        var t = duration - deadDelayTime;
        if (Task.isInspected)
            Task.current.debugInfo = string.Format("t={0:0.00}", t);

        if (t <= 0)
        {
            Task.current.Succeed();
            DestroyImmediate(this.gameObject);
        }
    }
    //-------------------------------------------------------
    #endregion
}

