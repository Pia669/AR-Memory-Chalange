using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreenScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestartGame()
    {
        SceneManager.LoadScene("MemoryChallenge");
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("MemoryChallenge");
    }
}
