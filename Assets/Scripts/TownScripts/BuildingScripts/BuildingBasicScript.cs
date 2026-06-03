using UnityEngine;

public abstract class BuildingBasicScript
{
    private int level;
    private int cost;
    private string name;

    public abstract void CreateBuilding();

    public abstract void UpgradeBuilding();

    public abstract void DeleteBuilding();
}
