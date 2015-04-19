using UnityEngine;
using System.Collections;

public class JumpLevel : MonoBehaviour {

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") 
		{
			Application.LoadLevel(Application.loadedLevel + 1);
			World.ResetWorld();
		}
	}
}
