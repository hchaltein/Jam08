using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;


public class FallingPlatform : GameBehaviour {

	public float timeToFall=1.0f;
	public bool resetWhenActive;

	[EnumMask]
	public Dimensions FallingDimensions;

	bool isTimerTicking;
	bool isFalling;
	float currentTime;
	private Dimensions ActiveDimensions;

	GameObject meshContainer;
		
	Vector3 platformCheckPoint;
	
	protected override void Awake()
	{
		base.Awake();

		var g = GetComponent<Platform> ();
		ActiveDimensions = g.ActiveDimensions;
		meshContainer = gameObject.FindChild("Mesh");
		currentTime = timeToFall;
	}


	public override void ShiftTo(Dimensions dimension)
	{
		base.ShiftTo(dimension);
		var active = (dimension & ActiveDimensions) == dimension;
		//meshContainer.SetActive(active);
		if(resetWhenActive && active)
		{
			LoadCheckpoint();
		}
	}


	void OnCollisionStay2D(Collision2D collision)
	{
		var player = collision.gameObject.GetComponent<CharacterController2D>();
		if(player != null)
		{
			var contact = collision.contacts[0];
			var normal = contact.normal;
			var willFall = (World.Dimension & FallingDimensions) == World.Dimension;
			if(willFall && normal == -Vector2.up)
			{
				isTimerTicking=true;
			} 
			else if(isFalling && normal == Vector2.up)
			{
				player.Interact(this);
			}
		}
	}

	protected override void Update()
	{
		if (isTimerTicking) 
		{
			currentTime -= Time.deltaTime;
		}
		if(currentTime < 0.0)
		{
			rigidbody2D.isKinematic=false;
			meshContainer.collider2D.enabled = false;
			isFalling=true;
		}
	}
	
	public override void SetCheckpoint ()
	{
		base.SetCheckpoint ();

		//save position, booleans, timer and rigidBody state
		platformCheckPoint = transform.position;

	}

	public override void LoadCheckpoint ()
	{
		base.LoadCheckpoint ();
		transform.position = platformCheckPoint;
		rigidbody2D.isKinematic = true;
		currentTime = timeToFall;
		isFalling = false;
		isTimerTicking = false;
		meshContainer.collider2D.enabled = true;
	}

}