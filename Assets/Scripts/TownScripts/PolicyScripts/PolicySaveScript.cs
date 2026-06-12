using UnityEngine;

public class PolicySaveScript
{
    private int choiceMade;
    private int ID;

    public void SavePolicy(PolicyBasicScript policy) {
        choiceMade = policy.GetChoiceMade();
        ID = policy.GetID();
    }

    public int GetID()
    {
        return ID;
    }

    public int GetChoiceMade()
    {
        return choiceMade;
    }
}
