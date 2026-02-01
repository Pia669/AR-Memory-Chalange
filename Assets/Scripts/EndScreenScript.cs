using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    public NetworkUI canvas;

    public void RestartGame()
    {
        canvas.StartGame();
    }
}
