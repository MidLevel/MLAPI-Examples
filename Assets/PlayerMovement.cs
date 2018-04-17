using MLAPI;
using MLAPI.MonoBehaviours.Core;
using MLAPI.NetworkingManagerComponents;
using MLAPI.NetworkingManagerComponents.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : NetworkedBehaviour
{
    void Update ()
    {
		if(isLocalPlayer)
        {
            transform.Translate(new Vector3(Input.GetAxis("Horizontal") * 2f * Time.deltaTime, 0, Input.GetAxis("Vertical") * 2f * Time.deltaTime));
            if(Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(Simulate());
                return;
                LagCompensationManager.Simulate(1f, () =>
                {
                    for (int i = 0; i < LagCompensationManager.SimulationObjects.Count; i++)
                    {
                        GameObject go = new GameObject();
                        go.transform.position = LagCompensationManager.SimulationObjects[i].transform.position;
                    }
                });
            }
        }
	}

    IEnumerator Simulate()
    {
        Debug.Log("STARTPOS: " + transform.position);
        yield return new WaitForSeconds(1);
        LagCompensationManager.Simulate(1f, () =>
        {
            for (int i = 0; i < LagCompensationManager.SimulationObjects.Count; i++)
            {
                Debug.Log(LagCompensationManager.SimulationObjects[i].transform.position);
            }
        });
    }
}
