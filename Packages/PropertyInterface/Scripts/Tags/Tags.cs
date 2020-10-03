using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEditor;
using UnityEditor.SceneManagement;
using Nectunia.Utility;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// List of Tag stored in a ScriptableObject file in the Asset folder.
	/// </summary>
	/// <remarks>
	/// 1. This is a singleton. /!\ You should only acces it by using "Tags.Instance". /!\<br>
	/// 2. Tags are automaticaly load when Unity Editor is opened so you don't need to instanciate them.<br>
	/// 3. If many file exist only the first one found at random will be load.<br>
	/// </remarks>
	/// <example>
	/// <code>
	/// // To get the Tag list
	/// List<Tag> tagsList = Tags.Instance.TagsList;
	/// 
	/// // To get a specifique Tag with it's TagID
	/// long tagID1 = 111111111; // TagID have an implicite conversion with long value
	/// TagID tagID2 = tagsList[1].Id;
	/// 
	/// Tag tag1 = tagsList.Find(t => t.Id == tagID1);
	/// Tag tag2 = tagsList.Find(t => t.Id == tagID2);
	/// 
	/// Tag tag3 = Tags.GetTagByID(3333333333) // TagID have an implicite conversion with long value
	/// Tag tag4 = Tags.GetTagByID(tagID2) 
	/// 
	/// // To set a specifique Tag
	/// tagsList[1] = tag1;
	/// Tags.Instance.TagsList[0] = tag2;
	/// Tags.Instance.TagsList[3].Id = 44444444; // TagID have an implicite conversion with long value
	/// </code>
	/// </example>
	public class Tags : ScriptableObject {
		#region CONSTANTS ____________________________________________________________
		/// <summary>
		/// The default Path where the Tags file will be saved.
		/// </summary>
		public const string C_DEFAULT_ASSET_FILE_PATH = "Assets/Plugins/PropertyInterface";

		/// <summary>
		/// The default file name to save the Tags file.
		/// </summary>
		public const string C_DEFAULT_TAGS_FILE_NAME = "Tags.asset";
		#endregion


		#region PROPERTIES  __________________________________________________________
		[SerializeField]
		private List<Tag> _tagsList = new List<Tag>();

		/// <summary>
		/// The Tag list.
		/// </summary>
		public List<Tag> TagsList {
			get { return _tagsList; }
			set { _tagsList = value; }
		}
		#endregion


		#region SINGLETON ____________________________________________________________
		private static Tags _instance;
		private static string _assetFilePath = "";

		/// <summary>
		/// The path where the Tags file is saved.
		/// </summary>
		public static string AssetFilePath {
			get { return _assetFilePath;}
		}

		/// <summary>
		/// Get the Tags instance.
		/// </summary>
		/// <remarks>
		/// This should be the only way to access Tags.
		/// </remarks>
		public static Tags Instance {
			get {
				// In case where the file as been moved since the last call of Instance
				if (!EditorApplication.isPlayingOrWillChangePlaymode) { UpdateAssetFilePath(); }

				if (_instance == null) { _instance = GetExistingInstance(); }
 				if (_instance == null) { _instance = GetAssetFileInstance(); }
				if (_instance == null ) { _instance = CreateInstance<Tags>(); }
				if (_assetFilePath == "") { CreateNewAssetFile(); }
				return _instance;
			}
		}

		/// <summary>
		/// Get the existing instance if one already exists.
		/// </summary>
		/// <returns>The instance found or null if none exists.</returns>
		private static Tags GetExistingInstance () {
			Tags result = null;			
			Tags[] fileList = Resources.FindObjectsOfTypeAll<Tags>();
			if(fileList.Length > 0) {
				result = fileList[0];
				_assetFilePath = AssetDatabase.GetAssetPath(result);
			}
			return result; 
		}

		/// <summary>
		/// Get the existing Asset file instance if one already exists.
		/// </summary>
		/// <returns>The instance found or null if none exists.</returns>
		private static Tags GetAssetFileInstance () {
			Tags result = null;

			string[] assetFilesGUID = AssetDatabase.FindAssets("t:" + typeof(Tags).FullName);
			if(assetFilesGUID.Length > 0) {
				_assetFilePath = AssetDatabase.GUIDToAssetPath(assetFilesGUID[0]);
				if(_assetFilePath != "") {
					result = (Tags) AssetDatabase.LoadAssetAtPath<Tags>(_assetFilePath);
					if(result == null) {
						Debug.Log("Impossible to load the Property Tags asset from file : " + _assetFilePath);
						_assetFilePath = "";
					} //else { Debug.Log("Load completed for the Property Tags asset file : " + _assetFilePath, result); }
				}
			}			
			return result;
		}

		/// <summary>
		/// Try to create an asset file to save _instance.
		/// </summary>
		/// <returns>True if the file has been created or false otherwise.</returns>
		private static bool CreateNewAssetFile () {
			bool result = false;
			if (_instance != null) {
				try {
					if (!AssetDatabase.IsValidFolder(C_DEFAULT_ASSET_FILE_PATH)) { ExtendedAssetDatabase.CreatePath(C_DEFAULT_ASSET_FILE_PATH); }
					AssetDatabase.CreateAsset(_instance, C_DEFAULT_ASSET_FILE_PATH + "/" + C_DEFAULT_TAGS_FILE_NAME);
					_assetFilePath = AssetDatabase.GetAssetPath(_instance);
					//Debug.Log("The Tags asset file has been created in the path : " + _assetFilePath);
					result = true;

				} catch (Exception e) {
					Debug.Log("Error while creating Tags asset file : " + e.Message);
				}
			}			
			return result;
		}
		
		/// <summary>
		/// Update the asset file path if it has been modified.
		/// </summary>
		private static void UpdateAssetFilePath () {
			if (_instance != null) {
				string newPath = AssetDatabase.GetAssetPath(_instance);
				if (newPath != _assetFilePath) { _assetFilePath = newPath; }
			} else { _assetFilePath = ""; }
		}
		#endregion


		#region EVENTS _______________________________________________________________			
		public delegate void OnChangedTagListDelegate ();
		public delegate void OnAfterUndoRedoDelegate ();
		public delegate void OnChangedTagDelegate (Tag tag);
		public event OnChangedTagListDelegate	OnChangedTagListCallBack;
		public event OnChangedTagDelegate		OnChangedTagCallBack;
		public event OnAfterUndoRedoDelegate	OnAfterUndoRedoCallBack;
		
		/// <summary>
		/// Force Tags load when Editor is loading.
		/// </summary>
		[InitializeOnLoadMethod]
		public static void OnEditorLoaded () {
			// Load the first Tags file found if one exists
			if (_instance == null && !EditorApplication.isPlayingOrWillChangePlaymode) {
				// At this point, the AssetDatabase haven't load ".asset" files yet. We need to parse them manualy
				// Get Absolute Path
				string[] assetsFilesPath = Directory.GetFiles(Application.dataPath, "*.asset", SearchOption.AllDirectories);
				foreach(string currentPath in assetsFilesPath) {
					// Get relative Path
					string currentAssetPath = "Assets" + currentPath.Replace(Application.dataPath, "").Replace('\\', '/');
					// Force the ".asset" files to load
					UnityEngine.Object[] assetsAtPath = AssetDatabase.LoadAllAssetsAtPath(currentAssetPath);
					foreach(UnityEngine.Object currentAsset in assetsAtPath) {
						if (currentAsset != null && currentAsset.GetType() == typeof(Tags)) {
							_instance = currentAsset as Tags;
							_assetFilePath = currentAssetPath;
							break;
						}
					}
					if(_instance != null) { break; }
				}
			}
			
			// If no Tags file exists, create a new one
			if (_instance == null) { _instance = Instance; }
		}
		
		public void OnEnable () {
			// If there are more than 1 Tags ".asset" file, it avoid  to change the file used
			if (_instance != null) { _instance = this; } 
			// OnEnable can be called twice for unknown reason. This avoid to call events twice.
			Undo.undoRedoPerformed			-= this.OnAfterUndoRedo;
			EditorSceneManager.sceneOpened	-= this.CheckMonoBehavioursTaggedOnSceneOpened;
			this.OnChangedTagCallBack		-= CheckMonoBehavioursTaggedOnSceneOpened;
			// Subscribe Events
			Undo.undoRedoPerformed			+= this.OnAfterUndoRedo;
			EditorSceneManager.sceneOpened	+= this.CheckMonoBehavioursTaggedOnSceneOpened;
			this.OnChangedTagCallBack		+= CheckMonoBehavioursTaggedOnSceneOpened;
		}

		public void OnDisable () {
			Undo.undoRedoPerformed			-= this.OnAfterUndoRedo;
			EditorSceneManager.sceneOpened	-= this.CheckMonoBehavioursTaggedOnSceneOpened;
			this.OnChangedTagCallBack		-= CheckMonoBehavioursTaggedOnSceneOpened;
		}

		/// <summary>
		/// Handler triggered when Tags has been changed.
		/// </summary>
		public void OnChangedTagList () {
			this.OnChangedTagListCallBack?.Invoke();
		}	
		
		/// <summary>
		/// Handler triggered when a Tag is delete to the Tag list.
		/// </summary>
		public void OnChangedTag (Tag tag) {
			this.OnChangedTagList();
			this.OnChangedTagCallBack?.Invoke(tag);
		}

		/// <summary>
		/// Handler triggered when undo is performed.
		/// </summary>
		public void OnAfterUndoRedo () {
			this.OnChangedTagList();
			this.OnAfterUndoRedoCallBack?.Invoke();
		}
		#endregion
		

		#region METHODES _____________________________________________________________		
		/// <summary>
		/// Get the GUID of the asset file if it exist.
		/// </summary>
		/// <returns>The GUID if the file exist, "" otherwise.</returns>
		public static string GetGUID () {
			return AssetDatabase.AssetPathToGUID(_assetFilePath);
		}
				
		/// <summary>
		/// Get the GUID of the asset file if it exist.
		/// </summary>
		/// <returns>The GUID if the file exist, "" otherwise.</returns>
		public static Tag GetTagByID (TagID tagID) {
			Tag result = _instance?.TagsList.Find(t => t.Id == tagID);
			if(result == null) { result = Tags.Instance.TagsList.Find(t => t.Id == tagID); }
			return result;
		}

		/// <summary>
		/// Check Integrity of all MonoBehaviourTagged in the scene.
		/// </summary>
		/// <param name="scene">The Scene that was opened.</param>
		/// <param name="mode">The mode used to open the Scene.</param>
		private void CheckMonoBehavioursTaggedOnSceneOpened (Scene scene, OpenSceneMode mode) {
			List<MonoBehaviourTagged> taggedComponents = MonoBehaviourTagged.GetMonoBehavioursTagged(true);
			this.CheckMonoBehavioursTaggedOnSceneOpened(taggedComponents);
		}
		
		/// <summary>
		/// Check Integrity of all MonoBehaviourTagged in the scene that refer the passed Tag.
		/// </summary>
		/// <param name="tag">The Tag to check.</param>
		private void CheckMonoBehavioursTaggedOnSceneOpened (Tag tag) {
			List<MonoBehaviourTagged> taggedComponents = MonoBehaviourTagged.GetMonoBehavioursTagged(tag.Id, true);
			this.CheckMonoBehavioursTaggedOnSceneOpened(taggedComponents);
		}
		
		/// <summary>
		/// Check Integrity of all MonoBehaviourTagged in the scene that refer the passed Tag. Update the components wich have no integrity.
		/// </summary>
		/// <param name="taggedComponents">The list of components to check</param>
		private void CheckMonoBehavioursTaggedOnSceneOpened (List<MonoBehaviourTagged> taggedComponents) {
			// Parse all MonoBehaviourTagged
			foreach(MonoBehaviourTagged currentComponent in taggedComponents) {
				if (!currentComponent.CheckIntegrity()) { currentComponent.UpdateValuesType(); }
			}
		}

		/// <summary>
		/// Save the modifications on the .asset file.
		/// </summary>
		public static void SaveAsset () {
			if (_instance != null) {
				AssetDatabase.Refresh();
				EditorUtility.SetDirty(_instance);
				AssetDatabase.SaveAssets();
			}
		}
		#endregion
	}
}
