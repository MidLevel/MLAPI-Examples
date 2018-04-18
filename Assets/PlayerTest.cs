using MLAPI.Attributes;
using MLAPI.MonoBehaviours.Core;
using MLAPI.NetworkingManagerComponents.Binary;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : NetworkedBehaviour
{
    [SyncedVar]
    public string MySyncedName;
    public Text TextField;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Material planeMaterial;

    public override void NetworkStart()
    {
        if (isServer && isLocalPlayer)
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000);

        planeMaterial = GameObject.Find("Plane").GetComponent<MeshRenderer>().material;
        if (isClient)
            RegisterMessageHandler("OnChangeColor", OnChangeColor);
    }

    private void OnChangeColor(uint clientId, byte[] data)
    {
        BitReader reader = new BitReader(data);
        float r = reader.ReadFloat();
        float g = reader.ReadFloat();
        float b = reader.ReadFloat();
        planeMaterial.color = new Color(r, g, b);
    }

    private void OnGUI()
    {
        int y = 25;
        if (isServer && isLocalPlayer)
        {
            y += 25;
            if (GUI.Button(new Rect(200, y, 200, 20), "Change Text with SyncVar"))
            {
                MySyncedName = "SyncVarTest: " + Random.Range(50, 10000);
            }

            y += 25;
            if (GUI.Button(new Rect(200, y, 200, 20), "Spawn cube"))
            {
                GameObject go = Instantiate(cubePrefab);
                go.transform.position = transform.position + new Vector3(0, 3f, 0);
                go.GetComponent<NetworkedObject>().Spawn();
            }

            y += 25;
            if (GUI.Button(new Rect(200, y, 200, 20), "Spawn sphere"))
            {
                GameObject go = Instantiate(spherePrefab);
                go.transform.position = transform.position + new Vector3(0, 3f, 0);
                go.GetComponent<NetworkedObject>().Spawn();
            }

            y += 25;
            if (GUI.Button(new Rect(200, y, 200, 20), "Set random plane color"))
            {
                planeMaterial.color =  Random.ColorHSV();
                using (BitWriter writer = new BitWriter())
                {
                    writer.WriteFloat(planeMaterial.color.r);
                    writer.WriteFloat(planeMaterial.color.g);
                    writer.WriteFloat(planeMaterial.color.b);
                    SendToClientsTarget("OnChangeColor", "ColorChannel", writer.Finalize());
                }
            }
        }
    }

    private void Update()
    {
        TextField.text = MySyncedName;
    }

}
