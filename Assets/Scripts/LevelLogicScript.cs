using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelLogicScript : MonoBehaviour
{
    public static bool isPaused = false;
    public static bool isAlive = true;

    public int currentMoney;
    public int currentHealth;
    public TMPro.TMP_Text moneyText;
    public TMPro.TMP_Text healthText;
    public GameObject gameOverScreen;
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
            GameOver();
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

    public void GameOver()
    {
        isPaused = true;
        isAlive = false;
        Time.timeScale = 0.0f;
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void MainMenu()
    {
        Debug.Log("GO MAIN MENU");
    }
}
