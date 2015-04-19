using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a compact list with the provided values (Vectors).
/// </summary>

[CustomPropertyDrawer(typeof(CompactAttribute))]
class CompactDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUIUtility.LookLikeControls();

		// Now draw the property as a Slider or an IntSlider based on whether it's a float or integer.
		if (property.propertyType == SerializedPropertyType.Vector3)
		{
			property.vector3Value = EditorGUI.Vector3Field(position, label.text, property.vector3Value);
		}
		else if (property.propertyType == SerializedPropertyType.Vector2)
		{
			property.vector2Value = EditorGUI.Vector2Field(position, label.text, property.vector2Value);
		}
		else if (property.propertyType == SerializedPropertyType.Rect)
		{
			property.rectValue = EditorGUI.RectField(position, label.text, property.rectValue);
		}
		else
		{
			EditorGUI.LabelField(position, label.text, "Use Compact with rect, Vector3 or Vector2.");
		}
	}

	// Here you must define the height of your property drawer. Called by Unity.
	public override float GetPropertyHeight (SerializedProperty prop, GUIContent label)
	{
		var h = 0;

		if (prop.propertyType == SerializedPropertyType.Vector3 || prop.propertyType == SerializedPropertyType.Vector2)
		{
			h = 20;
		}
		else if (prop.propertyType == SerializedPropertyType.Rect)
		{
			h = 38;
		}


	   return base.GetPropertyHeight(prop, label) + h;
	}

}