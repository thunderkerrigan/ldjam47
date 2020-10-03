using UnityEngine;
using System;
using System.Collections.Generic;

namespace Nectunia.PropertyInterface {
	[Serializable]
	[AddComponentMenu("Property Interface/PropertyOptim")]
	public class PropertyOptim : MonoBehaviourTagged {

		#region ENUM _______________________________________________________________
		public enum SourceType {
			Manual,
			Component
		}
		#endregion


		#region CALLBACK ___________________________________________________________
		public delegate void BeforeUpdateCallbackDelegate ();
		public delegate void AfterUpdateCallbackDelegate ();
		public BeforeUpdateCallbackDelegate onBeforeUpdateCallback;
		public AfterUpdateCallbackDelegate onAfterUpdateCallback;

		#endregion


		#region ATTRIBUTES _________________________________________________________
		[SerializeField]
		protected PropertyValue	_A;

		#region PROPERTIES -----------------------			
		public PropertyValue A {
			get { return this._A; }
			set { this._A = value; }
		}			

		public Property.SourceType SourcesType {
			get { return this._A.SourceType; }
			set { this.SetValuesSourceType(value); }
		}
		
		public Component Component {
			get { return this._A.Component; }
			set { this.SetValuesComponent(value); }
		}
		
		public Type ComponentType {
			get { return this._A.ComponentType; }			
		}	
		
		#endregion
		#endregion


		#region METHODS ____________________________________________________________
		/// <summary>
		/// Set the Tag.ValueType for all PropertyValue
		/// </summary>
		public virtual void SetValuesType () {
			this._A.ValueType = this._valuesType;
		}	

		/// <summary>
		/// Set a new Property.SourceType for all PropertyValue
		/// </summary>
		/// <param name="newType">The new Property.SourceType</param>
		 protected virtual void SetValuesSourceType (Property.SourceType newType) {
			this._A.SourceType = newType;
		}
		
		/// <summary>
		/// Set a new Component for all PropertyValue
		/// </summary>
		/// <param name="newComponent">The new Component</param>
		protected virtual void SetValuesComponent (Component newComponent) {
			this._A.Component = newComponent;
		}	
		
		/// <summary>
		/// Get the System Type of the values
		/// </summary>
		public virtual Type GetValuesSystemType () {
			return Tag.GetSystemType(this.ValuesType);
		}

		/// <summary>
		/// Update all object values with the serializedValues
		/// </summary>
		public virtual void UpdateValuesObject () {
			this._A.RefreshValueObject();
		}
				
		/// <summary>
		/// Synchronize the values with their sources if need
		/// </summary>
		public virtual void SynchronizeValues () {
			this._A.SynchronizeValue();
		}
		
		/// <summary>
		/// Synchronize the values with their sources if need
		/// </summary>
		public virtual bool AtLeastOneSourceIsSet () {
			return this._A.Source != "";
		}
				

		#region GetProperties() ---

		#region RETURN List<Property> **
		/// <summary>
		/// Get all Property in the current scene
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The list of all Property component in the scene</returns>
		public static List<Property> GetProperties (bool includeInactive = false) {
			List<Property> result = new List<Property>();

			if (includeInactive) { result.AddRange(Resources.FindObjectsOfTypeAll<Property>()); }
			else { result.AddRange(GameObject.FindObjectsOfType<Property>()); }

			return result;
		}

		/// <summary>
		/// Get all Property in the current scene wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The list of all Property in the scene wich have the passed TagID</returns>
		public static List<Property> GetProperties (TagID tagID, bool includeInactive = false) {
			List<Property> result = Property.GetProperties(includeInactive);
			return result.FindAll(p => p.TagID == tagID);
		}

		/// <summary>
		/// Get all Property in the target gameobject
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <returns>The list of all Property component in the target gameobject</returns>
		public static List<Property> GetProperties (GameObject target) {
			List<Property> result = new List<Property>();

			if (target != null) { result.AddRange(target.GetComponents<Property>()); }

			return result;
		}

		/// <summary>
		/// Get all Property in the target gameobject wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <returns>The list of all Property in the target gameobject wich have the passed TagID</returns>
		public static List<Property> GetProperties (GameObject target, TagID tagID) {
			List<Property> result = Property.GetProperties(target);
			return result.FindAll(p => p.TagID == tagID);
		}		
		#endregion
		
		#region REF List<Property> **
		/// <summary>
		/// Get all Property in the current scene
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="results">The list of all Property component in the scene</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetProperties (ref List<Property> results, bool includeInactive = false) {
			results = Property.GetProperties(includeInactive);
		}

		/// <summary>
		/// Get all Property in the current scene wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="results">The list of all Property in the scene wich have the passed TagID</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetProperties (TagID tagID, ref List<Property> results, bool includeInactive = false) {
			results = Property.GetProperties(tagID, includeInactive);
		}

		/// <summary>
		/// Get all Property in the target gameobject
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="results">The list of all Property component in the target gameobject</param>
		public static void GetProperties (GameObject target, ref List<Property> results) {
			results = Property.GetProperties(target);
		}

		/// <summary>
		/// Get all Property in the target gameobject wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="results">The list of all Property in the target gameobject wich have the passed TagID</param>
		public static void GetProperties (GameObject target, TagID tagID, ref List<Property> results) {
			results = Property.GetProperties(target, tagID);			
		}		
		#endregion

		#endregion
				

		#region GetPropertiesInChildren() ---

		#region RETURN List<Property> **
		/// <summary>
		/// Get all Property in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The list of all Property component in the target gameobject</returns>
		public static List<Property> GetPropertiesInChildren (GameObject target, bool includeInactive = false) {
			List<Property> result = new List<Property>();

			if (target != null) { result.AddRange(target.GetComponentsInChildren<Property>(includeInactive)); }

			return result;
		}
		
		/// <summary>
		/// Get all Property with the passed TagID in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The list of all Property component with the passed TagID in the target gameobject and it's children</returns>
		public static List<Property> GetPropertiesInChildren (GameObject target, TagID tagID, bool includeInactive = false) {
			List<Property> result = Property.GetPropertiesInChildren(target, includeInactive);
			return result.FindAll(p => p.TagID == tagID);
		}
		#endregion
		
		#region REF List<Property> **
		/// <summary>
		/// Get all Property in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="results">The list of all Property component in the target gameobject</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetPropertiesInChildren (GameObject target, ref List<Property> results, bool includeInactive = false) {
			results = Property.GetPropertiesInChildren(target, includeInactive);
		}
		
		/// <summary>
		/// Get all Property with the passed TagID in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="results">The list of all Property component with the passed TagID in the target gameobject and it's children</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetPropertiesInChildren (GameObject target, TagID tagID, ref List<Property> results, bool includeInactive = false) {
			results = Property.GetPropertiesInChildren(target, tagID, includeInactive);
		}
		#endregion

		#endregion


		#region GetProperty() ---

		#region RETURN Property **
		/// <summary>
		/// Get the First Property in the current scene
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The First Property in the current scene or null if none is found</returns>
		public static Property GetProperty (bool includeInactive = false) {
			Property result = null;

			if (includeInactive) {
				List<Property> list = new List<Property>();
				list.AddRange(Resources.FindObjectsOfTypeAll<Property>());
				if( list.Count > 0) { result = list[0]; }
			} else { result = GameObject.FindObjectOfType<Property>();}
			return result;
		}

		/// <summary>
		/// Get the first Property in the current scene wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The first Property in the scene wich have the passed TagID</returns>
		public static Property GetProperty (TagID tagID, bool includeInactive = false) {
			List<Property> list = Property.GetProperties(includeInactive);	
			return list.Find(p => p.TagID == tagID);
		}

		/// <summary>
		/// Get the first Property in the target gameobject. 
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <returns>The first Property component in the target gameobject</returns>
		public static Property GetProperty (GameObject target) {			
			return target?.GetComponent<Property>();
		}

		/// <summary>
		/// Get the first Property in the target gameobject wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <returns>The first Property in the target gameobject wich have the passed TagID</returns>
		public static Property GetProperty (GameObject target, TagID tagID) {
			List<Property> list = Property.GetProperties(target);
			return  list.Find(p => p.TagID == tagID);
		}		
		#endregion
		
		#region REF Property **
		/// <summary>
		/// Get the first Property in the current scene
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="result">The first Property component in the scene</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetProperty (ref Property result, bool includeInactive = false) {
			result = Property.GetProperty(includeInactive);
		}

		/// <summary>
		/// Get the first Property in the current scene wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="result">The first Property in the scene wich have the passed TagID</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetProperty (TagID tagID, ref Property result, bool includeInactive = false) {
			result = Property.GetProperty(tagID, includeInactive);
		}

		/// <summary>
		/// Get the first Property in the target gameobject
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="result">The first Property component in the target gameobject</param>
		public static void GetProperty (GameObject target, ref Property result) {
			result = Property.GetProperty(target);
		}

		/// <summary>
		/// Get the first Property in the target gameobject wich have the passed TagID
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="result">The first Property in the target gameobject wich have the passed TagID</param>
		public static void GetProperty (GameObject target, TagID tagID, ref Property result) {
			result = Property.GetProperty(target, tagID);			
		}		
		#endregion

		#endregion
		
		
		#region GetPropertyInChildren() ---

		#region RETURN Property **
		/// <summary>
		/// Get the first Property in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The first Property component in the target gameobject</returns>
		public static Property GetPropertyInChildren (GameObject target, bool includeInactive = false) {			
			return target?.GetComponentInChildren<Property>(includeInactive);
		}
		
		/// <summary>
		/// Get the first Property with the passed TagID in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>The first Property component with the passed TagID in the target gameobject and it's children</returns>
		public static Property GetPropertyInChildren (GameObject target, TagID tagID, bool includeInactive = false) {
			List<Property> result = Property.GetPropertiesInChildren(target, includeInactive);
			return result.Find(p => p.TagID == tagID);
		}
		#endregion
		
		#region REF Property **
		/// <summary>
		/// Get the first Property in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="result">The first Property component in the target gameobject</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetPropertyInChildren (GameObject target, ref Property result, bool includeInactive = false) {
			result = Property.GetPropertyInChildren(target, includeInactive);
		}
		
		/// <summary>
		/// Get the first Property with the passed TagID in the target gameobject and it's children
		/// /!\ WARNING : This function is very slow. It is not recommended to use this function every frame.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagID">The tagID we are looking for</param>
		/// <param name="result">The first Property component with the passed TagID in the target gameobject and it's children</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		public static void GetPropertyInChildren (GameObject target, TagID tagID, ref Property result, bool includeInactive = false) {
			result = Property.GetPropertyInChildren(target, tagID, includeInactive);
		}
		#endregion

		#endregion

		
		#region Exists() ---		
		/// <summary>
		/// Return True if at least one Property exists in the current scene or False otherwise
		/// </summary>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>True if at least one Property exists in the current scene or False otherwise</returns>
		public static new bool Exists (bool includeInactive = false) {
			return Property.GetProperty(includeInactive) != null;
		}
		
		/// <summary>
		/// Return True if at least one Property with passed TagID exists in the current scene or False otherwise
		/// </summary>
		/// <param name="tagId">The TagID we are looking for</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>True if at least one Property with passed TagID exists in the current scene or False otherwise</returns>
		public static new bool Exists (TagID tagId, bool includeInactive = false) {
			return Property.GetProperty(tagId, includeInactive) != null;
		}
		
		/// <summary>
		/// Return True if at least one Property exists in the target gameobject or False otherwise
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <returns>True if at least one Property exists in the target gameobject or False otherwise</returns>
		public static new bool Exists (GameObject target) {
			return Property.GetProperty(target) != null;
		}
		
		/// <summary>
		/// Return True if at least one Property with passed TagID exists in the target gameobject or False otherwise
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagId">The TagID we are looking for</param>
		/// <returns>True if at least one Property with passed TagID exist in the target gameobject or False otherwise</returns>
		public static new bool Exists (GameObject target, TagID tagId) {
			return Property.GetProperty(target, tagId) != null;
		}

		#endregion


		#region ExistsInChildren() ---		

		/// <summary>
		/// Return True if at least one Property exists in the target gameobject and its children or False otherwise
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="includeInactive">Include Inactive component</param>
		/// <returns>True if at least one Property exists or in the target gameobject and its children or False otherwise</returns>
		public static new bool ExistsInChildren (GameObject target, bool includeInactive = false) {
			return Property.GetPropertyInChildren(target, includeInactive) != null;
		}
				
		/// <summary>
		/// Return True if at least one Property with passed TagID exists in the target gameobject and its children or False otherwise
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search</param>
		/// <param name="tagId">The TagID we are looking for</param>
		/// <param name="includeInactive">Include Inactive component</param>
		/// <returns>True if at least one Property with passed TagID exists in the target gameobject and its children or False otherwise</returns>
		public static new bool ExistsInChildren (GameObject target, TagID tagId, bool includeInactive = false) {
			return Property.GetPropertyInChildren(target, tagId, includeInactive) != null;
		}
		#endregion

		#endregion


		#region EVENTS _____________________________________________________________
		public void OnEnable () {
			PropertyOptimUpdater.Instance.Properties.Add(this);
		}

		public void DelayedUpdate () {
			// Call callback OnBeforeUpdate if it has beend set
			if(onBeforeUpdateCallback != null) { this.onBeforeUpdateCallback(); }
			// Update the type of the values
			this.UpdateValuesType();
			// Synchronize the values with there source
			this.SynchronizeValues();
			// Call callback OnAfterUpdate if it has beend set
			if(onAfterUpdateCallback != null) { this.onAfterUpdateCallback(); }
		}

		public void OnDisable () {
			PropertyOptimUpdater.Instance.Properties.Remove(this);
		}

		protected override void OnValuesTypeChanged () {			
			this.SetValuesType();
		}
		#endregion
	}
}
