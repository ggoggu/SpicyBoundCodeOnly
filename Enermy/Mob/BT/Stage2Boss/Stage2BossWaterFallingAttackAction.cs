using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stage2BossWaterFallingAttack", story: "Stage2BossWaterFallingAttack", category: "Action", id: "fabfa57aba162716e569bd0b1d6c5d4f")]
public partial class Stage2BossWaterFallingAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Stage2Boss> Boss2;

    protected override Status OnStart()
    {

        Boss2.Value.BossFallingWaterAttack();

        return Status.Success;


    }
}

