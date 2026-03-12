using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[RequireComponent(typeof(HorizonMovement))]
[Serializable, GeneratePropertyBag]
[NodeDescription(name: "SetDirectionToTarget", story: "SetTargetDirectionToTarget [target]", category: "Action", id: "855fd046ac0cd07bfebb612fab5676f9")]
public partial class SetDirectionToTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Transform> Target;

    private HorizonMovement movement;

    protected override Status OnStart()
    {
        movement = Self.Value.GetComponentInChildren<HorizonMovement>();
        if(movement == null )
        {
            return Status.Failure;
        }

        movement.ChangeDirection(Target.Value.position.x - Self.Value.transform.position.x);

        return Status.Running;
    }


}

