using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Wall : GameBehaviour {

	// Use this for initialization
    [EnumMask]
    public Dimensions ActiveDimensions;

    private GameObject meshContainer;

    protected override void Awake()
    {
        base.Awake();

        meshContainer = this.gameObject;
    }

    public override void ShiftTo(Dimensions dimension)
    {
        base.ShiftTo(dimension);

        meshContainer.SetActive((dimension & ActiveDimensions) == dimension);
    }
}
