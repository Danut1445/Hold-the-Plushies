using UnityEngine;
using System;

[Serializable]
public class SaveGameScript
{
    public float reputation;
    public float populatioPower;
    public int leather;
    public int plush;
    public int population;
    public int weapons;
    public int guards;
    public int damageBoost;
    public int reloadBoost;
    public int currentDay;
    public int nextAttackDay;
    public int currentLevel = 0;
    public int policyTimer;
    public BuildingSaveGame[] buildingSaves = new BuildingSaveGame[10];
    public PolicySaveGame[] policySaves = new PolicySaveGame[100];
}
