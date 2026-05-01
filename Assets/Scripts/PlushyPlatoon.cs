using UnityEngine;

public class PlushyPlatoon
{
    public GameObject plushy;
    public int count;
    public float interval;
    public float timer;

    public PlushyPlatoon(GameObject plushy, int count, float interval)
    {
        this.plushy = plushy;
        this.count = count;
        this.interval = interval;
        this.timer = interval;
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
