using UnityEngine;
using UnityEngine.UI;

public class PolicyAdvancedAimingScript : PolicyBasicScript
{
    public GameObject button1stChoice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PolicySaveScript savedPolicy = PlayerStats.GetPolicy(3);
        if (savedPolicy == null)
        {
            choiceMade = 0;
            ID = 3;
            choosable = false;
            notChoosableCanvas.SetActive(!choosable);
        }
        else
        {
            this.LoadFromSave(savedPolicy);
            ID = 3;
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
        BuildingSaveScript weaponsFactory = PlayerStats.GetSavedBuilding(4);
        weaponsFactory.SetOutput2(weaponsFactory.GetOutput2() * 125 / 100);
        weaponsFactory.SetOutput3(weaponsFactory.GetOutput3() * 125 / 100);

        button1stChoice.GetComponent<Button>().interactable = false;
        this.SetRequirementsOutTrue();
    }
}
