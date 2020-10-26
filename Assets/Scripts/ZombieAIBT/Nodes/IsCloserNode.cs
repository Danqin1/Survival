using System;
using UnityEngine;

class IsCloserNode : Node
{
    private Transform origin;
    private Transform target;
    private float range;

    public IsCloserNode(Transform origin, Transform target, float range)
    {
        this.origin = origin;
        this.target = target;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        state = Vector3.Distance(origin.position, target.position) < range ? NodeState.SUCCES : NodeState.FAILURE;
        return state;
    }
}
