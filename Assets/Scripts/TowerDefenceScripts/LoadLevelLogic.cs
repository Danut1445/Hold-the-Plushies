using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadLevelLogic : MonoBehaviour
{
    private LinkedList<CheckpointScript> checkpoints = new LinkedList<CheckpointScript>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("CheckpointTag");
        CheckpointScript[] checkpointScripts = new CheckpointScript[checkpointObjects.Length];

        for (int i = 0; i < checkpointObjects.Length; i++)
        {
            checkpointScripts[i] = checkpointObjects[i].GetComponent<CheckpointScript>();
        }

        Array.Sort(checkpointScripts);
        
        foreach (CheckpointScript checkpoint in checkpointScripts)
        {
            checkpoints.AddLast(checkpoint);
        }
    }

    public LinkedList<CheckpointScript> GetCheckpoints()
    {
        return checkpoints;
    }
}
