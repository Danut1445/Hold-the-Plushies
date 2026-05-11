using System;
using System.Collections.Generic;
using UnityEngine;

public class PlushyOfficerScript : UpgradableTurretScript
{
    private LinkedList<GameObject> turretsBuffed;
    private int damageBuff;
    private int reloadBuff;
    private float range;
    private Vector2 officerLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretsBuffed = new LinkedList<GameObject>();
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");

        foreach (GameObject turret in turrets)
        {
            if (!CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), 0.0f))
            {
                continue;
            }

            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().AddOfficer(gameObject.transform.parent.gameObject);
            turretsBuffed.AddLast(turret);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool CheckInRange(GameObject turret, float addedRange)
    {
        if (Vector2.Distance(turret.GetComponent<TurretBasicScript>().GetTurretLocation(), officerLocation) > range + addedRange)
        {
            return false;
        }
        return true;
    }

    public void BuffTurret(GameObject turret)
    {

    }

    public void UnbuffTurret(GameObject turret)
    {

    }

    public override void UpgradeTurret(int value1, float value2)
    {
        foreach (GameObject turret in turretsBuffed)
        {
            UnbuffTurret(turret);
        }

        reloadBuff += value1;
        range += value2;

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            if (! (CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), 0.0f) ^ CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), -value2)))
            {
                continue;
            }

            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().AddOfficer(gameObject.transform.parent.gameObject);
            turretsBuffed.AddLast(turret);
        }

        foreach (GameObject turret in turretsBuffed)
        {
            BuffTurret(turret);
        }
    }
}
