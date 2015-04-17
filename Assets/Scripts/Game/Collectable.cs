using UnityEngine;
using System;
using Assets.Scripts.Utils;

public class Collectable : GameBehaviour {

	[EnumMask]
	public Dimensions ActiveDimensions;
	public bool resetOnLoad;
	public bool IsCollected { get; private set; }

	public event Action<Collectable> Collected;

	bool isActive;
	GameObject container;

	bool cpCollected;

	protected override void Awake()
	{
		base.Awake();

		container = gameObject.FindChild("Container");
	}


	public override void ShiftTo(Dimensions dimension)
	{

		base.ShiftTo(dimension);

		isActive = (dimension & ActiveDimensions) == dimension;
		container.SetActive(isActive && !IsCollected);

		if(isActive && !IsCollected)
		{
			var mask = 1 << LayerMask.NameToLayer ("Player");
			
			var collider=Physics2D.OverlapArea ((Vector2)collider2D.bounds.max, (Vector2)collider2D.bounds.min, mask);
			if(collider !=null)
			{
				Collect (collider);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if(!isActive || IsCollected)
		{
			return;
		}

		Collect (collider);
	}


	private void Collect(Collider2D collider)
	{
		var player = Utils.GetPlayerFromCollider (collider);

		if(player != null)
		{
			var canCollect = (World.Dimension & ActiveDimensions) == World.Dimension;
			if(canCollect)
			{
				IsCollected = true;
				container.SetActive(false);
				if(Collected!=null)
				{
                    Collected(this);
				}

			}
		}
	}

	public override void SetCheckpoint ()
	{
		base.SetCheckpoint ();

		cpCollected = IsCollected;

	}
	
	public override void LoadCheckpoint ()
	{
		base.LoadCheckpoint ();
		if(resetOnLoad)
		{
			IsCollected=cpCollected;
			container.SetActive(isActive && !IsCollected);
		}
	}
}