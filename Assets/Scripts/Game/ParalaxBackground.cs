using UnityEngine;
using System.Collections;

public class ParalaxBackground : MonoBehaviour {

    public GameObject bgParticles;
    public GameObject particleContainer;

	void Start () {

        Debug.Log(particleContainer);
        var container = (GameObject)Instantiate(particleContainer, transform.position, Quaternion.identity);
        container.transform.parent = transform;
        container.name = "Container";

        for (int i = 0; i < 20; i++)
        {
            var particle = (GameObject)Instantiate(bgParticles, transform.position, Quaternion.identity);
            particle.transform.parent = container.transform;
            particle.name = "Particles";
        }
	}
	
	void Update () {
	
	}
}
