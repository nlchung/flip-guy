using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UIElements;

[RequireComponent(typeof(NetworkManager))]
[DisallowMultipleComponent]
public class NetworkManagerHud : MonoBehaviour
{
    NetworkManager m_NetworkManager;
    UnityTransport m_Transport;

    [SerializeField] private GameObject m_PinkGuyPrefab;

    [SerializeField] private GameObject m_BlueGuyPrefab;

    void Awake()
    {
        // Only cache networking manager but not transport here because transport could change anytime.
        m_NetworkManager = GetComponent<NetworkManager>();
        m_NetworkManager.ConnectionApprovalCallback += ConnectionApprovalWithPrefabSpawn;
    }

    void Start()
    {
        m_Transport = (UnityTransport)m_NetworkManager.NetworkConfig.NetworkTransport;

        NetworkManager.Singleton.OnConnectionEvent += OnConnectionEvent;
    }

    void OnConnectionEvent(NetworkManager networkManager, ConnectionEventData connectionEventData)
    {
        if (connectionEventData.EventType == ConnectionEvent.ClientConnected)
        {

        }
        else if (connectionEventData.EventType == ConnectionEvent.ClientDisconnected)
        {
            if (NetworkManager.Singleton.IsServer && connectionEventData.ClientId != NetworkManager.ServerClientId)
            {
                return;
            }

        }
    }
    void ConnectionApprovalWithPrefabSpawn(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
  {
    GameObject newPlayer;
    Vector3 position;
    NetworkObject netObj;

    if (m_NetworkManager.IsServer)
    {
      newPlayer = Instantiate(m_PinkGuyPrefab);
      position = new Vector3(-5, 3, 0);
    }
    else
    {
      newPlayer = Instantiate(m_BlueGuyPrefab);
      position = new Vector3(-5, -3, 0);
    }

    netObj = newPlayer.GetComponent<NetworkObject>();
    newPlayer.SetActive(true);
    netObj.SpawnAsPlayerObject(NetworkManager.ServerClientId, true);


    response.CreatePlayerObject = true;
    response.Position = position;
    response.Approved = true;
  }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IsRunning(NetworkManager networkManager) => networkManager.IsServer || networkManager.IsClient;


    void OnDestroy()
    {

        m_NetworkManager.OnConnectionEvent -= OnConnectionEvent;
    }
}
