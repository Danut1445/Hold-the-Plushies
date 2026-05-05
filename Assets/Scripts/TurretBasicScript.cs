using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretBasicScript : MonoBehaviour
{
    public int bulletDamage;
    public float bulletSpeed;
    public Sprite bulletSprite;
    public float range;
    public GameObject bullet;
    public float reloadTime;
    private GameObject currentTarget;
    private float timeSinceLastShot;
    private bool loaded;
    private bool activated;
    private Vector2 turretLocation;
    private Queue<GameObject> targets;
    private bool wasInRange;
    private Rigidbody2D turretBody;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timeSinceLastShot = 0.0f;
        currentTarget = null;
        loaded = true;
        turretLocation = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
        targets = new Queue<GameObject>();
        activated = true;
        wasInRange = false;
        turretBody = gameObject.GetComponent<Rigidbody2D>();

        GameObject[] plushies = GameObject.FindGameObjectsWithTag("EnemyPlushy");
        BallonComparableDistance[] comparablePlushies = new BallonComparableDistance[plushies.Length];
        for (int i = 0; i < plushies.Length; i++)
        {
            comparablePlushies[i] = new BallonComparableDistance(plushies[i], turretLocation);
        }
        Array.Sort(comparablePlushies);

        foreach (BallonComparableDistance plushy in comparablePlushies)
        {
            targets.Enqueue(plushy.getPlushy());
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            return;
        }

        if (currentTarget == null)
        {
            turretBody.rotation = 0;
            if (targets.Count == 0)
            {
                return;
                activated = false;
                loaded = true;
            }

            currentTarget = targets.Dequeue();
            while (currentTarget == null && targets.Count > 0)
            {
                currentTarget = targets.Dequeue();
            }

            if (targets.Count == 0)
            {
                return;
                activated = false;
                loaded = true;
            }

            GameObject nextCandidate;
            while (targets.Count > 0)
            {
                nextCandidate = targets.Peek();
                if (nextCandidate == null)
                {
                    targets.Dequeue();
                    continue;
                }

                float distance1 = Vector2.Distance(new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.y), turretLocation);
                float distance2 = Vector2.Distance(new Vector2(nextCandidate.transform.position.x, nextCandidate.transform.position.y), turretLocation);
                if (distance2 >= distance1)
                {
                    break;
                }
                currentTarget = targets.Dequeue();
            }
            wasInRange = false;
        } 
        else
        {
            float distanceToTarget = Vector2.Distance(new Vector2(currentTarget.transform.position.x, currentTarget.transform.position.y), turretLocation);
            if (distanceToTarget <= range)
            {
                wasInRange = true;
            }
            
            if (distanceToTarget > range && wasInRange == true)
            {
                currentTarget = null;
                wasInRange = false;
                return;
            }

            Vector2 heading = new Vector2(currentTarget.transform.position.x - turretLocation.x, currentTarget.transform.position.y - turretLocation.y).normalized;
            float angle = Vector2.Angle(new Vector2(0, 1), heading);
            if (turretLocation.x < currentTarget.transform.position.x)
            {
                angle = angle * -1;
            }
            turretBody.rotation = angle - 90;

            if (!loaded)
            {
                timeSinceLastShot += Time.deltaTime;
                if (timeSinceLastShot >= reloadTime)
                {
                    loaded = true;
                }
                return;
            }

            if (wasInRange == true && loaded)
            {
                loaded = false;
                Vector2 speed = bulletSpeed * heading;
                GameObject bulletFired = GameObject.Instantiate(bullet, gameObject.transform.position, Quaternion.identity);
                bulletFired.GetComponent<BulletScript>().setBullet(speed, bulletSprite, bulletDamage, range / speed.magnitude, angle);
                timeSinceLastShot = 0;
            }
        }
    }

    public void addTarget(GameObject target)
    {
        targets.Enqueue(target);
    }
}
