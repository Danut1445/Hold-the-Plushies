using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class MainMenuScript : MonoBehaviour
{
    public GameObject continueButton;

    void Start()
    {
        if (PlayerPrefs.GetInt("HasSavefile") == 0)
        {
            continueButton.GetComponent<Button>().interactable = false;
        } else
        {
            continueButton.GetComponent<Button>().interactable = true;
        }
    }

    public void NewGame()
    {
        PlayerStats.NewGame();
        SceneManager.LoadScene("TownScene");
    }

    public void ContinueGame()
    {
        SaveGameScript savedGame = SaveSystem.LoadGame();
        PlayerStats.LoadGame(savedGame);
        SceneManager.LoadScene("TownScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
