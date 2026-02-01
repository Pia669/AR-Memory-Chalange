using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenuScript : MonoBehaviour
{
    public NetworkUI canvas;

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
        canvas.HideMainMenu();
        canvas.ShowSingelplayer();
        GameSettings.Instance.gameMode = GameMode.Singleplayer;
        canvas.StartGame();
    }

    public void QuitGame()
    {
        Debug.Log("quit");
        Application.Quit();
    }

    public void PlayAsHost()
    {
        canvas.HideMultiplayerPlay();
        GameSettings.Instance.gameMode = GameMode.Multiplayer;
        canvas.ShowCreateGame();
    }

    public void PlayAsClient()
    {
        canvas.HideMultiplayerPlay();
        GameSettings.Instance.gameMode = GameMode.Multiplayer;
        canvas.ShowJoinGame();
    }

    public void BackToMenu()
    {
        canvas.HideMultiplayerPlay();
        canvas.ShowMainMenu();
    }
}
