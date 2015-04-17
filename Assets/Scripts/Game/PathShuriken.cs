using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class PathShuriken : GameBehaviour {

	[EnumMask]
	public Dimensions dimensionsToShow;
	public Dimensions dimensionsToMove;
	public bool resetWhenVisible;
	public bool ignorePlayerPresence;
	/*public GameObject[] hidenMeshes;*/
	public Transform[] waypoints;
	private GameObject meshContainer;
	private int i;

    private bool isMoving;
	protected override void Awake()
	{
        isMoving = false;
		base.Awake();
		i = 0;
		meshContainer = gameObject.FindChild("Mesh");
		meshContainer.transform.position = waypoints [0].position;
	}


	protected override void Update()
	{
		base.Update();
        
		if (((World.Dimension & dimensionsToMove) == World.Dimension) && (isMoving || ignorePlayerPresence))
		{
            if (i < waypoints.Length -1)
            {
                var dir = (meshContainer.transform.position - waypoints[i].position);
                float distance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);

                dir = (waypoints[i + 1].position - waypoints[i].position);
                float currentDistance = (dir.x * dir.x) + (dir.y * dir.y) + (dir.z * dir.z);

                Debug.Log(currentDistance.ToString());


                dir = dir.normalized;

                Debug.Log(distance.ToString());

                if (distance < currentDistance)
                {
                    meshContainer.transform.Translate(dir * 4 * Time.deltaTime);
                }
                else
                {
                    i++;
                }
            }
            else
            {
                
                i = 0;
                meshContainer.transform.position = waypoints[0].position;
                
            }

		}
		
	}

	public override void ShiftTo(Dimensions dimension)
	{
		base.ShiftTo(dimension);
		var isVisible = (dimension & dimensionsToShow) == dimension;

		meshContainer.SetActive(isVisible);
		// show when active or when inactive and we dont need to hide
		/*foreach (var mesh in hidenMeshes)
		{
			mesh.SetActive(isActive || !hideWhenInactive);
		}*/

		if (isVisible && resetWhenVisible)
		{
			meshContainer.transform.position = waypoints[0].transform.position;
		}
	}

	public void EnableMoving() {
		isMoving = true;
	}
	public void DisableMoving() {
		isMoving = false;
	}


#if UNITY_EDITOR
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.magenta;
        for (int index = 0; index < waypoints.Length - 1; index++)
        {
            Gizmos.DrawLine(waypoints[index].position, waypoints[index + 1].position);
        }

        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(waypoints[0].position, .1f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(waypoints[waypoints.Length - 1].position, .1f);
    } 
#endif

}
