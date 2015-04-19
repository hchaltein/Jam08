
using System;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor.Utils
{
	public static class EditorUtils
	{
		public static int GetEnumMask(object enumInstance, Type enumType)
		{
			var values = Enum.GetValues(enumType);
			var instanceValue = Convert.ToInt32(enumInstance);

			int mask = 0;
			int totalMask = 0;
			foreach (var enumValue in values)
			{
				var value = Convert.ToInt32(enumValue);
				totalMask |= value;
				mask |= value & instanceValue;
			}

			if (totalMask == mask)
				mask = -1;

			return mask;
		}

		public static int GetEnumValue(int mask, Type enumType)
		{
			var isAll = mask == -1;

			var realMask = mask;
			if (isAll)
			{
				realMask = 0;
				var values = Enum.GetValues(enumType);
				foreach (var value in values)
				{
					realMask |= Convert.ToInt32(value);
				}
			}

			return realMask;
		}

		public static TEnum EnumMaskField<TEnum>(string label, TEnum property, bool useSelectAllUnityPattern = false) where TEnum : struct, IConvertible
		{
			Type enumType = typeof(TEnum);
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUILayout.MaskField(label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue<TEnum>(mask, enumType, useSelectAllUnityPattern);
		}

		public static TEnum EnumMaskField<TEnum>(Rect position, string label, TEnum property, bool useSelectAllUnityPattern = false) where TEnum : struct, IConvertible
		{
			Type enumType = typeof(TEnum);
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUI.MaskField(position, label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue<TEnum>(mask, enumType, useSelectAllUnityPattern);
		}


		public static TEnum EnumMaskField<TEnum>(Rect position, GUIContent label, TEnum property, bool useSelectAllUnityPattern = false) where TEnum : struct, IConvertible
		{
			Type enumType = typeof(TEnum);
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUI.MaskField(position, label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue<TEnum>(mask, enumType, useSelectAllUnityPattern);
		}

		public static int EnumMaskField(Type enumType, string label, int property, bool useSelectAllUnityPattern = false)
		{
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUILayout.MaskField(label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue(mask, enumType, useSelectAllUnityPattern);
		}

		public static int EnumMaskField(Type enumType, Rect position, GUIContent label, int property, bool useSelectAllUnityPattern = false)
		{
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUI.MaskField(position, label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue(mask, enumType, useSelectAllUnityPattern);
		}

		public static int EnumMaskField(Type enumType, Rect position, string label, int property, bool useSelectAllUnityPattern = false)
		{
			int mask = GetBeforeEnumMaskValue(property, enumType, useSelectAllUnityPattern);

			mask = EditorGUI.MaskField(position, label, mask, Enum.GetNames(enumType));

			return GetAfterEnumMaskValue(mask, enumType, useSelectAllUnityPattern);
		}

		private static int GetBeforeEnumMaskValue<TEnum>(TEnum property, Type enumType, bool useSelectAllUnityPattern)
		{
			if (!enumType.IsEnum)
			{
				throw new ArgumentException("TEnum must be an enumerated type");
			}

			return useSelectAllUnityPattern ? Convert.ToInt32(property) : GetEnumMask(property, enumType);
		}

		private static int GetBeforeEnumMaskValue(int property, Type enumType, bool useSelectAllUnityPattern)
		{
			return useSelectAllUnityPattern ? Convert.ToInt32(property) : GetEnumMask(property, enumType);
		}

		private static TEnum GetAfterEnumMaskValue<TEnum>(int mask, Type enumType, bool useSelectAllUnityPattern)
		{
			return useSelectAllUnityPattern ? (TEnum)(object)mask : (TEnum)(object)GetEnumValue(mask, enumType);
		}

		private static int GetAfterEnumMaskValue(int mask, Type enumType, bool useSelectAllUnityPattern)
		{
			return useSelectAllUnityPattern ? mask : GetEnumValue(mask, enumType);
		} 

	}
}
