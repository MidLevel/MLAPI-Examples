using MLAPI.Attributes;
using MLAPI.MonoBehaviours.Core;
using UnityEngine;
using UnityEngine.UI;

public class SyncVarTest : NetworkedBehaviour
{
    [SyncedVar]
    public string MySyncedName;
    public Text TextField;
    public GameObject prefab;

    public override void NetworkStart()
    {
        if (isServer)
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000) + " (Press space on server)";
    }

    private void Update()
    {
        TextField.text = MySyncedName;
        if(isServer && Input.GetKeyDown(KeyCode.Space) && isLocalPlayer)
        {
            MySyncedName = "SyncVarTest: " + Random.Range(50, 10000) + " (Press space on server)";
            GameObject go = Instantiate(prefab);
            go.transform.position = transform.position + new Vector3(0, 1f, 0);
            go.GetComponent<NetworkedObject>().Spawn();
        }
    }

}
