
using UnityEngine;
using System;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Base component to add Property with 2 values on Gameobjects.
	/// </summary>
	[Serializable]
	[AddComponentMenu("Property Interface/Property2")]
	public class Property2 : Property {
		#region PROPERTIES _________________________________________________________
		[SerializeField]
		private PropertyValue	_B;

		#region ACCESSORS ----------------------------------------------------------
		/// <summary>
		/// The PropertyValue B.
		/// </summary>
		public PropertyValue B {
			get { return this._B; }
			set { this._B = value; }
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
		}	

		/// <summary>
		/// Set a new Property.SourceType for all PropertyValue.
		/// </summary>
		/// <param name="newType">The new Property.SourceType.</param>
		 protected override void SetValuesSourceType (SourceType newType) {
			this._A.SourceType = newType;
			this._B.SourceType = newType;
		}
		
		/// <summary>
		/// Set a new Component for all PropertyValue.
		/// </summary>
		/// <param name="newComponent">The new Component.</param>
		protected override void SetValuesComponent (Component newComponent) {
			this._A.Component = newComponent;
			this._B.Component = newComponent;
		}	
		
		/// <summary>
		/// Refresh all object values with the serializedValues.
		/// </summary>
		public override void RefreshValuesObject () {
			this._A.RefreshValueObject();
			this._B.RefreshValueObject();
		}

		/// <summary>
		/// Synchronize the values with their sources if need.
		/// </summary>
		public override void SynchronizeValues () {
			this._A.SynchronizeValue();
			this._B.SynchronizeValue();
		}
		
		/// <summary>
		/// Check if the sources of the values are set or not.
		/// </summary>
		/// <returns>
		/// True if at least one value.Source is set. False otherwise.
		/// </returns>
		public override bool AtLeastOneSourceIsSet () {
			return this._A.Source != "" || this._B.Source != "";
		}
		
		#endregion

	}
}
