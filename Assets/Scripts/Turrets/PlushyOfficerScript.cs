using System;
using System.Collections.Generic;
using UnityEngine;

public class PlushyOfficerScript : UpgradableTurretScript
{
    private LinkedList<GameObject> turretsBuffed;
    private int damageBuff;
    private int reloadBuff;
    private float range = 4;
    private Vector2 officerLocation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        turretsBuffed = new LinkedList<GameObject>();
        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        officerLocation = new Vector2(gameObject.transform.parent.position.x, gameObject.transform.parent.position.y);
        reloadBuff = 10;
        damageBuff = 30;

        foreach (GameObject turret in turrets)
        {
            if (!CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), 0.0f))
            {
                continue;
            }

            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().AddOfficer(gameObject.transform.parent.gameObject, this);
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
        TurretBasicScript turretScript = turret.transform.GetChild(0).gameObject.GetComponent<TurretBasicScript>();
        int damage = turretScript.GetDamage();
        float reload = turretScript.GetReload();

        damage = damage * (100 + damageBuff) / 100;
        reload = reload * (100 - reloadBuff) / 100;

        turretScript.SetDamage(damage);
        turretScript.SetReload(reload);
    }

    public void UnbuffTurret(GameObject turret)
    {
        TurretBasicScript turretScript = turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>();
        int damage = turretScript.GetDamage();
        float reload = turretScript.GetReload();

        damage = damage * 100 / (100 + damageBuff);
        reload = reload * 100 / (100 - reloadBuff);

        turretScript.SetDamage(damage);
        turretScript.SetReload(reload);
    }

    public override void UpgradeTurret(int value1, float value2)
    {
        foreach (GameObject turret in turretsBuffed)
        {
            if (turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().GetSupremeOfficer() == gameObject.transform.parent.gameObject)
            {
                UnbuffTurret(turret);
                turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().UpgradeSupremeOfficer();
            }
        }

        reloadBuff += value1;
        range += value2;
        gameObject.transform.parent.gameObject.GetComponent<TurretLogicScript>().SetRange(range);

        GameObject[] turrets = GameObject.FindGameObjectsWithTag("Turret");
        foreach (GameObject turret in turrets)
        {
            if (! (CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), 0.0f) ^ CheckInRange(turret.GetComponent<TurretLogicScript>().GetTurretObject(), -value2)))
            {
                continue;
            }

            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().AddOfficer(gameObject.transform.parent.gameObject, this);
            UnbuffTurret(turret);
            turretsBuffed.AddLast(turret);
        }

        foreach (GameObject turret in turretsBuffed)
        {
            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().NewSupremeOfficer(gameObject.transform.parent.gameObject);
            if (turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().GetSupremeOfficer() == gameObject.transform.parent.gameObject)
            {
                BuffTurret(turret);
            }
        }
    }

    public override void RemoveTurret()
    {
        foreach (GameObject turret in turretsBuffed)
        {
            turret.GetComponent<TurretLogicScript>().GetTurretObject().GetComponent<TurretBasicScript>().RemoveOfficer(gameObject.transform.parent.gameObject);
        }
    }

    public override float GetRange()
    {
        return range;
    }

    public void RemoveBuffedTurret(GameObject turret)
    {
        turretsBuffed.Remove(turretsBuffed.Find(turret));
    }

    public void AddNewTurret(GameObject turret)
    {
        turretsBuffed.AddLast(turret);
    }
}
