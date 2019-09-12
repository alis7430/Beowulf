using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Agent의 뼈대
public class Agent : MonoBehaviour
{

    public float maxSpeed;          // 최대 속도
    public float maxAccel;          // 최대 가속도
    public float orientation;       // 방향
    public float rotation;          // 회전(쿼터니언)
    public Vector3 velocity;        // 속력
    protected Steering steering;    // 에이전트의 이동 및 회전을 저장하는 클래스

    private void Start()
    {
        velocity = Vector3.zero;
        steering = new Steering();
    }

    public void SetSteering(Steering steering)
    {
        this.steering = steering;
    }

    public virtual void Update()
    {
        Vector3 displacement = velocity * Time.deltaTime;
        orientation += rotation * Time.deltaTime;
        // 회전 값들의 범위를 0에서 360도 사이로 제한
        if (orientation < 0.0f)
            orientation += 360.0f;
        else if (orientation > 360.0f)
            orientation -= 360.0f;

        transform.Translate(displacement, Space.World);
        transform.rotation = new Quaternion();
        transform.Rotate(Vector3.up, orientation);
    }

    public virtual void LateUpdate()
    {
        velocity += steering.linear * Time.deltaTime;
        rotation += steering.angular * Time.deltaTime;

        if(velocity.magnitude > maxSpeed)
        {
            velocity.Normalize();
            velocity = velocity * maxSpeed;
        }
        if(steering.angular == 0.0f)
        {
            rotation = 0.0f;
        }
        if(steering.linear.sqrMagnitude == 0.0f)
        {
            velocity = Vector3.zero;
        }
        steering = new Steering();

    }
}
