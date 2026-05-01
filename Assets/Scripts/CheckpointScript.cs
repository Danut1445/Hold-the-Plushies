using System;
using System.Collections;
using UnityEngine;

public class CheckpointScript : MonoBehaviour, IComparable
{
    public int order;
    private Vector3 coordonates;
    private GameObject[] checkpoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool foundPredecesor = false;
        CheckpointScript currentChecpoint = null;

        coordonates = gameObject.transform.position;
        checkpoints = GameObject.FindGameObjectsWithTag("CheckpointTag");

        foreach (GameObject checkpoint in checkpoints)
        {
            currentChecpoint = checkpoint.GetComponent<CheckpointScript>();
            if (currentChecpoint.order == order && currentChecpoint != this)
            {
                Debug.LogError("We have duplicate order for checkpoints: " + order + " with coordonates " + coordonates);
            }

            if (currentChecpoint.order == order - 1)
            {
                foundPredecesor = true;
            }
        }

        if (!foundPredecesor && order != 0)
        {
            Debug.LogError("We do not have a predecesor for checkpoint: " + order + " with coordonates " + coordonates);
        }
    }

    public Vector2 GetCoordonates()
    {
        return new Vector2(coordonates.x, coordonates.y);
    }

    int IComparable.CompareTo(object otherCheckpoint)
    {
        return this.order - ((CheckpointScript)otherCheckpoint).order;
    }
}
