using System;
using UnityEngine;

[Serializable]
public class PlushyPlatoon
{
    private GameObject plushy;
    public int count;
    public float interval;
    public float timer;
    public int enemyID;

    public PlushyPlatoon(int enemyID, int count, float interval)
    {
        this.enemyID = enemyID;
        this.count = count;
        this.interval = interval;
        this.timer = interval;
    }

    public void getPlushyAssets()
    {
        plushy = GameObject.FindGameObjectWithTag("Logic").GetComponent<LevelLogicScript>().enemyPrefabs[enemyID];
    }

    public bool spawnEnemy(float timePassed, Vector3 location)
    {
        timer -= timePassed;
        if (timer <= 0)
        {
            GameObject.Instantiate(this.plushy, location, Quaternion.identity);
            count--;
            timer = interval;
            if (count <= 0)
            {
                return true;
            }
        }
        return false;
    }
}
