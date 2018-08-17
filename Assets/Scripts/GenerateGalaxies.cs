using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateGalaxies : MonoBehaviour {
    public Transform galaxyParticle, galaxies;

    int numGalaxies = 6;

	// Use this for initialization
	void Start ()
    {
        galaxies.transform.position = Vector3.zero;

        for (int i = 0; i < numGalaxies; i++)
        {
            GameObject galaxy = new GameObject();
            //Instantiate(galaxy, Vector3.zero, Quaternion.identity);
            galaxy.transform.parent = galaxies;
            spawnStarsSpiral(galaxy);
            galaxy.transform.rotation = Quaternion.Euler(Random.Range(-450f, 45f), 0f, Random.Range(-45f, 45f));
            galaxy.transform.position = Random.onUnitSphere * 10000f;
        }
    }

    void spawnStarsSpiral(GameObject galaxy)
    {
        float A = Random.Range(400f, 600f), B = Random.Range(14f, 18f), N = Random.Range(0.5f, 0.7f);

        for (int i = 0; i < 360 * 8; i++)
        {
            float angle = i * Mathf.Deg2Rad;
            float dist = A / Mathf.Log10(B * Mathf.Tan(angle / (2 * N)));
            if (dist < Mathf.Abs(A) && dist > Random.Range(50f, 200f))
            {
                float angleOffset = Random.Range(-10f, 10f) * Mathf.Deg2Rad;
                float x = Mathf.Cos(angle + angleOffset) * dist;
                float z = Mathf.Sin(angle + angleOffset) * dist;
                Vector3 position = new Vector3(x, 0f, z);
                Transform newGalaxyParticle = Instantiate(galaxyParticle, position, Quaternion.identity);
                newGalaxyParticle.parent = galaxy.transform;
                newGalaxyParticle.localScale = newGalaxyParticle.localScale * Random.Range(0f, 1f - (Mathf.Clamp(Mathf.Abs(dist), 0f, A) / A));
            }
        }
    }
}
