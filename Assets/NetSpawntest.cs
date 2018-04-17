using MLAPI.MonoBehaviours.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetSpawntest : NetworkedBehaviour
{

    public override void NetworkStart()
    {
        Debug.Log("NetStart");
    }

}
