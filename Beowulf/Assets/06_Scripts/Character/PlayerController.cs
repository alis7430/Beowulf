 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//-----------------------------------------------------------
// Scripts\Character\PlayerController.cs
//
// 플레이어의 상태 및 움직임을 관리하는 클래스
// 1. 전체적으로 FSM을 이용하여 캐릭터의 움직임과 상태를 제어한다.
// 2. BaseCharacter를 상속받아 이벤트를 사용
//-----------------------------------------------------------
public class PlayerController : BaseCharacter
{
    #region C# properties
    #endregion

    #region variables

    //-----------------------------------------------------------
    // 플레이어 상태를 나타내는 열거체
    public enum STEP
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        JUMP,
        JUMP_RUNNING,
        LANDING,
        WEAPON_CHANGE,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        BLOCKING,
        DIE,
        CONVERSATION,
        NUM_OF_STATES
    }

    //-----------------------------------------------------------
    // 플레이어 상태 및 상태 타이머 변수
    public STEP step = STEP.NONE;        // 현재 상태
    public STEP next_step = STEP.NONE;   // 다음 상태
    public float stepTimer = 0.0f;


    //-----------------------------------------------------------
    // 클래스 참조 변수
    [HideInInspector]
    public GameObject weaponManager;
    [HideInInspector]
    public GameObject weapon;    // 플레이어 무기
    public BoxCollider weaponCollider;

    private Transform modelTransform;       // 플레이어 모델의 트랜스폼    
    private CharacterController cc;         // 캐릭터 컨트롤러 컴포넌트
    private Animator ani;                   // 애니메이터를 저장

    //-----------------------------------------------------------
    // 플레이어 데이터 전역변수

    public static float gravity = 9.8f;             // 중력 크기
    public static float mouseSensitivity = 2.0f;    // 카메라 마우스 감도
    public static float cameraHeight = 0.5f;        // 카메라 높이


    public static float aniSpped = 1.2f;            // 애니메이션 속도   


    //-----------------------------------------------------------
    // 플레이어 움직임 관련 변수

    [HideInInspector]
    public float runSpeed = 2.5f;            // 달리는 속도
    [HideInInspector]
    public float jumpSpeed = 4.0f;           // 점프 속도

    private float attackSpeedRatio;         // 공격속도
    private float movingSpeedRatio;         // 움직임 속도 비율

    Vector3 move;                             // 플레이어의 이동 벡터

    //-----------------------------------------------------------
   
    [SerializeField]
    float currentSpeed = 0.0f;              // 현재 속도
    [SerializeField]
    float curretMoveIncreaseRatio;         // 현재 이동 속도 비율 증/감소 
    float tempMoveIncreaseRatio;           // 이동 속도 비율 증/감소 임시저장


    //-----------------------------------------------------------
    // 카메라 컨트롤 변수
    Transform cameraParentTransform;    // 카메라의 부모 트랜스폼
    Transform cameraTransform;          // 메인 카메라의 트랜스폼

    Vector3 mouseMove;                  // 마우스 움직임을 저장

    //-----------------------------------------------------------
    // 상태 체크 변수
    public bool is_Player_Armed = false;
    bool is_Gradient_Check = true;
    bool is_Calculate_Move = true;
    bool is_CameraMoveOnPlayer = true;

    public bool is_player_Control = true;
    //-----------------------------------------------------------
    // 퀘스트 시스템 변수
    //-----------------------------------------------------------
    public List<Quest> quests;
    #endregion

    #region methods
    //-----------------------------------------------------------
    // Use this for initialization
    protected override void Initialize()
    {
        DontDestroyOnLoad(this.gameObject);

        // 컴포넌트 클래스 초기화
        cc = GetComponent<CharacterController>();
        modelTransform = transform.GetChild(0);
        ani = modelTransform.GetComponent<Animator>();

        // 3인칭 카메라 클래스 초기화
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;


        // 상태 초기화
        this.step = STEP.NONE;
        this.next_step = STEP.IDLE;

        // 이동속도 증감 비율 초기화
        curretMoveIncreaseRatio = 1.0f;
        tempMoveIncreaseRatio = 1.0f;

        ani.speed = aniSpped;

        movingSpeedRatio = 1 / aniSpped;
        attackSpeedRatio = 1 / aniSpped;
      
        weaponManager = GameObject.FindWithTag("WeaponManager") as GameObject;

        weaponCollider = weaponManager.GetComponent<BoxCollider>();
        weaponCollider.enabled = false;

        quests = new List<Quest>();
    }
    //-----------------------------------------------------------
    // Update문에서 플레이어 상태 검사 및 동작 수행
    private void Update()
    {
        this.stepTimer += Time.deltaTime;

        // 상태를 변화 시킨다
        if (this.next_step == STEP.NONE)
        {
            switch (this.step)
            {
                case STEP.IDLE:
                    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
                    {
                        next_step = STEP.MOVE;
                    }
                    if (Input.GetKeyDown(KeyCode.Space) && cc.isGrounded)
                    {
                        next_step = STEP.JUMP;
                    }
                    if (Input.GetMouseButtonDown(0))
                    {
                        // 공격
                        if (is_Player_Armed)
                            next_step = STEP.ATTACK1;
                    }
                    if (Input.GetMouseButton(1))
                    {
                        // 블로킹
                        if (is_Player_Armed)
                            next_step = STEP.BLOCKING; 
                    }
                    if (Input.GetKeyDown(KeyCode.X) && GetWeapon())
                    {
                        next_step = STEP.WEAPON_CHANGE;
                    }
                    break;
                case STEP.MOVE:
                    if (currentSpeed == 0)
                    {
                        next_step = STEP.IDLE;
                    }
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        next_step = STEP.JUMP;
                    }
                    break;
                case STEP.JUMP:
                    if (stepTimer > 0.7f * movingSpeedRatio && cc.isGrounded)  // 점프 애니메이션 때문에 0.7초 이후에 상태 변환
                    {
                        if (ani.GetCurrentAnimatorStateInfo(0).IsName("Unarmed Jump Running"))
                        {
                            next_step = STEP.MOVE;
                        }
                        else
                        {
                            next_step = STEP.LANDING;
                            curretMoveIncreaseRatio = tempMoveIncreaseRatio;
                        }
                    }
                    break;
                case STEP.LANDING:
                    if(stepTimer > 0.6f * movingSpeedRatio)
                    {
                        next_step = STEP.IDLE;
                        is_Calculate_Move = true;
                    }
                    break;
                case STEP.WEAPON_CHANGE:
                    if(stepTimer > 1.1f * movingSpeedRatio)    // 무기교체 애니메이션 때문에 1.1초 이후에 상태 변환
                    {
                        next_step = STEP.IDLE;
                    }
                    break;
                case STEP.ATTACK1:
                    if (Input.GetMouseButtonDown(0) && stepTimer < attackSpeedRatio * 1.3f && stepTimer > attackSpeedRatio * 0.9f)
                    {
                        weaponCollider.enabled = false;
                        next_step = STEP.ATTACK2;
                    }
                    if(stepTimer > 1.2f * attackSpeedRatio)
                    {
                        weaponCollider.enabled = false;
                        next_step = STEP.IDLE;
                    }
                    break;
                case STEP.ATTACK2:
                    if (Input.GetMouseButtonDown(0) && stepTimer < attackSpeedRatio* 0.9f && stepTimer > attackSpeedRatio * 0.5f)
                    {
                        weaponCollider.enabled = false;
                        next_step = STEP.ATTACK3;
                    }
                    if (stepTimer > 0.8f * attackSpeedRatio)
                    {
                        next_step = STEP.IDLE;
                    }
                    break;
                case STEP.ATTACK3:
                    if (stepTimer > 1.6f * attackSpeedRatio)
                    {
                        next_step = STEP.IDLE;
                    }
                    break;
                case STEP.BLOCKING:
                    if(Input.GetMouseButtonUp(1))
                    {
                        ani.SetBool("Blocking", false);
                        next_step = STEP.IDLE;
                    }
                    break;
            }
        }
        // 상태가 변화할 때
        while (this.next_step != STEP.NONE)
        {
            this.step = this.next_step;
            this.next_step = STEP.NONE;

            // 상태가 전이될 때 한번 호출
            switch (this.step)
            {
                case STEP.IDLE:
                    is_Gradient_Check = true;       // 기본 상태에서는 경사로 검색을 해줌
                    is_Calculate_Move = true;       // 기본 상태에서는 이동이 가능
                    is_CameraMoveOnPlayer = true;
                    is_player_Control = true;
                    weaponCollider.enabled = false;
                    break;
                case STEP.MOVE:
                    break;
                case STEP.JUMP:                
                    is_Gradient_Check = false;      // 점프 중에는 경사로 탐색을 안함
                    
                    if (currentSpeed < 2.5)
                    {
                        tempMoveIncreaseRatio = curretMoveIncreaseRatio;    // 원래 이동 속도 증가 비율을 저장해 둔다
                        curretMoveIncreaseRatio *= 0.02f;                   // 점프 시 이동 속도 증가 비율 감소
                        ani.Play("Unarmed Jump");
                    }
                    else
                    {
                        ani.Play("Unarmed Jump Running");
                    }
                    break;
                case STEP.LANDING:
                    is_Calculate_Move = false;
                    ani.Play("Unarmed Jump_2");
                    break;
                case STEP.WEAPON_CHANGE:
                    is_Calculate_Move = false;
                    if (!is_Player_Armed)
                    {
                        is_Player_Armed = true;
                        ani.Play("Unarmed Equip Underarm");
                        weapon.SetActive(true);
                    }
                    else
                    {
                        is_Player_Armed = false;
                        ani.Play("Standing Disarm Underarm");
                    }
                    break;
                case STEP.ATTACK1:
                    is_Calculate_Move = false;
                    ani.Play("Standing Melee Combo Attack1");
                    SoundManager.instance.PlaySFX("Swing_s01", 0.6f * attackSpeedRatio);
                    break;
                case STEP.ATTACK2:
                    is_Calculate_Move = false;
                    ani.Play("Standing Melee Combo Attack2");
                    SoundManager.instance.PlaySFX("Swing_s02", 0.3f * attackSpeedRatio);
                    break;
                case STEP.ATTACK3:
                    is_Calculate_Move = false;
                    ani.Play("Standing Melee Combo Attack3");

                    SoundManager.instance.PlaySFX("Swing_s03", 0.6f * attackSpeedRatio);
                    break;
                case STEP.BLOCKING:
                    ani.SetBool("Blocking", true);
                    is_Calculate_Move = false;      // 막고있는 동안에는 이동 불가 
                    break;
                case STEP.DIE:
                    break;
                case STEP.CONVERSATION:
                    ani.Rebind();
                    move = Vector3.zero;
                    is_Calculate_Move = false;
                    break;
            }
            this.stepTimer = 0.0f;
        }
        // 각 상태에서 반복
        switch (this.step)
        {
            case STEP.IDLE:
                if (!is_Player_Armed)
                {
                    ani.Play("Unarmed Idle");
                }
                else
                {
                    ani.Play("Standing Idle");
                }
                break;
            case STEP.MOVE:
                if(currentSpeed > 3.0)
                {
                    if (!is_Player_Armed)
                    {
                        ani.Play("Unarmed Run Forward");
                    }
                    else
                    {
                        ani.Play("Standing Run Forward");
                    }
                }
                else
                {
                    if (!is_Player_Armed)
                    {
                        ani.Play("Unarmed Walk Forward");
                    }
                    else
                    {
                        ani.Play("Standing Walk Forward");
                    }
                }
                break;
            case STEP.JUMP:
                if (stepTimer >= 0.45f * movingSpeedRatio)
                {
                    StartJump();
                }
                break;
            case STEP.WEAPON_CHANGE:
                if(!is_Player_Armed && stepTimer >= 0.6f * movingSpeedRatio)
                {

                    weapon.SetActive(false);
                }
                break;
            case STEP.ATTACK1:
                if (stepTimer > 0.6f * attackSpeedRatio)
                {
                    weaponCollider.enabled = true;
                }
                break;
            case STEP.ATTACK2:
                if (stepTimer > 0.3f * attackSpeedRatio)
                {
                    weaponCollider.enabled = true;
                }
                break;
            case STEP.ATTACK3:
                if (stepTimer > 0.6f * attackSpeedRatio)
                {
                    weaponCollider.enabled = true;
                }
                break;

        }
    }
    //-----------------------------------------------------------
    // 플레이어 이동 
    void FixedUpdate()
    {
        BalancePlayerAngle();

        // 플레이어가 땅에 있을 경우
        if (cc.isGrounded)
        {
            GradientCheck();
            CalculateMove(curretMoveIncreaseRatio);
        }
        // 공중에 떠 있는 경우
        else
        {
            move.y -= gravity * Time.deltaTime;
            CalculateMove(0.1f);
        }

        cc.Move(move * Time.deltaTime);
        //Debug.Log(move);
    }
    //-----------------------------------------------------------
    // 카메라 위치 및 로직 처리
    void LateUpdate()
    {
        if (is_CameraMoveOnPlayer)
        {
            cameraParentTransform.position = transform.position + Vector3.up * cameraHeight;  //캐릭터의 머리 높이쯤

            mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0);   //마우스의 움직임을 가감

            if (mouseMove.x < -20)  //높이는 제한을 둔다. 슈팅 게임이라면 거의 90에 가깝게 두는게 좋을수도 있다.
                mouseMove.x = -20;
            else if (30 < mouseMove.x)
                mouseMove.x = 30;

            //여기서 헷갈리면 안 되는게 GetAxisRaw("Mouse XY") 는 실제 마우스의 움직임의 x좌표 y좌표를 가져오지만 회전은 축 기준이라 x가 위아래고 y가 좌우이다.
            cameraParentTransform.localEulerAngles = mouseMove;

            ControlCameraDistance();
        }
    }

    // ----------------------------------------------------------
    // public Fuctions
    // ----------------------------------------------------------
    // 공격하는 무기의 OnTriggerEnter에서 호출한다.
    public override void CollisionDetected(Weapon weapon, Collider other)
    {
        if (other.tag == "Enemy")
            other.gameObject.SendMessage("OnAttacked", DAMAGE);

        EventManager.Instance.PostNotification(EVENT_TYPE.PLAYER_HIT, this, null);
    }
    // ----------------------------------------------------------
    protected override void OnAttacked(object param)
    {
        base.OnAttacked(param);
    }
    //-----------------------------------------------------------
    public bool GetWeapon()
    {
        if (weaponManager.transform.childCount != 0)
        {
            weapon = weaponManager.transform.GetChild(0).gameObject;
            return true;
        }
        else
            return false;
    }
    //-----------------------------------------------------------
    void ActiveWeapon()
    {
        if (weapon != null)
            weapon.SetActive(!weapon.activeSelf);
    }
    //-----------------------------------------------------------
    public void AddQuest(Quest quest)
    {
        this.quests.Add(quest);
    }
    //-----------------------------------------------------------
    public bool IsReachedQuest(Quest quest)
    {
        if(quest.isActive)
        {
            if (quest.goal.IsReached())
            {
                return true;
            }
            else
                return false;
        }
        return false;
    }
    // ----------------------------------------------------------
    // Private Fuctions
    // ----------------------------------------------------------*/

    //-----------------------------------------------------------
    // 호출 시 플레이어가 1회 점프합니다.
    void StartJump()
    {
        // 땅에 있을 때에만 점프 가능
        if (cc.isGrounded)
            move.y = jumpSpeed;
    }

    //-----------------------------------------------------------
    // 휠로 카메라를 조절합니다. (Update호출)
    void ControlCameraDistance()
    {
        cameraTransform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.

        if (-1 < cameraTransform.localPosition.z)
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, -1);    //최대로 가까운 수치
        else if (cameraTransform.localPosition.z < -5)
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, cameraTransform.localPosition.y, -5);    //최대로 먼 수치
    }
    //-----------------------------------------------------------
    // 이동 벡터를 계산하고 움직입니다. (Update호출)
    void CalculateMove(float increaseRatio)
    {
        if (!is_Calculate_Move)
            return;
        float tempMoveY = move.y;
        move.y = 0; //이동에는 xz값만 필요하므로 잠시 저장하고 빼둔다.

        Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        //대각선 이동이 루트2 배의 속도를 갖는 것을 막기위해 속도가 1 이상 된다면 노말라이즈 후 속도를 곱해 어느 방향이든 항상 일정한 속도가 되게 한다.
        float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
        inputMoveXZ = transform.TransformDirection(inputMoveXZ);

        if (inputMoveXZMgnitude <= 1)
            inputMoveXZ *= runSpeed;
        else
            inputMoveXZ = inputMoveXZ.normalized * runSpeed;

        //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Quaternion cameraRotation = cameraParentTransform.rotation;
            cameraRotation.x = cameraRotation.z = 0;        //y축만 필요하므로 나머지 값은 0으로 바꾼다.

            transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, 10.0f * Time.deltaTime);      //자연스러움을 위해 Slerp로 회전시킨다.

            if (move != Vector3.zero)       //Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, characterRotation, 5.0f * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.LeftShift) && step == STEP.MOVE )        // 이동중 shift 키가 눌렸을 때
            {
                move = Vector3.MoveTowards(move, inputMoveXZ * 1.5f, increaseRatio * runSpeed);     //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.(달리기)
            }
            else
            {
                move = Vector3.MoveTowards(move, inputMoveXZ, increaseRatio * runSpeed);    //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.(걷기)
            }
        }
        else
        {
            move = Vector3.MoveTowards(move, Vector3.zero, runSpeed * increaseRatio);       //조작이 없으면 서서히 멈춘다.
        }

        currentSpeed = move.magnitude;       // 현재 속도를 구한다.
        move.y = tempMoveY;                  //y값 복구
    }

    //-----------------------------------------------------------
    //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다. (Update호출)
    void GradientCheck()
    {
        if (!is_Gradient_Check)
            return;

        //CharacterController는 밑으로 지속적으로 Move가 일어나야 땅을 체크하는데 -y값이 너무 낮으면 조금만 경사져도 공중에 떠버리고 너무 높으면 절벽에서 떨어질때 추락하듯 바로 떨어진다.
        //완벽하진 않지만 캡슐 모양의 CharacterController에서 절벽에 떨어지기 직전엔 중앙에서 밑으로 쏘아지는 레이에 아무것도 닿지 않으므로 그때만 -y값을 낮추면 경사로에도 잘 다니고
        //절벽에도 자연스럽게 천천히 떨어지는 효과를 줄 수 있다.
        if (Physics.Raycast(transform.position, Vector3.down, 0.2f))
        {
            move.y = -5;
        }
        else
            move.y = -1;
    }
    //-----------------------------------------------------------
    //모종의 이유로 기울어진다면 바로잡는다. (Update호출)
    void BalancePlayerAngle()
    {
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    //-----------------------------------------------------------

    #endregion
}
