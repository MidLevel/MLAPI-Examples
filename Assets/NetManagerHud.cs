using MLAPI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetManagerHud : MonoBehaviour {

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 20), "Start client"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration();
            config.Address = "127.0.0.1";
            config.Port = 7777;
            config.Channels.Add("PositionUpdates", UnityEngine.Networking.QosType.Reliable);
            config.MessageTypes.Add("PositionUpdate");
            config.MessageTypes.Add("SetClientPosition");
            NetworkingManager.singleton.StartClient(config);
        }

        if (GUI.Button(new Rect(20, 70, 100, 20), "Start server"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration();
            config.Address = "127.0.0.1";
            config.Port = 7777;
            config.Channels.Add("PositionUpdates", UnityEngine.Networking.QosType.Reliable);
            config.MessageTypes.Add("PositionUpdate");
            config.MessageTypes.Add("SetClientPosition");
            NetworkingManager.singleton.StartServer(config);
        }

        if (GUI.Button(new Rect(20, 120, 100, 20), "Start host"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration()
            {
                Address = "127.0.0.1",
                Port = 7777
            };
            NetworkingManager.singleton.StartHost(config);
        }
    }
}
