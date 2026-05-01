using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallonMovement
{
    private Rigidbody2D ballonRigidBody = null;
    private LinkedList<Vector2> checkpoints;
    private LinkedListNode<Vector2> lastTarget, currentTarget;
    private float currentProcentRoad, speedProcentRoad;
    private bool arrived = false;
    private float speed;

    public void initializeMovement(Rigidbody2D ballonBody, float speed)
    {
        this.ballonRigidBody = ballonBody;
        this.speed = speed;
        checkpoints = GameObject.FindGameObjectWithTag("Logic").GetComponent<LoadLevelLogic>().GetCheckpoints();
        lastTarget = checkpoints.First;
        currentTarget = lastTarget.Next;
        ballonRigidBody.position = lastTarget.Value;
        currentProcentRoad = 0.0f;
        speedProcentRoad = 1.0f / (Vector2.Distance(lastTarget.Value, currentTarget.Value) / speed);
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
                speedProcentRoad = 1.0f / (Vector2.Distance(lastTarget.Value, currentTarget.Value) / speed);
            }
            ballonRigidBody.position = (1.0f - currentProcentRoad) * lastTarget.Value + currentProcentRoad * currentTarget.Value;
        }
    }
}
