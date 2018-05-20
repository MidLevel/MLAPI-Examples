using MLAPI;
using MLAPI.MonoBehaviours.Core;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAgentTest : NetworkedBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    float lastTime;
	void Update () {
        if (!isServer)
            return;

        if (Time.time - lastTime > 10)
        {
            Vector2 offset = Random.insideUnitCircle * 5;
            GetComponent<NavMeshAgent>().SetDestination(transform.position + new Vector3(offset.x, 0, offset.y));
            lastTime = Time.time;
        }
	}
}
