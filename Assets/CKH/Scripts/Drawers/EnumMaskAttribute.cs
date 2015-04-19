using UnityEngine;

/// <summary>
/// Prepares variables to be used by PopupDrawer.
/// </summary>

// This is not an editor script. The property attribute class should be placed in a regular script file.
public class EnumMaskAttribute : PropertyAttribute
{
	public bool UseUnityPattern;

	public EnumMaskAttribute(bool useUnityPattern = false)
	{
		UseUnityPattern = useUnityPattern;
	}
}