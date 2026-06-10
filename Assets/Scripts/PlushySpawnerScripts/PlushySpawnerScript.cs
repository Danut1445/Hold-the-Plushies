using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PlushySpawnerScript : MonoBehaviour
{
    public float timeBetweenWaves;
    public GameObject plushy;

    private List<WaveClass> waves;
    private WaveClass currentWave;
    private Vector3 location;
    private bool spawnWave;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeBetweenWaves = 5;
        string json = WaveManager.LoadJSONFromFile(".\\Assets\\Level JSONs\\Level1.json");
        Debug.Log(json);
        EnemyComposition enemyComposition = JsonUtility.FromJson<EnemyComposition>(json);
        enemyComposition.getPlushyAssets();
        waves = enemyComposition.GetWavesForLevel();
        spawnWave = false;

        currentWave = waves[0];
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (!spawnWave)
        {
            return;
        }

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
            spawnWave = false;
            GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>().FinnishedSpawningWave();
        }

        if (currentWave == null)
        {
            GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>().FinnishedLevel();
            Destroy(gameObject);
        }
    }
    
    public int GetRemainingWaves()
    {
        return waves.Count;
    }

    public void StartSpwaningNextWave()
    {
        spawnWave = true;
    }
}
