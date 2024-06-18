using Unity.Netcode;
using UnityEngine;
public class PlayerSpawner : NetworkBehaviour {
    [SerializeField] private GameObject playerPrefabA;
    [SerializeField] private GameObject playerPrefabB;
 
    [ServerRpc(RequireOwnership=false)] //server owns this object but client can request a spawn
    public void SpawnPlayerServerRpc(ulong clientId,int prefabId) {
        GameObject newPlayer;
        NetworkObject netObj;

        if (prefabId == 0)
             newPlayer = Instantiate(playerPrefabA);
        else
            newPlayer=Instantiate(playerPrefabB);
        netObj = newPlayer.GetComponent<NetworkObject>();
        newPlayer.SetActive(true);
        netObj.SpawnAsPlayerObject(clientId,true);
    }
}