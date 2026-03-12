using System;
using System.Collections.Generic;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(HorizonMovement))]
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "PatrolWayointHorizon", story: "PatrolWaypoint, [PatrolPoints]", category: "Action", id: "0b298fb6e1e43655fb522a3a208d521b")]
public partial class PatrolWayointHorizonAction : Action
{
    [SerializeReference] public BlackboardVariable<List<GameObject>> PatrolPoints;
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<float> WaypointWaitTime = new(1.0f);
    [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new("speed");
    [SerializeReference] public BlackboardVariable<float> DistanceThreshold = new(0.5f);
    [SerializeReference] public BlackboardVariable<float> PatrolSpeed = new(1.0f);

    private HorizonMovement m_Navigation;
    private Animator m_Animator;

    [CreateProperty] private float m_WaypointWaitTimer;
    [CreateProperty] private int m_CurrentPatrolPoint = -1;
    [CreateProperty] Vector2 m_CurrentTarget;
    [CreateProperty] private bool m_Waiting;


    protected override Status OnStart()
    {
        if (Agent.Value == null)
        {
            LogFailure("No agent assigned.");
            return Status.Failure;
        }

        if (PatrolPoints.Value == null || PatrolPoints.Value.Count == 0)
        {
            LogFailure("No waypoints to patrol assigned.");
            return Status.Failure;
        }


        Init();
        m_Waiting = false;
        m_WaypointWaitTimer = 0.0f;
        m_Navigation.SetMoveSpeed(PatrolSpeed);


        return Status.Running;

    }

    protected override Status OnUpdate()
    {
        if (Agent.Value == null || PatrolPoints.Value == null)
        {
            return Status.Failure;
        }

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

                m_Navigation.SetMoveSpeed(PatrolSpeed);
                MoveToNextPatrolPoint();

                
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

                m_Navigation.SetMoveSpeed(0);



            }

        }

        m_Navigation.MoveToTargetDirection(m_CurrentTarget);
        UpdateAnimatorSpeed();

        return Status.Running;
    }

    protected override void OnEnd()
    {
        UpdateAnimatorSpeed();
    }

    private void Init()
    {
        m_Animator = Agent.Value.GetComponentInChildren<Animator>();
        m_Navigation = Agent.Value.GetComponent<HorizonMovement>();

        MoveToNextPatrolPoint();
        UpdateAnimatorSpeed();
    }

    private float GetDistanceToPatrolPoint()
    {
        float targetPos = m_CurrentTarget.x;
        float agentPos = Agent.Value.transform.position.x;



        return Math.Abs(targetPos - agentPos);
    }

    private void MoveToNextPatrolPoint()
    {
        m_CurrentPatrolPoint = (m_CurrentPatrolPoint + 1) % PatrolPoints.Value.Count;
        m_CurrentTarget = PatrolPoints.Value[m_CurrentPatrolPoint].transform.position;

        m_Navigation.MoveToTargetDirection(m_CurrentTarget);
    }

    private void UpdateAnimatorSpeed()
    {
        m_Navigation.UpdateAnimationSpeed(AnimatorSpeedParam, ref m_Animator);
    }



}

