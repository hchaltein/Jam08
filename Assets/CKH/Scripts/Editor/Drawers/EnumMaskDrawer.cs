using System;
using System.Reflection;
using Assets.Scripts.Editor.Utils;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Creates a compact list with the provided values (Vectors).
/// </summary>

[CustomPropertyDrawer(typeof(EnumMaskAttribute))]
class EnumMaskDrawer : PropertyDrawer
{
	// Draw the property inside the given rect
	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		EditorGUIUtility.LookLikeControls();

		var enumMask = attribute as EnumMaskAttribute;

		if (property.propertyType != SerializedPropertyType.Enum)
		{
			EditorGUI.LabelField(position, label.text, "Use EnumMask with Enum.");
			return;
		}

		var targetObject = property.serializedObject.targetObject;
		var objType = targetObject.GetType();
		
		var member = objType.GetMember(property.name)[0];
		Type type = null;
		
		var propertyInfo = member as PropertyInfo;
		var fieldInfo = member as FieldInfo;

		if (propertyInfo != null) type = (propertyInfo).PropertyType;
		else if (fieldInfo != null) type = (fieldInfo).FieldType;
		
		//var value = 0;
		//if (propertyInfo != null) value = (int)propertyInfo.GetValue(targetObject, null);
		//else if (fieldInfo != null) value = (int)fieldInfo.GetValue(targetObject);

		var value = property.intValue;

		value = EditorUtils.EnumMaskField(type, position, label, value, enumMask.UseUnityPattern);
		
		//if (propertyInfo != null) propertyInfo.SetValue(targetObject, value, null);
		//else if (fieldInfo != null) fieldInfo.SetValue(targetObject, value);

		property.intValue = value;
		
	}

}