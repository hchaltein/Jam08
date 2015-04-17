using System.Linq;
using UnityEngine;

public class DrawColliders : MonoBehaviour {

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;

		DrawCollider(transform);
	}

	private void DrawCollider(Transform t)
	{
		if (t.collider != null)
		{
			var mc = t.collider as MeshCollider;
			if (mc != null)
			{
				DrawMeshCollider(mc, t.position);
			}

			var bc = t.collider as BoxCollider;
			if (bc != null)
			{
				DrawBoxCollider(bc, t.position);
			}
		}

		foreach (Transform child in t)
		{
			DrawCollider(child);
		}
	}

	
	private void DrawBoxCollider(BoxCollider meshCollider, Vector3 offset)
	{
		Gizmos.DrawWireCube(meshCollider.center + offset + new Vector3(0, 0, -10), meshCollider.size);
	}

	private void DrawMeshCollider(MeshCollider meshCollider, Vector3 offset)
	{
		var r = Quaternion.Euler(meshCollider.transform.eulerAngles);

		var vertices = meshCollider.sharedMesh.vertices.Select(v => r * v).ToArray();
		var triangles = meshCollider.sharedMesh.triangles;
		
		offset += new Vector3(0, 0, -10);
		
		Vector3 v1, v2, v3;
		for (int j = 0; j < triangles.Length; j += 3)
		{
			v1 = vertices[triangles[j + 0]];
			v2 = vertices[triangles[j + 1]];
			v3 = vertices[triangles[j + 2]];


			Gizmos.DrawLine(offset + v1, offset + v2);
			Gizmos.DrawLine(offset + v2, offset + v3);
			Gizmos.DrawLine(offset + v3, offset + v1);
		}
	}
}
