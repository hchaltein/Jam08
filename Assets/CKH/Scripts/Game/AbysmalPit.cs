using UnityEngine;
using System.Collections;

public class AbysmalPit : GameBehaviour {

    void OnTriggerEnter2D(Collider2D coll)
    {
		var player = Utils.GetPlayerFromCollider(coll);
        if (player != null)
        {
            player.Interact(this);
        }
    }
}
