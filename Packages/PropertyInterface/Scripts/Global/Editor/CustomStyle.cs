using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Group all custom style used by the PropertyInterface
	/// </summary>
	public static class CustomStyle {		
		
		#region TAGS LIST ___________________________
		/// <summary>
		/// Group all custom style used to draw Tags list
		/// </summary>
		public static class TagsList {
			/// <summary>
			/// Get the list of all custom style used to draw Tags list
			/// </summary>
			/// <returns>The list of all custom style</returns>
			public static Dictionary<string, GUIStyle> GetStyles () {
				Dictionary<string, GUIStyle> result = new Dictionary<string, GUIStyle>();

				result.Add("Container",	Container());
				result.Add("Label",		Label());
				result.Add("IconLabel",	IconLabel());

				return result;
			}

			/// <summary>
			/// Used to draw the List container
			/// </summary>
			public static GUIStyle Container () {
				GUIStyle result = new GUIStyle(EditorStyles.label);
			
				result.padding = new RectOffset(2, 2, 2, 2);
				result.margin = new RectOffset(2, 2, 2, 2);
				// This is not set here else it'l be used by all the list element
				/*result.fixedWidth = 450;
				result.fixedHeight = 500;
				*/
				return result;
			}
			
			/// <summary>
			/// Used to draw Tags labels
			/// </summary>
			public static GUIStyle Label () {
				GUIStyle result = new GUIStyle(EditorStyles.label);

				result.alignment = TextAnchor.UpperLeft;

				return result;
			}
			
			/// <summary>
			/// Used to draw icons labels
			/// </summary>
			public static GUIStyle IconLabel () {
				GUIStyle result = new GUIStyle(EditorStyles.label);

				result.alignment = TextAnchor.UpperRight;

				return result;
			}
		}
		#endregion

		#region EDITORGUI_PROPERTYINTERFACE ________
		/// <summary>
		/// Group all custom style used in Tag field drawer
		/// </summary>
		public static class EditorGUI_PropertyInterface {
			/// <summary>
			/// Get the list of all custom style used in Tag field drawer
			/// </summary>
			/// <returns>The list of all custom style</returns>
			public static Dictionary<string, GUIStyle> GetStyles () {
				Dictionary<string, GUIStyle> result = new Dictionary<string, GUIStyle>();

				result.Add("TagIcon",			TagIcon());
				result.Add("SingleLineIcon",	SingleLineIcon());

				return result;
			}

			/// <summary>
			/// Used to draw Tag icon
			/// </summary>
			public static GUIStyle TagIcon () {
				GUIStyle result = new GUIStyle(EditorStyles.label);

				result.fixedWidth	= Nectunia.PropertyInterface.EditorGUI_PropertyInterface.TagIconWidth;
				result.fixedHeight	= Nectunia.PropertyInterface.EditorGUI_PropertyInterface.TagIconWidth;
				
				return result;
			}

			/// <summary>
			/// Used to draw Tag icon in a single line
			/// </summary>
			public static GUIStyle SingleLineIcon () {
				GUIStyle result = new GUIStyle(EditorStyles.label);

				result.fixedWidth	= Nectunia.PropertyInterface.EditorGUI_PropertyInterface.SingleLineIconWidth;
				result.fixedHeight	= Nectunia.PropertyInterface.EditorGUI_PropertyInterface.SingleLineIconWidth;
				
				return result;
			}
		}
		#endregion
	}
}
