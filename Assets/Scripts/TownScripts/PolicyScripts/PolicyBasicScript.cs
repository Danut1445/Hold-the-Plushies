using UnityEngine;
using System;

public abstract class PolicyBasicScript : MonoBehaviour, IComparable
{
    protected int choiceMade;
    protected int ID;
    protected bool choosable;
    public GameObject[] requirementsIn;
    public GameObject[] requirementsOut;
    public GameObject notChoosableCanvas;

    public abstract void ChooseOption(int value);

    public void CheckIfChoseable()
    {
        choosable = true;
        foreach (GameObject requirement in requirementsIn)
        {
            choosable &= requirement.GetComponent<RequirementScript>().IsActive();
        }
        notChoosableCanvas.SetActive(!choosable);
    }

    public void SetRequirementsOutTrue()
    {
        foreach (GameObject requirement in requirementsOut)
        {
            requirement.GetComponent<RequirementScript>().Activate();
        }
    }

    public void LoadFromSave(PolicySaveScript policy)
    {
        choiceMade = policy.GetChoiceMade();
    }

    int IComparable.CompareTo(object otherPolicy)
    {
        return this.ID - ((PolicyBasicScript) otherPolicy).GetID();
    }

    public int GetChoiceMade()
    {
        return choiceMade;
    }

    public int GetID()
    {
        return ID;
    }
}
