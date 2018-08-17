using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratePlanets : MonoBehaviour {
    public GameObject prefab;
    public Transform planets;
    public Mesh sphere, ring;
    public Shader shader;

    const float speedCoefficient = 10000000.0f;

    // Planet positioning parameters
    public float xPosMin, xPosMax, yPosMin, yPosMax, zPosMin, zPosMax;
    public float ScaMax, ScaMin;
    public int noOfPlanets;

    // Use this for initialization
    void Start() {
        for (int i = 0; i < noOfPlanets; i++)
            GeneratePlanet();
    }

    // Update is called once per frame
    void Update() {

    }

    void GeneratePlanet()
    {
        GameObject newPlanet;
        float planetScale = Random.Range(ScaMin, ScaMax);
        // Set position multipliers to either -1 or 1
        int xSide = (Random.Range(1, 3) * 2) - 3;
        int zSide = (Random.Range(1, 3) * 2) - 3;

        newPlanet = Instantiate(prefab,
            new Vector3(Random.Range(xSide * xPosMin, xSide * xPosMax),
            Random.Range(yPosMin, yPosMax),
            Random.Range(zSide * zPosMin, zSide * zPosMax)),
            Quaternion.identity);

        newPlanet.transform.parent = planets;
        newPlanet.transform.localScale = new Vector3(planetScale, planetScale, planetScale);
        newPlanet.GetComponent<MeshFilter>().mesh = sphere;
        newPlanet.AddComponent<PlanetScript>();

        Renderer rend = newPlanet.GetComponent<Renderer>();
        rend.material = new Material(Shader.Find("Standard"));

        // PLANET COLOUR ASSIGNMENT

        float distanceToStar = newPlanet.transform.position.magnitude;
        float max = 12000f;
        float distanceAsProportion = Mathf.Clamp(distanceToStar, 0f, max) / max;
        float closenessAsProportion = (max - Mathf.Clamp(distanceToStar, 0f, max)) / max;

        rend.material.color = new Color(
            Random.Range(0f, closenessAsProportion),
            Random.Range(0f, distanceAsProportion),
            Random.Range(0f, distanceAsProportion));

        if (dice(4))
        {
            GameObject newRing = Instantiate(prefab, newPlanet.transform.position, Quaternion.Euler(90f, 0f, 0f));
            newRing.transform.parent = newPlanet.transform;
            newRing.transform.localScale = Vector3.one;
            newRing.GetComponent<MeshFilter>().mesh = ring;
        }

        ///////////////////////////

        rend.material.SetFloat("_Glossiness", 0.1f);

        newPlanet.GetComponent<PlanetScript>().orbital = true;
        newPlanet.GetComponent<PlanetScript>().orbitalSpeed = speedCoefficient / (Mathf.PI * Mathf.Pow(newPlanet.transform.position.magnitude, 2f));
        newPlanet.GetComponent<PlanetScript>().rotateSpeed = 0.05f;
    }

    bool dice(int sides)
    {
        if (Random.Range(0, sides) < 1)
            return true;
        else
            return false;
    }
}