using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 추적 행위 클래스
public class Pursue : Seek
{
    // 예측을 위한 특성들
    public float maxPrediction;
    private GameObject targetAux;
    private Agent targetAgent;

    public override void Awake()
    {
        base.Awake();
        targetAgent = target.GetComponent<Agent>();
        targetAux = target;
        target = new GameObject();
    }

    private void OnDestroy()
    {
        Destroy(targetAux);
    }

    public override Steering GetSteering()
    {
        Vector3 direction = targetAux.transform.position - transform.position;
        float distance = direction.magnitude;
        float speed = agent.velocity.magnitude;
        float prediction;

        if (speed <= distance / maxPrediction)
            prediction = maxPrediction;
        else
            prediction = distance / speed;
        target.transform.position = targetAux.transform.position;
        target.transform.position += targetAgent.velocity * prediction;

        return base.GetSteering();
    }
}
