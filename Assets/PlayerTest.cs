using MLAPI;
using MLAPI.Attributes;
using MLAPI.MonoBehaviours.Core;
using MLAPI.NetworkingManagerComponents.Binary;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public struct CustomStruct
{
    public float myFloat;
    public byte myByte;
    public CustomStruct[] customStructAr;
}

[System.Serializable]
public struct MyOtherCustomStruct
{
    public CustomStruct customStruct;
    public CustomStruct[] customStructArray;
}

public class PlayerTest : NetworkedBehaviour
{
    [SyncedVar]
    public int aSyncedInt = 0;
    [SyncedVar]
    public int[] bSyncedInts = new int[10];
    [SyncedVar]
    public int cSyncedInt = 0;
    [SyncedVar]
    public CustomStruct myCustomStruct;
    [SyncedVar]
    public MyOtherCustomStruct myOtherCustomStruct;
    public Text TextField;
    public GameObject cubePrefab;
    public GameObject spherePrefab;
    public Material planeMaterial;

    public override void NetworkStart()
    {
        if (isServer && isLocalPlayer)
        {
            //MySyncedName = "SyncVarTest: " + Random.Range(50, 10000);
        }

        planeMaterial = GameObject.Find("Plane").GetComponent<MeshRenderer>().material;
        if (isClient)
            RegisterMessageHandler("OnChangeColor", OnChangeColor);
    }

    private void OnChangeColor(uint clientId, BitReader reader)
    {
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
                //MySyncedName = "SyncVarTest: " + Random.Range(50, 10000);
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
                using (BitWriter writer = BitWriter.Get())
                {
                    writer.WriteFloat(planeMaterial.color.r);
                    writer.WriteFloat(planeMaterial.color.g);
                    writer.WriteFloat(planeMaterial.color.b);
                    SendToClientsTarget("OnChangeColor", "ColorChannel", writer.Finalize());
                }
            }
        }
    }

    float lastSet = 0;
    private void Update()
    {
        string s = "";
        s += aSyncedInt + ":";
        for (int i = 0; i < bSyncedInts.Length; i++)
        {
            s += bSyncedInts[i] + ":";
        }
        s += cSyncedInt + ":CustomStruct:" + myCustomStruct.myByte + ":" + myCustomStruct.myFloat + ":";
        for (int i = 0; i < myCustomStruct.customStructAr.Length; i++)
        {
            s += myCustomStruct.customStructAr[i].myByte + ":" + myCustomStruct.customStructAr[i].myFloat + ":";
        }
        TextField.text = s;
        int min = 0;
        int max = 99;
        if (isServer && Time.time - lastSet > 5)
        {
            aSyncedInt = Random.Range(min, max);
            bSyncedInts[Random.Range(0, bSyncedInts.Length)] = Random.Range(min, max);
            cSyncedInt = Random.Range(min, max);
            lastSet = Time.time;
        }
    }

}
