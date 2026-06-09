using UnityEngine;
using System;

public class BuildingSaveScript
{
    private int ID;
    private int level;
    private int cost;
    private int fulfilment;
    private int upgradeAmmount;
    private bool isActive;
    private int input1, input2, input3, input4;
    private int output1, output2, output3, output4;

    public void SaveBuilding(BuildingBasicScript building)
    {
        ID = building.GetID();
        level = building.GetLevel();
        cost = building.GetCost();
        fulfilment = building.GetFulfilment();
        upgradeAmmount = building.GetUpgradeAmmount();
        isActive = building.GetIsActive();
    }

    public void SaveBuilding(PlushFactoryScript building)
    {
        this.SaveBuilding((BuildingBasicScript) building);
        input1 = building.GetPopulationInput();
        output1 = building.GetPlushOutput();
    }

    public void SaveBuilding(LeatherFactoryScript building)
    {
        this.SaveBuilding((BuildingBasicScript)building);
        input1 = building.GetPopulationInput();
        input2 = building.GetPlushyInput();
        output1 = building.GetLeatherOutput();
    }

    public void SaveBuilding(PlushyHomeScript building)
    {
        this.SaveBuilding((BuildingBasicScript)building);
        input1 = building.GetPlushyInput();
        output1 = building.GetHousingOutput();
    }

    public void SaveBuilding(WeaponsFactoryScript building)
    {
        this.SaveBuilding((BuildingBasicScript) building);
        input1 = building.GetPopulationNeeded();
        input2 = building.GetPlushyInput();
        input3 = building.GetLeatherInput();
        output1 = building.GetWeaponsOutput();
        output2 = building.GetDamageBoostOutput();
        output3 = building.GetReloadBoostOutput();
    }

    public int GetID()
    {
        return ID;
    }

    public int GetLevel()
    {
        return level;
    }

    public int GetCost()
    {
        return cost;
    }

    public int GetFulfilment()
    {
        return fulfilment;
    }

    public int GetUpgradeAmmount()
    {
        return upgradeAmmount;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    public int GetInput1()
    {
        return input1;
    }

    public int GetInput2()
    {
        return input2;
    }

    public int GetInput3()
    {
        return input3;
    }

    public int GetInput4()
    {
        return input4;
    }

    public int GetOutput1()
    {
        return output1;
    }

    public int GetOutput2()
    {
        return output2;
    }

    public int GetOutput3()
    {
        return output3;
    }

    public int GetOutput4()
    {
        return output4;
    }
}
