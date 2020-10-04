using UnityEditor;
using UnityEngine;
using System;
using System.Collections.Generic;
using Nectunia.Utility;
using System.Reflection;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Interface for PropertyInterface gui with manual positionning
	/// </summary>
	public static class EditorGUI_PropertyInterface {
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
				if (_warnIcon == null) { _warnIcon = new GUIContent(EditorGUIUtility.Load("icons/console.warnicon.png") as Texture2D); }
				return _warnIcon;
			}
		}
		#endregion

		#region METHODS ______________________________________________________
		#region TagIdField_WithIcon() ---
		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, long tagIDValue, GUIStyle style = null) {
			return TagIdField_WithIcon(position, "", tagIDValue, style);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, GUIContent label, long tagIDValue, GUIStyle style = null) {
			return TagIdField_WithIcon(position, label.text, tagIDValue, style);
		}
		
		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, string label, long tagIDValue, GUIStyle style = null) {
			return TagIdField_WithIcon(position, label, (TagID)tagIDValue, style);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, TagID tagID, GUIStyle style = null) {
			return TagIdField_WithIcon(position, "", tagID, style);
		}

		/// <summary>
		/// Draw a TagID Field and it's icon.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, GUIContent label, TagID tagID, GUIStyle style = null) {
			return TagIdField_WithIcon(position, label.text, tagID, style);
		}
				
		/// <summary>
		/// Draw a TagID Field and it's icon .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="displayedOption">The Tag list to search in.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField_WithIcon (Rect position, string label, TagID tagID, GUIStyle style = null) {
			TagID result = tagID;
			Tags tags = Tags.Instance;
			// Init TagList
			string[] tagsNames = tags.TagsList.ToStringArray();
			// Init index
			int index = tags.TagsList.FindIndex(p => p.Id == tagID);
			
			// Property Picture
			TagIconField(position, index);

			// Tag ---			
			if(style == null) { style = EditorStyles.popup; }
			float margin = 0f;
			if(position.height > EditorGUIUtility.singleLineHeight) { margin = (position.height - EditorGUIUtility.singleLineHeight) / 2; }
			Rect popupRect = new Rect(position.x + TagIconWidth, position.y + margin, position.width - TagIconWidth, EditorGUIUtility.singleLineHeight); 
			EditorGUIUtility.labelWidth -= (TagIconWidth);
			int newIndex = EditorGUI.Popup(popupRect, label, index, tagsNames, style);
			EditorGUIUtility.labelWidth += (TagIconWidth);


			if(newIndex != index) { result = tags.TagsList[newIndex].Id; }

			return result;
		}
		#endregion


		#region TagIdField() ---
		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, long tagIDValue, GUIStyle style = null) {
			return TagIdField(position, "", (TagID)tagIDValue, style);
		}
		
		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, GUIContent label, long tagIDValue, GUIStyle style = null) {
			return TagIdField(position, label.text, (TagID)tagIDValue, style);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, string label, long tagIDValue, GUIStyle style = null) {
			return TagIdField(position, label, (TagID)tagIDValue, style);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, TagID tagID, GUIStyle style = null) {
			return TagIdField(position, "", tagID, style);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field. /!\ WARNING : Only the text will be use.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, GUIContent label, TagID tagID, GUIStyle style = null) {
			return TagIdField(position, label.text, tagID, style);
		}

		/// <summary>
		/// Draw a TagID Field .
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label to draw between the picture and the TagID Field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new Tag selected in the field.</returns>
		public static TagID TagIdField (Rect position, string label, TagID tagID, GUIStyle style = null) {
			TagID result = tagID;
			Tags tags = Tags.Instance;
			// Init TagList
			string[] tagsNames = tags.TagsList.ToStringArray();
			// Init index
			int index = tags.TagsList.FindIndex(p => p.Id == tagID);			

			if(style == null) { style = EditorStyles.popup; }
			int newIndex = EditorGUI.Popup(position, label, index, tagsNames, style);

			if(newIndex != index) { result = tags.TagsList[newIndex].Id; }

			return result;
		}
		#endregion


		#region TagIconField() ---
		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagIDValue">The TagID.Value of the Tag to draw.</param>
		public static void TagIconField (Rect position, long tagIDValue) {
			TagIconField(position, (TagID)tagIDValue);
		}

		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="tagID">The TagID of the Tag to draw.</param>
		public static void TagIconField (Rect position, TagID tagID) {
			TagIconField(position, Tags.Instance.TagsList.FindIndex(p => p.Id == tagID));
		}

		/// <summary>
		/// Draw the icon of a Tag.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="index">The index of the Tag, in Tags.TagsList list, to draw.</param>
		public static void TagIconField (Rect position, int index) {
			Tags tags = Tags.Instance;
			GUIContent propertyIcon = new GUIContent((Texture2D)null);
			if( index < tags.TagsList.Count && index >= 0) { propertyIcon = new GUIContent(tags.TagsList[index].Icon); }
			EditorGUI.LabelField(position, propertyIcon, CustomStyle.EditorGUI_PropertyInterface.TagIcon());
		}
		
		#endregion


		#region GetPropertyValueHeight() ---
		/// <summary>
		/// Calcul the height need to draw the PropertyValue fields.
		/// </summary>
		/// <param name="propertyValue">The PropertyValue.</param>
		/// <returns>The height need to draw the PropertyValue fields.</returns>
		public static float GetPropertyValueHeight (PropertyValue propertyValue) {
			float nbLigne = 1f;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			
			// Anyway we have to draw the SourceType and Value fields
			// If SourceType is Component, will have to draw Component and Source fields as well
			if(propertyValue.SourceType == Property.SourceType.Component_Master) { nbLigne = 5f; }
			else { nbLigne = 3f; }

			// When inspector width is lower than MinInlineVectorFieldWidth, Vector fields need an extra line to be draw
			if(Screen.width < MinInlineVectorFieldWidth && Enum.GetName(typeof(Tag.ValueType), propertyValue.ValueType).Contains("Vect")) { nbLigne += 1; }

			return heightWithMargin * nbLigne;
		}
		
		/// <summary>
		/// Calcul the height need to draw the PropertyValue fields.
		/// </summary>
		/// <param name="propertyValue">The PropertyValue as a SerializedProperty.</param>
		/// <returns>The height need to draw the PropertyValue fields.</returns>
		public static float GetPropertyValueHeight (SerializedProperty propertyValue) {
			float nbLigne = 1f;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			
			SerializedProperty sourceType	= propertyValue.FindPropertyRelative("_sourceType");

			// Anyway we have to draw the SourceType and Value fields
			// If SourceType is Component, will have to draw Component and Source fields as well
			if(sourceType.enumValueIndex == (int)Property.SourceType.Component_Master) { nbLigne = 5f; }
			else { nbLigne = 3f; }

			// When Inspector width is lower than MinInlineVectorFieldWidth, Vector fields need an extra line to be draw
			SerializedProperty valueType= propertyValue.FindPropertyRelative("_typedValue")?.FindPropertyRelative("_valueType");
			if(Screen.width < MinInlineVectorFieldWidth && Enum.GetName(typeof(Tag.ValueType), (Tag.ValueType)valueType.intValue).Contains("Vec")) { nbLigne += 1; }

			return heightWithMargin * nbLigne;
		}
		#endregion


		#region PropertyValueFields() ---	
		/// <summary>
		/// Draw all PropertyValue fields.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="propertyValue">The PropertyValue.</param>
		public static void PropertyValueFields (Rect position, PropertyValue propertyValue) {
			float height = EditorGUIUtility.singleLineHeight;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			int valueFieldPosition = 2; // The first field is to the position 0. Here the Value field will be the third field to be draw
			
			// Value Type
			Rect valueTypePosition = new Rect(position.x, position.y, position.width, height);
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			propertyValue.ValueType = (Tag.ValueType) EditorGUI.EnumPopup(valueTypePosition, valueTypeLabel, propertyValue.ValueType);

			// Source Type
			Rect sourceTypePosition = new Rect(position.x, position.y + heightWithMargin, position.width, height);
			GUIContent  sourceTypeLabel	= new GUIContent("SourcesType",	"How the values of the Property will be set");
			propertyValue.SourceType = (Property.SourceType) EditorGUI.EnumPopup(sourceTypePosition, sourceTypeLabel, propertyValue.SourceType);

			if(propertyValue.SourceType == Property.SourceType.Component_Master) {
				// Set the value field position to after Component and Source fields
				valueFieldPosition = 4;

				// Component
				Rect componentPosition = new Rect(position.x, position.y + (heightWithMargin * 2), position.width, height);
				GUIContent  componentLabel	= new GUIContent("Component",	"In wich Component the value will be sourced");
				propertyValue.Component = (Component)EditorGUI.ObjectField(componentPosition, componentLabel, propertyValue.Component, typeof(Component), true);

				// Source
				Rect sourcePosition = new Rect(position.x, position.y + (heightWithMargin * 3), position.width, height);
				GUIContent  sourceLabel	= new GUIContent("Source",	"The component's field that will be used to source the value");
				PropertyValueSourceField(sourcePosition, sourceLabel, propertyValue);
				
				// Update value with Source
				propertyValue.SynchronizeValue();
			}

			// Value			
			EditorGUI.BeginDisabledGroup(propertyValue.SourceType == Property.SourceType.Component_Master);
			Rect valuesPosition = new Rect(position.x, position.y + (heightWithMargin * valueFieldPosition), position.width, height);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valuesPosition, valueLabel, propertyValue.TypedValue);
			EditorGUI.EndDisabledGroup();
		}
			
		/// <summary>
		/// Draw all PropertyValue fields.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="propertyValue">The PropertyValue as a SerializedProperty.</param>
		public static void PropertyValueFields (Rect position, SerializedProperty propertyValue) {
			float height = EditorGUIUtility.singleLineHeight;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			int valueFieldPosition = 2; // The first field is to the position 0. Here the Value field will be the third field to be draw
			
			// Get Property fields
			SerializedProperty typedValue		= propertyValue.FindPropertyRelative("_typedValue");
			SerializedProperty sourceType		= propertyValue.FindPropertyRelative("_sourceType");
			SerializedProperty component		= propertyValue.FindPropertyRelative("_component");
			SerializedProperty source			= propertyValue.FindPropertyRelative("_source");
			// Get TypedValue fields
			SerializedProperty valueSerialized	= typedValue.FindPropertyRelative("_valueSerialized");
			SerializedProperty valueType		= typedValue.FindPropertyRelative("_valueType");

			// Value Type
			Rect valueTypePosition = new Rect(position.x, position.y, position.width, height);
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			EditorGUI.PropertyField(valueTypePosition, valueType, valueTypeLabel);				

			// Source Type
			Rect sourceTypePosition = new Rect(position.x, position.y + heightWithMargin, position.width, height);
			GUIContent  sourceTypeLabel	= new GUIContent("SourcesType",	"How the values of the Property will be set");
			int oldValueType = sourceType.intValue;
			EditorGUI.PropertyField(sourceTypePosition, sourceType, sourceTypeLabel);
			// Reset all settings when the type change
			if(oldValueType != sourceType.intValue) {
				valueSerialized.stringValue = PropertySerialization.NULL_VALUE;
				component.objectReferenceValue = null;
				source.stringValue = "";
			}
				
			if(sourceType.enumValueIndex == (int)Property.SourceType.Component_Master) {
				// Set the value field position to after Component and Source fields
				valueFieldPosition = 4;
				// Component
				Rect componentPosition = new Rect(position.x, position.y + (heightWithMargin * 2), position.width, height);
				GUIContent  componentLabel	= new GUIContent("Component",	"In wich Component the value will be sourced");
				EditorGUI.PropertyField(componentPosition, component, componentLabel);
				Component componentObject = (Component)component.objectReferenceValue;

				// Source
				Rect sourcePosition = new Rect(position.x, position.y + (heightWithMargin * 3), position.width, height);
				GUIContent  sourceLabel	= new GUIContent("Source",	"The component's field that will be used to source the value");
				string newSource = PropertyValueSourceField(sourcePosition, sourceLabel, source.stringValue, componentObject, (Tag.ValueType)valueType.intValue);
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
			Rect valuesPosition = new Rect(position.x, position.y + (heightWithMargin * valueFieldPosition), position.width, height);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valuesPosition, valueLabel, typedValue);
			EditorGUI.EndDisabledGroup();
		}
		#endregion
		

		#region PropertyValueSourceField() ---		
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="style">Optional GUIStyle.</param>
		public static void PropertyValueSourceField (Rect position, string label, PropertyValue propertyValue, GUIStyle style = null) {
			PropertyValueSourceField(position, new GUIContent(label), propertyValue, style);
		}

		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="style">Optional GUIStyle.</param>
		public static void PropertyValueSourceField (Rect position, GUIContent label, PropertyValue propertyValue, GUIStyle style = null) {			
			List<string> fieldsNames = new List<string>();
			if(propertyValue.Component != null) {
				fieldsNames.AddRange(propertyValue.Component.GetPropertiesNames(Tag.GetSystemType(propertyValue.ValueType), BindingFlags.Public | BindingFlags.Instance));					
				fieldsNames.AddRange(propertyValue.Component.GetFieldsNames(Tag.GetSystemType(propertyValue.ValueType), BindingFlags.Public | BindingFlags.Instance));
				fieldsNames.Sort();
			}
			PropertyValueSourceField(position, label, propertyValue, fieldsNames, style);
		}
		
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle.</param>
		public static void PropertyValueSourceField (Rect position, string label, PropertyValue propertyValue, List<string> displayedOptions, GUIStyle style = null) {
			PropertyValueSourceField(position, new GUIContent(label), propertyValue, displayedOptions, style);
		}

		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The Label of the field.</param>
		/// <param name="propertyValue">The PropertyValue to draw the source.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle.</param>
		public static void PropertyValueSourceField (Rect position, GUIContent label, PropertyValue propertyValue, List<string> displayedOptions, GUIStyle style = null) {			
			GUIContent icon = null;
			if (propertyValue.IsReadOnly) {
				icon = Warnicon;
				icon.tooltip = "This source is reandOnly. The value will be updated by the source, but the source won't be updated by the value";
			}
			List<string> popupOptions = new List<string> ();
			popupOptions.Add("-");
			popupOptions.AddRange(displayedOptions);
			EditorGUI.BeginDisabledGroup(propertyValue.Component == null);
			string newSource = StringPopup_WithIcon(position, icon, label, propertyValue.Source, popupOptions, 0, style);
			EditorGUI.EndDisabledGroup();
			if(newSource == "-") { newSource = ""; }
			propertyValue.Source = newSource;
		}
		
		/// <summary>
		/// Draw the source field of a PropertyValue.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The Label of the field.</param>
		/// <param name="selectedValue">The selected value in the field.</param>
		/// <param name="component">The component of the PropertyValue.</param>
		/// <param name="valueType">The ValueType of the PropertyValue.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new string selected in the field.</returns>
		public static string PropertyValueSourceField (Rect position, GUIContent label, string selectedValue, Component component, Tag.ValueType valueType, GUIStyle style = null) {
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
			result = StringPopup_WithIcon(position, icon, label, selectedValue, popupOptions, 0, style);
			EditorGUI.EndDisabledGroup();
			if(result == "-") { result = ""; }

			return result;
		}
		#endregion


		#region GetTypedValueHeight() ---
		/// <summary>
		/// Calcul the height need to draw the TypedValue fields.
		/// </summary>
		/// <param name="typedValue">The TypedValue.</param>
		/// <returns>The height need to draw the TypedValue fields.</returns>
		public static float GetTypedValueHeight (TypedValue typedValue) {
			float nbLigne = 2f;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			
			// When inspector width is lower than MinInlineVectorFieldWidth, Vector fields need an extra line to be draw
			if(Screen.width < MinInlineVectorFieldWidth && Enum.GetName(typeof(Tag.ValueType), typedValue.ValueType).Contains("Vect")) { nbLigne += 1; }

			return heightWithMargin * nbLigne;
		}
		
		/// <summary>
		/// Calcul the height need to draw the TypedValue fields.
		/// </summary>
		/// <param name="typedValue">The TypedValue as a SerializedProperty.</param>
		/// <returns>The height need to draw the TypedValue fields.</returns>
		public static float GetTypedValueHeight (SerializedProperty typedValue) {
			float nbLigne = 2f;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			
			// When inspector width is lower than MinInlineVectorFieldWidth, Vector fields need an extra line to be draw
			SerializedProperty valueType= typedValue.FindPropertyRelative("_valueType");
			if(Screen.width < MinInlineVectorFieldWidth && Enum.GetName(typeof(Tag.ValueType), (Tag.ValueType)valueType.intValue).Contains("Vec")) { nbLigne += 1; }

			return heightWithMargin * nbLigne;
		}
		#endregion


		#region TypedValueFields() ---
		/// <summary>
		/// Draw a TypedValue fields.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the fields.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		public static void TypedValueFields (Rect position, TypedValue typedValue) {
			float height = EditorGUIUtility.singleLineHeight;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;

			// Value Type
			Rect valueTypePosition = new Rect(position.x, position.y, position.width, height);
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			typedValue.ValueType = (Tag.ValueType) EditorGUI.EnumPopup(valueTypePosition, valueTypeLabel, typedValue.ValueType);
			
			// Value
			Rect valuesPosition = new Rect(position.x, position.y + heightWithMargin, position.width, height);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valuesPosition, valueLabel, typedValue);
		}

		/// <summary>
		/// Draw a TypedValue fields.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the fields.</param>
		/// <param name="typedValue">The TypedValue to draw as SerializedProperty.</param>
		public static void TypedValueFields (Rect position, SerializedProperty typedValue) {
			float height = EditorGUIUtility.singleLineHeight;
			float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;
			// Get fields
			SerializedProperty valueType = typedValue.FindPropertyRelative("_valueType");

			// Value Type
			Rect valueTypePosition = new Rect(position.x, position.y, position.width, height);
			GUIContent  valueTypeLabel	= new GUIContent("ValueType",	"The type of value to use");
			EditorGUI.PropertyField(valueTypePosition, valueType, valueTypeLabel);
			
			// Value
			Rect valuesPosition = new Rect(position.x, position.y + heightWithMargin, position.width, height);
			GUIContent  valueLabel	= new GUIContent("Value",	"The value");
			TypedValueField(valuesPosition, valueLabel, typedValue);
		}
		#endregion


		#region TypedValueField() ---
		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, TypedValue typedValue, GUIStyle style = null) {
			TypedValueField(position, new GUIContent(""), typedValue, style);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, string label, TypedValue typedValue, GUIStyle style = null) {
			TypedValueField(position, new GUIContent(label), typedValue, style);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, GUIContent label, TypedValue typedValue, GUIStyle style = null) {
			typedValue.Value = TypedValueField(position, label, typedValue.ValueType, typedValue.Value, style);
		}
		
		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, SerializedProperty typedValue, GUIStyle style = null) {
			TypedValueField(position, new GUIContent(""), typedValue, style);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, string label, SerializedProperty typedValue, GUIStyle style = null) {
			TypedValueField(position, new GUIContent(label), typedValue, style);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <remarks>If TypedValue.Value had a bad type : the new TypedValue will have the default value for the given TypedValue.ValueType</remarks>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="typedValue">The TypedValue to draw as a SerializedProperty.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		public static void TypedValueField (Rect position, GUIContent label, SerializedProperty typedValue, GUIStyle style = null) {
			SerializedProperty valueSerialized	= typedValue.FindPropertyRelative("_valueSerialized");
			SerializedProperty valueType		= typedValue.FindPropertyRelative("_valueType");

			// Check if the serialized property is a TypedValue
			if(valueSerialized != null && valueType != null) {
				object value = PropertySerialization.StringToObject(valueSerialized.stringValue);
				if(value == null) { value = ((Tag.ValueType) valueType.intValue).GetDefaultSystemValue(); }
				value = TypedValueField(position, label, (Tag.ValueType) valueType.intValue, value, style);
				valueSerialized.stringValue = PropertySerialization.ObjectToString(value);
			}
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="valueType">The type of the value as a Tag.ValueType.</param>
		/// <param name="value">The object to draw.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		/// <returns>The new value. If value had a bad type : return the default value for the given ValueType.</returns>
		public static object TypedValueField (Rect position, string label, Tag.ValueType valueType, object value, GUIStyle style = null) {
			return TypedValueField(position, new GUIContent(label), valueType, value, style);
		}

		/// <summary>
		/// Draw a TypedValue field.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="valueType">The type of the value as a Tag.ValueType.</param>
		/// <param name="value">The object to draw.</param>
		/// <param name="style">Optional GUIStyle. /!\ This may not be apply depend on ValueType.</param>
		/// <returns>The new value. If value had a bad type : return the default value for the given ValueType.</returns>
		public static object TypedValueField (Rect position, GUIContent label, Tag.ValueType valueType, object value, GUIStyle style = null) {
			object result = valueType.GetDefaultSystemValue();
			try {
				switch ( valueType ){
					case Tag.ValueType.Boolean :	// Boolean
						if (typeof(bool).IsInstanceOfType(value))		{ result = EditorGUI.Toggle(position, label,			(bool) value, (style == null ? EditorStyles.toggle : style)); }
						break;	

					case Tag.ValueType.Color :		// Color
						if (typeof(Color).IsInstanceOfType(value))		{ result = EditorGUI.ColorField(position, label,		(Color) value); }
						break;

					case Tag.ValueType.Double :		// Double
						if (typeof(Double).IsInstanceOfType(value))		{ result = EditorGUI.DoubleField(position, label,		(Double) value, (style == null ? EditorStyles.numberField : style)); }
						break;

					case Tag.ValueType.Float :		// Float
						if (typeof(float).IsInstanceOfType(value))		{ result = EditorGUI.FloatField(position, label,		(float) value, (style == null ? EditorStyles.numberField : style)); }
						break;

					case Tag.ValueType.Int :		// Int
						if (typeof(int).IsInstanceOfType(value))		{ result = EditorGUI.IntField(position, label,			(int) value, (style == null ? EditorStyles.numberField : style)); }
						break;

					case Tag.ValueType.Long :		// Long
						if (typeof(long).IsInstanceOfType(value))		{ result = EditorGUI.LongField(position, label,			(long) value, (style == null ? EditorStyles.numberField : style)); }
						break;

					case Tag.ValueType.String :		// String
						if (typeof(string).IsInstanceOfType(value))		{ result = EditorGUI.TextField(position, label,			(string) value, (style == null ? EditorStyles.textField : style)); }
						break;

					case Tag.ValueType.Vector2 :	// Vector2
						if (typeof(Vector2).IsInstanceOfType(value))	{ result = EditorGUI.Vector2Field(position, label,		(Vector2) value); }
						break;

					case Tag.ValueType.Vector2Int : // Vector2Int
						if (typeof(Vector2Int).IsInstanceOfType(value)) { result = EditorGUI.Vector2IntField(position, label,	(Vector2Int) value); }
						break;

					case Tag.ValueType.Vector3 :	// Vector3
						if (typeof(Vector3).IsInstanceOfType(value))	{ result = EditorGUI.Vector3Field(position, label,		(Vector3) value); }
						break;

					case Tag.ValueType.Vector3Int :// Vector3Int
						if (typeof(Vector3Int).IsInstanceOfType(value)) { result = EditorGUI.Vector3IntField(position, label,	(Vector3Int) value); }
						break;

					case Tag.ValueType.Vector4 :	// Vector4
						if (typeof(Vector4).IsInstanceOfType(value))	{ result = EditorGUI.Vector4Field(position, label,		(Vector4) value); }
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
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, Texture2D icon, string label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup_WithIcon(position, new GUIContent(icon), label, selectedString, displayedOptions, -1, style);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, Texture2D icon, GUIContent label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup_WithIcon(position, new GUIContent(icon), label.text, selectedString, displayedOptions, -1, style);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, GUIContent icon, string label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup_WithIcon(position, icon, label, selectedString, displayedOptions, -1, style);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, GUIContent icon, GUIContent label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup_WithIcon(position, icon, label.text, selectedString, displayedOptions, -1, style);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, Texture2D icon, string label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			return StringPopup_WithIcon(position, new GUIContent(icon), label, selectedString, displayedOptions, defaultIndex, style);
		}
		
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, Texture2D icon, GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			return StringPopup_WithIcon(position, new GUIContent(icon), label.text, selectedString, displayedOptions, defaultIndex, style);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, GUIContent icon, GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			return StringPopup_WithIcon(position, icon, label.text, selectedString, displayedOptions, defaultIndex, style);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="icon">The Icon to draw before the label.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="style">Optional GUIStyle. This style will only be apply to the field and not on the icon.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup_WithIcon (Rect position, GUIContent icon, string label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			string result = "";
			if(icon != null) {
				// Icon
				EditorGUI.LabelField(position, icon, CustomStyle.EditorGUI_PropertyInterface.SingleLineIcon());

				// Popup ---
				Rect popupRect = new Rect(position.x + SingleLineIconWidth, position.y, position.width - SingleLineIconWidth, EditorGUIUtility.singleLineHeight); 
				EditorGUIUtility.labelWidth -= (SingleLineIconWidth);
				result = StringPopup(popupRect, label, selectedString, displayedOptions, defaultIndex, style);
				EditorGUIUtility.labelWidth += (SingleLineIconWidth);
			} else { result = StringPopup(position, label, selectedString, displayedOptions, defaultIndex, style); }

			return result;
		}
		#endregion


		#region StringPopup() ---
		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (Rect position, GUIContent label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup(position, label.text, selectedString, displayedOptions, -1, style);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (Rect position, string label, string selectedString, List<string> displayedOptions, GUIStyle style = null) {
			return StringPopup(position, label, selectedString, displayedOptions, -1, style);
		}		

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (Rect position, GUIContent label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			return StringPopup(position, label.text, selectedString, displayedOptions, defaultIndex, style);
		}

		/// <summary>
		/// Draw a Popup field from a selected string and return the new string selected.
		/// </summary>
		/// <param name="position">Rectangle on the screen to use for the field.</param>
		/// <param name="label">The label of the field.</param>
		/// <param name="selectedString">The selected string.</param>
		/// <param name="displayedOptions">The list of options to display.</param>
		/// <param name="defaultIndex">The default index to select if the selected string is not find.</param>
		/// <param name="style">Optional GUIStyle.</param>
		/// <returns>The new selected string.</returns>
		public static string StringPopup (Rect position, string label, string selectedString, List<string> displayedOptions, int defaultIndex, GUIStyle style = null) {
			string result = "";
			if (style == null) { style = EditorStyles.popup; }

			// Init index
			int index = displayedOptions.FindIndex(s => s == selectedString);
			if(index == -1) { index = defaultIndex; }

			int newIndex = EditorGUI.Popup(position, label, index, displayedOptions.ToArray(), style );
			
			if(displayedOptions.Count > 0 && newIndex > -1) { result = displayedOptions[newIndex]; }
			return result;
		}
		#endregion
		#endregion
	}
}
