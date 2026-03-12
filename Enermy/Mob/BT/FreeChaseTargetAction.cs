using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "FreeChaseTarget", story: "FreeChaseTarget", category: "Action", id: "b59f450b998c493dfb7441e076d85621")]
public partial class FreeChaseTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> target;
    [SerializeReference] public BlackboardVariable<float> speed;
    [SerializeReference] public BlackboardVariable<string> AnimatorSpeedParam = new("speed");

    private HorizonMovement movement;
    private Animator animator;

    protected override Status OnStart()
    {
        movement = Agent.Value.GetComponentInChildren<HorizonMovement>();
        animator = Agent.Value.GetComponentInChildren<Animator>();

        if (target == null || movement == null || animator == null)
        {
            return Status.Failure;
        }

        Init();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        movement.FreeMoveToTargetDirection(target.Value.position);
        movement.UpdateAnimationSpeed(AnimatorSpeedParam, ref animator);
        return Status.Running;
    }

    protected override void OnEnd()
    {
    }

    private void Init()
    {
        movement.bIsHorizon = false;
        float ChaseSpeed = speed.Value * 1.5f;
        movement.SetMoveSpeed(ChaseSpeed);
    }
}

