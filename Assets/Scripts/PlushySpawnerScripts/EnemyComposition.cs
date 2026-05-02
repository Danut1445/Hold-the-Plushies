using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyComposition
{
    public List<WaveClass> waves;

    public EnemyComposition(int numberWaves)
    {
        waves = new List<WaveClass>(numberWaves);
    }

    public List<WaveClass> GetWavesForLevel()
    {
        return waves;
    }

    public void AddWave(WaveClass wave)
    {
        waves.Add(wave);
    }

    public void getPlushyAssets()
    {
        foreach (WaveClass wave in waves)
        {
            wave.getPlushyAssets();
        }
    }
}
