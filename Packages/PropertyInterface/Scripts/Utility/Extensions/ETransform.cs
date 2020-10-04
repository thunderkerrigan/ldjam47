using UnityEngine;

namespace Nectunia.Utility {

	/// <summary>
	/// Extension class for Transform.
	/// </summary>
	public static class ETransform{
		/// <summary>
		/// Get the path of the current transform.
		/// </summary>
		/// <param name="current">the Transform to get the path.</param>
		/// <returns>The path of the transform.</returns>
		public static string GetPath(this Transform current) {
			if (current.parent == null) { return "/" + current.name; }
			return current.parent.GetPath() + "/" + current.name;
		}
	}
}
