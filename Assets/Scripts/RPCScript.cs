using System.Collections;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class RPCScript : MonoBehaviour
{
    private CardControl cardControl;
    private Camera mainCamera;

    private PlayerScript player;

    private void Awake()
    {
        Debug.Log("Network Awake");
        cardControl = new CardControl();
        mainCamera = GetComponent<Camera>();

        cardControl.Touch.Tap.started += _ => TapRay(cardControl.Touch.Position);
        cardControl.Touch.DebugClick.started += _ => TapRay(cardControl.Touch.DebugPos);
    }

    private void OnEnable()
    {
        cardControl.Enable();
    }

    private void OnDisable()
    {
        cardControl.Disable();
    }

    public void SetPlayer(PlayerScript p)
    {
        player = p;
    }

    void TapRay(UnityEngine.InputSystem.InputAction action)
    {
        Ray ray = mainCamera.ScreenPointToRay(action.ReadValue<Vector2>());
        player.RayToServer(ray);
    }
}