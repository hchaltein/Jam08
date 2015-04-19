using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class FollowingShuriken : GameBehaviour {

	[EnumMask]
	public Dimensions ActiveDimensions;
	public bool isActive;
	public bool resetWhenActive;
	public bool hideWhenInactive;
	
	float startX;
	float startY;
	bool started = false;


	private GameObject Player;
	private GameObject meshContainer;

	protected override void Awake ()
	{
		base.Awake ();
		Player = null;
		Player = GameObject.Find ("Player");
		if (Player == null) 
		{
			Debug.Log("player object not found");
		}
		meshContainer = gameObject.FindChild("Mesh");
		isActive = false;
		
		startX = meshContainer.transform.position.x;
		startY = meshContainer.transform.position.y;

	}

	protected override void Start ()
	{
		//Debug.Log(startX + " x");
		//Debug.Log(startY + " y");
	}

	protected override void Update ()
	{
		//Debug.Log(startX + " x");
		//Debug.Log(startY + " y");
		if(started == false)
		{
			startX = meshContainer.transform.position.x;
			startY = meshContainer.transform.position.y;
			started = true;
		}

		base.Update ();
		if (isActive)
		{
			var dir = Player.transform.position - meshContainer.transform.position;
			meshContainer.transform.Translate(dir * 2 * Time.deltaTime); 
		}
	}

    public void EnableMoving()
    {
        isActive = true;
    }
    public void DisableMoving()
    {
        isActive = false;

		meshContainer.transform.position = new Vector3(startX,startY,0);
    }

}
