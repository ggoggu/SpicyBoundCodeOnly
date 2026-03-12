using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Stage2BossHandAttack", story: "Stage2BossHandAttack", category: "Action", id: "4d21ae99d5b08399f47e294bdd31e254")]
public partial class Stage2BossHandAttackAction : Action
{
    [SerializeReference] public BlackboardVariable<Stage2Boss> Stage2Boss;

    protected override Status OnStart()
    {
        Stage2Boss.Value.BossHandAttack();
        return Status.Success;
    }
}

