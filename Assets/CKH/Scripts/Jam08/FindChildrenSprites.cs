using UnityEngine;
using System.Collections;

public class FindChildrenSprites : MonoBehaviour 
{
	void Start () 
	{
		foreach(Transform child in transform)
		{
			child.gameObject.AddComponent("ChangeSpriteLayers");
		}
	}
}
