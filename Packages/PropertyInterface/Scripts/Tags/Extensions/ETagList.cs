using System.Collections.Generic;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Allow a Tag list to return the list of its name.
	/// </summary>
	public static class ExtensionTagList {

		/// <summary>
		/// Return a string array with the list of all Tag name.
		/// </summary>
		/// <param name="list">the Tag list source.</param>
		/// <returns>The list of Tag name as a strgin array.</returns>
		public static string[] ToStringArray (this List<Tag> list) {
			int listCount = list.Count;
			string[] result = new string[listCount];

			if(listCount != 0) {
				for(int i = 0; i < listCount; i++) {
					result[i] = list[i].Name;
				}
			}
			return result;
		}
	}
}
