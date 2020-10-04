using UnityEngine;
using UnityEditor;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Custom Drawer for TypedValue.
	/// </summary>
	[CustomPropertyDrawer(typeof(TypedValue), true)]
	public class Drawer_TypedValue : PropertyDrawer {
		protected bool isFoldout = true;
		protected float height = EditorGUIUtility.singleLineHeight;
		protected float heightWithMargin = EditorGUIUtility.singleLineHeight + 2f;

		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			float result = heightWithMargin;

			if (isFoldout) { result += EditorGUI_PropertyInterface.GetTypedValueHeight(property); }

			return result;
		}
		
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			// Override logic works on the entire property
			EditorGUI.BeginProperty(position, label, property);

			Rect foldOutPosition = new Rect(position.x, position.y, position.width, height);
			isFoldout = EditorGUI.Foldout(foldOutPosition, isFoldout, property.name);
			if(isFoldout) {
				Rect propertyValuePosition = new Rect(position.x + 5, position.y + heightWithMargin, position.width - 5, position.height);
				EditorGUI_PropertyInterface.TypedValueFields(propertyValuePosition, property); // à tester				
			}

			EditorGUI.EndProperty();

			// Apply changes to the serializedProperty
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}

