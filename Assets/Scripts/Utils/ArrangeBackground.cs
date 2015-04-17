using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ArrangeBackground : MonoBehaviour {

    public float distance = 1;

    public void ArrangeParticles()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);

            child.localPosition = new Vector3((i + 1) * distance, 0, 0);
        }
    }

    void Update()
    {
        if (!Application.isPlaying)
        {
            ArrangeParticles();
        }
    }


}
