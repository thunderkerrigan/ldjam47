using UnityEngine;
using System;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Allow to a Tag.ValueType to return it's equivalent as a System.Type.
	/// </summary>	
	public static class ExtensionValuesType {			
		/// <summary>
		/// Return the Type of the Tag.ValueType.
		/// </summary>
		/// <param name="type">The Tag.ValueType to return the system Type.</param>
		/// <returns>The System.Type of the Tag.ValueType.</returns>
		public static System.Type GetSystemType (this Tag.ValueType type) {
			switch (type){
				case Tag.ValueType.Boolean:		return typeof(Boolean);
				case Tag.ValueType.Color:		return typeof(Color);
				//case Type.Decimal:		return typeof(Decimal); => Can't be simply show in the inspector by Unity. TODO Later : https://answers.unity.com/questions/808352/how-to-make-decimal-variables-visible-in-inspector.html
				case Tag.ValueType.Double:		return typeof(Double);
				case Tag.ValueType.Float:		return typeof(float);
				case Tag.ValueType.Int:			return typeof(int);
				case Tag.ValueType.Long:		return typeof(long);
				case Tag.ValueType.String:		return typeof(string);
				case Tag.ValueType.Vector2:		return typeof(Vector2);
				case Tag.ValueType.Vector2Int:	return typeof(Vector2Int);
				case Tag.ValueType.Vector3:		return typeof(Vector3);
				case Tag.ValueType.Vector3Int:	return typeof(Vector3Int);
				case Tag.ValueType.Vector4:		return typeof(Vector4);
				default:						return typeof(float);
			}
		}
					
		/// <summary>
		/// Get the default value for current valuesType.
		/// </summary>
		/// <param name="type">The Tag.ValueType to return the default value.</param>
		/// <returns>The default value.</returns>
		public static System.Object GetDefaultSystemValue (this Tag.ValueType type) {
			Type currentType = type.GetSystemType();
			// return the default value for System.ValueType, "" for string or null for other type;
			return currentType.IsValueType ? Activator.CreateInstance(currentType) : (type == Tag.ValueType.String ? "" : null);			
		}
	}
}

