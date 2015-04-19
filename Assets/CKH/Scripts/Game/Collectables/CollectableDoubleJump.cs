using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class CollectableDoubleJump : GameBehaviour
{

    public GameObject PlayerObj;
    public Collectable[] collectables;

	protected override void Start()
	{
		base.Start ();

		foreach (var collectable in collectables) {
			collectable.Collected += OnCollect;
		}
	}

	void OnCollect(Collectable collectable)
	{

		int collectedCounter = 0;
		foreach (var collectableObject in collectables) 
		{
			if(collectableObject.IsCollected)
			{
				collectedCounter++;
			}
		}
        
        // Makes player interact with collectable
        PlayerObj.GetComponent<CharacterController2D>().Interact(this);

	}
}
