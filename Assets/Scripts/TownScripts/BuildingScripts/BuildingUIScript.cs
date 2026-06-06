using UnityEngine;
using UnityEngine.UI;
using System;

public class BuildingUIScript : MonoBehaviour
{
    public GameObject buildlingUI;
    public Sprite[] buildingSprites = new Sprite[6];
    public GameObject buildingButton;
    public TMPro.TMP_Text input1;
    public TMPro.TMP_Text input2;
    public TMPro.TMP_Text input3;
    public TMPro.TMP_Text input4;
    public TMPro.TMP_Text output1;
    public TMPro.TMP_Text output2;
    public TMPro.TMP_Text output3;
    public TMPro.TMP_Text output4;
    public TMPro.TMP_Text currentFulfilment;
    public TMPro.TMP_Text levelText;
    public TMPro.TMP_Text upgradeCost;

    private BuildingBasicScript buildingScript;

    public void Start()
    {
        buildingScript = gameObject.GetComponent<BuildingBasicScript>();
        CloseBuildingUI();
    }

    public void OpenBuildingUI()
    {
        buildlingUI.SetActive(true);
    }

    public void CloseBuildingUI()
    {
        buildlingUI.SetActive(false);
    }

    public void SetImage(int level)
    {
        buildingButton.GetComponent<Image>().sprite = buildingSprites[level];
    }

    public void SetInput1(int value)
    {
        input1.SetText(value.ToString());
    }

    public void SetInput2(int value)
    {
        input2.SetText(value.ToString());
    }

    public void SetInput3(int value)
    {
        input3.SetText(value.ToString());
    }

    public void SetInput4(int value)
    {
        input4.SetText(value.ToString());
    }

    public void SetOutput1(int value)
    {
        output1.SetText(value.ToString());
    }

    public void SetOutput2(int value)
    {
        output2.SetText(value.ToString());
    }

    public void SetOutput3(int value)
    {
        output3.SetText(value.ToString());
    }

    public void SetOutput4(int value)
    {
        output4.SetText(value.ToString());
    }

    public void SetFulfilment(int value)
    {
        value = (int) MathFunctions.CheckRightSideInterval(value, 100);
        currentFulfilment.SetText(value.ToString());
    }

    public void SetLevel(int level, int maxLevel)
    {
        levelText.SetText(level.ToString() + "/" + maxLevel.ToString());
    }

    public void SetUpgradeCost(int value)
    {
        upgradeCost.SetText(value.ToString());
    }
}
