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
        PlushFactoryScript plushyFactory = (PlushFactoryScript) PlayerStats.GetBuilding(1);
        if (plushyFactory == null)
        {
            level = 0;
            maxLevel = 5;
            cost = 100;
            ID = 1;
            populationInput = 10;
            plushOutput = 50;
            fulfilment = 0;
            upgradeAmmount = 35;
            isActive = false;
        } else
        {
            this.level = plushyFactory.level;
            this.maxLevel = plushyFactory.maxLevel;
            this.cost = plushyFactory.cost;
            this.populationInput = plushyFactory.populationInput;
            this.plushOutput = plushyFactory.plushOutput;
            this.isActive = plushyFactory.isActive;
        }
        PlayerStats.AddBuilding(this);

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetImage(level);
        if (isActive)
        {
            UIScript.CreateBuilding();
            UIScript.SetFulfilment(fulfilment);
            UIScript.SetInput1(populationInput);
            UIScript.SetOutput1(plushOutput);
            UIScript.SetUpgradeCost(cost);
            UIScript.SetLevel(level, maxLevel);
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

        if (population < populationInput)
        {
            double fulfilmentDouble = (float)population / (float)populationInput * 100f;
            fulfilment = (int) Math.Round(fulfilmentDouble);
            UIScript.SetInput1((int) (fulfilmentDouble * populationInput / 100));
            UIScript.SetOutput1((int) (plushOutput * fulfilmentDouble / 100));
            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ProducePlush(plushOutput * fulfilment / 100);
            return 0;
        }

        fulfilment = 100;
        UIScript.SetInput1(populationInput);
        UIScript.SetOutput1(plushOutput);

        PlayerStats.ProducePlush(plushOutput);

        return population - populationInput;
    }
}
