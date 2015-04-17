using UnityEngine;

/// <summary>
/// Prepares variables to be used by PopupDrawer.
/// </summary>

// This is not an editor script. The property attribute class should be placed in a regular script file.
public class OptionsAttribute : PropertyAttribute
{
	public bool UseSelectAllUnityPattern;
	public string[] DisplayedOptions;
	public int[] OptionValues;

	public OptionsAttribute(params string[] displayedOptions)
	{
		DisplayedOptions = displayedOptions;
	}
}