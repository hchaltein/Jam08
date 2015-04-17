using UnityEngine;
using System.Collections;

public class CheckPoint : GameBehaviour {

	void OnTriggerEnter2D(Collider2D coll)
	{
		Debug.Log (coll.gameObject.name);
		if (coll.gameObject.name == "Body") 
		{
			World.SetCheckpoint();		
			Debug.Log("checkpoint set");
		}
	}

}
