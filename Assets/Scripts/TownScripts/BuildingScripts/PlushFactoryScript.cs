using UnityEngine;

public class PlushFactoryScript : BuildingBasicScript
{
    private BuildingUIScript UIScript;

    void Start()
    {
        level = 0;
        cost = 100;
        ID = 1;
        //Need to change to save between levels.

        UIScript = gameObject.GetComponent<BuildingUIScript>();
        UIScript.SetImage(level);
    }

    public override void CreateBuilding()
    {

    }

    public override void UpgradeBuilding()
    {

    }

    public override void DeleteBuilding()
    {

    }

    public override void PassDay()
    {

    }
}
