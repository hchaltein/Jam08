using UnityEngine;
using System.Collections;

public class FollowingShurikenScript : MonoBehaviour {


	// Use this for initialization
    void OnTriggerEnter2D(Collider2D coll)
    {
        var player = Utils.GetPlayerFromCollider(coll);
        if (player != null)
        {
            this.transform.parent.GetComponent<FollowingShuriken>().EnableMoving();
        }
    }

    void OnTriggerExit2D(Collider2D coll)
    {
        var player = Utils.GetPlayerFromCollider(coll);
        if (player != null)
        {
            this.transform.parent.GetComponent<FollowingShuriken>().DisableMoving();
        }
    }
}
