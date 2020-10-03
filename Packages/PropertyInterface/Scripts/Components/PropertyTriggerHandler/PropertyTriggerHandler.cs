using UnityEngine;
using System;
using System.Collections.Generic;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Allow to handle targeted Property on trigger.
	/// </summary>
	/// <remarks>
	/// This class is used as an example on how to create a custom tagged component wich will use Nectunia.PropetyInterface asset.
	/// </remarks>
	[Serializable]
	[RequireComponent(typeof(Collider))]
	public class PropertyTriggerHandler : MonoBehaviourTagged {
		#region ENUM _______________________________________________________________
		/// <summary>
		/// List of possible action.
		/// </summary>
		public enum ActionOnTrigger {
			Add,
			ChangeTo
		}

		#endregion


		#region ATTRIBUTS __________________________________________________________
		[SerializeField]
		private TypedValue _typedValue;
		[SerializeField]
		private ActionOnTrigger _action = ActionOnTrigger.Add;
		[SerializeField]
		private bool _affectChildren = false;

		[SerializeField]
		private List<GameObject> _targets = new List<GameObject>();


		#region ACCESSORS ----------------------------------------------------------
		/// <summary>
		/// The Typed value to apply on trigger.
		/// </summary>
		public TypedValue TypedValue {
			get { return this._typedValue; }
			set { this._typedValue = value; }
		}
		
		/// <summary>
		/// The action to do on trigger.
		/// </summary>
		public ActionOnTrigger Action {
			get { return this._action; }
			set { this._action = value; }
		}
		
		/// <summary>
		/// Does the handler have to change the children as well.
		/// </summary>
		public bool AffectChildren {
			get { return this._affectChildren; }
			set { this._affectChildren = value; }
		}

		/// <summary>
		/// List of all targets that have triggered the PropertyTriggerHandler.
		/// </summary>
		public List<GameObject> Targets {
			get { return this._targets; }
		}

		#endregion
		#endregion


		#region THE COMPONENT MECHANIC _____________________________________________
		#region TRIGGER ***
		/// <summary>
		/// Happens when an other object trigger the collider attached.
		/// </summary>
		/// <param name="other">the object wich have triggered the collider.</param>
		public void OnTriggerEnter (Collider other) {
			//Debug.Log("OnTriggerEnter : " + other.name);
			this._targets.Add(other.gameObject);
		}
		
		/// <summary>
		/// Happens when a triggered object exit the collider attached.
		/// </summary>
		/// <param name="other">the object wich exit the collider.</param>
		public void OnTriggerExit (Collider other) {
			//Debug.Log("OnTriggerExit : " + other.name);
			this._targets.Remove(other.gameObject);
		}
		#endregion


		#region EVENTS ***
		public void Update () {
			// Apply modification to all targets
			foreach(GameObject currentTarget in this._targets) {
				// Get the properties in the current Target
				List<Property> properties;
				if(this._affectChildren) { properties = currentTarget.GetPropertiesInChildren(this._tagID); }
				else { properties = currentTarget.GetProperties(this._tagID); }

				// Apply this._typeValue to all properties
				foreach(Property currentProperty in properties) {
					if(this._action == ActionOnTrigger.ChangeTo) {
						currentProperty.A.Value = this._typedValue.Value;
					} else {
						currentProperty.A.TypedValue = currentProperty.A.TypedValue + this._typedValue;						
					}
				}
			}
			this._targets.Clear();
		}
		#endregion
		#endregion


		#region MonoBehaviourTagged IMPLEMENTATION _________________________________
		/// <summary>
		/// Event triggered when the Type of the values changed.
		/// </summary>
		protected override void OnValuesTypeChanged () {
			this.TypedValue.ValueType = this._valuesType;
		}
		#endregion
	}
}

