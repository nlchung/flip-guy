using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    public override void OnNetworkSpawn()
    {
        if (!IsOwner) 
        {
            print("camera follow working");
            gameObject.SetActive(false);
        }
        if (!IsHost) 
        {
            print("camera transform working");
            gameObject.transform.Rotate(-180, 0, 0);
        }
    }
}
