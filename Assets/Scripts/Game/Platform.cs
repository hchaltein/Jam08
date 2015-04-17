using UnityEngine;
using System.Collections;
using Assets.Scripts.Utils;

public class Platform : GameBehaviour {

    [EnumMask]
    public Dimensions ActiveDimensions;

    public bool isYellowPlatform;

    public Sprite activeSprite;
    public Sprite inactiveSprite;

    private Color Red { get { return World.IsColorFuckingBlind ? new Color(213 / 255.0f, 94 / 255.0f, 0 / 255.0f) : new Color(214 / 255.0f, 69 / 255.0f, 64 / 255.0f); } }
    private Color Green { get { return World.IsColorFuckingBlind ? new Color(0 / 255.0f, 158 / 255.0f, 115 / 255.0f) : new Color(57 / 255.0f, 163 / 255.0f, 65 / 255.0f); } }
    private Color Both = new Color(59 / 255.0f, 99 / 255.0f, 161 / 255.0f);
    private Color Yellow { get { return World.IsColorFuckingBlind ? new Color(236 / 255.0f, 211 / 255.0f, 67 / 255.0f) : new Color(240 / 255.0f, 228 / 255.0f, 66 / 255.0f); } }

    private GameObject meshContainer;
    private SpriteCover spriteCover;

	private Dimensions cpActiveDimensions;

    protected override void Awake()
    {
        base.Awake();

		meshContainer = gameObject.FindChild("Mesh");
        spriteCover = gameObject.FindChild("Sprites").GetComponent<SpriteCover>();
    }

	public void SetActiveDimensions(Dimensions dimension)
	{
		ActiveDimensions = dimension;
		ShiftTo (World.Dimension);
	}

    public override void ShiftTo(Dimensions dimension)
    {
        base.ShiftTo(dimension);
        var active = (dimension & ActiveDimensions) == dimension;
        meshContainer.SetActive(active);

        spriteCover.Sprite = active ? activeSprite : inactiveSprite;
		spriteCover.Color = (ActiveDimensions == Dimensions.Red) ? Red : (ActiveDimensions == Dimensions.Green) ? Green : (isYellowPlatform ? Yellow : Both);
    }

	public override void SetCheckpoint ()
	{
		base.SetCheckpoint ();
		cpActiveDimensions = ActiveDimensions;

	}

	public override void LoadCheckpoint ()
	{
		base.LoadCheckpoint ();
		SetActiveDimensions (cpActiveDimensions);

	}
}
