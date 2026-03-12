using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Boss1JumpAttack", story: "Boss1JumpAttack", category: "Action", id: "5c22e95c77d86dd4e254ba9b38cf386d")]
public partial class Boss1JumpAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Self;
    [SerializeReference] public BlackboardVariable<Boss1> Boss1;

    protected override Status OnStart()
    {

        if (Boss1 == null)
        {
            Boss1.Value = Self.Value.gameObject.GetComponent<Boss1>();
        }

        Boss1.Value.TriggerJumpAttack();

        return Status.Success;  
    }
}

