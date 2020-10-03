using UnityEngine;
using UnityEditor;
using System;
using System.Reflection;

namespace Nectunia.Utility {

	/// <summary>
	/// Tool class to manage Object icon in Editor.
	/// </summary>
	public static class CustomIcon {
		/// <summary>
		/// Change the icon to a Unity Object in the Inspector and Project tabs.
		/// </summary>
		/// <param name="unityObject">The Unity Object to change.</param>
		/// <param name="icon">The new icon.</param>
		public static void SetUnityObjectIcon(UnityEngine.Object unityObject, Texture2D icon) {
			if(unityObject != null) {
				Type editorGUIUtilityType = typeof(EditorGUIUtility);
				BindingFlags bindingFlags = BindingFlags.InvokeMethod | BindingFlags.Static | BindingFlags.NonPublic;
				object[] args = new object[] { unityObject, icon };
				editorGUIUtilityType.InvokeMember("SetIconForObject", bindingFlags, null, null, args);
				//var editorUtilityType = typeof(EditorUtility);
				//editorUtilityType.InvokeMember("ForceReloadInspectors", bindingFlags, null, null, null);
			} 
		}

		/*
		public static SetUnityObjectIcon (UnityEngine.Object unityObject, Type objectType, Texture2D icon) {
			if(unityObject != null && objectType != null && icon != null) { EditorGUIUtility.ObjectContent(unityObject, objectType).image = icon; }			
		}*/
	}
}
