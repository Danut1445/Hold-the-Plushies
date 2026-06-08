using UnityEngine;
using UnityEngine.UI;

public class TownLogicScript : MonoBehaviour
{
    public TMPro.TMP_Text plushText;
    public TMPro.TMP_Text leatherText;
    public TMPro.TMP_Text populationText;
    public TMPro.TMP_Text currentDayText;
    public TMPro.TMP_Text nextAttackText;
    public Slider populationPower;
    public Slider reputation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlayerStats.NewGame();

        plushText.SetText(PlayerStats.GetPlush().ToString());
        leatherText.SetText(PlayerStats.GetLeather().ToString());
        populationText.SetText(PlayerStats.GetPopulation().ToString());
        currentDayText.SetText(PlayerStats.GetCurrentDay().ToString());
        nextAttackText.SetText(2.ToString());
        reputation.value = PlayerStats.GetReputation();
        populationPower.value = PlayerStats.GetPopulationPower();
    }

    public void PassDay()
    {
        PlayerStats.PassDay();

        plushText.SetText(PlayerStats.GetPlush().ToString());
        leatherText.SetText(PlayerStats.GetLeather().ToString());
        populationText.SetText(PlayerStats.GetPopulation().ToString());
        currentDayText.SetText(PlayerStats.GetCurrentDay().ToString());
        nextAttackText.SetText(2.ToString());
        reputation.value = PlayerStats.GetReputation();
        populationPower.value = PlayerStats.GetPopulationPower();
    }

    public void UpdateUIResources()
    {
        plushText.SetText(PlayerStats.GetPlush().ToString());
        leatherText.SetText(PlayerStats.GetLeather().ToString());
    }
}
