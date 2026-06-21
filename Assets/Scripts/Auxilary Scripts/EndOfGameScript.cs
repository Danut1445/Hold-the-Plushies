using UnityEngine;

public class EndOfGameScript : MonoBehaviour
{
    public void ExitGame()
    {
        PlayerPrefs.SetInt("HasSavefile", 0);
        Application.Quit();
    }
}
