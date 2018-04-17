using MLAPI.Attributes;
using MLAPI.MonoBehaviours.Core;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTest : NetworkedBehaviour
{
    [SyncedVar]
    public string MySyncedName;
    public Text TextField;
    public GameObject cubePrefab;
    public GameObject spherePrefab;

    public override void NetworkStart()
    {
        if (isServer)
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000);
    }

    private void OnGUI()
    {
        int y = 25;
        if (isServer)
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
        }
    }

    private void Update()
    {
        TextField.text = MySyncedName;
        if(isServer && Input.GetKeyDown(KeyCode.Space) && Input.GetKeyDown(KeyCode.LeftShift) && isLocalPlayer)
        {
            GameObject go = Instantiate(cubePrefab);
            go.transform.position = transform.position + new Vector3(0, 1f, 0);
            go.GetComponent<NetworkedObject>().Spawn();
        }
        else if(isServer && Input.GetKeyDown(KeyCode.Space) && isLocalPlayer)
        {
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000) + " (Press space on server)";
            GameObject go = Instantiate(spherePrefab);
            go.transform.position = transform.position + new Vector3(0, 1f, 0);
            go.GetComponent<NetworkedObject>().Spawn();
        }
    }

}
