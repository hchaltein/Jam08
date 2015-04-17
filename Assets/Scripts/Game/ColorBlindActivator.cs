using UnityEngine;
using System.Collections;

public class ColorBlindActivator : GameBehaviour
{

    void OnTriggerEnter2D(Collider2D coll)
    {
        var player = Utils.GetPlayerFromCollider(coll);
        if (player != null)
        {
            World.IsColorFuckingBlind = true;
        }
    }
}
