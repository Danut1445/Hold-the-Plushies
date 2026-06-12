using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class PlayerStats
{
    private static float reputation;
    private static float populatioPower;
    private static int leather;
    private static int plush;
    private static int population;
    private static int weapons;
    private static int guards;
    private static int damageBoost;
    private static int reloadBoost;
    private static int currentDay;
    private static int nextAttackDay;
    private static int currentLevel = 0;
    private static int policyTimer;
    private static BuildingBasicScript[] buildings;
    private static BuildingSaveScript[] buildingSaves;
    private static PolicySaveScript[] policySaves;

    public static void NewGame()
    {
        buildings = new BuildingBasicScript[10];
        buildingSaves = new BuildingSaveScript[10];
        policySaves = new PolicySaveScript[100];
        reputation = 0;
        populatioPower = 50;
        policyTimer = 0;
        leather = 1000;
        plush = 1000;
        weapons = 0;
        damageBoost = 0;
        reloadBoost = 0;
        currentDay = 1;
        nextAttackDay = 5;
        currentLevel = 1;
        population = 50;
    }

    public static void PassDay()
    {
        currentDay++;
        int remainingPopulation = population;

        foreach (BuildingBasicScript building in buildings)
        {
            if (building == null)
            {
                continue;
            }
            remainingPopulation = building.PassDay(remainingPopulation);
        }
    }

    public static void AddBuilding(BuildingBasicScript buildingScript)
    {
        buildings[buildingScript.GetID()] = buildingScript;
    }

    public static BuildingBasicScript GetBuilding(int ID)
    {
        return buildings[ID];
    }

    public static void SaveAllBuildings()
    {
        for (int i = 0; i < 10; i++)
        {
            if (buildings[i] != null)
            {
                buildingSaves[i] = buildings[i].SaveBuilding();
            }
        }
    }

    public static void SavePolicy(PolicyBasicScript policy)
    {
        PolicySaveScript policySave = new PolicySaveScript();
        policySave.SavePolicy(policy);
        policySaves[policySave.GetID()] = policySave;
    }

    public static PolicySaveScript GetPolicy(int ID)
    {
        return policySaves[ID];
    } 

    public static BuildingSaveScript GetSavedBuilding(int ID)
    {
        return buildingSaves[ID];
    }

    public static float GetReputation()
    {
        return reputation;
    }

    public static void ChangeReputation(float change)
    {
        reputation = reputation + (1f - Math.Abs((MathFunctions.Sigmoid(reputation / 20f) - 0.5f) * 2f)) * change;
        reputation = MathFunctions.CheckInterval(reputation, 100f);
    }

    public static float GetPopulationPower()
    {
        return populatioPower;
    }

    public static void ChangePopulationPower(float change)
    {
        populatioPower += change;
        populatioPower = MathFunctions.CheckRightSideInterval(populatioPower, 100f);
    }

    public static float GetLeather()
    {
        return leather;
    }

    public static void ProduceLeather(int value)
    {
        leather += value;
    }

    public static bool CheckEnoughLeather(int value)
    {
        return (value <= leather);
    }

    public static void ConsumeLeather(int value)
    {
        if (value <= leather)
        {
            leather -= value;
        }
    }

    public static float GetPlush()
    {
        return plush;
    }

    public static void ProducePlush(int value)
    {
        plush += value;
    }

    public static bool CheckEnoughPlush(int value)
    {
        return (value <= plush);
    }

    public static void ConsumePlush(int value)
    {
        if (value <= plush)
        {
            plush -= value;
        }
    }

    public static int GetNumberWeapons()
    {
        return weapons;
    }
    
    public static void SetNumberWeapons(int value)
    {
        weapons = value;
    }

    public static int GetGuards()
    {
        return guards;
    }

    public static void SetGuards(int value)
    {
        guards = value;
    }

    public static int GetDamageBoost()
    {
        return damageBoost;
    }

    public static void SetDamageBoost(int value)
    {
        damageBoost = value;
    }

    public static int GetReloadBoost()
    {
        return reloadBoost;
    }

    public static void SetReloadBoost(int value)
    {
        reloadBoost = value;
    }

    public static int GetPopulation()
    {
        return population;
    }

    public static void ChangePopulation(float procent)
    {
        population = (int) Math.Floor(population * (100f + procent) / 100f);
    }

    public static void ChangePopulation(int value)
    {
        population += value;
        if (population < 0)
        {
            population = 0;
        }
    }

    public static int GetCurrentDay()
    {
        return currentDay;
    }

    public static int GetNextAttackDay()
    {
        return nextAttackDay;
    }

    public static void SetNextAttackDay(int value)
    {
        nextAttackDay = value;
        //currentLevel++;
    }

    public static int GetCurrentLevel()
    {
        return currentLevel;
    }

    public static void ChangeCurrentLevel(int value)
    {
        currentLevel = value;
    }

    public static int GetPolicyTimer()
    {
        return policyTimer;
    }

    public static void SetPolicyTimer(int value)
    {
        policyTimer = value;
    }

    public static void ResetPolicyTimer()
    {
        policyTimer = 4;
        buildingSaves[6].SetOutput1(4);
    }
}
