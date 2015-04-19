using UnityEngine;
using System.Collections;

public class GoToLevel : MonoBehaviour
{
	public string LevelNumber;

	void OnCollisionEnter2D(Collision2D coll)
	{

		if (coll.gameObject.name == "Player") 
		{
            /*Application.LoadLevel("CKH0" + LevelNumber);
			World.ResetWorld();*/

            CycleManager.Instance.AdvanceToNext();
		}
	}
}
