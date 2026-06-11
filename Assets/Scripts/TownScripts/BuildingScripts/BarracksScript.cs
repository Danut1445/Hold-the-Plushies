using UnityEngine;
using System;

public class BarracksScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;
    private TownLogicScript townLogicScript;
    private int populationInput;
    private int plushyInput;
    private int leatherInput;
    private int guardsOutput;
    private int populationPowerOutput;
    private int reputationOutput;

    void Start()
    {
        BuildingSaveScript savedBarracks = PlayerStats.GetSavedBuilding(5);
        if (savedBarracks == null)
        {
            level = 0;
            maxLevel = 5;
            cost = 100;
            ID = 5;
            plushyInput = 5;
            populationInput = 5;
            leatherInput = 5;
            guardsOutput = 5;
            populationPowerOutput = -10;
            reputationOutput = -10;
            fulfilment = 0;
            upgradeAmmount = 40;
            isActive = false;
        }
        else
        {
            this.LoadFromSave(savedBarracks);
        }
        PlayerStats.AddBuilding(this);

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetImage(level);
        if (level > 0)
        {
            UIScript.CreateBuilding();
            UIScript.SetFulfilment(fulfilment);

            UIScript.SetInput1(populationInput);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(guardsOutput);
            UIScript.SetOutput2(populationPowerOutput);
            UIScript.SetOutput3(reputationOutput);

            UIScript.SetUpgradeCost(cost);
            UIScript.SetLevel(level, maxLevel);
            if (!isActive)
            {
                this.DeactivateBuilding();
            }
        }

        townLogicScript = GameObject.FindGameObjectWithTag("Logic").GetComponent<TownLogicScript>();
        townLogicScript.UpdateUIReputation();
    }

    public override void CreateBuilding()
    {
        int plushNeeded = cost / 5 * 4;
        int leatherNeeded = cost - plushNeeded;

        if (PlayerStats.CheckEnoughLeather(leatherNeeded) && PlayerStats.CheckEnoughPlush(plushNeeded))
        {
            PlayerStats.ConsumeLeather(leatherNeeded);
            PlayerStats.ConsumePlush(plushNeeded);
            PlayerStats.ChangeReputation(reputationOutput);
            PlayerStats.ChangePopulationPower(populationPowerOutput);

            level = 1;
            UIScript.SetLevel(level, maxLevel);
            fulfilment = 100;
            UIScript.SetFulfilment(fulfilment);

            UIScript.SetInput1(populationInput);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(guardsOutput);
            UIScript.SetOutput2(populationPowerOutput);
            UIScript.SetOutput3(reputationOutput);

            UIScript.SetUpgradeCost(cost);
            isActive = true;
            UIScript.CreateBuilding();
            UIScript.SetImage(level);

            townLogicScript.UpdateUIResources();
        }
        townLogicScript.UpdateUIReputation();
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
            populationInput += populationInput * upgradeAmmount / 100;
            leatherInput += leatherInput * upgradeAmmount / 100;
            guardsOutput += guardsOutput * upgradeAmmount / 100;
            if (isActive) {
                PlayerStats.ChangeReputation(reputationOutput * upgradeAmmount / 100);
                PlayerStats.ChangePopulationPower(populationPowerOutput * upgradeAmmount / 100);
            }
            reputationOutput += reputationOutput * upgradeAmmount / 100;
            populationPowerOutput += populationPowerOutput * upgradeAmmount / 100;
            cost = cost * 2;

            if (level >= maxLevel)
            {
                UIScript.SetUpgradeButton(false);
                cost = 0;
            }

            UIScript.SetFulfilment(fulfilment);

            UIScript.SetInput1(populationInput);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(guardsOutput);
            UIScript.SetOutput2(populationPowerOutput);
            UIScript.SetOutput3(reputationOutput);

            UIScript.SetUpgradeCost(cost);
            UIScript.CreateBuilding();
            UIScript.SetImage(level);

            townLogicScript.UpdateUIResources();
        }

        townLogicScript.UpdateUIReputation();
    }

    public override void DeactivateBuilding()
    {
        fulfilment = 0;
        UIScript.SetFulfilment(fulfilment);
        isActive = false;
        UIScript.SetStopButton(false);
        UIScript.SetRestartButton(true);
        PlayerStats.ChangeReputation(-reputationOutput);
        PlayerStats.ChangePopulationPower(-populationPowerOutput);
        townLogicScript.UpdateUIReputation();
    }

    public override void ReactivateBuilding()
    {
        fulfilment = 100;
        UIScript.SetFulfilment(fulfilment);
        isActive = true;
        UIScript.SetStopButton(true);
        UIScript.SetRestartButton(false);
        PlayerStats.ChangeReputation(reputationOutput);
        PlayerStats.ChangePopulationPower(populationPowerOutput);
        townLogicScript.UpdateUIReputation();
    }

    public override int PassDay(int population)
    {
        if (!isActive)
        {
            PlayerStats.SetGuards(0);
            return population;
        }

        float fulfilmentFloat = CheckEnoughResources(population, populationInput, 100f);
        fulfilmentFloat = CheckEnoughResources(PlayerStats.GetPlush(), plushyInput, fulfilmentFloat);
        fulfilmentFloat = CheckEnoughResources(PlayerStats.GetLeather(), leatherInput, fulfilmentFloat);

        if (fulfilmentFloat < 100f)
        {
            fulfilment = (int)Math.Round(fulfilmentFloat);
            UIScript.SetInput1((int)(fulfilmentFloat * populationInput / 100));
            UIScript.SetInput2((int)(fulfilmentFloat * plushyInput / 100));
            UIScript.SetInput3((int)(fulfilmentFloat * leatherInput / 100));

            int realGuards = (int)(fulfilmentFloat * guardsOutput / 100);
            UIScript.SetOutput1(realGuards);
            PlayerStats.SetGuards(realGuards);

            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ConsumePlush((int)(fulfilmentFloat * plushyInput / 100));
            PlayerStats.ConsumeLeather((int)(fulfilmentFloat * leatherInput / 100));
            return Math.Max(population - ((int)(fulfilmentFloat * populationInput / 100)), 0);
        }

        fulfilment = 100;
        UIScript.SetInput1(populationInput);
        UIScript.SetInput2(plushyInput);
        UIScript.SetInput3(leatherInput);

        UIScript.SetOutput1(guardsOutput);
        PlayerStats.SetGuards(guardsOutput);

        PlayerStats.ConsumePlush(plushyInput);
        PlayerStats.ConsumeLeather(leatherInput);
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
        ID = 5;
        populationInput = building.GetInput1();
        plushyInput = building.GetInput2();
        leatherInput = building.GetInput3();
        guardsOutput = building.GetOutput1();
        populationPowerOutput = building.GetOutput2();
        reputationOutput = building.GetOutput3();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public int GetPopulationInput()
    {
        return populationInput;
    }

    public int GetPlushyInput()
    {
        return plushyInput;
    }

    public int GetLeatherInput()
    {
        return leatherInput;
    }

    public int GetGuardsOutput()
    {
        return guardsOutput;
    }

    public int GetPopulationPowerOutput()
    {
        return populationPowerOutput;
    }

    public int GetReputationOutput()
    {
        return reputationOutput;
    }
}
