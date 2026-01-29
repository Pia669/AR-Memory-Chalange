using UnityEngine;

public enum GameMode { Singleplayer, Multiplayer }
public enum Difficulty { Easy, Medium, Hard }
public enum CardTheme { Numbers, Animals, Fruits, Nature, Objects, Shapes }

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance;

    public GameMode gameMode = GameMode.Singleplayer;
    public Difficulty difficulty = Difficulty.Easy;
    public CardTheme theme = CardTheme.Numbers;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetNumberOfPairs()
    {
        switch (difficulty)
        {
            case Difficulty.Easy: return 8;
            case Difficulty.Medium: return 10;
            case Difficulty.Hard: return 12;
            default: return 10;
        }
    }
}

