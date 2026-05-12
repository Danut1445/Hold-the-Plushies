using UnityEngine;
using UnityEngine.UI;

public class LevelLogicScript : MonoBehaviour
{
    public int currentMoney;
    public int currentHealth;
    public TMPro.TMP_Text moneyText;
    public TMPro.TMP_Text healthText;
    public GameObject[] enemyPrefabs = new GameObject[5];

    private void Start()
    {
        moneyText.SetText(currentMoney.ToString());
        healthText.SetText(currentHealth.ToString());
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            //Debug.Log("Game Over!");
        }
        healthText.SetText(currentHealth.ToString());
    }

    public void gainMoney(int money)
    {
        currentMoney += money;
        moneyText.SetText(currentMoney.ToString());
    }

    public bool spendMoney(int money)
    {
        if (currentMoney < money)
        {
            return false;
        }
        currentMoney -= money;
        moneyText.SetText(currentMoney.ToString());
        return true;
    }
}
