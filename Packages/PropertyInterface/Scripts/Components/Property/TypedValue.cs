using UnityEngine;
using System;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Manage value with Tag.ValueType at runtime.
	/// </summary>
	[Serializable]
	public class TypedValue : ISerializationCallbackReceiver{
		#region PROPERTIES _________________________________________________________
		private object				_value;
		[SerializeField]
		private string				_valueSerialized	= PropertySerialization.NULL_VALUE;
		[SerializeField]
		private Tag.ValueType		_valueType			= Tag.DEFAULT_VALUESTYPE;

		#region ACCESSORS ----------------------------------------------------------
		/// <summary>
		/// The Typed value as an Object.
		/// </summary>
		public object Value {
			get {
				object result = this._value;
				if (result == null) {
					if (this._valueSerialized != PropertySerialization.NULL_VALUE) { result = PropertySerialization.StringToObject(this._valueSerialized); }
					else {
						// Reset the value in case it can't be null
						this.ResetValues();
						result = this._value;
					}
				}
				return result;
			}
			set {
				if (!System.Object.Equals( this._value, value)) {
					this._value = value;
					this._valueSerialized = PropertySerialization.ObjectToString(value);
				}				
			}
		}

		/// <summary>
		/// The Typed value as a serialized string.
		/// </summary>
		public string ValueSerialized {
			get { return this._valueSerialized; }
		}
		
		/// <summary>
		/// The Tag.ValueType of the Typed value.
		/// </summary>
		public Tag.ValueType ValueType {
			get { return this._valueType; }			
			set {
				if (value != this._valueType) {
					this._valueType = value;
					// Reset the value if we can't cast it to the new ValueType
					if (!this.CastValueToType()) { this.ResetValues(); }
				}
			}			
		}

		#endregion
		#endregion


		#region METHODS ____________________________________________________________
		/// <summary>
		/// Try to convert the Typed value to the current ValueType.
		/// </summary>
		/// <remarks>
		/// Used when ValueType has changed to try to not loose the current Typed value.
		/// </remarks>
		/// <returns>True if the cast successed. False otherwise.</returns>
		public bool CastValueToType () {
			bool result = true;
			Type currentType = Tag.GetSystemType(this._valueType);
			try {
				this._value				= Convert.ChangeType(this._value, currentType);
				this._valueSerialized	= PropertySerialization.ObjectToString(this._value);
			} catch {
				result = false;
			}

			return result;
		}

		/// <summary>
		/// Set the default values for the current ValueType.
		/// </summary>
		public void ResetValues () {
			this._value				= this.GetDefaultValue();
			this._valueSerialized	= PropertySerialization.ObjectToString(this._value);
		}		
		
		/// <summary>
		/// Refresh the object value with the serializedValue
		/// </summary>
		public void RefreshValueObject () {
			this._value	= PropertySerialization.StringToObject(this._valueSerialized);
		}

		/// <summary>
		/// Get the default value for current valuesType
		/// </summary>
		/// <returns>The default value</returns>
		public System.Object GetDefaultValue () {
			Type currentType = Tag.GetSystemType(this._valueType);
			// return the default value for System.ValueType, "" for string or null for other type;
			return currentType.IsValueType ? Activator.CreateInstance(currentType) : (this._valueType == Tag.ValueType.String ? "" : null);
		}
		
		/// <summary>
		/// Called before the value is serialized.
		/// </summary>
		/// <remarks>
		/// Not used at present.
		/// </remarks>
		public void OnBeforeSerialize () {
			//Nothing special to do here.
			//!\ WARNING /!\: Be carefull it's call very ofen (20-30 time before Custom PropertyDrawer.OnGUI()
		}

		/// <summary>
		/// Called after the value is deserialized.
		/// </summary>
		/// <remarks>
		/// OnAfterDeserialize is call only after a modification have been done on the SerializedProperty of the actual Object.
		/// </remarks>
		public void OnAfterDeserialize () {
			// Force the update to the object value. We do this because SerializedProperty can only acces to _valueSerialized and not to _value.
			this.RefreshValueObject();
		}

		#endregion


		#region OPERATOR ___________________________________________________________
		public static TypedValue operator + (TypedValue a, TypedValue b) {
			TypedValue result = null;
			if (a != null && b != null && a.ValueType == b.ValueType) {
				result = new TypedValue();
				result.ValueType = a.ValueType;
				switch (a.ValueType) {
					case Tag.ValueType.Boolean : 
						result.Value = (bool) a.Value & (bool) b.Value; 
						break;

					case Tag.ValueType.Color : 
						result.Value = (Color) a.Value + (Color) b.Value; 
						break;

					case Tag.ValueType.Double : 
						result.Value = (double) a.Value + (double) b.Value; 
						break;

					case Tag.ValueType.Float : 
						result.Value = (float) a.Value + (float) b.Value; 
						break;

					case Tag.ValueType.Int : 
						result.Value = (int) a.Value + (int) b.Value; 
						break;

					case Tag.ValueType.Long : 
						result.Value = (long) a.Value + (long) b.Value; 
						break;

					case Tag.ValueType.String : 
						result.Value = (string) a.Value + (string) b.Value; 
						break;

					case Tag.ValueType.Vector2 : 
						result.Value = (Vector2) a.Value + (Vector2) b.Value; 
						break;

					case Tag.ValueType.Vector2Int : 
						result.Value = (Vector2Int) a.Value + (Vector2Int) b.Value; 
						break;

					case Tag.ValueType.Vector3 : 
						result.Value = (Vector3) a.Value + (Vector3) b.Value; 
						break;

					case Tag.ValueType.Vector3Int : 
						result.Value = (Vector3Int) a.Value + (Vector3Int) b.Value;
						break;

					case Tag.ValueType.Vector4 : 
						result.Value = (Vector4) a.Value + (Vector4) b.Value; 
						break;
					default:
						result.Value = a.Value;
						break;
				}
			}
			return result;
		}
		#endregion
	}
}

