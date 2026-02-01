using UnityEngine;
using Unity.Netcode;
using TMPro;
using Unity.XR.CoreUtils;

public class NetworkUI : MonoBehaviour
{
    public GameBoardScript gameBoard;
    public GameObject camera;
    public GameObject arCamera;
    public NetworkManager networkManager;
    private PlayerScript player;

    [Header("Panels")]
    public GameObject mainMenuPanel;
    public GameObject multiplayerPlayPanel;
    public GameObject settingsPanel;
    public GameObject singelplayerPanel;
    public GameObject multiplayerPanel;
    public GameObject createGamePanel;
    public GameObject joinGamePanel;
    public GameObject lobbyPanel;

    public TextMeshProUGUI playerTurnName;
    public GameObject boardButton;
    public GameObject board;
    public GameObject LobbyStart;

    public void StartGame()
    {
        if (GameSettings.Instance.gameMode == GameMode.Singleplayer)
        {
            networkManager.StartHost();
        } else
        {
            Debug.Log("UI Start Game Multiplayer");
            player.StartGameRpc();
        }
        gameBoard.ReadyBoard();
    }

    public void EndGame()
    {
        networkManager.Shutdown();
        SwitchCameras();
        mainMenuPanel.SetActive(true);
    }

    public void SwitchCameras()
    {
        camera.SetActive(!camera.active);
        arCamera.SetActive(!arCamera.active);
    }

    public void SetPlayer(PlayerScript p)
    {
        player = p;
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
    }

    public void ShowMultiplayerPlay()
    {
        multiplayerPlayPanel.SetActive(true);
    }

    public void ShowSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void ShowSingelplayer()
    {
        singelplayerPanel.SetActive(true);
    }

    public void ShowMultiplayer()
    {
        multiplayerPanel.SetActive(true);
    }

    public void ShowCreateGame()
    {
        createGamePanel.SetActive(true);
    }

    public void ShowJoinGame()
    {
        joinGamePanel.SetActive(true);
    }

    public void ShowLobby()
    {
        lobbyPanel.SetActive(true);
        if (GameSettings.Instance.gameType != GameType.Host)
        {
            LobbyStart.SetActive(false);
        }
    }

    public void HideMainMenu()
    {
        mainMenuPanel.SetActive(false);
    }

    public void HideMultiplayerPlay()
    {
        multiplayerPlayPanel.SetActive(false);
    }

    public void HideSettings()
    {
        settingsPanel.SetActive(false);
    }

    public void HideSingelplayer()
    {
        singelplayerPanel.SetActive(false);
    }

    public void HideMultiplayer()
    {
        multiplayerPanel.SetActive(false);
    }

    public void HideCreateGame()
    {
        createGamePanel.SetActive(false);
    }

    public void HideJoinGame()
    {
        joinGamePanel.SetActive(false);
    }

    public void HideLobby()
    {
        lobbyPanel.SetActive(false);
    }
}

