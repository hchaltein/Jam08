using UnityEngine;
using System.Collections;

public class ChangeLayer : MonoBehaviour 
{
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.LeftShift))
			gameObject.layer = 14;
		if(Input.GetKeyUp(KeyCode.LeftShift))
			gameObject.layer = 13;
	}
}
