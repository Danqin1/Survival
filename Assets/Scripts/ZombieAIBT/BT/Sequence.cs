using System.Collections.Generic;

class Sequence : Node
{
    protected List<Node> nodes = new List<Node>();

    public Sequence(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        bool isAnyNodeRunning = false;
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    isAnyNodeRunning = true;
                    break;

                case NodeState.SUCCES:
                    break;

                case NodeState.FAILURE:
                    state = NodeState.FAILURE;
                    return state;

                default:
                    break;
            }
        }

        state = isAnyNodeRunning ? NodeState.RUNNING : NodeState.SUCCES;
        return state;
    }
}
