using UnityEngine;

public abstract class BuildingBasicScript : MonoBehaviour
{
    protected int level;
    protected int maxLevel;
    protected int cost;
    protected int ID;
    protected int fulfilment;
    protected int upgradeAmmount;
    protected bool isActive;

    public abstract void CreateBuilding();

    public abstract void UpgradeBuilding();

    public abstract void DeactivateBuilding();

    public abstract void ReactivateBuilding();

    public abstract int PassDay(int population);

    public int GetID()
    {
        return ID;
    }

    public bool GetIsActive()
    {
        return isActive;
    }
}
