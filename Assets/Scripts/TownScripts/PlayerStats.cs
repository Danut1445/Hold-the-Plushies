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
    private static float damageBoost;
    private static float reloadBoost;
    private static LinkedList<BuildingBasicScript> buildings;

    public static void NewGame()
    {
        buildings = new LinkedList<BuildingBasicScript>();
        reputation = 0;
        populatioPower = 50;
        leather = 100;
        plush = 100;
        damageBoost = 0;
        reloadBoost = 0;
    }

    public static float GetReputation()
    {
        return reputation;
    }

    public static void ChangeReputation(float change)
    {
        reputation = reputation + Math.Abs((MathFunctions.Sigmoid(reputation / 20f) - 0.5f) * 2f) * change;
        reputation = MathFunctions.CheckInterval(reputation, 100f);
    }

    public static float GetPopulationPower()
    {
        return populatioPower;
    }

    public static void ChangePopulationPower(float change)
    {
        populatioPower += change;
        populatioPower = MathFunctions.CheckInterval(populatioPower, 100f);
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

    public static float GetDamageBoost()
    {
        return damageBoost;
    }

    public static void SetDamageBoost(float value)
    {
        damageBoost = value;
    }

    public static float GetReloadBoost()
    {
        return reloadBoost;
    }

    public static void SetReloadBoost(float value)
    {
        reloadBoost = value;
    }
}
