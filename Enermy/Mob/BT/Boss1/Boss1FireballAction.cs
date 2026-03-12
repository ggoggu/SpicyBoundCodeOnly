using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Boss1Fireball", story: "BossFireball", category: "Action", id: "1559369340c78436461fd8565602d31c")]
public partial class Boss1FireballAction : Action
{
    [SerializeReference] public BlackboardVariable<Boss1> Boss1;

    protected override Status OnStart()
    {
        if (Boss1 == null)
        {
            Debug.LogError("Boss1 variable is not set.");
            return Status.Failure;
        }

        Boss1.Value.TriggerFireball();
        return Status.Running;
    }

}

