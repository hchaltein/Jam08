using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a compact list with the provided values (Vectors).
/// </summary>

[CustomPropertyDrawer(typeof(MinMaxRangeAttribute))]
class MinMaxRangeDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUIUtility.LookLikeControls();

		var attr = attribute as MinMaxRangeAttribute;

		if (attr == null)
		{
			return;
		}

		if (property.propertyType == SerializedPropertyType.Vector2)
		{
			var min = property.vector2Value.x;
			var max = property.vector2Value.y;

			var rect = position;
			rect.xMin += 5;

			

			var labelRect = new Rect(rect.x, rect.y, rect.width * .5f, rect.height);
			var contentRect = new Rect(labelRect.xMax, rect.y, rect.width *.5f, rect.height);

			EditorGUI.LabelField(labelRect, label);
			EditorGUI.MinMaxSlider(contentRect, ref min, ref max, attr.MinLimit, attr.MaxLimit);

			property.vector2Value = new Vector2(min, max);
		}
		else
		{
			EditorGUI.LabelField(position, label.text, "Use MinMaxRange with Vector2.");
		}
	}

	// Test if the propertys string value matches the regex pattern.
	bool IsValid(SerializedProperty prop)
	{
		return prop.propertyType == SerializedPropertyType.Vector2;
	}

	// Here you must define the height of your property drawer. Called by Unity.
	public override float GetPropertyHeight (SerializedProperty prop, GUIContent label) 
	{
	   return base.GetPropertyHeight(prop, label) + (IsValid(prop) ? 20.0f : 0.0f);
	}

}