using UnityEngine;
using System;

[Serializable]
public class BuildingSaveGame
{
    public int ID;
    public int level;
    public int cost;
    public int fulfilment;
    public int upgradeAmmount;
    public bool isActive;
    public int input1, input2, input3, input4;
    public int output1, output2, output3, output4;

    public BuildingSaveGame(BuildingSaveScript building)
    {
        this.ID = building.GetID();
        this.level = building.GetLevel();
        this.cost = building.GetCost();
        this.fulfilment = building.GetFulfilment();
        this.upgradeAmmount = building.GetUpgradeAmmount();
        this.isActive = building.GetIsActive();
        this.input1 = building.GetInput1();
        this.input2 = building.GetInput2();
        this.input3 = building.GetInput3();
        this.input4 = building.GetInput4();
        this.output1 = building.GetOutput1();
        this.output2 = building.GetOutput2();
        this.output3 = building.GetOutput3();
        this.output4 = building.GetOutput4();
    }
}
