using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "OnlyUpDownPatorlPoint", story: "OnlyUpDownPatorlPoint, [PatrolPoints]", category: "Action", id: "fa14e3a6baf6c7bc66129d7e093812be")]
public partial class OnlyUpDownPatorlPointAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoints;
    [SerializeReference] public BlackboardVariable<float> WaypointWaitTime = new(1.0f);
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.2f);
    [SerializeReference] public BlackboardVariable<float> Speed;

    private Rigidbody2D rb;

    [CreateProperty] private Vector2 m_moveDirection = Vector2.zero;
    [CreateProperty] private float m_currentspeed = 0;
    [CreateProperty] private int m_CurrentPatrolPoint = -1;
    [CreateProperty] private Vector2 m_CurrentTarget;
    [CreateProperty] private float m_WaypointWaitTimer;
    [CreateProperty] private bool m_Waiting;

    protected override Status OnStart()
    {
        rb = Agent.Value.GetComponent<Rigidbody2D>();

        if(rb == null)
        {
            LogFailure("No rigidbody2D assigned.");
            return Status.Failure;
        }

        Init();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        if (m_Waiting)
        {
            if (m_WaypointWaitTimer > 0.0f)
            {
                m_WaypointWaitTimer -= Time.deltaTime;

            }
            else
            {
                m_WaypointWaitTimer = 0f;
                m_Waiting = false;

                MoveToNextPatrolPoint();
                m_currentspeed = Speed.Value;

            }
        }
        else
        {


            float distance = GetDistanceToPatrolPoint();
            bool destinationReached = distance <= DistanceThreshold;

            if (destinationReached)
            {
                m_WaypointWaitTimer = WaypointWaitTime.Value;
                m_Waiting = true;
                m_currentspeed = 0;
            }

        }

        SetMoveDirection();
        UpdateRigidBody2d();

        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private float GetDistanceToPatrolPoint()
    {
        float targetPos = m_CurrentTarget.y;
        float agentPos = Agent.Value.transform.position.y;



        return Math.Abs(targetPos - agentPos);
    }

    private void MoveToNextPatrolPoint()
    {
        m_CurrentPatrolPoint = (m_CurrentPatrolPoint + 1) % PatrolPoints.Value.Count;
        m_CurrentTarget = PatrolPoints.Value[m_CurrentPatrolPoint].transform.position;

        
    }

    private void SetMoveDirection()
    {
        Vector2 agentPos = Agent.Value.transform.position;

        m_moveDirection = (m_CurrentTarget - agentPos);
        m_moveDirection.Normalize();
    }

    private void UpdateRigidBody2d()
    {
        
        rb.linearVelocity = m_moveDirection * m_currentspeed;
    }

    private void Init()
    {
        MoveToNextPatrolPoint();
        m_currentspeed = Speed.Value;
    }
}

