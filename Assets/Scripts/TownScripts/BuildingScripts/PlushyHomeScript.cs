using UnityEngine;
using System;

public class PlushyHomeScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;
    private TownLogicScript townLogicScript;
    private int plushyInput;
    private int housingOutput;

    void Start()
    {
        BuildingSaveScript savedHouse = PlayerStats.GetSavedBuilding(3);
        if (savedHouse == null)
        {
            level = 0;
            maxLevel = 5;
            cost = 50;
            ID = 3;
            plushyInput = 20;
            housingOutput = 50;
            fulfilment = 0;
            upgradeAmmount = 50;
            isActive = false;
        }
        else
        {
            this.LoadFromSave(savedHouse);
        }
        PlayerStats.AddBuilding(this);

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetImage(level);
        if (level > 0)
        {
            UIScript.CreateBuilding();
            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(plushyInput);
            UIScript.SetOutput1(housingOutput);
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
            UIScript.SetInput1(plushyInput);
            UIScript.SetOutput1(housingOutput);
            UIScript.SetUpgradeCost(cost);
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
            plushyInput += plushyInput * upgradeAmmount / 100;
            housingOutput += housingOutput * upgradeAmmount / 100;
            cost = cost * 2;

            if (level >= maxLevel)
            {
                UIScript.SetUpgradeButton(false);
                cost = 0;
            }

            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(plushyInput);
            UIScript.SetOutput1(housingOutput);
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
            PlayerStats.ChangeReputation(-5f);
            return population;
        }

        float fulfilmentFloat = CheckEnoughResources(PlayerStats.GetPlush(), plushyInput, 100f);
        int realHousing;
        if (fulfilmentFloat < 100f)
        {
            fulfilment = (int)Math.Round(fulfilmentFloat);
            UIScript.SetInput1((int)(fulfilmentFloat * plushyInput / 100));
            realHousing = (int)(housingOutput * fulfilmentFloat / 100);
            UIScript.SetOutput1(realHousing);
            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ConsumePlush((int)(fulfilmentFloat * plushyInput / 100));
        }
        else
        {

            fulfilment = 100;
            UIScript.SetInput1(plushyInput);
            UIScript.SetOutput1(housingOutput);
            realHousing = housingOutput;
            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ConsumePlush(plushyInput);
        }

        PlayerStats.ChangeReputation(-5f * Math.Max((PlayerStats.GetPopulation() - realHousing) / PlayerStats.GetPopulation(), -0.25f));
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
        maxLevel = 5;
        cost = building.GetCost();
        ID = 3;
        plushyInput = building.GetInput1();
        housingOutput = building.GetOutput1();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public override void ResetBuilding()
    {
        BuildingSaveScript savedBuilding = PlayerStats.GetSavedBuilding(ID);
        LoadFromSave(savedBuilding);
        UIScript.SetInput1(plushyInput);
        UIScript.SetOutput1(housingOutput);
    }

    public int GetPlushyInput()
    {
        return plushyInput;
    }

    public int GetHousingOutput()
    {
        return housingOutput;
    }
}
