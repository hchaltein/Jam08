using UnityEngine;
using System.Collections;

public class ChangeSpriteLayers : MonoBehaviour 
{
	void Start () 
	{
		gameObject.layer = gameObject.transform.parent.gameObject.layer;
	}
}
