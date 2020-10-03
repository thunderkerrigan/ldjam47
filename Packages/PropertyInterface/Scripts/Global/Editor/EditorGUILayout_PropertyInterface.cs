using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using Nectunia.Utility;
using System.Reflection;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Interface for PropertyInterface gui with automatic layout
	/// </summary>
	public static class EditorGUILayout_PropertyInterface {
		#region PROPERTIES ___________________________________________________
		/// <summary>
		/// With for a Tag icon field.
		/// </summary>
		public static float TagIconWidth = EditorGUIUtility.singleLineHeight * 2;

		/// <summary>
		/// With for a single line Tag icon field.
		/// </summary>
		public static float SingleLineIconWidth = EditorGUIUtility.singleLineHeight - 2;

		/// <summary>
		/// Min width to be able to draw a Vector field in a single line.
		/// </summary>
		public static int MinInlineVectorFieldWidth = 416; // When the inspector window is smaller than this value, Vector fields need an extra line to be draw
		
		private static GUIContent _warnIcon = null;
		
		/// <summary>
		/// The unity console Warn icon.
		/// </summary>
		public static GUIContent Warnicon {
			get {
				if (_warnIcon == null) {
					_warnIcon = new GUIContent(EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D);
				}
				return _warnIcon;
			}
		}
		#endregion

		#region METHODS ______________________________________________________
		#region TagIdField_WithIcon() ---
		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		///<param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField_WithIcon(new GUIContent(""), tagIDValue, options);
		}
		
		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		///<param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (string label, long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField_WithIcon(new GUIContent(label), tagIDValue, options);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		///<param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (GUIContent label, long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField_WithIcon(label, (TagID)tagIDValue, options);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (TagID tagID, params GUILayoutOption[] options) {
			return TagIdField_WithIcon(new GUIContent(""), tagID, options);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (string label, TagID tagID, params GUILayoutOption[] options) {
			return TagIdField_WithIcon(new GUIContent(label), tagID, options);
		}
				
		/// <summary>
		/// Draw a TagID Field and it's icon .
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="displayedOption">The Tag list to search in.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties. These options will only be apply to the field and not on the picture.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (GUIContent label, TagID tagID, params GUILayoutOption[] options) {
			TagID result = tagID;
			Tags tags = Tags.Instance;
			// Init TagList
			string[] tagsNames = tags.TagsList.ToStringArray();
			// Init index
			int index = tags.TagsList.FindIndex(p => p.Id == tagID);			

			EditorGUILayout.BeginHorizontal();		// Begin - 1
			// Property Picture
			TagIconField(index);

			// Tag ---
			EditorGUILayout.BeginVertical();		// Begin - 2
			GUILayout.FlexibleSpace();

			EditorGUIUtility.labelWidth -= (TagIconWidth + 3); // Add 3 for margin between element
			int newIndex = EditorGUILayout.Popup(label, index, tagsNames, options);
			EditorGUIUtility.labelWidth += (TagIconWidth + 3);

			GUILayout.FlexibleSpace();
			EditorGUILayout.EndVertical();			// End - 2

			if(newIndex != index) { result = tags.TagsList[newIndex].Id; }
			
			EditorGUILayout.EndHorizontal();        // End - 1
			return result;
		}
		#endregion


		#region TagIdField() ---
		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField(new GUIContent(""), (TagID)tagIDValue, options);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (string label, long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField(new GUIContent(label), (TagID)tagIDValue, options);
		}
		
		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (GUIContent label, long tagIDValue, params GUILayoutOption[] options) {
			return TagIdField(label, (TagID)tagIDValue, options);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (TagID tagID, params GUILayoutOption[] options) {
			return TagIdField(new GUIContent(""), tagID, options);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (string label, TagID tagID, params GUILayoutOption[] options) {
			return TagIdField(new GUIContent(label), tagID, options);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (GUIContent label, TagID tagID, params GUILayoutOption[] options) {
			TagID result = tagID;
			Tags tags = Tags.Instance;
			// Init TagList
			string[] tagsNames = tags.TagsList.ToStringArray();
			// Init index
			int index = tags.TagsList.FindIndex(p => p.Id == tagID);
			
			int newIndex = EditorGUILayout.Popup(label, index, tagsNames, options);

			if(newIndex != index) { result = tags.TagsList[newIndex].Id; }

			return result;
		}
		#endregion


		#region TagIconField() ---
		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		public static void TagIconField (long tagIDValue) {
			TagIconField((TagID)tagIDValue);
		}

		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		public static void TagIconField (TagID tagID) {
			TagIconField(Tags.Instance.TagsList.FindIndex(p => p.Id == tagID));
		}

		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="index">The index of the Tag, in Tags.TagsList list, to draw.</param>
		public static void TagIconField (int index) {
			Tags tags = Tags.Instance;
			GUIContent propertyIcon = new GUIContent((Texture2D)null);
			if( index < tags.TagsList.Count && index >= 0) { propertyIcon = new GUIContent(tags.TagsList[index].Icon); }
			EditorGUILayout.LabelField(propertyIcon, GUILayout.Width(TagIconWidth), GUILayout.Height(TagIconWidth));			
		}

		#endregion

		
		#region PropertyValueFields() ---			
		/// <summary>
		/// Draw all PropertyValue fields.
		/// </summary>
		/// <param name="propertyValue">The PropertyValue to draw the fields.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties. It will be applied to all fields except to the Component's one.</param>
		public static void PropertyValueFields (PropertyValue propertyValue, params GUILayoutOption[] options) {
			// Value Type
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			propertyValue.ValueType = (Tag.ValueType) EditorGUILayout.EnumPopup(valueTypeLabel, propertyValue.ValueType, options);

			// Source Type
			GUIContent  sourceTypeLabel	= new GUIContent("SourcesType",	"How the values of the Property will be set");
			propertyValue.SourceType = (Property.SourceType) EditorGUILayout.EnumPopup(sourceTypeLabel, propertyValue.SourceType, options);

			if(propertyValue.SourceType == Property.SourceType.Component_Master) {
				// Component
				GUIContent  componentLabel	= new GUIContent("Component",	"In wich Component the value will be sourced");
				propertyValue.Component = (Component)EditorGUILayout.ObjectField(componentLabel, propertyValue.Component, typeof(Component), true);

				// Source
				GUIContent  sourceLabel	= new GUIContent("Source",	"The component's field that will be used to source the value");
				PropertyValueSourceField(sourceLabel, propertyValue, options);

				// Update value with Source
				propertyValue.SynchronizeValue();
			}
			
			// Value			
			EditorGUI.BeginDisabledGroup(propertyValue.SourceType == Property.SourceType.Component_Master);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valueLabel, propertyValue.TypedValue, options);
			EditorGUI.EndDisabledGroup();
		}
			
		/// <summary>
		/// Draw all PropertyValue fields.
		/// </summary>
		/// <param name="propertyValue">The PropertyValue as a SerializedProperty.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties. It will be applied to all fields.</param>
		public static void PropertyValueFields (SerializedProperty propertyValue, params GUILayoutOption[] options) {			
			// Get Property fields
			SerializedProperty typedValue		= propertyValue.FindPropertyRelative("_typedValue");
			SerializedProperty sourceType		= propertyValue.FindPropertyRelative("_sourceType");
			SerializedProperty component		= propertyValue.FindPropertyRelative("_component");
			SerializedProperty source			= propertyValue.FindPropertyRelative("_source");
			// Get TypedValue fields
			SerializedProperty valueSerialized	= typedValue.FindPropertyRelative("_valueSerialized");
			SerializedProperty valueType		= typedValue.FindPropertyRelative("_valueType");

			// Value Type
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			EditorGUILayout.PropertyField(valueType, valueTypeLabel, options);				

			// Source Type
			GUIContent  sourceTypeLabel	= new GUIContent("SourcesType",	"How the values of the Property will be set");
			int oldValueType = sourceType.intValue;
			EditorGUILayout.PropertyField(sourceType, sourceTypeLabel, options);
			// Reset all settings when the type change
			if(oldValueType != sourceType.intValue) {
				valueSerialized.stringValue = PropertySerialization.NULL_VALUE;
				component.objectReferenceValue = null;
				source.stringValue = "";
			}
				
			if(sourceType.enumValueIndex == (int)Property.SourceType.Component_Master) {
				// Component
				GUIContent  componentLabel	= new GUIContent("Component",	"In wich Component the value will be sourced");
				EditorGUILayout.PropertyField(component, componentLabel, options);
				Component componentObject = (Component)component.objectReferenceValue;

				// Source
				GUIContent  sourceLabel	= new GUIContent("Source",	"The component's field that will be used to source the value");
				string newSource = PropertyValueSourceField(sourceLabel, source.stringValue, componentObject, (Tag.ValueType)valueType.intValue, options);
				if (newSource != source.stringValue) {
					source.stringValue = newSource;
					valueSerialized.stringValue = PropertySerialization.NULL_VALUE;
				}

				// Update value with Source
				if(source.stringValue != "") {
					PropertyInfo propertyInfo = componentObject?.GetType().GetProperty(source.stringValue);
					FieldInfo fieldInfo = componentObject?.GetType().GetField(source.stringValue);
					if(propertyInfo != null) { valueSerialized.stringValue = PropertySerialization.ObjectToString(propertyInfo.GetValue(componentObject)); }
					if(fieldInfo != null) { valueSerialized.stringValue = PropertySerialization.ObjectToString(fieldInfo.GetValue(componentObject)); }
				}
			}
				
			// Value
			EditorGUI.BeginDisabledGroup(sourceType.enumValueIndex == (int)Property.SourceType.Component_Master);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valueLabel, typedValue, options);
			EditorGUI.EndDisabledGroup();
		}
		#endregion


		#region PropertyValueSourceField() ---	
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void PropertyValueSourceField (string label, PropertyValue propertyValue,  params GUILayoutOption[] options) {
			PropertyValueSourceField(new GUIContent(label), propertyValue, options);
		}

		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void PropertyValueSourceField (GUIContent label, PropertyValue propertyValue,  params GUILayoutOption[] options) {			
			List<string> fieldsNames = new List<string>();
			if(propertyValue.Component != null) {
				fieldsNames.AddRange(propertyValue.Component.GetPropertiesNames(Tag.GetSystemType(propertyValue.ValueType), BindingFlags.Public | BindingFlags.Instance));					
				fieldsNames.AddRange(propertyValue.Component.GetFieldsNames(Tag.GetSystemType(propertyValue.ValueType), BindingFlags.Public | BindingFlags.Instance));
				fieldsNames.Sort();
			}
			PropertyValueSourceField(label, propertyValue, fieldsNames, options);
		}
					
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void PropertyValueSourceField (string label, PropertyValue propertyValue, List<string> displayedOptions, params GUILayoutOption[] options) {
			PropertyValueSourceField(new GUIContent(label), propertyValue, displayedOptions, options);
		}

		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void PropertyValueSourceField (GUIContent label, PropertyValue propertyValue, List<string> displayedOptions, params GUILayoutOption[] options) {			
			GUIContent icon = null;
			if (propertyValue.IsReadOnly) {
				icon = Warnicon;
				icon.tooltip = "This source is reandOnly. The value will be updated by the source, but the source won't be updated by the value";
			}
			List<string> popupOptions = new List<string> ();
			popupOptions.Add("-");
			popupOptions.AddRange(displayedOptions);
			EditorGUI.BeginDisabledGroup(propertyValue.Component == null);
			string newSource = StringPopup_WithIcon(icon, label, propertyValue.Source, popupOptions, 0, options);
			EditorGUI.EndDisabledGroup();
			if(newSource == "-") { newSource = ""; }
			propertyValue.Source = newSource;
		}
		
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="label">The Label of the field.</param>
		/// <param name="selectedValue">The selected value in the field.</param>
		/// <param name="component">The component of the PropertyValue.</param>
		/// <param name="valueType">The ValueType of the PropertyValue.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new string selected in the field.</returns>
		public static string PropertyValueSourceField (GUIContent label, string selectedValue, Component component, Tag.ValueType valueType, params GUILayoutOption[] options) {
			// Init values
			string result = selectedValue;
			GUIContent icon = null;
			List<string> popupOptions = new List<string> ();
			popupOptions.Add("-");

			if(component != null) {
				// Init Field / Properties list
				popupOptions.AddRange(component.GetPropertiesNames(Tag.GetSystemType(valueType), BindingFlags.Public | BindingFlags.Instance));					
				popupOptions.AddRange(component.GetFieldsNames(Tag.GetSystemType(valueType), BindingFlags.Public | BindingFlags.Instance));
				popupOptions.Sort();
				
				// Draw a warning if the selected field / property is read only
				bool isReadOnly = false;
				PropertyInfo propertyInfo = component.GetType().GetProperty(selectedValue);
				FieldInfo fieldInfo = component.GetType().GetField(selectedValue);
				if(propertyInfo != null) { isReadOnly = !propertyInfo.CanWrite; }
				if(fieldInfo != null) { isReadOnly = fieldInfo.IsInitOnly; }
				if (isReadOnly) {
					icon = Warnicon;
					icon.tooltip = "This source is reandOnly. The value will be updated by the source, but the source won't be updated by the value";
				}
			}
			
			EditorGUI.BeginDisabledGroup(component == null);
			result = StringPopup_WithIcon(icon, label, selectedValue, popupOptions, 0, options);
			EditorGUI.EndDisabledGroup();
			if(result == "-") { result = ""; }

			return result;
		}
		#endregion

				
		#region TypedValueFields() ---
		/// <summary>
		/// Draw a TypedValue fields.
		/// </summary>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueFields (TypedValue typedValue,  params GUILayoutOption[] options) {
			// Value Type
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			typedValue.ValueType = (Tag.ValueType) EditorGUILayout.EnumPopup(valueTypeLabel, typedValue.ValueType, options);
			
			// Value
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valueLabel, typedValue, options);
		}

		/// <summary>
		/// Draw a TypedValue fields.
		/// </summary>
		/// <param name="typedValue">The TypedValue to draw as SerializedProperty.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueFields (SerializedProperty typedValue,  params GUILayoutOption[] options) {
			// Get fields
			SerializedProperty valueType = typedValue.FindPropertyRelative("_valueType");

			// Value Type
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			EditorGUILayout.PropertyField(valueType, valueTypeLabel, options);
			
			// Value
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valueLabel, typedValue, options);
		}
		#endregion


		#region TypedValueField() ---
		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (TypedValue typedValue, params GUILayoutOption[] options) {
			TypedValueField(new GUIContent(""), typedValue, options);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (string label, TypedValue typedValue, params GUILayoutOption[] options) {
			TypedValueField(new GUIContent(label), typedValue, options);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (GUIContent label, TypedValue typedValue, params GUILayoutOption[] options) {
			typedValue.Value = TypedValueField(label, typedValue.ValueType, typedValue.Value, options);
		}
		
		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (SerializedProperty typedValue, params GUILayoutOption[] options) {
			TypedValueField(new GUIContent(""), typedValue, options);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (string label, SerializedProperty typedValue, params GUILayoutOption[] options) {
			TypedValueField(new GUIContent(label), typedValue, options);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		public static void TypedValueField (GUIContent label, SerializedProperty typedValue, params GUILayoutOption[] options) {
			SerializedProperty valueSerialized	= typedValue.FindPropertyRelative("_valueSerialized");
			SerializedProperty valueType		= typedValue.FindPropertyRelative("_valueType");

			// Check if the serialized property is a TypedValue
			if(valueSerialized != null && valueType != null) {
				object value = PropertySerialization.StringToObject(valueSerialized.stringValue);
				if(value == null) { value = ((Tag.ValueType) valueType.intValue).GetDefaultSystemValue(); }
				value = TypedValueField( label, (Tag.ValueType) valueType.intValue, value, options);
				valueSerialized.stringValue = PropertySerialization.ObjectToString(value);
			}
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="valueType">The type of the value as a Tag.ValueType.</param>
		/// <param name="value">The object to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new value. If value had a bad type : return the default value for the given ValueType.</returns>
		public static object TypedValueField (string label, Tag.ValueType valueType, object value, params GUILayoutOption[] options) {
			return TypedValueField( new GUIContent(label), valueType, value, options);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="valueType">The type of the value as a Tag.ValueType.</param>
		/// <param name="value">The object to draw.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new value. If value had a bad type : return the default value for the given ValueType.</returns>
		public static object TypedValueField (GUIContent label, Tag.ValueType valueType, object value, params GUILayoutOption[] options) {
			object result = valueType.GetDefaultSystemValue();
			try {				
				switch ( valueType ){
					case Tag.ValueType.Boolean :	// Boolean
						if (typeof(bool).IsInstanceOfType(value))		{ result = EditorGUILayout.Toggle(label,			(bool) value, options); }
						break;	

					case Tag.ValueType.Color :		// Color
						if (typeof(Color).IsInstanceOfType(value))		{ result = EditorGUILayout.ColorField(label,		(Color) value, options); }
						break;

					case Tag.ValueType.Double :		// Double
						if (typeof(Double).IsInstanceOfType(value))		{ result = EditorGUILayout.DoubleField(label,		(Double) value, options); }
						break;

					case Tag.ValueType.Float :		// Float
						if (typeof(float).IsInstanceOfType(value))		{ result = EditorGUILayout.FloatField(label,		(float) value, options); }
						break;

					case Tag.ValueType.Int :		// Int
						if (typeof(int).IsInstanceOfType(value))		{ result = EditorGUILayout.IntField(label,			(int) value, options); }
						break;

					case Tag.ValueType.Long :		// Long
						if (typeof(long).IsInstanceOfType(value))		{ result = EditorGUILayout.LongField(label,			(long) value, options); }
						break;

					case Tag.ValueType.String :		// String
						if (typeof(string).IsInstanceOfType(value))		{ result = EditorGUILayout.TextField(label,			(string) value, options); }
						break;

					case Tag.ValueType.Vector2 :	// Vector2
						if (typeof(Vector2).IsInstanceOfType(value))	{ result = EditorGUILayout.Vector2Field(label,		(Vector2) value, options); }
						break;

					case Tag.ValueType.Vector2Int : // Vector2Int
						if (typeof(Vector2Int).IsInstanceOfType(value)) { result = EditorGUILayout.Vector2IntField(label,	(Vector2Int) value, options); }
						break;

					case Tag.ValueType.Vector3 :	// Vector3
						if (typeof(Vector3).IsInstanceOfType(value))	{ result = EditorGUILayout.Vector3Field(label,		(Vector3) value, options); }
						break;

					case Tag.ValueType.Vector3Int :	// Vector3Int
						if (typeof(Vector3Int).IsInstanceOfType(value)) { result = EditorGUILayout.Vector3IntField(label,	(Vector3Int) value, options); }
						break;

					case Tag.ValueType.Vector4 :	// Vector4
						if (typeof(Vector4).IsInstanceOfType(value))	{ result = EditorGUILayout.Vector4Field(label,		(Vector4) value, options); }
						break;
				}

			} catch {
				// When Undo is used to change a Tag on a property, and if the new Tag.ValueType != Property.ValueType, an event Error can be throw.
				// This error happears just when Undo is pressed, but the Property is correctly drawn. I didn't found how to avoid it. So i catch it and ignore it for the moment.
				// Here the error message : Getting control 0's position in a group with only 0 controls when doing ValidateCommandAborting
				// TODO : solve this bug properly
			}

			return result;
		}		
		#endregion


		#region StringPopup_WithIcon() ---
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Texture2D icon, string label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(new GUIContent(icon), new GUIContent(label), selectedString, displayedOptions, -1, options);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Texture2D icon, GUIContent label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(new GUIContent(icon), label, selectedString, displayedOptions, -1, options);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (GUIContent icon, string label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(icon, new GUIContent(label), selectedString, displayedOptions, -1, options);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (GUIContent icon, GUIContent label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(icon, label, selectedString, displayedOptions, -1, options);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Texture2D icon, string label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(new GUIContent(icon), new GUIContent(label), selectedString, displayedOptions, defaultIndex, options);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Texture2D icon, GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(new GUIContent(icon), label, selectedString, displayedOptions, defaultIndex, options);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (GUIContent icon, string label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			return StringPopup_WithIcon(icon, new GUIContent(label), selectedString, displayedOptions, defaultIndex, options);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (GUIContent icon, GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			string result = ""; 
			if(icon != null) {
				EditorGUILayout.BeginHorizontal();      // Begin - 1
				// Icon
				EditorGUILayout.LabelField(icon, GUILayout.Width(SingleLineIconWidth), GUILayout.Height(SingleLineIconWidth));

				// Popup ---
				EditorGUILayout.BeginVertical();		// Begin - 2
				GUILayout.FlexibleSpace();

				EditorGUIUtility.labelWidth -= (SingleLineIconWidth + 3); // Add 3 for margin between element
				result = StringPopup(label, selectedString, displayedOptions, defaultIndex, options);
				EditorGUIUtility.labelWidth += (SingleLineIconWidth + 3);

				GUILayout.FlexibleSpace();
				EditorGUILayout.EndVertical();			// End - 2
						
				EditorGUILayout.EndHorizontal();        // End - 1
			} else { result = StringPopup(label, selectedString, displayedOptions, defaultIndex, options); }
			return result;
		}
		#endregion


		#region StringPopup() ---
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (string label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup(new GUIContent(label), selectedString, displayedOptions, -1, options);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (GUIContent label, string selectedString, List<string> displayedOptions, params GUILayoutOption[] options) {
			return StringPopup(label, selectedString, displayedOptions, -1, options);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (string label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			return StringPopup(new GUIContent(label), selectedString, displayedOptions, defaultIndex, options);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="options">An optional list of Layout options that specify extra layout properties.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, params GUILayoutOption[] options) {
			string result = "";
			// Init index
			int index = displayedOptions.FindIndex(s => s == selectedString);
			if(index == -1) { index = defaultIndex; }

			int newIndex = EditorGUILayout.Popup(label, index, displayedOptions.ToArray(), options);
			if(displayedOptions.Count > 0 && newIndex > -1) { result = displayedOptions[newIndex]; }
			return result;
		}
		#endregion
		#endregion
	}
}
