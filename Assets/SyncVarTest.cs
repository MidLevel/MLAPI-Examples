using MLAPI;
using MLAPI.Attributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SyncVarTest : NetworkedBehaviour
{
    [SyncedVar]
    public string MySyncedName;
    public Text TextField;

    public override void NetworkStart()
    {
        if (isServer)
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000) + " (Press space on server)";
    }

    private void Update()
    {
        TextField.text = MySyncedName;
        if(isServer && Input.GetKeyDown(KeyCode.Space))
        {
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000) + " (Press space on server)";
        }
    }

}
