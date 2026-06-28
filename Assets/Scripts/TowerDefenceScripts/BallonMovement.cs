using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallonMovement
{
    private Rigidbody2D ballonRigidBody = null;
    private LinkedList<CheckpointScript> checkpoints;
    private LinkedListNode<CheckpointScript> lastTarget, currentTarget;
    private float currentProcentRoad, speedProcentRoad;
    private bool arrived = false;
    private float speed;

    public void initializeMovement(Rigidbody2D ballonBody, float speed, int divisionID)
    {
        this.ballonRigidBody = ballonBody;
        this.speed = speed;
        checkpoints = GameObject.FindGameObjectWithTag("Logic").GetComponent<LoadLevelLogic>().GetCheckpoints();
        checkpoints = new LinkedList<CheckpointScript>(checkpoints.Where<CheckpointScript>(checkopoint => checkopoint.divisionID == divisionID).ToList<CheckpointScript>());
        lastTarget = checkpoints.First;
        currentTarget = lastTarget.Next;
        ballonRigidBody.position = lastTarget.Value.GetCoordonates();
        currentProcentRoad = 0.0f;
        speedProcentRoad = 1.0f / (Vector2.Distance(lastTarget.Value.GetCoordonates(), currentTarget.Value.GetCoordonates()) / speed);
    }

    public void move()
    {
        if (!arrived)
        {
            currentProcentRoad += Time.deltaTime * speedProcentRoad;
            if (currentProcentRoad >= 1.0f)
            {
                currentProcentRoad = 0.0f;
                lastTarget = currentTarget;
                if (currentTarget.Next == null)
                {
                    arrived = true;
                    return;
                }
                currentTarget = lastTarget.Next;
                speedProcentRoad = 1.0f / (Vector2.Distance(lastTarget.Value.GetCoordonates(), currentTarget.Value.GetCoordonates()) / speed);
            }
            ballonRigidBody.position = (1.0f - currentProcentRoad) * lastTarget.Value.GetCoordonates() + currentProcentRoad * currentTarget.Value.GetCoordonates();
        }
    }

    public Vector2 GetNextCheckpoint()
    {
        return currentTarget.Value.GetCoordonates();
    }
}
