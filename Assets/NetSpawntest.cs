using MLAPI;
using MLAPI.MonoBehaviours.Core;
using UnityEngine;

public class NetSpawntest : NetworkedBehaviour
{

    public override void NetworkStart()
    {
        Debug.Log("NetStart");
    }

}
