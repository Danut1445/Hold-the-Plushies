using UnityEngine;
using System;

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

    public abstract BuildingSaveScript SaveBuilding();

    public abstract void LoadFromSave(BuildingSaveScript building);

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

    public float CheckEnoughResources(float resourcesWeHave, float resourcesWeNeed, float fulilmentInput)
    {
        float fulfilment = 100f;
        if (resourcesWeHave < resourcesWeNeed)
        {
            fulfilment = resourcesWeHave / resourcesWeNeed * 100;
        }
        fulfilment = Math.Min(fulilmentInput, fulfilment);
        return fulfilment;
    }
}
