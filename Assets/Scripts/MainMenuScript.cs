using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void StartGame()
    {
        SceneManager.LoadScene("MemoryChallenge"); 
    }

    public void OpenSettings()
    {
        Debug.Log("Settings not implemented yet");
    }
}
