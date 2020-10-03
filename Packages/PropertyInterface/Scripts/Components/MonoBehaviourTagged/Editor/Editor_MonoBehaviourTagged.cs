using UnityEditor;

namespace Nectunia.PropertyInterface {
	
	/// <summary>
	/// The default custom Editor for MonoBehaviourTagged component
	/// </summary>
	[CustomEditor(typeof(MonoBehaviourTagged), true)]
	[CanEditMultipleObjects]
	public class Editor_MonoBehaviourTagged : Editor {

		#region ATTRIBUTES ______________________________________________________________________________
		private MonoBehaviourTagged   _targetMonoBehaviourTagged;
		private string[] MonoBehaviourTaggedBaseAttributs = new string[]{"_tagID", "_valuesType", "m_Script"};
		#endregion


		#region EVENTS ___________________________________________________________________________________
		public virtual void OnEnable () {			
			if (Selection.gameObjects.Length == 1) {
				this._targetMonoBehaviourTagged = (MonoBehaviourTagged) this.target;
			}
		}

		public override void OnInspectorGUI () {
			if (Selection.gameObjects.Length == 1) {
				Undo.RecordObject(this._targetMonoBehaviourTagged, "MonoBehaviourTagged");
				//TagID
				this._targetMonoBehaviourTagged.TagID = EditorGUILayout_PropertyInterface.TagIdField_WithIcon("TagID", this._targetMonoBehaviourTagged.TagID);

				// Disable the ValuesType field to avoid that it's been changed. This value is set by the TagID selected.
				EditorGUI.BeginDisabledGroup(true);
				EditorGUILayout.EnumPopup("ValuesType", this._targetMonoBehaviourTagged.ValuesType);
				EditorGUI.EndDisabledGroup();

				//Draw child default inspector excluding the MonoBehaviourTagged fields and Script field
				serializedObject.Update();
				DrawPropertiesExcluding(serializedObject, MonoBehaviourTaggedBaseAttributs);
				serializedObject.ApplyModifiedProperties();
			} else { EditorGUILayout.HelpBox("Multi-object editing not supported", MessageType.None); }
		}
		#endregion
	}
}
	