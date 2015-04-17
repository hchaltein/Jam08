using UnityEngine;

namespace Assets.Scripts.Utils {

	public static class GameObjectExtension {
		
		public static GameObject FindChild (this GameObject obj, string childName) {
			var child = obj.transform.FindChild(childName);
			return child == null ? null : child.gameObject;
		}
		
	}
	
}