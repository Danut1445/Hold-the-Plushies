using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WaveClass
{
    public List<PlushyPlatoon> wave;

    public WaveClass(int numberPlatoons)
    {
        wave = new List<PlushyPlatoon>(numberPlatoons);
    }

    public void getPlushyAssets()
    {
        foreach (PlushyPlatoon platoon in wave)
        {
            platoon.getPlushyAssets();
        }
    }

    public bool spawnEnemies(float timePassed, Vector3 location)
    {
        PlushyPlatoon currentPlatoon;
        for (int i = 0; i < wave.Count; i++)
        {
            currentPlatoon = wave[i];
            if (currentPlatoon.spawnEnemy(timePassed, location))
            {
                wave.Remove(currentPlatoon);
                i--;
            }
        }
        if (wave.Count == 0)
        {
            return true;
        }
        return false;
    }
}
