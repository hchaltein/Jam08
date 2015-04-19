using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Assets.Scripts.Utils {

	public static class TransformExtension {
		
		public static Transform[] FindChildren (this Transform obj, string childName)
		{
			return FindChildren(obj, new Regex(childName));
		}

		public static Transform[] FindChildren (this Transform obj, Regex childName)
		{
			return obj.transform.OfType<Transform>().Where(t => childName.IsMatch(t.name)).ToArray();
		}

		public static Transform[] FindChildrenRecursive(this Transform obj, string childName)
		{
			return FindChildrenRecursive(obj, new Regex(childName));
		}

		public static Transform[] FindChildrenRecursive(this Transform obj, Regex childName)
		{
			var children = new Transform[0];

			return obj.FindChildrenRecursive(childName, ref children);
		}

		private static Transform[] FindChildrenRecursive(this Transform obj, Regex childName, ref Transform[] children, bool stopAtFirst = false)
		{
			if (stopAtFirst && children.Length > 0)
			{
				return children;
			}

			foreach (Transform child in obj)
			{
				if (childName.IsMatch(child.name))
				{
					ArrayHelper.AddArrayElement(ref children, child);
					break;
				}

				child.FindChildrenRecursive(childName, ref children);
			}

			return children;
		}

		public static Transform FindChildRecursive(this Transform obj, string childName)
		{
			return FindChildRecursive(obj, new Regex(childName));
		}
			
		public static Transform FindChildRecursive(this Transform obj, Regex childName)
		{
			var children = new Transform[0];

			return obj.FindChildrenRecursive(childName, ref children).FirstOrDefault();
		}
	}
	
}