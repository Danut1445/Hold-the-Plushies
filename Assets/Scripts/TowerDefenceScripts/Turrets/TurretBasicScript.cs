using System;
using System.Collections.Generic;
using UnityEngine;

public class TurretBasicScript : UpgradableTurretScript
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
    private LinkedList<GameObject> officers;
    private GameObject supremeOfficer;
    private int supremeOfficerLevel;

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

        officers = new LinkedList<GameObject>();
        GameObject[] officersArray = GameObject.FindGameObjectsWithTag("Leader");
        supremeOfficerLevel = 0;

        foreach (GameObject officer in officersArray)
        {
            PlushyOfficerScript plushyOfficer = officer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>();
            if (!plushyOfficer.CheckInRange(gameObject, 0.0f))
            {
                continue;
            }

            officers.AddLast(officer);
            plushyOfficer.AddNewTurret(gameObject.transform.parent.gameObject);
            if (officer.GetComponent<TurretLogicScript>().GetLevel() > supremeOfficerLevel)
            {
                if (supremeOfficer != null)
                {
                    supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
                }
                supremeOfficer = officer;
                supremeOfficerLevel = officer.GetComponent<TurretLogicScript>().GetLevel();
                SetSupremeOfficerMark();
                plushyOfficer.BuffTurret(gameObject.transform.parent.gameObject);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!activated)
        {
            turretBody.rotation = 0;
            return;
        }

        if (currentTarget == null)
        {
            turretBody.rotation = 0;
            if (targets.Count == 0)
            {
                return;
            }

            currentTarget = targets.Dequeue();
            while (currentTarget == null && targets.Count > 0)
            {
                currentTarget = targets.Dequeue();
            }

            if (targets.Count == 0)
            {
                return;
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

    public void AddTarget(GameObject target)
    {
        targets.Enqueue(target);
    }

    public void AddOfficer(GameObject officer, PlushyOfficerScript officerScript)
    {
        officers.AddLast(officer);
        if (officer.GetComponent<TurretLogicScript>().GetLevel() > supremeOfficerLevel)
        {
            if (supremeOfficer != null)
            {
                supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
            }
            supremeOfficerLevel = officer.GetComponent<TurretLogicScript>().GetLevel();
            supremeOfficer = officer;
            SetSupremeOfficerMark();
            officerScript.BuffTurret(gameObject.transform.parent.gameObject);
        }
    }

    public void RemoveOfficer(GameObject officer)
    {
        officers.Remove(officers.Find(officer));
        if (officer == supremeOfficer)
        {
            supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
            supremeOfficer = null;
            supremeOfficerLevel = 0;
            ResetSupremeOfficerMark();
            foreach (GameObject newSupreme in officers)
            {
                if (newSupreme.GetComponent<TurretLogicScript>().GetLevel() > supremeOfficerLevel)
                {
                    if (supremeOfficer != null)
                    {
                        supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
                    }
                    supremeOfficer = newSupreme;
                    supremeOfficerLevel = newSupreme.GetComponent<TurretLogicScript>().GetLevel();
                    supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().BuffTurret(gameObject.transform.parent.gameObject);
                    SetSupremeOfficerMark();
                }
            }
        }
    }

    public Vector2 GetTurretLocation()
    {
        return turretLocation;
    }

    public override void UpgradeTurret(int value1, float value2)
    {
        if (supremeOfficer != null)
        {
            supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
        }

        bulletDamage += value1;
        reloadTime -= value2;

        if (supremeOfficer != null)
        {
            supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().BuffTurret(gameObject.transform.parent.gameObject);
        }
    }

    public override void RemoveTurret()
    {
        foreach (GameObject officer in officers)
        {
            officer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().RemoveBuffedTurret(gameObject.transform.parent.gameObject);
        }
    }

    public override float GetRange()
    {
        return range;
    }

    public GameObject GetSupremeOfficer()
    {
        return supremeOfficer;
    }

    public void UpgradeSupremeOfficer()
    {
        supremeOfficerLevel += 1;
    }

    public void NewSupremeOfficer(GameObject officer)
    {
        if (officer.GetComponent<TurretLogicScript>().GetLevel() > supremeOfficerLevel)
        {
            if (supremeOfficer != null)
            {
                supremeOfficer.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<PlushyOfficerScript>().UnbuffTurret(gameObject.transform.parent.gameObject);
            }
            supremeOfficer = officer;
            supremeOfficerLevel = officer.GetComponent<TurretLogicScript>().GetLevel();
            SetSupremeOfficerMark();
        }
    }

    public int GetDamage()
    {
        return bulletDamage;
    }

    public void SetDamage(int damage)
    {
        bulletDamage = damage;
    }

    public float GetReload()
    {
        return reloadTime;
    }

    public void SetReload(float reload)
    {
        reloadTime = reload;
    }

    public void SetSupremeOfficerMark()
    {
        GameObject supremeOfficerMark = gameObject.transform.parent.GetChild(1).GetChild(5).gameObject;
        supremeOfficerMark.transform.position = supremeOfficer.transform.position;
        supremeOfficerMark.SetActive(true);
    }

    public void ResetSupremeOfficerMark()
    {
        GameObject supremeOfficerMark = gameObject.transform.parent.GetChild(1).GetChild(5).gameObject;
        supremeOfficerMark.transform.position = new Vector3(0, 0, 0);
        supremeOfficerMark.SetActive(false);
    }
}
