using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a compact list with the provided values (Vectors).
/// </summary>

[CustomPropertyDrawer(typeof(OptionsAttribute))]
class OptionsDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUIUtility.LookLikeControls();

		var enumMask = attribute as OptionsAttribute;

		if (property.propertyType != SerializedPropertyType.Enum && property.propertyType != SerializedPropertyType.Integer)
		{
			EditorGUI.LabelField(position, label.text, "Use Options with Enum or int.");
			return;
		}

		var targetObject = property.serializedObject.targetObject;
		var objType = targetObject.GetType();

		var member = objType.GetMember(property.name)[0];

		var propertyInfo = member as PropertyInfo;
		var fieldInfo = member as FieldInfo;

		var value = 0;
		if (propertyInfo != null) value = (int)propertyInfo.GetValue(targetObject, null);
		else if (fieldInfo != null) value = (int)fieldInfo.GetValue(targetObject);

		var options = enumMask.DisplayedOptions;
		var attrValues = enumMask.OptionValues;

		var content = new GUIContent[options.Length];
		var values = new int[options.Length];

		var customValues = false;
		if (attrValues != null && attrValues.Length == options.Length)
		{
			values = attrValues;
			customValues = true;
		}

		for (int i = 0, len = options.Length; i < len; i++)
		{
			content[i] = new GUIContent(options[i]);

			if (!customValues)
			{
				values[i] = i;
			}

		}

		value = EditorGUI.IntPopup(position, label, value, content, values);

		if (propertyInfo != null) propertyInfo.SetValue(targetObject, value, null);
		else if (fieldInfo != null) fieldInfo.SetValue(targetObject, value);
	}

}