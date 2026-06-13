using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StrikeEventScript : BasicEventScript
{

    void Start()
    {
        ID = 1;
        probability = 0.15f;
        PlayerStats.AddEvent(this);
    }

    public override bool CheckConditions()
    {
        if (PlayerStats.GetReputation() < -50 && PlayerStats.GetSavedBuilding(1).GetIsActive() && PlayerStats.GetSavedBuilding(2).GetIsActive())
        {
            return true;
        }
        return false;
    }

    public override void SetActive()
    {
        EventCanvas.SetActive(true);
        if (PlayerStats.GetPopulationPower() > 30)
        {
            secondButton.GetComponent<Button>().interactable = false;
        }
    }

    public override void FirstChoice()
    {
        PlayerStats.ChangeReputation(30);
        BuildingSaveScript plushyFactory = PlayerStats.GetSavedBuilding(1);
        plushyFactory.SetOutput1(plushyFactory.GetOutput1() * 70 / 100);
        PlayerStats.GetBuilding(1).ResetBuilding();

        BuildingSaveScript leatherFactory = PlayerStats.GetSavedBuilding(2);
        leatherFactory.SetInput2(leatherFactory.GetInput2() * 70 / 100);
        leatherFactory.SetOutput1(leatherFactory.GetOutput1() * 70 / 100);
        PlayerStats.GetBuilding(2).ResetBuilding();
        EventCanvas.SetActive(false);
    }

    public override void SecondChoice()
    {
        PlayerStats.ChangePopulationPower(-10);
        PlayerStats.ChangeReputation(-30);
        GameObject.FindGameObjectWithTag("Logic").GetComponent<TownLogicScript>().UpdateUIReputation();
        EventCanvas.SetActive(false);
    }

    public override void ThirdChoice()
    {

    }
}
