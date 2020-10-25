using System;
using UnityEngine;

public class IsPlayerVisibleNode : Node
{
    private Transform origin;
    private float agroRadius;
    private float fieldOfViewAngle;
    private LayerMask playerLayer;

    public IsPlayerVisibleNode(Transform origin, float agroRadius, float fieldOfViewAngle, LayerMask playerLayer)
    {
        this.origin = origin;
        this.agroRadius = agroRadius;
        this.fieldOfViewAngle = fieldOfViewAngle;
        this.playerLayer = playerLayer;
    }

    public override NodeState Evaluate()
    {
        if(Vector3.Distance(origin.position, Player.instance.transform.position) < agroRadius)
        {
            Vector3 directionToPlayer = Player.instance.transform.position = origin.transform.position;

            if(Vector3.Angle(origin.transform.forward, directionToPlayer) < fieldOfViewAngle)
            {
                RaycastHit hit;
                if(Physics.Raycast(origin.transform.position, directionToPlayer, out hit, agroRadius, playerLayer))
                {
                    if(hit.transform.gameObject.GetComponent<Player>() != null) 
                    {
                        state = NodeState.SUCCES;
                        return state;
                    }
                }
            }
        }

        state = NodeState.FAILURE;
        return state;
    }
}
