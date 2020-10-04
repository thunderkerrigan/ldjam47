using UnityEditor;
using UnityEngine;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Custom drawer for TagID.
	/// </summary>
	[CustomPropertyDrawer(typeof(TagID))]
	public class Drawer_TagID : PropertyDrawer {
		/// <summary>
		/// Return the height of a TagID Field.
		/// </summary>
		/// <param name="property">The serialized TagID.</param>
		/// <param name="label">The TagID's label.</param>
		/// <returns>The height of the field.</returns>
		public override float GetPropertyHeight (SerializedProperty property, GUIContent label) {
			return EditorGUI_PropertyInterface.TagIconWidth;
		}

		/// <summary>
		/// Draw the TagID field.
		/// </summary>
		/// <param name="position">The TagID's field position.</param>
		/// <param name="property">The serialized TagID.</param>
		/// <param name="label">The TagID's label.</param>
		public override void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
			// Override logic works on the entire property
			EditorGUI.BeginProperty(position, label, property);
			
			SerializedProperty value = property.FindPropertyRelative("_value");

			value.longValue = EditorGUI_PropertyInterface.TagIdField_WithIcon(position, label, value.longValue);

			EditorGUI.EndProperty();

			// Apply changes to the serializedProperty
			property.serializedObject.ApplyModifiedProperties();
		}
	}
}
