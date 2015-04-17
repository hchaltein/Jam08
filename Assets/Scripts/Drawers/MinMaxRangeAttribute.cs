using UnityEngine;

/// <summary>
/// Prepares variables to be used by PopupDrawer.
/// </summary>

// This is not an editor script. The property attribute class should be placed in a regular script file.
public class MinMaxRangeAttribute : PropertyAttribute
{
	public float MinLimit;
	public float MaxLimit;

	public MinMaxRangeAttribute(float minLimit = 0f, float maxLimit = 1f)
	{
		MinLimit = minLimit;
		MaxLimit = maxLimit;
	}
}