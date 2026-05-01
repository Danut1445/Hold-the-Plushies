using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveClass
{
    public LinkedList<PlushyPlatoon> wave = new LinkedList<PlushyPlatoon>();

    public bool spawnEnemies(float timePassed, Vector3 location)
    {
        LinkedListNode<PlushyPlatoon> currentPlatoon = wave.First;
        LinkedListNode<PlushyPlatoon> nextPlatoon;
        for (int i = 0; i < wave.Count; i++)
        {
            nextPlatoon = currentPlatoon.Next;
            if (currentPlatoon.Value.spawnEnemy(timePassed, location))
            {
                wave.Remove(currentPlatoon);
            }
            currentPlatoon = nextPlatoon;
        }
        if (wave.Count == 0)
        {
            return true;
        }
        return false;
    }
}
