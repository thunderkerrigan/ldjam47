using UnityEditor;

namespace Nectunia.Utility {

	/// <summary>
	/// Extension to AssetDatabase.
	/// </summary>
	public static class ExtendedAssetDatabase {

		/// <summary>
		/// Create the given path and return the GUID of the last folder.
		/// </summary>
		/// <param name="path">The path to create.</param>
		/// <returns>Return the GUID of the last folder created or null if path is not valide.</returns>
		public static string CreatePath (string path) {
			string result = null;
			if (path != "" && path != null) {			
				string[] folders = path.Split(new char[]{'/'});
				string currentPath = "";
				string parentPath = "";
				foreach (string currentFolder in folders) {
					currentPath += currentFolder;
					// If the current Folder exists return it's GUID, else try to create it and return it's GUID.
					if (!AssetDatabase.IsValidFolder(currentPath)) {
						result = AssetDatabase.CreateFolder(parentPath, currentFolder);
					} else { result = AssetDatabase.AssetPathToGUID(currentPath); }

					parentPath = currentPath;
					currentPath += "/";
				}				
			}			
			return result;
		}
	}
}
