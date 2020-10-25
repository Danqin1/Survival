public class WasHittedNode : Node
{
    private ZombieAI zombieAI;

    public WasHittedNode(ZombieAI zombieAI)
    {
        this.zombieAI = zombieAI;
    }

    public override NodeState Evaluate()
    {
        state = zombieAI.Hitted ? NodeState.SUCCES : NodeState.FAILURE;
        return state;
    }
}
