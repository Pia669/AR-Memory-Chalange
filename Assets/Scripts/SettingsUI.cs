using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsUI : MonoBehaviour
{
    public TMP_Dropdown modeDropdown;
    public TMP_Dropdown difficultyDropdown;
    public TMP_Dropdown themeDropdown;

    void Start()
    {
        modeDropdown.value = (int)GameSettings.Instance.gameMode;
        difficultyDropdown.value = (int)GameSettings.Instance.difficulty;
        themeDropdown.value = (int)GameSettings.Instance.theme;
    }

    public void SaveAndBack()
    {
        GameSettings.Instance.gameMode = (GameMode)modeDropdown.value;

        GameSettings.Instance.difficulty = (Difficulty)difficultyDropdown.value;

        GameSettings.Instance.theme = (CardTheme)themeDropdown.value;

        SceneManager.LoadScene("MainMenuScene");
    }
}

