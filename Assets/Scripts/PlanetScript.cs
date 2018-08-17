using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetScript : MonoBehaviour {
    public bool orbital = false;
    public float orbitalSpeed = 0f;
    public float rotateSpeed = 0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (orbital)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, orbitalSpeed * Time.deltaTime);
            transform.Rotate(new Vector3(0f, rotateSpeed, 0f));
        }
    }
}
