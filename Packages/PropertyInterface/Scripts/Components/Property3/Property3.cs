using UnityEngine;
using System;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Base component to add Property with 3 values on Gameobjects.
	/// </summary>
	[Serializable]
	[AddComponentMenu("Property Interface/Property3")]
	public class Property3 : Property {
		#region ATTRIBUTES _________________________________________________________
		[SerializeField]
		private PropertyValue	_B;
		[SerializeField]
		private PropertyValue	_C;

		#region PROPERTIES -----------------------
		public PropertyValue B {
			get { return this._B; }
			set { this._B = value; }
		}	

		public PropertyValue C {
			get { return this._C; }
			set { this._C = value; }
		}			
		#endregion
		#endregion


		#region METHODS ____________________________________________________________
		/// <summary>
		/// Set the Tag.ValueType for all PropertyValue.
		/// </summary>
		public override void SetValuesType () {
			this._A.ValueType = this._valuesType;
			this._B.ValueType = this._valuesType;
			this._C.ValueType = this._valuesType;
		}

		/// <summary>
		/// Set a new Property.SourceType for all PropertyValue.
		/// </summary>
		/// <param name="newType">The new Property.SourceType.</param>
		 protected override void SetValuesSourceType (SourceType newType) {
			this._A.SourceType = newType;
			this._B.SourceType = newType;
			this._C.SourceType = newType;
		}
		
		/// <summary>
		/// Set a new Component for all PropertyValue.
		/// </summary>
		/// <param name="newComponent">The new Component.</param>
		protected override void SetValuesComponent (Component newComponent) {
			this._A.Component = newComponent;
			this._B.Component = newComponent;
			this._C.Component = newComponent;
		}	
		
		/// <summary>
		/// Refresh all object values with the serializedValues.
		/// </summary>
		public override void RefreshValuesObject () {
			this._A.RefreshValueObject();
			this._B.RefreshValueObject();
			this._C.RefreshValueObject();
		}

		/// <summary>
		/// Synchronize the values with their sources if need.
		/// </summary>
		public override void SynchronizeValues () {
			this._A.SynchronizeValue();
			this._B.SynchronizeValue();
			this._C.SynchronizeValue();
		}
		
		/// <summary>
		/// Check if the sources of the values are set or not.
		/// </summary>
		/// <returns>
		/// True if at least one value.Source is set. False otherwise.
		/// </returns>
		public override bool AtLeastOneSourceIsSet () {
			return this._A.Source != "" || this._B.Source != "" || this._C.Source != "";
		}
		
		#endregion

	}
}
