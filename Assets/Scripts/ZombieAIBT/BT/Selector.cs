using System.Collections.Generic;

class Selector : Node
{
    protected List<Node> nodes = new List<Node>();
    public Selector(List<Node> nodes)
    {
        this.nodes = nodes;
    }

    public override NodeState Evaluate()
    {
        foreach (var node in nodes)
        {
            switch (node.Evaluate())
            {
                case NodeState.RUNNING:
                    state = NodeState.RUNNING;
                    return state;

                case NodeState.SUCCES:
                    state = NodeState.SUCCES;
                    return state;

                case NodeState.FAILURE:
                    break;
                default:
                    break;
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
