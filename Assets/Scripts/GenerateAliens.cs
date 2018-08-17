using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAliens : MonoBehaviour {
    public GameObject prefab, aliens;
    public Mesh saucer, dome;
    public Material saucerMat, domeMat;

    public float xPosMin, xPosMax, yPosMin, yPosMax, zPosMin, zPosMax;
    public int numAliens;

    // Use this for initialization
    void Start () {
        for (int i = 0; i < numAliens; i++)
        {
            GenerateAlien(
                Random.Range(xPosMin,xPosMax),
                Random.Range(yPosMin, yPosMax),
                Random.Range(zPosMin,zPosMax));
        }

        GetComponent<GenerateShips>().Generate();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void GenerateAlien(float x, float y, float z)
    {
        GameObject newAlien;
        newAlien = Instantiate(prefab, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f));

        newAlien.transform.parent = aliens.transform;
        newAlien.GetComponent<MeshFilter>().mesh = saucer;
        newAlien.GetComponent<MeshRenderer>().material = saucerMat;
        newAlien.AddComponent<AlienScript>();
        newAlien.GetComponent<AlienScript>().rotateSpeed = 1f;
        newAlien.transform.position += new Vector3(x, y, z);

        GameObject newDome = Instantiate(prefab, newAlien.transform.position + new Vector3(0f, 0.8f, 0f), Quaternion.identity);
        newDome.transform.parent = newAlien.transform;
        newDome.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
        newDome.GetComponent<MeshFilter>().mesh = dome;
        newDome.GetComponent<MeshRenderer>().material = domeMat;
    }
}
