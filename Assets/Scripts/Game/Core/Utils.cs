using UnityEngine;

public class Utils
{
	public static CharacterController2D GetPlayerFromCollider(Collider2D collider)
	{
		CharacterController2D player = null;
		Transform t = collider.transform;

		do
		{
			player=t.GetComponent<CharacterController2D>();

			if(player!=null)
			{
				return player;
			}
		}while((t=t.parent) != null);

		return null;


	}
}


