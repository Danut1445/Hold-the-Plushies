using UnityEngine;
using UnityEngine.UI;

public class LivingQuartersPolicy : PolicyBasicScript
{
    public GameObject button1stChoice;
    public GameObject button2ndChoice;
    private Color unselectedColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PolicySaveScript savedPolicy = PlayerStats.GetPolicy(2);
        if (savedPolicy == null)
        {
            choiceMade = 0;
            ID = 2;
            choosable = false;
            notChoosableCanvas.SetActive(!choosable);
        }
        else
        {
            this.LoadFromSave(savedPolicy);
            ID = 2;
        }
        unselectedColor = new Color(0.5f, 0.5f, 0.5f);

        if (choiceMade != 0)
        {
            button1stChoice.GetComponent<Button>().interactable = false;
            button2ndChoice.GetComponent<Button>().interactable = false;
            this.SetRequirementsOutTrue();
            if (choiceMade == 1)
            {
                button2ndChoice.GetComponent<Image>().color = unselectedColor;
            } else
            {
                button1stChoice.GetComponent<Image>().color = unselectedColor;
            }
        }
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

        choiceMade = value;
        if (choiceMade == 1)
        {
            PlayerStats.ChangeReputation(-15);
            BuildingSaveScript plushyHouses = PlayerStats.GetSavedBuilding(3);
            plushyHouses.SetOutput1(plushyHouses.GetOutput1() * 150 / 100);
            button2ndChoice.GetComponent<Image>().color = unselectedColor;
        } else
        {
            PlayerStats.ChangeReputation(15);
            BuildingSaveScript plushyHouses = PlayerStats.GetSavedBuilding(3);
            plushyHouses.SetOutput1(plushyHouses.GetOutput1() * 65 / 100);
            button1stChoice.GetComponent<Image>().color = unselectedColor;
        }

        button1stChoice.GetComponent<Button>().interactable = false;
        button2ndChoice.GetComponent<Button>().interactable = false;
        this.SetRequirementsOutTrue();
    }
}