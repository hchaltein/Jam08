using UnityEngine;
using System.Collections;

public class GoToLevel : MonoBehaviour
{
	public int LevelNumber;

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.name == "Player") 
		{
			Application.LoadLevel(LevelNumber);
			World.ResetWorld();
		}
	}
}
