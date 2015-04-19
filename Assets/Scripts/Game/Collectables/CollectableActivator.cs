using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class CollectableActivator : GameBehaviour {

	public Platform platform;
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

		if(collectedCounter == collectables.Length)
		{
			platform.SetActiveDimensions((Dimensions)0);
		}

	}
}
