using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "CheckTarget", story: "CheckTarget [CheckTarget]", category: "Action", id: "8c2def1cca3c818900634f44d76f223b")]
public partial class CheckTargetAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<Transform> Target;
    [SerializeReference] public BlackboardVariable<CheckTargetTrigger> CheckTarget;
    [SerializeReference] public BlackboardVariable<bool> IsTargetDetected;
    [SerializeReference] public BlackboardVariable<float> currentDistance;


    protected override Status OnStart()
    {
        if(CheckTarget == null)
        {
            CheckTarget.Value = Agent.Value.GetComponentInChildren<CheckTargetTrigger>();

            if(CheckTarget == null)
            {
                return Status.Failure;
            }
        }

        return Status.Running;
    }


    protected override Status OnUpdate()
    {
        IsTargetDetected.Value = CheckTarget.Value.playerDetected;
        currentDistance.Value = Vector2.Distance(Agent.Value.transform.position, Target.Value.position);

        return Status.Running;
    }


}

