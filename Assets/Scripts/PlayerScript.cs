using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class PlayerScript : NetworkBehaviour
{

    private GameBoardScript gameBoard;
    private NetworkUI ui;
    private ulong playerIndex = 0;

    public NetworkVariable<List<int>> scores = new NetworkVariable<List<int>>();

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            GetComponent<RPCScript>().SetPlayer(this);

            ui = GetComponent<NetworkUI>();
        }
    }

    public void RayToServer(Ray ray)
    {
        ServerDetectObjectRpc(ray, NetworkObjectId);
    }

    [Rpc(SendTo.Server)]
    private void ServerDetectObjectRpc(Ray ray, ulong sourceNetworkObjectId)
    {
        if (sourceNetworkObjectId != playerIndex) { return; }
        if (gameBoard.DetectObject(ray)) { UpdateClientsRpc(playerIndex); }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void UpdateClientsRpc(ulong index)
    {
        playerIndex = index;
        //if (index == NetworkObjectId) { playerTurnName.SetText(""); }
        //else { playerTurnName.SetText("Playing: " + playerIndex);  }
    }

    [Rpc(SendTo.ClientsAndHost)]
    public void StartGameRpc()
    {
        ui.HideLobby();
        ui.ShowMultiplayer();
        ui.SwitchCameras();
    }
}
