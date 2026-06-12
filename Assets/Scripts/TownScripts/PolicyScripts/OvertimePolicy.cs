using UnityEngine;
using UnityEngine.UI;

public class OvertimePolicy : PolicyBasicScript
{
    public GameObject button1stChoice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PolicySaveScript savedPolicy = PlayerStats.GetPolicy(1);
        if (savedPolicy == null)
        {
            choiceMade = 0;
            ID = 1;
            choosable = false;
            notChoosableCanvas.SetActive(!choosable);
        } else
        {
            this.LoadFromSave(savedPolicy);
            ID = 1;
        }

        if (choiceMade != 0)
        {
            button1stChoice.GetComponent<Button>().interactable = false;
            this.SetRequirementsOutTrue();
        }

        //PlayerStats.SavePolicy(this);
    }

    public override void ChooseOption(int value)
    {
        if (!choosable)
        {
            return;
        }

        if (PlayerStats.GetPolicyTimer() > 0)
        {
            return;
        }
        PlayerStats.ResetPolicyTimer();
        GameObject.FindGameObjectWithTag("Logic").GetComponent<PolicyTreeScript>().ResetPolicyTimer();

        choiceMade = 1;
        PlayerStats.ChangeReputation(-10);
        BuildingSaveScript plushyFactory = PlayerStats.GetSavedBuilding(1);
        plushyFactory.SetOutput1(plushyFactory.GetOutput1() * 120 / 100);

        BuildingSaveScript leatherFactory = PlayerStats.GetSavedBuilding(2);
        leatherFactory.SetInput2(leatherFactory.GetInput2() * 120 / 100);
        leatherFactory.SetOutput1(leatherFactory.GetOutput1() * 120 / 100);

        button1stChoice.GetComponent<Button>().interactable = false;
        this.SetRequirementsOutTrue();
    }
}
