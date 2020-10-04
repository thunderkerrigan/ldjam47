using UnityEngine;
using UnityEditor;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Editor for PropertyTriggerHandler component.
	/// </summary>
	/// <remarks>
	/// This class is used as an example on how to create a custom Editor for a component wich will use Nectunia.PropetyInterface asset.
	/// </remarks>
	[CustomEditor(typeof(PropertyTriggerHandler),true)]
	public class Editor_PropertyTriggerHandler : Editor { 
		#region PROPERTIES _________________________________________________________
		private PropertyTriggerHandler _propertyHandler;

		#endregion


		#region EVENTS _____________________________________________________________
		public void OnEnable () {
			this._propertyHandler =  (PropertyTriggerHandler) target;
		}
		
		public override void OnInspectorGUI () {
			// Allow undo in editor
			Undo.RecordObject(this._propertyHandler, "PropertyTriggerHandler");

			// Refresh ValuesType in case the Tag have changed it's ValuesType
			this._propertyHandler.UpdateValuesType();

			// Draw TagID field with it's attached icon
			this._propertyHandler.TagID = EditorGUILayout_PropertyInterface.TagIdField_WithIcon( "Tag ID", this._propertyHandler.TagID);

			/*
			// Draw TagID field when Tag icon are not used
			this._propertyHandler.TagID = EditorGUILayout_PropertyInterface.TagIdField( "Tag ID", this._propertyHandler.TagID);
			*/

			// Draw Action and TypedValue field
			EditorGUILayout.BeginHorizontal();
			this._propertyHandler.Action = (PropertyTriggerHandler.ActionOnTrigger) EditorGUILayout.EnumPopup(this._propertyHandler.Action, GUILayout.MaxWidth(100), GUILayout.MinWidth(85));
			EditorGUILayout_PropertyInterface.TypedValueField(this._propertyHandler.TypedValue, GUILayout.MinWidth(160));
			EditorGUILayout.EndHorizontal();

			// Drax ApplyToChildren Fields
			this._propertyHandler.AffectChildren = EditorGUILayout.Toggle("Affect children", this._propertyHandler.AffectChildren);
		}
		#endregion
	}
}

