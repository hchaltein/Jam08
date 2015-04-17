using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Spike : GameBehaviour {

    [EnumMask]
    public Dimensions ActiveDimensions;

    private GameObject meshContainer;

    protected override void Awake()
    {
        base.Awake();

        meshContainer = gameObject.FindChild("Mesh");
    }

    public override void ShiftTo(Dimensions dimension)
    {
        base.ShiftTo(dimension);

        var active = (dimension & ActiveDimensions) == dimension;
        meshContainer.SetActive(active);
        collider2D.enabled = active;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        var player = coll.gameObject.GetComponent<CharacterController2D>();
        if (player != null)
        {
            player.Interact(this);
        }
    }
}
