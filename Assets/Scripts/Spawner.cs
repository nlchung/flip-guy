using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(NetworkManager))]

public class Spawner : MonoBehaviour
{
      [SerializeField] private bool isHost;

    [SerializeField] private GameObject m_PinkGuyPrefab;

    [SerializeField] private GameObject m_BlueGuyPrefab;

  private void Awake() 
  {
    var networkManager = gameObject.GetComponent<NetworkManager>();
    networkManager.ConnectionApprovalCallback += ConnectionApprovalWithPrefabSpawn;
  }

  void ConnectionApprovalWithPrefabSpawn(NetworkManager.ConnectionApprovalRequest request, NetworkManager.ConnectionApprovalResponse response)
  {
    GameObject newPlayer;
    Vector3 position;
    NetworkObject netObj;

    if (isHost)
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

  void Update()
  {
    if (!NetworkManager.Singleton.IsServer)
    {
        return;
    }
  }
}