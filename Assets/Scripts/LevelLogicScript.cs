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
    public TMPro.TMP_Text currentWaveText;
    public TMPro.TMP_Text maximumWaveText;
    public TMPro.TMP_Text timerWaveText;
    public TMPro.TMP_Text reputationGainedText;
    public TMPro.TMP_Text reputationGainedMoneyText;
    public TMPro.TMP_Text reputationLostText;
    public TMPro.TMP_Text reputationTotalText;

    public GameObject gameOverScreen;
    public GameObject[] enemyPrefabs = new GameObject[5];
    public GameObject plushySpawner;
    public GameObject timerInformation;
    public GameObject winScreen;

    private bool spawningWaveActive;
    private bool existsWave;
    private bool finnishedAllWaves;
    private int numberEnemies;
    private float timer;
    private int currentWave;

    private void Start()
    {
        numberEnemies = 0;
        spawningWaveActive = true;
        existsWave = true;
        finnishedAllWaves = false;
        currentWave = 1;
        plushySpawner.GetComponent<PlushySpawnerScript>().StartSpwaningNextWave();

        maximumWaveText.SetText(plushySpawner.GetComponent<PlushySpawnerScript>().GetRemainingWaves().ToString());
        currentWaveText.SetText(currentWave.ToString());
        moneyText.SetText(currentMoney.ToString());
        healthText.SetText(currentHealth.ToString());
        winScreen.SetActive(false);
    }

    private void Update()
    {
        if (spawningWaveActive)
        {
            return;
        }

        if (existsWave && numberEnemies > 0)
        {
            return;
        }

        if (existsWave)
        {
            if (finnishedAllWaves)
            {
                WinGame();
                spawningWaveActive = true;
                return;
            }
            timer = plushySpawner.GetComponent<PlushySpawnerScript>().timeBetweenWaves;
            Debug.Log(timer);
            existsWave = false;
            currentWave++;
            timerInformation.SetActive(true);
            return;
        }

        timer -= Time.deltaTime;
        timerWaveText.SetText(((int)timer).ToString());
        if (timer <= 0)
        {
            plushySpawner.GetComponent<PlushySpawnerScript>().StartSpwaningNextWave();
            spawningWaveActive = true;
            existsWave = true;
            currentWaveText.SetText(currentWave.ToString());
            timerInformation.SetActive(false);
        }
    }

    private void WinGame()
    {
        winScreen.SetActive(true);
        reputationGainedText.SetText("10");
        reputationGainedMoneyText.SetText(Mathf.Min(10, currentMoney / 20).ToString());
        reputationLostText.SetText(((100 - currentHealth) / 10).ToString());
        int totalreputaion = 10 + Mathf.Min(10, currentMoney / 20) - ((100 - currentHealth) / 10);
        reputationTotalText.SetText(totalreputaion.ToString());
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
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }

    public void BackToTown()
    {
        SceneManager.LoadScene("TownScene");
    }

    public void DecreaseNumberEnemies(int value)
    {
        numberEnemies -= value;
    }

    public void IncreaseNumberEnemies(int value)
    {
        numberEnemies += value;
    }

    public void FinnishedSpawningWave()
    {
        spawningWaveActive = false;
    }

    public void FinnishedLevel()
    {
        finnishedAllWaves = true;
    }
}
