using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[RequireComponent(typeof(HorizonMovement), typeof(Animator))]
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "ChaseTarget", story: "ChaseTarget [target]", category: "Action", id: "21cabc431733acfe3fdf013166c69f10")]
public partial class ChaseTargetAction : Action
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

        if (target == null || movement == null || animator ==null)
        {
            return Status.Failure;
        }

        Init();

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        movement.MoveToTargetDirection(target.Value.position);
        movement.UpdateAnimationSpeed(AnimatorSpeedParam, ref animator);
        return Status.Running;
    }

    private void Init()
    {
        float ChaseSpeed = speed.Value * 1.5f;
        movement.SetMoveSpeed(ChaseSpeed);
    }

    
}

