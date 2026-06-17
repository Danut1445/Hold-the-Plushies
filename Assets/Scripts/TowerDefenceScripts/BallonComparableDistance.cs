using System;
using System.Collections;
using UnityEngine;

public class BallonComparableDistance : IComparable
{
    public float distance;
    private GameObject ballon;

    public BallonComparableDistance(GameObject plushy, Vector2 turretLocation)
    {
        ballon = plushy;
        distance = Vector2.Distance(turretLocation, new Vector2(plushy.transform.position.x, plushy.transform.position.y));
    }

    public GameObject getPlushy()
    {
        return ballon;
    }

    int IComparable.CompareTo(object otherPlushy)
    {
        float comparableDistance = this.distance - ((BallonComparableDistance)otherPlushy).distance;
        if (comparableDistance > 0)
        {
            return 1;
        }
        else if (comparableDistance == 0)
        {
            return 0;
        }
        return -1;
    }
}
