using UnityEngine;
using System.Collections;

namespace GameExtensionMethods {
	public static class BoundsExtensions {
		public static float GetTop (this Bounds bounds) {
			return (bounds.center.y + bounds.extents.y);
		}

		public static float GetBottom (this Bounds bounds) {
			return (bounds.center.y - bounds.extents.y);
		}

		public static float GetLeft (this Bounds bounds) {
			return (bounds.center.x - bounds.extents.x);
		}

		public static float GetRight (this Bounds bounds) {
			return (bounds.center.x + bounds.extents.x);
		}

		public static float GetRandomY (this Bounds bounds) {
			return Random.Range(bounds.GetBottom(), bounds.GetTop());
		}

		public static float GetRandomX (this Bounds bounds) {
			return Random.Range(bounds.GetLeft(), bounds.GetRight());
		}
	}
}