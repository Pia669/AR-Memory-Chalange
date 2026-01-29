using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuScript : MonoBehaviour
{
    public TextMeshProUGUI settingsText;
    void Start()
    {
        UpdateSettingsText();
       
    }

    void UpdateSettingsText()
    {
        var settings = GameSettings.Instance;

        settingsText.text =
            $"Mode: {settings.gameMode}\n" +
            $"Difficulty: {settings.difficulty} ({settings.GetNumberOfPairs()} pairs)\n" +
            $"Theme: {settings.theme}";
    }

    public void StartGame()
    {
        SceneManager.LoadScene("MemoryChallenge"); 
    }

    public void OpenSettings()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
