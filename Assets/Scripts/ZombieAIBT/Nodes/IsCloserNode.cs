using System;
using UnityEngine;

class IsCloserNode : Node
{
    private Transform target;
    private Transform origin;
    private float range;

    public IsCloserNode(Transform target, Transform origin, float range)
    {
        this.target = target;
        this.origin = origin;
        this.range = range;
    }

    public override NodeState Evaluate()
    {
        state = Vector3.Distance(target.position, origin.position) < range ? NodeState.SUCCES : NodeState.FAILURE;
        return state;
    }
}
