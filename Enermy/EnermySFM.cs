using System.Linq;
using Unity.Behavior;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(BehaviorGraphAgent))]
public class EnermySFM : MonoBehaviour
{
    [SerializeField] public Transform target;
    [SerializeField] private string speedName = "speed";
    protected BehaviorGraphAgent behaviorAgent;


    public void SetUp(Transform target)
    {
        this.target = target;
        behaviorAgent = GetComponent<BehaviorGraphAgent>();

        string targetname = "target";
        if (target != null)
            behaviorAgent.SetVariableValue(targetname, target);
    }

    public void SetSpeed(float speed)
    {
        behaviorAgent.SetVariableValue(speedName, speed);
    }

    public void SetBoolean(string name, bool value)
    {
        behaviorAgent.SetVariableValue(name, value);
    }

    public void ChangeBehaviorGraph(BehaviorGraph graph)
    {
        if (behaviorAgent != null)
        {
            behaviorAgent.Graph = graph;
        }
    }

}
