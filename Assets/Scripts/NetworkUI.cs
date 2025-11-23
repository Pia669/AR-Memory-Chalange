using UnityEngine;
using Unity.Netcode;


public class NetworkUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartHost()
    {
        Debug.Log("HOST START");
        NetworkManager.Singleton.StartHost();
    }

    public void StartClient()
    {
        Debug.Log("CLIENT START");
        NetworkManager.Singleton.StartClient();
    }
}

