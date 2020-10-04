using UnityEngine;
using System;
using System.Reflection;
using UnityEditor;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Manage a <c>Property</c> value.
	/// </summary>
	[Serializable]
	public class PropertyValue {
		#region ATTRIBUTES _________________________________________________________
		[SerializeField]
		private TypedValue          _typedValue;
		[SerializeField]
		private Property.SourceType	_sourceType				= Property.SourceType.Manual;
		[SerializeField]
		private Component			_component				= null;
		[SerializeField]
		private string				_source					= "";
		[SerializeField]
		private Type				_componentType			= null;
		[SerializeField]
		private PropertyInfo        _sourceProperty			= null;
		[SerializeField]
		private FieldInfo			_sourceField			= null;
		private bool				_mustUpdateSource		= false;
		private bool				_canUpdateSource		= true;
		private bool				_isSourceInfoSet		= false;
		private bool                _isSourceInitialized	= false;

		#region PROPERTIES -----------------------		
		public object Value {
			get { return this._typedValue.Value; }
			set {				
				string oldSerializedValue = this._typedValue.ValueSerialized;
				this._typedValue.Value = value;
				// While the object value has not been initialized, it can be null when the serialized value is not. It's why we use serializedValue to test.
				if (this._typedValue.ValueSerialized != oldSerializedValue) { this._mustUpdateSource = true; }
			}
		}
				
		public Tag.ValueType ValueType {
			get { return this._typedValue.ValueType; }			
			set {
				if (value != this._typedValue.ValueType) {
					this._typedValue.ValueType = value;
					this.ResetSource();
				}
			}			
		}

		public Property.SourceType SourceType {
			get { return this._sourceType; }
			set {
				if (value != this._sourceType) {
					this.ResetComponent();
					this._sourceType = value;
					this.ResetSource();
				}
			}
		}
		
		public Component Component {
			get { return this._component; }
			set {
				if (this._component != value) { this.ResetComponent(value); }
			}
		}
		
		public Type ComponentType {
			get { return this._componentType; }			
		}
		
		public PropertyInfo SourceProperty {
			get { return this._sourceProperty; }			
		}
		
		public FieldInfo SourceField {
			get { return this._sourceField; }			
		}

		public string Source {
			get { return this._source; }
			set { 
				if(this._source != value) {
					this._mustUpdateSource = false;
					this._source = value;
					this._sourceField = null;
					this._sourceProperty = null;
					this._isSourceInfoSet = false;
					this.ResetValue();
				}
			}
		}
		
		public bool IsReadOnly {
			get { return this._sourceType == Property.SourceType.Component_Master && this._source != "" && !this._canUpdateSource; }			
		}

		public TypedValue TypedValue {
			get { return this._typedValue; }
			set { 
				// While the object value has not been initialized, it can be null when the serialized value is not. It's why we use serializedValue to test.
				if (this._typedValue.ValueSerialized != value.ValueSerialized) { this._mustUpdateSource = true; }
				this._typedValue = value; 
			}
		}
		#endregion
		#endregion


		#region METHODS ____________________________________________________________
		/// <summary>
		/// Reset the component
		/// </summary>
		protected void ResetComponent (Component newComponent = null) {
			this.ResetSource();
			this._componentType	= null;
			this._component		= newComponent;
		}

		/// <summary>
		/// Reset the source
		/// </summary>
		protected void ResetSource () {
			this.Source = "";
		}
		
		/// <summary>
		/// Reset the value
		/// </summary>
		protected void ResetValue () {
			this._typedValue.ResetValues();
		}
		
		/// <summary>
		/// Refresh the object value with the serializedValue
		/// </summary>
		public void RefreshValueObject () {
			this._typedValue.RefreshValueObject();
		}

		/// <summary>
		/// Get the default value for current valuesType
		/// </summary>
		/// <returns>The default value</returns>
		public System.Object GetDefaultValue () {
			return this._typedValue.GetDefaultValue();
		}
		
		/// <summary>
		/// Synchronize the value and it's source
		/// </summary>
		public void SynchronizeValue () {
			// Synchronize the value with it's source
			if ((this._sourceType == Property.SourceType.Component_Master || this._sourceType == Property.SourceType.Component_Slave) && this._component != null && this.Source != "") {				
				if (this._componentType == null) { this._componentType = this._component.GetType(); }					
				if (!this._isSourceInfoSet) {
					this._sourceProperty = this._componentType.GetProperty(this.Source);
					this._sourceField = this._componentType.GetField(this.Source);
					if(this._sourceProperty != null) { this._canUpdateSource = this._sourceProperty.CanWrite; }
					if(this._sourceField != null) { this._canUpdateSource = !this._sourceField.IsInitOnly; }
					this._isSourceInfoSet = true;
				}

				// If the PropertyValue is Linked, always update the source with defined value while we are not at runtime
				if(this._sourceType == Property.SourceType.Component_Slave && !this._isSourceInitialized && this._canUpdateSource){
					this._mustUpdateSource = true;
					if (EditorApplication.isPlaying) { this._isSourceInitialized = true; }
				}

				/*bool synchroOK = this.SynchronizeValue_PropertyInfo(this._componentType.GetProperty(this.Source));
				if(!synchroOK) { this.SynchronizeValue_FieldInfo(this._componentType.GetField(this.Source)); }*/
				bool synchroOK = this.SynchronizeValue_PropertyInfo();
				if(!synchroOK) { this.SynchronizeValue_FieldInfo(); }
			}
		}
		
		/// <summary>
		/// Try to synchronize the value with the passed PropertyInfo
		/// </summary>
		/// <param name="propertyInfo">The PropertyInfo wich will try to synchronize with the value</param>
		/// <returns>True if the PropertyInfo is not null, False otherwise</returns>
		protected bool SynchronizeValue_PropertyInfo () {
			bool result = false;
			if(this._sourceProperty != null) {
				// Update the value
				if (!this._mustUpdateSource) { this.Value = this._sourceProperty.GetValue(this._component); }
				// Update the source
				else if(this._sourceProperty.CanWrite) { this._sourceProperty.SetValue(this._component, this.Value); }
				// At this point the source is always up to date so we can set this._mustUpdateSource to false
				this._mustUpdateSource = false;
				result = true;
			}
			return result;
		}
				
		/// <summary>
		/// Try to synchronize the value with the passed FieldInfo
		/// </summary>
		/// <param name="fieldInfo">The FieldInfo wich will try to synchronize with the value</param>
		/// <returns>True if the FieldInfo is not null, False otherwise</returns>
		protected bool SynchronizeValue_FieldInfo () {
			bool result = false;
			if(this._sourceField != null) {
				// Update the value
				if (!this._mustUpdateSource) { this.Value = this._sourceField.GetValue(this._component); }
				// Update the source
				else if(!this._sourceField.IsInitOnly) { this._sourceField.SetValue(this._component, this.Value); }
				// At this point the source is always up to date so we can set this._mustUpdateSource to false
				this._mustUpdateSource = false;
				result = true;
			}
			return result;
		}
		#endregion
	}
}
