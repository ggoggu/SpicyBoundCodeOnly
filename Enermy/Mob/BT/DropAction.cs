using System;
using Unity.Behavior;
using UnityEngine;
using Action = Unity.Behavior.Action;
using Unity.Properties;

[Serializable, GeneratePropertyBag]
[NodeDescription(name: "Drop", story: "Drop", category: "Action", id: "92c212c8166b354a685e72f53b9b4b7b")]
public partial class DropAction : Action
{
    [SerializeReference] public BlackboardVariable<GameObject> Agent;
    [SerializeReference] public BlackboardVariable<bool> IsGround;

    private HorizonMovement movement;

    private Rigidbody2D rb;

    protected override Status OnStart()
    {
        Debug.Log("DropStart");

        movement = Agent.Value.GetComponentInChildren<HorizonMovement>();
        rb = Agent.Value.GetComponentInChildren<Rigidbody2D>();

        if (movement == null || rb == null)
        {
            return Status.Failure;
        }

        rb.constraints = RigidbodyConstraints2D.None;
        rb.WakeUp();
        rb.freezeRotation = true;

        Debug.Log("Check");

        return Status.Running;
    }

    protected override Status OnUpdate()
    {
        IsGround.Value = movement.isGrounded;

        return Status.Running;
    }
}

