[System.Serializable]
public abstract class Node
{
    public NodeState state { get; protected set; } = NodeState.FAILURE;
    public abstract NodeState Evaluate();
}

public enum NodeState
{
    RUNNING, SUCCES, FAILURE
}