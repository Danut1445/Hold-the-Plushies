using UnityEngine;
using System;

[Serializable]
public class PolicySaveGame
{
    public int choiceMade;
    public int ID;

    public PolicySaveGame(PolicySaveScript policy)
    {
        this.choiceMade = policy.GetChoiceMade();
        this.ID = policy.GetID();
    }
}
