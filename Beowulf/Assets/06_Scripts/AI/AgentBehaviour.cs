using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Agent의 대부분의 행위에 대한 템플릿 클래스
public class AgentBehaviour : MonoBehaviour
{
    public GameObject target;
    protected Agent agent;

    public virtual void Awake()
    {
        agent = gameObject.GetComponent<Agent>();
    }

    public virtual void Update()
    {
        agent.SetSteering(GetSteering());
    }
    public virtual Steering GetSteering()
    {
        return new Steering();
    }
}
