using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour {
    public GameObject planets, ships, aliens;
    public int mode = 1;
    //  Camera modes
    //  1       Orbit sun
    //  2       View planets
    //  3       Cycle fleet (rear view)
    //  4       Cycle fleet (front view)
    //  5       View first alien

    bool findPlanet = false, findFleet = false;
    int planetIndex = -1, fleetIndex = -1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ProcessInput();

        switch (mode)
        {
            case 1:
                transform.RotateAround(Vector3.zero, Vector3.up, 3 * Time.deltaTime);
                break;

            case 2:
                if (findPlanet)
                {
                    planetIndex++;
                    if (planetIndex >= planets.transform.childCount) planetIndex = 0;
                    transform.position = planets.transform.GetChild(planetIndex).position + new Vector3(0f, 0f, 200f);
                    transform.LookAt(planets.transform.GetChild(planetIndex).position);
                    findPlanet = false;
                }

                transform.RotateAround(planets.transform.GetChild(planetIndex).position, Vector3.up, 1.5f * Time.deltaTime);
                break;

            case 3:
                if (findFleet)
                {
                    fleetIndex++;
                    if (fleetIndex >= ships.transform.childCount) fleetIndex = 0;
                    findFleet = false;
                }

                transform.position = ships.transform.GetChild(fleetIndex).position + new Vector3(-5f, 5f, -5f);
                transform.LookAt(ships.transform.GetChild(fleetIndex).position);
                break;

            case 4:
                if (findFleet)
                {
                    fleetIndex++;
                    if (fleetIndex >= ships.transform.childCount) fleetIndex = 0;
                    findFleet = false;
                }

                transform.position = ships.transform.GetChild(fleetIndex).position + new Vector3(23f, 1f, 23f);
                transform.LookAt(ships.transform.GetChild(fleetIndex).position);
                break;

            case 5:
                transform.position = aliens.transform.GetChild(0).position + new Vector3(10f, 10f, 7.5f);
                transform.LookAt(aliens.transform.GetChild(0).position);
                break;
        }
    }

    void ProcessInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            mode = 1;
            transform.position = new Vector3(0f, 0f, 3000f);
            transform.LookAt(Vector3.zero);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            mode = 2;
            findPlanet = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            mode = 3;
            findFleet = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            mode = 4;
            findFleet = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
            mode = 5;
    }
}
