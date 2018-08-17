using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetScript : MonoBehaviour {
    public Transform aliens;

    public Vector3 target = Vector3.zero;
    int targetIndex = -1;
    float speed = 30f;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (targetIndex != -1) target = aliens.GetChild(targetIndex).position;

        if ((target - transform.position).magnitude < 50f)
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).GetComponent<ShipScript>().inCombat = true;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, target, Time.deltaTime * speed);
        }
    }

    public void SetTarget(int index)
    {
        targetIndex = index;
    }
}
