using UnityEngine;

namespace Nectunia.Utility {

	/// <summary>
	/// Extension class for Component
	/// </summary>
	public static class EComponent{
		/// <summary>
		/// Get the path of the current Component.
		/// </summary>
		/// <param name="component">The Component to get the path.</param>
		/// <returns>The path of the Component.</returns>
		public static string GetPath(this Component component) {
			return component.transform.GetPath() + "/" + component.GetType().ToString();
		}
	}
}
