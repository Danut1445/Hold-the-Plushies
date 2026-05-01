using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlushySpawnerScript : MonoBehaviour
{
    private float timerBetweenWaves;
    public GameObject plushy;
    private LinkedList<WaveClass> waves = new LinkedList<WaveClass>();
    private WaveClass currentWave;
    private Vector3 location;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timerBetweenWaves = 5;
        WaveClass firstWave = new WaveClass();
        GameObject[] enemyPrefabs = GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>().enemyPrefabs;
        firstWave.wave.AddLast(new PlushyPlatoon(enemyPrefabs[0], 10, 2));
        firstWave.wave.AddLast(new PlushyPlatoon(enemyPrefabs[0], 5, 3));

        waves.AddLast(firstWave);

        currentWave = waves.First.Value;
        location = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        timerBetweenWaves -= Time.deltaTime;
        if (timerBetweenWaves <= 0)
        {
            timerBetweenWaves = 5;
            Instantiate(plushy, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0), Quaternion.identity);
        }
        */

        if (currentWave.spawnEnemies(Time.deltaTime, location))
        {
            waves.RemoveFirst();
            if (waves.Count > 0)
            {
                currentWave = waves.First.Value;
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
