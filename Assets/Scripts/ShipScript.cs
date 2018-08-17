using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour {
    public Transform aliens;
    public int thrust, firepower, strength;
    public bool inCombat = false;

    Vector3 target = Vector3.zero;

    int targetIndex = -1;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        transform.LookAt(target);
        transform.Rotate(-90f, 90f, 180f);
	}

    public void SetTarget(int index)
    {
        targetIndex = index;
    }
}
