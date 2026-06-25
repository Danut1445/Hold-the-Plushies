using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class TownHallScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;
    private TownLogicScript townLogicScript;
    private int plushInput;
    private int timer;

    void Start()
    {
        BuildingSaveScript savedHall = PlayerStats.GetSavedBuilding(6);
        if (savedHall == null)
        {
            level = 0;
            maxLevel = 1;
            cost = 200;
            ID = 6;
            plushInput = 10;
            timer = 0;
            fulfilment = 0;
            upgradeAmmount = 0;
            isActive = false;
        }
        else
        {
            this.LoadFromSave(savedHall);
        }
        PlayerStats.AddBuilding(this);

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetUpgradeButton(false);
        UIScript.SetImage(level);
        if (level > 0)
        {
            UIScript.CreateBuilding();
            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(plushInput);
            UIScript.SetOutput1(timer);
            UIScript.SetUpgradeCost(cost);
            UIScript.SetLevel(level, maxLevel);
            if (!isActive)
            {
                this.DeactivateBuilding();
            }
        }

        townLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<TownLogicScript>();
    }

    public override void CreateBuilding()
    {
        int plushNeeded = cost / 5 * 4;
        int leatherNeeded = cost - plushNeeded;

        if (PlayerStats.CheckEnoughLeather(leatherNeeded) && PlayerStats.CheckEnoughPlush(plushNeeded))
        {
            PlayerStats.ConsumeLeather(leatherNeeded);
            PlayerStats.ConsumePlush(plushNeeded);

            level = 1;
            UIScript.SetLevel(level, maxLevel);
            fulfilment = 100;
            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(plushInput);
            UIScript.SetOutput1(timer);
            UIScript.SetUpgradeCost(0);
            isActive = true;
            UIScript.CreateBuilding();
            UIScript.SetImage(level);

            townLogicScript.UpdateUIResources();
        }
    }

    public override void UpgradeBuilding()
    {
        int plushNeeded = cost / 5 * 4;
        int leatherNeeded = cost - plushNeeded;

        if (PlayerStats.CheckEnoughLeather(leatherNeeded) && PlayerStats.CheckEnoughPlush(plushNeeded))
        {
            PlayerStats.ConsumeLeather(leatherNeeded);
            PlayerStats.ConsumePlush(plushNeeded);

            level++;
            UIScript.SetLevel(level, maxLevel);
            fulfilment = 100;
            cost = cost * 2;

            if (level >= maxLevel)
            {
                UIScript.SetUpgradeButton(false);
                cost = 0;
            }

            UIScript.SetFulfilment(fulfilment);
            UIScript.SetUpgradeCost(cost);
            UIScript.CreateBuilding();
            UIScript.SetImage(level);

            townLogicScript.UpdateUIResources();
        }
    }

    public override void DeactivateBuilding()
    {
        fulfilment = 0;
        UIScript.SetFulfilment(fulfilment);
        isActive = false;
        UIScript.SetStopButton(false);
        UIScript.SetRestartButton(true);
    }

    public override void ReactivateBuilding()
    {
        fulfilment = 100;
        UIScript.SetFulfilment(fulfilment);
        isActive = true;
        UIScript.SetStopButton(true);
        UIScript.SetRestartButton(false);
    }

    public override int PassDay(int population)
    {
        if (!isActive)
        {
            return population;
        }

        float fulfilmentFloat = CheckEnoughResources(PlayerStats.GetPlush(), plushInput, 100f);
        if (fulfilmentFloat < 100f)
        {
            fulfilment = (int)Math.Round(fulfilmentFloat);
            UIScript.SetInput1((int)(fulfilmentFloat * plushInput / 100));
            UIScript.SetFulfilment(fulfilment);
        } else
        {
            fulfilment = 100;
            UIScript.SetInput1(plushInput);
            UIScript.SetFulfilment(fulfilment);
            timer -= 1;
        }

        if (timer < 0)
        {
            timer = 0;
        }
        UIScript.SetOutput1(timer);
        PlayerStats.SetPolicyTimer(timer);

        return population;
    }

    public override BuildingSaveScript SaveBuilding()
    {
        BuildingSaveScript savedInfo = new BuildingSaveScript();
        savedInfo.SaveBuilding(this);
        return savedInfo;
    }

    public override void LoadFromSave(BuildingSaveScript building)
    {
        level = building.GetLevel();
        maxLevel = 1;
        cost = building.GetCost();
        ID = 6;
        plushInput = building.GetInput1();
        timer = building.GetOutput1();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public override void ResetBuilding()
    {
        BuildingSaveScript savedBuilding = PlayerStats.GetSavedBuilding(ID);
        LoadFromSave(savedBuilding);
        UIScript.SetInput1(plushInput);
        UIScript.SetOutput1(timer);
    }

    public void GoToPolicies()
    {
        PlayerStats.SaveAllBuildings();
        SceneManager.LoadScene("PolicyTree");
    }
    public int GetPlushInput()
    {
        return plushInput;
    }

    public int GetTimer()
    {
        return timer;
    }

    public void ResetTimer()
    {
        timer = 4;
        UIScript.SetOutput1(timer);
    }
}
