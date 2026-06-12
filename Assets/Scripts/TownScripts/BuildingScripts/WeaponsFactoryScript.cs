using UnityEngine;
using System;

public class WeaponsFactoryScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;
    private TownLogicScript townLogicScript;
    private int plushyInput;
    private int populationNeeded;
    private int leatherInput;
    private int weaponsOutput;
    private int damageBoostOutput;
    private int reloadBoostOutput;

    void Start()
    {
        BuildingSaveScript savedFactory = PlayerStats.GetSavedBuilding(4);
        if (savedFactory == null)
        {
            level = 0;
            maxLevel = 5;
            cost = 200;
            ID = 4;
            plushyInput = 20;
            populationNeeded = 10;
            leatherInput = 5;
            weaponsOutput = 2;
            damageBoostOutput = 0;
            reloadBoostOutput = 0;
            fulfilment = 0;
            upgradeAmmount = 10;
            isActive = false;
        }
        else
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

            UIScript.SetInput1(populationNeeded);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(weaponsOutput);
            UIScript.SetOutput2(damageBoostOutput);
            UIScript.SetOutput3(reloadBoostOutput);
            
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

            UIScript.SetInput1(populationNeeded);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(weaponsOutput);
            UIScript.SetOutput2(damageBoostOutput);
            UIScript.SetOutput3(reloadBoostOutput);

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

            plushyInput += plushyInput / 2;
            populationNeeded += populationNeeded / 2;
            leatherInput += leatherInput / 2;

            if (level % 2 == 0)
            {
                damageBoostOutput += upgradeAmmount;
            } else
            {
                reloadBoostOutput += upgradeAmmount;
            }
            weaponsOutput++;

            cost = cost * 2;

            if (level >= maxLevel)
            {
                UIScript.SetUpgradeButton(false);
                cost = 0;
            }

            UIScript.SetFulfilment(fulfilment);

            UIScript.SetInput1(populationNeeded);
            UIScript.SetInput2(plushyInput);
            UIScript.SetInput3(leatherInput);

            UIScript.SetOutput1(weaponsOutput);
            UIScript.SetOutput2(damageBoostOutput);
            UIScript.SetOutput3(reloadBoostOutput);

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
            PlayerStats.SetNumberWeapons(0);
            PlayerStats.SetDamageBoost(0);
            PlayerStats.SetReloadBoost(0);
            return population;
        }

        float fulfilmentFloat = CheckEnoughResources(population, populationNeeded, 100f);
        fulfilmentFloat = CheckEnoughResources(PlayerStats.GetPlush(), plushyInput, fulfilmentFloat);
        fulfilmentFloat = CheckEnoughResources(PlayerStats.GetLeather(), leatherInput, fulfilmentFloat);

        if (fulfilmentFloat < 100f)
        {
            fulfilment = (int)Math.Round(fulfilmentFloat);
            UIScript.SetInput1((int)(fulfilmentFloat * populationNeeded / 100));
            UIScript.SetInput2((int)(fulfilmentFloat * plushyInput / 100));
            UIScript.SetInput3((int)(fulfilmentFloat * leatherInput / 100));

            int realWeaponsOutput = (int)(fulfilmentFloat * weaponsOutput / 100);
            UIScript.SetOutput1(realWeaponsOutput);
            PlayerStats.SetNumberWeapons(realWeaponsOutput);
            
            int realDamageOutput = (int)(fulfilmentFloat * damageBoostOutput / 100);
            UIScript.SetOutput2(realDamageOutput);
            PlayerStats.SetDamageBoost(realDamageOutput);

            int realReloadOutput = (int)(fulfilmentFloat * reloadBoostOutput / 100);
            UIScript.SetOutput3(realReloadOutput);
            PlayerStats.SetReloadBoost(realReloadOutput);

            UIScript.SetFulfilment(fulfilment);

            PlayerStats.ConsumePlush((int)(fulfilmentFloat * plushyInput / 100));
            PlayerStats.ConsumeLeather((int)(fulfilmentFloat * leatherInput / 100));
            return Math.Max(population - ((int)(fulfilmentFloat * populationNeeded / 100)), 0);
        }

        fulfilment = 100;
        UIScript.SetInput1(populationNeeded);
        UIScript.SetInput2(plushyInput);
        UIScript.SetInput3(leatherInput);

        UIScript.SetOutput1(weaponsOutput);
        PlayerStats.SetNumberWeapons(weaponsOutput);
        UIScript.SetOutput2(damageBoostOutput);
        PlayerStats.SetDamageBoost(damageBoostOutput);
        UIScript.SetOutput3(reloadBoostOutput);
        PlayerStats.SetReloadBoost(reloadBoostOutput);

        PlayerStats.ConsumePlush(plushyInput);
        PlayerStats.ConsumeLeather(leatherInput);
        return population - populationNeeded;
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
        ID = 4;
        populationNeeded = building.GetInput1();
        plushyInput = building.GetInput2();
        leatherInput = building.GetInput3();
        weaponsOutput = building.GetOutput1();
        damageBoostOutput = building.GetOutput2();
        reloadBoostOutput = building.GetOutput3();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public int GetPopulationNeeded()
    {
        return populationNeeded;
    }

    public int GetPlushyInput()
    {
        return plushyInput;
    }

    public int GetLeatherInput()
    {
        return leatherInput;
    }

    public int GetWeaponsOutput()
    {
        return weaponsOutput;
    }

    public int GetDamageBoostOutput()
    {
        return damageBoostOutput;
    }

    public int GetReloadBoostOutput()
    {
        return reloadBoostOutput;
    }
}
