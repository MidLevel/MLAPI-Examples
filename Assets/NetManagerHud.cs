using MLAPI;
using System.Net;
using UnityEngine;
using UnityEngine.Networking;

public class NetManagerHud : MonoBehaviour {

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 20), "Start client"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration()
            {
                Address = "127.0.0.1",
                Port = 7777,
            };
            config.RegisteredScenes = new System.Collections.Generic.List<string>();
            config.RegisteredScenes.Add("MenuScene");
            config.RegisteredScenes.Add("PlayScene");
            config.EnableSceneSwitching = false;
            NetworkingManager.singleton.StartClient(config);
        }

        if (GUI.Button(new Rect(20, 70, 100, 20), "Start server"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration()
            {
                Address = "127.0.0.1",
                Port = 7777,
            };
            config.RegisteredScenes = new System.Collections.Generic.List<string>();
            config.RegisteredScenes.Add("MenuScene");
            config.RegisteredScenes.Add("PlayScene");
            config.EnableSceneSwitching = false;
            NetworkingManager.singleton.StartServer(config);
        }

        if (GUI.Button(new Rect(20, 120, 100, 20), "Start host"))
        {
            NetworkingConfiguration config = new NetworkingConfiguration()
            {
                Address = "127.0.0.1",
                Port = 7777,
            };
            config.RegisteredScenes = new System.Collections.Generic.List<string>();
            config.RegisteredScenes.Add("MenuScene");
            config.RegisteredScenes.Add("PlayScene");
            config.EnableSceneSwitching = false;
            NetworkingManager.singleton.StartHost(config);
        }
    }

    private void OnUPnPComplete(bool success, IPAddress ipAddress)
    {
        //Did UPNP succeed
        Debug.Log(success);
        //If it succeded. This is the public ip address
        Debug.Log(ipAddress);
    }
}
