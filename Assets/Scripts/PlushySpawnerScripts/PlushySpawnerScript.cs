using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlushySpawnerScript : MonoBehaviour
{
    private float timerBetweenWaves;
    public GameObject plushy;
    private List<WaveClass> waves;
    private WaveClass currentWave;
    private Vector3 location;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerBetweenWaves = 5;
        string json = WaveManager.LoadJSONFromFile(".\\Assets\\Level JSONs\\Level1.json");
        Debug.Log(json);
        EnemyComposition enemyComposition = JsonUtility.FromJson<EnemyComposition>(json);
        enemyComposition.getPlushyAssets();
        waves = enemyComposition.GetWavesForLevel();

        currentWave = waves[0];
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWave.spawnEnemies(Time.deltaTime, location))
        {
            waves.Remove(waves[0]);
            if (waves.Count > 0)
            {
                currentWave = waves[0];
            } else
            {
                currentWave = null;
            }
        }

        if (currentWave == null)
        {
            Destroy(gameObject);
        }
    }
}
