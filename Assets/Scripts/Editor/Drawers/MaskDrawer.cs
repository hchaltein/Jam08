using System.Reflection;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a compact list with the provided values (Vectors).
/// </summary>

[CustomPropertyDrawer(typeof(MaskAttribute))]
class MaskDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUIUtility.LookLikeControls();

		var enumMask = attribute as MaskAttribute;

		if (property.propertyType != SerializedPropertyType.Enum && property.propertyType != SerializedPropertyType.Integer)
		{
			EditorGUI.LabelField(position, label.text, "Use Mask with Enum or int.");
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
		
		value = EditorGUI.MaskField(position, label, value, enumMask.DisplayedOptions);

		if (propertyInfo != null) propertyInfo.SetValue(targetObject, value, null);
		else if (fieldInfo != null) fieldInfo.SetValue(targetObject, value);
	}

}