using UnityEditor;
using UnityEngine;
using System;
using System.IO;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Class to manage all Menu entries.
	/// </summary>
	/// <remarks>
	/// Component menu entries have to be set in their own class file so they won't be defined here.
	/// </remarks>
	public class PropertyInterfaceMenu {
		#region Tags ____________________________________________________

		#region Open -> Ctrl+T ---
		/// <summary>
		/// Open the Tags Window.
		/// </summary>
		/// <remarks>
		/// Window/Property Interface/Open Tags => Ctrl + T
		/// </remarks>
		[MenuItem ("Window/Property Interface/Open Tags %T")]
		public static void  ShowTagsWindow () {
			//Show existing window instance. If none exist, make one.
			Window_Tags currentWindow = EditorWindow.GetWindow<Window_Tags>();
			// Set the Title text and Icon
			Texture icon = AssetDatabase.LoadAssetAtPath<Texture> ("Packages/com.nectunia.propertyinterface/Icons/Tags.png");
			GUIContent titleContent = new GUIContent ("Tags", icon);
			currentWindow.titleContent = titleContent;
			currentWindow.minSize = new Vector2(300, 150);
		}
		#endregion

		#region Export -> Ctrl+Shift+T ---
		/// <summary>
		/// Export Tags to a JSON file.
		/// </summary>
		/// <remarks>
		/// Window/Property Interface/Export Tags => Ctrl + Shift + T
		/// </remarks>
		[MenuItem ("Window/Property Interface/Export Tags %#T")]
		public static void  ExportTags () {
			string defaultName = Application.productName + "_" + DateTime.Now.ToString("yyyyMMdd") + "-Tags.json";
			string destination = EditorUtility.SaveFilePanel("Export Tags to ...", Application.dataPath, defaultName, "json");
			if(destination != "") {
				TagListWrapper tagsWrapper = new TagListWrapper(Tags.Instance.TagsList);
				// Convert the TagsInstance to JSON and write the result in the file selected
				string content = EditorJsonUtility.ToJson(tagsWrapper, true);
				using (StreamWriter streamWriter = new StreamWriter(destination)) { streamWriter.Write(content); }
			}
		}
				
		/// <summary>
		/// Enable the Export Tags Menu.
		/// </summary>
		/// <returns>True if at least one Tag exists, False otherwise.</returns>
		[MenuItem ("Window/Property Interface/Export %#T", true)]
		public static bool ValidateExportTags () {
			return Tags.Instance.TagsList.Count > 0;
		}
		#endregion
		
		#region Import -> Ctrl+Alt+T ---
		/// <summary>
		/// Import Tags from JSON file.
		/// </summary>
		/// <remarks>
		/// Window/Property Interface/Import Tags => Ctrl + Alt + T
		/// </remarks>
		[MenuItem ("Window/Property Interface/Import Tags %&T")]
		public static void  ImportTags () {
			string source = EditorUtility.OpenFilePanel("Import Tags from ...", Application.dataPath, "json");
			if(source != "") {
				if(EditorUtility.DisplayDialog("Warning!", "If you continu you will lose all your current Tags.\n/!\\ You cannot undo this action.\nDo you want to continue ?", "Yes", "No")) {
					try {
						// Read the file
						string json;
						using (StreamReader streamReader = new StreamReader(source)) { json = streamReader.ReadToEnd(); }

						if(json != "") {
							TagListWrapper tagsWrapper = new TagListWrapper(Tags.Instance.TagsList);
							EditorJsonUtility.FromJsonOverwrite(json, tagsWrapper);
							Tags.Instance.TagsList = tagsWrapper._tagsList;
						}
					} catch {
						Debug.Log("Impossible to load Tags : the file is not a valide JSON format");
					}
				}
			}
		}
		#endregion
				
		#endregion

		#region Property ________________________________________________
		// Property Menu entry is set in the Property class because it's a component entry
		#endregion

	}
}
