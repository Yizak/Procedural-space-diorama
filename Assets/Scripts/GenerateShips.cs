using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateShips : MonoBehaviour {
    public GameObject prefab, ships, aliens;
    public Material shipMat;
    public Mesh[] bases, engines, weapons;
    public float xPosMin, xPosMax, zPosMin, zPosMax, fleetSeparation;
    public int numFleets, sqrtNumShipsInFleet;

    int targetIndex;
    Transform currentFleet;

    // Use this for initialization
    void Start () {
    }

    public void Generate()
    {
        for (int i = 0; i < numFleets; i++)
        {
            float fleetX = Random.Range(xPosMin, xPosMax);
            float fleetZ = Random.Range(zPosMin, zPosMax);

            GameObject fleet = new GameObject();
            currentFleet = fleet.transform;
            var fleetScript = fleet.AddComponent<FleetScript>();

            fleet.transform.parent = ships.transform;
            fleet.transform.position = new Vector3(fleetX, 0f, fleetZ);

            for (int j = 0; j < 30; j++)
            {
                targetIndex = Random.Range(0, aliens.transform.childCount);
                if (aliens.transform.GetChild(targetIndex).GetComponent<AlienScript>().visited == false)
                    break;
            }
            aliens.transform.GetChild(targetIndex).GetComponent<AlienScript>().visited = true;

            fleetScript.aliens = aliens.transform;
            fleetScript.SetTarget(targetIndex);

            for (int j = 0; j < sqrtNumShipsInFleet; j++)
            {
                for (int k = 0; k < sqrtNumShipsInFleet; k++)
                {
                    GenerateShip((j * fleetSeparation) + fleetX, 0f, (k * fleetSeparation) + fleetZ);
                }
            }
        }
    }

    void GenerateShip(float x, float y, float z)
    {
        GameObject newShip;
        newShip = Instantiate(prefab, Vector3.zero, Quaternion.Euler(-90f, 0f, 0f));

        newShip.transform.parent = currentFleet;
        newShip.transform.position += new Vector3(x, y, z);
        newShip.GetComponent<MeshFilter>().mesh = bases[Random.Range(0, bases.Length)];
        newShip.GetComponent<MeshRenderer>().material = shipMat;
        var shipScript = newShip.AddComponent<ShipScript>();
        shipScript.aliens = aliens.transform;
        shipScript.SetTarget(targetIndex);

        GameObject newEngine;
        newEngine = Instantiate(prefab, new Vector3(1.2f, 0f, 0f), Quaternion.Euler(-90f, 0f, 0f));
        newEngine.transform.parent = newShip.transform;
        newEngine.transform.position += new Vector3(x, y, z);
        newEngine.GetComponent<MeshFilter>().mesh = engines[Random.Range(0, engines.Length)];
        newEngine.GetComponent<MeshRenderer>().material = shipMat;

        GameObject newWeapons;
        newWeapons = Instantiate(prefab, new Vector3(0f, 0f, 4f), Quaternion.Euler(-90f, 0f, 0f));
        newWeapons.transform.parent = newShip.transform;
        newWeapons.transform.position += new Vector3(x, y, z);
        newWeapons.GetComponent<MeshFilter>().mesh = weapons[Random.Range(0, weapons.Length)];
        newWeapons.GetComponent<MeshRenderer>().material = shipMat;
    }
}
