using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class Container : MonoBehaviour
{
    public int selectedTheme = 1;
    public TextMeshProUGUI totalPoints;

    public GameObject playScreen;
    public GameObject winScreen;
    public GameObject loseScreen;

    public List<string> playerList = new List<string>();

    public int localPoints;

    public int themeUnlocked
    {
        get { return PlayerPrefs.GetInt("themeUnlocked", 1); }
        set { PlayerPrefs.SetInt("themeUnlocked", value); }
    }
    public int points
    {
        get { return PlayerPrefs.GetInt("points", 0); }
        set
        {
            PlayerPrefs.SetInt("points", value);
            PlayerPrefs.Save();
            SetPoitText();
        }
    }

    public void SetPoitText()
    {
        totalPoints.text = points.ToString();
    }
}