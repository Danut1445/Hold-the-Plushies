using UnityEngine;

public abstract class BuildingBasicScript : MonoBehaviour
{
    protected int level;
    protected int cost;
    protected int ID;

    public abstract void CreateBuilding();

    public abstract void UpgradeBuilding();

    public abstract void DeleteBuilding();

    public abstract void PassDay();
}
