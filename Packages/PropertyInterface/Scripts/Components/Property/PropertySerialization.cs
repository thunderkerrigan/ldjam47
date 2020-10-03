/**
 * TODO :	-add a public enum to list all allowed type
 *			-add function isASerializedObject(string serializedObject)
 *			-add funtion getSerialiazedObjectType(string serializedObject)
 */
using UnityEngine;
using System;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Tools class used to Serialize and deserialize Object value.
	/// </summary>
	public static class PropertySerialization  {
		/// <summary>
		/// The string representation for a null object
		/// </summary>
		public const string NULL_VALUE = "n00";

		/// <summary>
		/// Serialize an object to a string.
		/// </summary>
		/// <param name="obj">The object to serialize.</param>
		/// <returns>The serialized Object value as a string.</returns>
		public static string ObjectToString (object obj) {
			string result = NULL_VALUE;

			if(obj != null) {
				var type = obj.GetType();

				if(type == typeof(bool)) {
					result = "b00" + ((bool)obj).ToString();
				}else if(type == typeof(Color)) {
					//result = "c00#" + ColorUtility.ToHtmlStringRGBA((Color)obj); // Add # if you want to use ColorUtility.TryParseHtmlString() in StringToObject()
					Color color = (Color)obj;					
					result = "c00" + color.r.ToString() + "|" +  color.g.ToString() + "|" + color.b.ToString() + "|" + color.a.ToString();
				}else if(type == typeof(double)) {
					result = "d00" + ((double)obj).ToString();
				}else if(type == typeof(float)) {
					result = "f00" + ((float)obj).ToString();
				}else if(type == typeof(int)) {
					result = "i00" + ((int)obj).ToString();
				}else if(type == typeof(long)) {
					result = "l00" + ((long)obj).ToString();
				}else if(type == typeof(string)) {
					result = "s00#" + obj; // Add # to avoid that empty string return null in StringToObject()
				}else if(type == typeof(Vector2)) {
					Vector2 vector = (Vector2)obj;
					result = "v20" + vector.x.ToString() + "|" + vector.y.ToString();
				}else if(type == typeof(Vector2Int)) {
					Vector2Int vector = (Vector2Int)obj;
					result = "v2i" + vector.x.ToString() + "|" + vector.y.ToString();
				}else if(type == typeof(Vector3)) {
					Vector3 vector = (Vector3)obj;
					result = "v30" + vector.x.ToString() + "|" + vector.y.ToString() + "|" + vector.z.ToString();
				}else if(type == typeof(Vector3Int)) {
					Vector3Int vector = (Vector3Int)obj;
					result = "v3i" + vector.x.ToString() + "|" + vector.y.ToString() + "|" + vector.z.ToString();
				}else if(type == typeof(Vector4)) {
					Vector4 vector = (Vector4)obj;
					result = "v40" + vector.x.ToString() + "|" + vector.y.ToString() + "|" + vector.z.ToString() + "|" + vector.w.ToString();
				}
			}

			return result;
		}

		/// <summary>
		/// Deserialize a string to an object.
		/// </summary>
		/// <param name="str">The string to deserialize.</param>
		/// <returns>The object deserialize from the string. Null if the string is not a allowed serialiezed object.</returns>
		public static object StringToObject (string str) {
			if(str.Length > 3) {
				string type = str.Substring(0,3);
				str = str.Substring(3);

				if(type == "b00") {
					return bool.Parse(str);
				}else if(type == "c00") {
					/*Color result;
					bool parse = ColorUtility.TryParseHtmlString(str, out result);
					if (parse) { return result; }*/	
					
					// We don't use ColorUtility.TryParseHtmlString() because it's not allowed during serialization process
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					return new Color(float.Parse(strVal[0]), float.Parse(strVal[1]), float.Parse(strVal[2]), float.Parse(strVal[3]));
				}else if(type == "d00") {
					return double.Parse(str);
				}else if(type == "f00") {
					return float.Parse(str);
				}else if(type == "i00") {
					return int.Parse(str);
				}else if(type == "l00") {
					return long.Parse(str);
				}else if(type == "s00") {
					return str.Substring(1); // Start to the second char to delete the # after s00
				}else if(type == "v20") {
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					if(strVal.Length == 2) { return new Vector2(float.Parse(strVal[0]), float.Parse(strVal[1])); }
					else{ return new Vector2(); }					
				}else if(type == "v2i") {
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					if(strVal.Length == 2) { return new Vector2Int(int.Parse(strVal[0]), int.Parse(strVal[1])); }
					else{ return new Vector2Int(); }
				}else if(type == "v30") {
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					if(strVal.Length == 3) { return new Vector3(float.Parse(strVal[0]), float.Parse(strVal[1]), float.Parse(strVal[2])); }
					else{ return new Vector3(); }
				}else if(type == "v3i") {
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					if(strVal.Length == 3) { return new Vector3Int(int.Parse(strVal[0]), int.Parse(strVal[1]), int.Parse(strVal[2])); }
					else{ return new Vector3Int(); }
				}else if(type == "v40") {
					string [] strVal = str.Split(new string[] {"|"}, StringSplitOptions.None);
					if(strVal.Length == 4) { return new Vector4(float.Parse(strVal[0]), float.Parse(strVal[1]), float.Parse(strVal[2]), float.Parse(strVal[3])); }
					else{ return new Vector4(); }
				} 
			}
			return null;
		}
	}
}
