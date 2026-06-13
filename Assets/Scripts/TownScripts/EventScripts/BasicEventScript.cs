using UnityEngine;
using System;

public abstract class BasicEventScript : MonoBehaviour
{
    public GameObject firstButton;
    public GameObject secondButton;
    public GameObject thirdButton;
    public GameObject EventCanvas;

    protected float probability;
    protected int ID;

    public abstract bool CheckConditions();

    public bool CheckChance()
    {
        float randomFloat = UnityEngine.Random.value;
        if (randomFloat < probability)
            return true;
        return false;
    }

    public abstract void SetActive();

    public abstract void FirstChoice();

    public abstract void SecondChoice();

    public abstract void ThirdChoice();

    public int GetID()
    {
        return ID;
    }
}
