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

    public BuildingSaveScript()
    {
    }

    public BuildingSaveScript(BuildingSaveGame buildingSave)
    {
        ID = buildingSave.ID;
        level = buildingSave.level;
        cost = buildingSave.cost;
        fulfilment = buildingSave.fulfilment;
        upgradeAmmount = buildingSave.upgradeAmmount;
        isActive = buildingSave.isActive;
        input1 = buildingSave.input1;
        input2 = buildingSave.input2;
        input3 = buildingSave.input3;
        input4 = buildingSave.input4;
        output1 = buildingSave.output1;
        output2 = buildingSave.output2;
        output3 = buildingSave.output3;
        output4 = buildingSave.output4;
    }

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

    public void SaveBuilding(BarracksScript building)
    {
        this.SaveBuilding((BuildingBasicScript)building);
        input1 = building.GetPopulationInput();
        input2 = building.GetPlushyInput();
        input3 = building.GetLeatherInput();
        output1 = building.GetGuardsOutput();
        output2 = building.GetPopulationPowerOutput();
        output3 = building.GetReputationOutput();
    }

    public void SaveBuilding(TownHallScript building)
    {
        this.SaveBuilding((BuildingBasicScript)building);
        input1 = building.GetPlushInput();
        output1 = building.GetTimer();
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

    public void SetInput1(int value)
    {
        input1 = value;
    }

    public int GetInput2()
    {
        return input2;
    }

    public void SetInput2(int value)
    {
        input2 = value;
    }

    public int GetInput3()
    {
        return input3;
    }

    public void SetInput3(int value)
    {
        input3 = value;
    }

    public int GetInput4()
    {
        return input4;
    }

    public void SetInput4(int value)
    {
        input4 = value;
    }

    public int GetOutput1()
    {
        return output1;
    }

    public void SetOutput1(int value)
    {
        output1 = value;
    }

    public int GetOutput2()
    {
        return output2;
    }

    public void SetOutput2(int value)
    {
        output2 = value;
    }

    public int GetOutput3()
    {
        return output3;
    }

    public void SetOutput3(int value)
    {
        output3 = value;
    }

    public int GetOutput4()
    {
        return output4;
    }

    public void SetOutput4(int value)
    {
        output4 = value;
    }
}
