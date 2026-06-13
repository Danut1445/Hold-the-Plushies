using UnityEngine;
using System;

public class PlushFactoryScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;
    private TownLogicScript townLogicScript;
    private int populationInput;
    private int plushOutput;

    void Start()
    {
        BuildingSaveScript savedFactory = PlayerStats.GetSavedBuilding(1);
        if (savedFactory == null)
        {
            level = 0;
            maxLevel = 5;
            cost = 100;
            ID = 1;
            populationInput = 10;
            plushOutput = 100;
            fulfilment = 0;
            upgradeAmmount = 35;
            isActive = false;
        } else
        {
            this.LoadFromSave(savedFactory);
        }
        PlayerStats.AddBuilding(this);

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetImage(level);
        if (level > 0)
        {
            UIScript.CreateBuilding();
            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(populationInput);
            UIScript.SetOutput1(plushOutput);
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
            UIScript.SetInput1(populationInput);
            UIScript.SetOutput1(plushOutput);
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
            populationInput += populationInput * upgradeAmmount / 100;
            plushOutput += plushOutput * upgradeAmmount / 100;
            cost = cost * 2;

            if (level >= maxLevel)
            {
                UIScript.SetUpgradeButton(false);
                cost = 0;
            }

            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(populationInput);
            UIScript.SetOutput1(plushOutput);
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
        if (!isActive) {
            return population;
        }

        float fulfilmentFloat = CheckEnoughResources(population, populationInput, 100f);
        if (fulfilmentFloat < 100f)
        {
            fulfilment = (int) Math.Round(fulfilmentFloat);
            UIScript.SetInput1((int) (fulfilmentFloat * populationInput / 100));
            UIScript.SetOutput1((int) (plushOutput * fulfilmentFloat / 100));
            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ProducePlush(plushOutput * fulfilment / 100);
            return 0;
        }

        fulfilment = 100;
        UIScript.SetInput1(populationInput);
        UIScript.SetOutput1(plushOutput);
        UIScript.SetFulfilment(fulfilment);

        PlayerStats.ProducePlush(plushOutput);

        return population - populationInput;
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
        ID = 1;
        populationInput = building.GetInput1();
        plushOutput = building.GetOutput1();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public override void ResetBuilding()
    {
        BuildingSaveScript savedBuilding = PlayerStats.GetSavedBuilding(ID);
        LoadFromSave(savedBuilding);
        UIScript.SetInput1(populationInput);
        UIScript.SetOutput1(plushOutput);
    }

    public int GetPopulationInput()
    {
        return populationInput;
    }

    public int GetPlushOutput()
    {
        return plushOutput;
    }
}
