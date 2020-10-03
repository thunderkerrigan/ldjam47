using UnityEditor;
using Nectunia.Utility;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// The custom Editor for Property component
	/// </summary>
	[CustomEditor(typeof(Property), true)]
	[CanEditMultipleObjects]
	public class Editor_Property : Editor{

		#region ATTRIBUTES ______________________________________________________________________________
		protected Property		_targetProperty;
		protected Tags			_tags;
		protected string[]		_tagsNamesArray;
		protected List<string>	_componentPropertiesNames;

		// Labels
		protected GUIContent  _typeLabel		= new GUIContent("Tag",				"The Tag of the Property");
		protected GUIContent  _ALabel			= new GUIContent("A",				"The Value A of the Property");
		protected GUIContent  _ASourceLabel		= new GUIContent("source : A",		"The Component's field that will be used to source the value A");
		protected GUIContent  _sourceTypeLabel	= new GUIContent("Sources Type",	"How the values of the Property will be set");
		protected GUIContent  _componentLabel	= new GUIContent("Component",		"In wich Component the value(s) will be sourced");
		//protected GUIContent  _warningContent;
		#endregion


		#region EVENTS ___________________________________________________________________________________
		public virtual void OnEnable () {
			if(Selection.gameObjects.Length == 1) {
				this._targetProperty = (Property) this.target;
				this._tags = Tags.Instance;
				this._componentPropertiesNames = new List<string>();			
			}
		}
		
		public override void OnInspectorGUI () {
			if (Selection.gameObjects.Length == 1) {
				if (this._tags.TagsList.Count > 0) {
					Undo.RecordObject(this._targetProperty, "Property modification ");
					// Reset the object value if undo action have been done
					if (Event.current.type == EventType.ValidateCommand && Event.current.commandName == "UndoRedoPerformed") { this._targetProperty.RefreshValuesObject(); }

					// Tag				
					this._targetProperty.TagID = EditorGUILayout_PropertyInterface.TagIdField_WithIcon(this._typeLabel, this._targetProperty.TagID);

					Tag currentTag = this._targetProperty.Tag;
					bool tagIsNull = currentTag == null;
					if (tagIsNull) {
						if (this._targetProperty.TagID != -1L) { EditorGUILayout.HelpBox("The Tag selected doesn't exist. You have to select a new one to be able to edit this Property", MessageType.Warning); } 
						else { EditorGUILayout.HelpBox("You must select a Tag to be able to edit this Property", MessageType.Warning); }
					}

					if (!tagIsNull || (tagIsNull && this._targetProperty.TagID != -1L)) {
						this.DrawSourceType(!tagIsNull);
						this.DrawComponent(!tagIsNull);
						// Force the targetProperty to update
						this.UpdateProperty();
						// All the Values ---
						this.DrawValues(!tagIsNull);
					}

				} else {
					EditorGUILayout.HelpBox("You must create Tag to be able to edit Property component", MessageType.Info);
					if(GUILayout.Button("Tags window")) { PropertyInterfaceMenu.ShowTagsWindow(); }
				}
			}else { EditorGUILayout.HelpBox("Multi-object editing not supported", MessageType.None); }
		}
		#endregion


		#region METHODS __________________________________________________________________________________
		/// <summary>
		/// Draw the sourceType.
		/// </summary>
		/// <param name="enable">Is the field Enable ?</param>
		protected virtual void DrawSourceType (bool enable) {
			EditorGUI.BeginDisabledGroup(!enable); // Begin - 1
			this._targetProperty.SourcesType = (Property.SourceType) EditorGUILayout.EnumPopup(this._sourceTypeLabel, this._targetProperty.SourcesType);
			EditorGUI.EndDisabledGroup(); // End - 1
		}

		/// <summary>
		/// Draw the Component Fields if source is set to Property.SourceType.Component.
		/// </summary>
		/// <param name="enable">Is the field Enable ?</param>
		protected void DrawComponent (bool enable) {
			if(this._targetProperty.SourcesType != Property.SourceType.Manual ) {
				EditorGUI.BeginDisabledGroup(!enable); // Begin - 1

				this._targetProperty.Component = (Component)EditorGUILayout.ObjectField(this._componentLabel,this._targetProperty.Component, typeof(Component), true);
				// Set Component properties list
				if(this._targetProperty.Component != null) {
					this._componentPropertiesNames.Clear();
					this._componentPropertiesNames.AddRange(this._targetProperty.Component.GetPropertiesNames(this._targetProperty.GetValuesSystemType(), BindingFlags.Public | BindingFlags.Instance));					
					this._componentPropertiesNames.AddRange(this._targetProperty.Component.GetFieldsNames(this._targetProperty.GetValuesSystemType(), BindingFlags.Public | BindingFlags.Instance));
					this._componentPropertiesNames.Sort();					
				} else { this._componentPropertiesNames.Clear(); }

				// Draw the sources
				EditorGUI.BeginDisabledGroup(this._targetProperty.Component == null); // Begin - 2
				this.DrawValuesSources();
				EditorGUI.EndDisabledGroup(); // End - 2
				EditorGUI.EndDisabledGroup(); // End - 1
			}
		}

		/// <summary>
		/// Draw All values.
		/// </summary>
		/// <param name="enable">Enable the fields or not.</param>
		protected void DrawValues (bool enable) {			
			EditorGUI.BeginDisabledGroup(!enable); // Begin - 1
			EditorGUILayout.Space();
			EditorGUILayout.BeginVertical(EditorStyles.helpBox);
			this.DrawValuesField();
			EditorGUILayout.EndVertical();
			EditorGUI.EndDisabledGroup(); // End - 1
		}

		/// <summary>
		/// Draw the fields of the values.
		/// </summary>
		protected virtual void DrawValuesField () {
			this.PropertyValueField(this._ALabel, this._targetProperty.A);
		}
		
		/// <summary>
		/// Draw the fields of the values sources.
		/// </summary>
		protected virtual void DrawValuesSources () {			
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._ASourceLabel, this._targetProperty.A, this._componentPropertiesNames);
		}
		
		/// <summary>
		/// Force the targetProperty to update.
		/// </summary>
		protected virtual void UpdateProperty () {			
			this._targetProperty.DelayedUpdate();
		}

		/// <summary>
		/// Draw a PropertyValue field and Enable / Disable it depending on the Property it's attached to.
		/// </summary>
		/// <remarks>
		/// The field will be enable in the following case :
		/// -The PropertyValue.SourceType is Manual.
		/// -The PropertyValue.SourceType is Component but the PropertyValue.Source is empty and the Property have other PropertyValue with source not empty.
		/// </remarks>
		/// <param name="label">The label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw.</param>
		protected void PropertyValueField (GUIContent label, PropertyValue propertyValue) {
			bool disableField = (propertyValue.SourceType == Property.SourceType.Component_Master && (!this._targetProperty.AtLeastOneSourceIsSet() || propertyValue.Source !=""));
			EditorGUI.BeginDisabledGroup(disableField);
			EditorGUILayout_PropertyInterface.TypedValueField(label, propertyValue.TypedValue);
			EditorGUI.EndDisabledGroup();
		}
		#endregion
	}
}
