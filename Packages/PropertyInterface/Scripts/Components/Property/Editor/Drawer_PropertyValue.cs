using UnityEngine;
using UnityEditor;
using System;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Custom Drawer for PropertyValue.
	/// </summary>
	[CustomPropertyDrawer(typeof(PropertyValue), true)]
	public class Drawer_PropertyValue : PropertyDrawer {
		protected bool isFoldout = true;
		protected float height = EditorGUIUtility.singleLineHeight;
		protected float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			float result = heightWithMargin;

			if (isFoldout) { 
				result += EditorGUI_PropertyInterface.GetPropertyValueHeight(property);
				float sWidth = Screen.width;
				// Add an extra line in some case to avoid display bug with vector field
				SerializedProperty valueType= property.FindPropertyRelative("_typedValue")?.FindPropertyRelative("_valueType");
				if( Enum.GetName(typeof(Tag.ValueType), (Tag.ValueType)valueType.intValue).Contains("Vec") && sWidth >= EditorGUI_PropertyInterface.MinInlineVectorFieldWidth && sWidth < EditorGUI_PropertyInterface.MinInlineVectorFieldWidth + 91f) { result += EditorGUIUtility.singleLineHeight; }
			}

			return result;
		}
		
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			// Override logic works on the entire property
			EditorGUI.BeginProperty(position, label, property);

			Rect foldOutPosition = new Rect(position.x, position.y, position.width, height);
			isFoldout = EditorGUI.Foldout(foldOutPosition, isFoldout, property.name);
			if(isFoldout) {
				Rect propertyValuePosition = new Rect(position.x + 5, position.y + heightWithMargin, position.width - 5, position.height);
				EditorGUI_PropertyInterface.PropertyValueFields(propertyValuePosition, property);
			}

			EditorGUI.EndProperty();

			// Apply changes to the serializedProperty
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}

