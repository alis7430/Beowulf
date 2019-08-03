using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static float runSpeed = 2.5f;            // 달리는 속도
    public static float jumpSpeed = 4.0f;           // 점프 속도
    public static float gravity = 9.8f;             // 중력 크기
    public static float mouseSensitivity = 2.0f;    // 카메라 마우스 감도
    public static float cameraHeight = 1.75f;       // 카메라 높이

    // 상태 열거체
    public enum STEP
    {
        NONE = -1,
        IDLE = 0,
        MOVE,
        JUMP,
        ATTACK1,
        ATTACK2,
        ATTACK3,
        DEFENSE,
        DIE,
        NUM_OF_STATES
    }

    public STEP step = STEP.NONE;        // 현재 상태
    public STEP next_step = STEP.NONE;   // 다음 상태
    public float stepTimer = 0.0f;

    [SerializeField]
    float currentSpeed = 0.0f;      // 현재 속도

    Transform modelTransform;
    CharacterController cc;
    Animator ani;

    Vector3 mouseMove;
    Vector3 move;

    Transform cameraParentTransform;
    Transform cameraTransform;
    
    bool is_Gradient_Check = true;
    bool is_MoveCalculate = true;
   
    // Use this for initialization
    void Awake()
    {
        cc = GetComponent<CharacterController>();
        modelTransform = transform.GetChild(0);

        ani = modelTransform.GetComponent<Animator>();
        cameraTransform = Camera.main.transform;
        cameraParentTransform = cameraTransform.parent;
    }

    private void Start()
    {
        this.step = STEP.NONE;
        this.next_step = STEP.IDLE;
    }

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
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        next_step = STEP.JUMP;
                    }
                    break;
                case STEP.MOVE:
                    if(currentSpeed == 0)
                    {
                        next_step = STEP.IDLE;
                    }
                    break;
                case STEP.JUMP:
                    if(stepTimer > 0.7f && cc.isGrounded)
                    {
                        next_step = STEP.IDLE;
                    }
                    break;
            }
        }
        // 상태가 변화할 때
        while(this.next_step != STEP.NONE)
        {
            this.step = this.next_step;
            this.next_step = STEP.NONE;

            // 상태가 전이될 때 한번 호출
            switch (this.step)
            {
                case STEP.IDLE:
                    ani.SetBool("isJump", false);
                    is_Gradient_Check = true;
                    break;
                case STEP.MOVE:
                    break;
                case STEP.JUMP:
                    ani.SetBool("isJump", true);
                    is_Gradient_Check = false;
                    break;
                case STEP.ATTACK1:
                    break;
                case STEP.ATTACK2:
                    break;
                case STEP.ATTACK3:
                    break;
                case STEP.DEFENSE:
                    break;
                case STEP.DIE:
                    break;
            }
            this.stepTimer = 0.0f;
        }
        // 각 상태에서 반복
        switch (this.step)
        {
            case STEP.IDLE:
                ani.SetFloat("Speed", currentSpeed);
                break;
            case STEP.MOVE:
                ani.SetFloat("Speed", currentSpeed);
                break;
            case STEP.JUMP:
                if (stepTimer >= 0.65f)
                {
                    StartJump();
                }
                break;
        }
    }

    void FixedUpdate()
    {
        //플레이어 이동 및 카메라 로직 처리
        Balance();
        CameraDistanceCtrl();

        if (cc.isGrounded)
        {
            GradientCheck();
            ani.SetBool("isGrounded", true);
            CalculateMove(1.0f);
        }
        else
        {
            ani.SetBool("isGrounded", false);
            move.y -= gravity * Time.deltaTime;
            CalculateMove(0.1f);
        }
        cc.Move(move * Time.deltaTime);
        Debug.Log(move);
    }

    void LateUpdate()
    {
        cameraParentTransform.position = transform.position + Vector3.up * cameraHeight;  //캐릭터의 머리 높이쯤

        mouseMove += new Vector3(-Input.GetAxisRaw("Mouse Y") * mouseSensitivity, Input.GetAxisRaw("Mouse X") * mouseSensitivity, 0);   //마우스의 움직임을 가감

        if (mouseMove.x < -20)  //높이는 제한을 둔다. 슈팅 게임이라면 거의 90에 가깝게 두는게 좋을수도 있다.
            mouseMove.x = -20;
        else if (30 < mouseMove.x)
            mouseMove.x = 30;
        //여기서 헷갈리면 안 되는게 GetAxisRaw("Mouse XY") 는 실제 마우스의 움직임의 x좌표 y좌표를 가져오지만 회전은 축 기준이라 x가 위아래고 y가 좌우이다.

        cameraParentTransform.localEulerAngles = mouseMove;
    }

    // 호출시 1회 점프
    void StartJump()
    {
        // 땅에 있을 때에만 점프 가능
        if (cc.isGrounded)
            move.y = jumpSpeed;
    }

    // 휠로 카메라를 조절
    void CameraDistanceCtrl()
    {
        Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.
       
        if (-1 < Camera.main.transform.localPosition.z)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -1);    //최대로 가까운 수치
        else if (Camera.main.transform.localPosition.z < -5)
            Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -5);    //최대로 먼 수치
    }

    // 이동 벡터를 계산
    void CalculateMove(float ratio)
    {
        if (!is_MoveCalculate)
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
            cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
            //자연스러움을 위해 Slerp로 회전시킨다.
            transform.rotation = Quaternion.Slerp(transform.rotation, cameraRotation, 10.0f * Time.deltaTime);
            if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
            {
                Quaternion characterRotation = Quaternion.LookRotation(move);
                characterRotation.x = characterRotation.z = 0;
                modelTransform.rotation = Quaternion.Slerp(modelTransform.rotation, characterRotation, 5.0f * Time.deltaTime);
            }
            if(Input.GetKey(KeyCode.LeftShift))
            {
                //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
                move = Vector3.MoveTowards(move, inputMoveXZ * 1.5f, ratio * runSpeed);
            }
            else
            {
                //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
                move = Vector3.MoveTowards(move, inputMoveXZ, ratio * runSpeed);
            }
        }
        else
        {
            //조작이 없으면 서서히 멈춘다.
            move = Vector3.MoveTowards(move, Vector3.zero, runSpeed * ratio);
        }

        // 현재 속도를 구한다.
        currentSpeed = move.magnitude;

        move.y = tempMoveY; //y값 복구
    }

    //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다.
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

    //모종의 이유로 기울어진다면 바로잡는다.
    void Balance()
    {
        if (transform.eulerAngles.x != 0 || transform.eulerAngles.z != 0)   
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
}
