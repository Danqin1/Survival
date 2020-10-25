using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class HasAgroNode : Node
{
    private ZombieAI zombieAI;

    public HasAgroNode(ZombieAI zombieAI)
    {
        this.zombieAI = zombieAI;
    }

    public override NodeState Evaluate()
    {
        return zombieAI.HasAgro ? NodeState.SUCCES : NodeState.FAILURE;
    }
}
