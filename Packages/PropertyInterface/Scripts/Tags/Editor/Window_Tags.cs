using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Class to draw the Tag Windows.
	/// </summary>
	public class Window_Tags : EditorWindow {

		#region PROPERTIES __________________________________________________
		private Tags							_tags;
		private SerializedObject				_tags_SerializedObject;
		private Drawer_TagsList					_tagsReorderableList;
		private Vector2							_scrollPosition_TagsList;
		private Dictionary<string, GUIStyle>    _customSkin;
		#endregion


		#region EVENTS ______________________________________________________
		/// <summary>
		/// Event Trigger when the windows is enable.
		/// </summary>
		public void OnEnable () {
			this._tags = Tags.Instance;
			if (this._tags != null) {
				this._tags_SerializedObject = new SerializedObject(this._tags);
				this._tagsReorderableList = new Drawer_TagsList(this._tags.TagsList, typeof(Tag), Tags.Instance, "TagsList");
			}
			this._tags.OnAfterUndoRedoCallBack += this.OnAfterTagsUndoRedo;
		}

		/// <summary>
		/// Draw the window.
		/// </summary>
		void OnGUI () {
			if (this._tags != null) {
				// Init the customs styles if it hasn't been done. /!\ Have to be done in OnGUI!
				if(this._customSkin == null) { this._customSkin = CustomStyle.TagsList.GetStyles(); }
				this._tags_SerializedObject.Update();
				this.DrawTagsList(); // TagsList
				this._tags_SerializedObject.ApplyModifiedProperties();
			} else { EditorGUILayout.LabelField("En error occured while accessing the Tags asset file. Please check the consol for more informations"); }
		}

		/// <summary>
		/// Event trigger when the window is disable.
		/// </summary>
		public void OnDisable () {
			this._tags.OnAfterUndoRedoCallBack -= this.OnAfterTagsUndoRedo;
			// We check the play mode to avoid null exception during playtime.
			if (!EditorApplication.isPlayingOrWillChangePlaymode) { Tags.SaveAsset(); }
		}

		#endregion


		#region METHODS ___________________________________________________
		/// <summary>
		/// Fit and draw the Tags List.
		/// </summary>
		/// <remarks>
		/// /!\ must be call in OnGUI method.
		/// </remarks>
		protected void DrawTagsList () {
			this._scrollPosition_TagsList = EditorGUILayout.BeginScrollView(this._scrollPosition_TagsList, this._customSkin["Container"]);
			this._tagsReorderableList.DoLayoutList();
			EditorGUILayout.EndScrollView();
		}

		/// <summary>
		/// Callback when undo/redo is performed on Tags.
		/// </summary>
		protected void OnAfterTagsUndoRedo () {			
			this.Repaint();
		}		

		#endregion
	}
}
