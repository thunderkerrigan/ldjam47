using UnityEngine;
using System;
using System.Collections.Generic;
using Nectunia.Utility;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Base classe for all components wich use PropertyInterface.Tags.
	/// </summary>
	[Serializable]
	public abstract class  MonoBehaviourTagged : MonoBehaviour{
		#region ATTRIBUTES _________________________________________________________
		
		[SerializeField]
		protected TagID			_tagID		= -1L;
		[SerializeField]
		protected Tag.ValueType _valuesType	= Tag.DEFAULT_VALUESTYPE;
		[NonSerialized]
		private Tags _tags;
			
		#region PROPERTIES -----------------------
		
		/// <summary>
		/// The TagID used by the component.
		/// </summary>
		public TagID TagID {
			get { return this._tagID; }
			set {
				if (this._tagID != value) {
					this._tagID = value;
					this.UpdateValuesType();
					this.OnTagIDChanged();					
				}
			}
		}

		/// <summary>
		/// The Tag.ValueType of the component's values.
		/// </summary>
		public Tag.ValueType ValuesType {
			get { return this._valuesType; }
			protected set {
				if (this._valuesType != value) {
					this._valuesType = value;
					this.OnValuesTypeChanged();
				}
			}
		}
		
		/// <summary>
		/// Get from the Tags list the Tag wich have the same TagID than this component.
		/// </summary>
		/// <remarks>
		/// This Property should be used sparingly.
		/// </remarks>
		public Tag Tag {
			// We can't store the Tag reference because it change when the TagsList is reordered. So we need to find it everytime.
			get {
				if(this._tags == null) { this._tags = Tags.Instance; }
				return this._tags.TagsList.Find(t => t.Id == this.TagID);
			}
		}

		#endregion
		#endregion


		#region EVENTS _____________________________________________________________	
		/// <summary>
		/// Event triggered when TagID changed.
		/// </summary>
		protected virtual void OnTagIDChanged () {}

		/// <summary>
		/// Event triggered when ValuesType changed.
		/// </summary>
		/// <remarks>
		/// This happens when :
		///  -> TagID changed for a Tag with another ValuesType.
		///  -> The refered Tag have its ValuesType changed.
		/// </remarks>
		protected virtual void OnValuesTypeChanged () {}

		#endregion


		#region METHODS ____________________________________________________________
		/// <summary>
		///  Update ValuesType.
		/// </summary>
		public virtual void UpdateValuesType () {
			Tag tag = this.Tag;
			if (tag != null) { this.ValuesType = tag.ValuesType; }
		}

		/// <summary>
		/// Check if the Tag and ValuesType are good.
		/// </summary>
		/// <returns>Return true if it's all OK, false otherwise.</returns>
		public virtual bool CheckIntegrity () {
			bool result = false;

			Tag currentTag = this.Tag;
			if(currentTag != null) {
				result = this.CheckValuesType(currentTag);
				if(!result) { this.WarningUpdate(currentTag); }
			} else { this.WarningUnknownTag(); }

			return result;
		}
		
		/// <summary>
		/// Check if the ValuesType is equals to the passed Tag.ValuesType.
		/// </summary>
		/// <returns>Return true if ValueType match, false otherwise.</returns>
		public bool CheckValuesType (Tag tag) {
			return (tag != null && tag.ValuesType == this.ValuesType);
		}

		/// <summary>
		/// Log a warning in the console to say the MonoBehaviourTagged have to be updated.
		/// </summary>
		protected virtual void WarningUpdate () {
			this.WarningUpdate(this.Tag);
		}

		/// <summary>
		/// Log a warning in the console to say the MonoBehaviourTagged have to be updated.
		/// </summary>
		/// <param name="tag">The concerned Tag.</param>
		protected virtual void WarningUpdate (Tag tag) {
			if (tag != null) { Debug.LogWarning("The "+ this.GetType().Name +" '" + tag.Name + "' has to be updated : " + this.gameObject.scene.name + " => " + this.transform.GetPath(), this.gameObject); }
			else { Debug.LogWarning("A "+ this.GetType().Name +" has to be updated : " + this.gameObject.scene.name + " => " + this.transform.GetPath(), this.gameObject); }
		}

		/// <summary>
		/// Log a warning in the console to say the MonoBehaviourTagged have an Unknown Tag.
		/// </summary>
		protected virtual void WarningUnknownTag () {
			Debug.LogWarning("A "+ this.GetType().Name +" has an unknown Tag : " + this.gameObject.scene.name + " => " + this.transform.GetPath(), this.gameObject); 
		}
		
		
		#region GetMonoBehavioursTagged() ---

		#region RETURN List<MonoBehaviourTagged> **
		/// <summary>
		/// Get all MonoBehaviourTagged in the current scene.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The list of all MonoBehaviourTagged component in the scene.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged (bool includeInactive = false) {
			List<MonoBehaviourTagged> result = new List<MonoBehaviourTagged>();

			if (includeInactive) { result.AddRange(Resources.FindObjectsOfTypeAll<MonoBehaviourTagged>()); }
			else { result.AddRange(GameObject.FindObjectsOfType<MonoBehaviourTagged>()); }

			return result;
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the current scene wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The list of all MonoBehaviourTagged in the scene wich have the passed TagID.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged (TagID tagID, bool includeInactive = false) {
			List<MonoBehaviourTagged> result = MonoBehaviourTagged.GetMonoBehavioursTagged(includeInactive);
			return result.FindAll(p => p.TagID == tagID);
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <returns>The list of all MonoBehaviourTagged component in the target gameobject.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged (GameObject target) {
			List<MonoBehaviourTagged> result = new List<MonoBehaviourTagged>();

			if (target != null) { result.AddRange(target.GetComponents<MonoBehaviourTagged>()); }

			return result;
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>The list of all MonoBehaviourTagged in the target gameobject wich have the passed TagID.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged (GameObject target, TagID tagID) {
			List<MonoBehaviourTagged> result = MonoBehaviourTagged.GetMonoBehavioursTagged(target);
			return result.FindAll(p => p.TagID == tagID);
		}		
		#endregion
		
		#region REF List<MonoBehaviourTagged> **
		/// <summary>
		/// Get all MonoBehaviourTagged in the current scene.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="results">The list of all MonoBehaviourTagged component in the scene.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehavioursTagged (ref List<MonoBehaviourTagged> results, bool includeInactive = false) {
			results = MonoBehaviourTagged.GetMonoBehavioursTagged(includeInactive);
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the current scene wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="results">The list of all MonoBehaviourTagged in the scene wich have the passed TagID.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehavioursTagged (TagID tagID, ref List<MonoBehaviourTagged> results, bool includeInactive = false) {
			results = MonoBehaviourTagged.GetMonoBehavioursTagged(tagID, includeInactive);
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="results">The list of all MonoBehaviourTagged component in the target gameobject.</param>
		public static void GetMonoBehavioursTagged (GameObject target, ref List<MonoBehaviourTagged> results) {
			results = MonoBehaviourTagged.GetMonoBehavioursTagged(target);
		}

		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="results">The list of all MonoBehaviourTagged in the target gameobject wich have the passed TagID.</param>
		public static void GetMonoBehavioursTagged (GameObject target, TagID tagID, ref List<MonoBehaviourTagged> results) {
			results = MonoBehaviourTagged.GetMonoBehavioursTagged(target, tagID);			
		}		
		#endregion

		#endregion
				

		#region GetMonoBehavioursTaggedInChildren() ---

		#region RETURN List<MonoBehaviourTagged> **
		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The list of all MonoBehaviourTagged component in the target gameobject and it's childrens.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTaggedInChildren (GameObject target, bool includeInactive = false) {
			List<MonoBehaviourTagged> result = new List<MonoBehaviourTagged>();

			if (target != null) { result.AddRange(target.GetComponentsInChildren<MonoBehaviourTagged>(includeInactive)); }

			return result;
		}
		
		/// <summary>
		/// Get all MonoBehaviourTagged with the passed TagID in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The list of all MonoBehaviourTagged component with the passed TagID in the target gameobject and it's children.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTaggedInChildren (GameObject target, TagID tagID, bool includeInactive = false) {
			List<MonoBehaviourTagged> result = MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(target, includeInactive);
			return result.FindAll(p => p.TagID == tagID);
		}
		#endregion
		
		#region REF List<MonoBehaviourTagged> **
		/// <summary>
		/// Get all MonoBehaviourTagged in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="results">The list of all MonoBehaviourTagged component in the target gameobject.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehavioursTaggedInChildren (GameObject target, ref List<MonoBehaviourTagged> results, bool includeInactive = false) {
			results = MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(target, includeInactive);
		}
		
		/// <summary>
		/// Get all MonoBehaviourTagged with the passed TagID in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="results">The list of all MonoBehaviourTagged component with the passed TagID in the target gameobject and it's children.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehavioursTaggedInChildren (GameObject target, TagID tagID, ref List<MonoBehaviourTagged> results, bool includeInactive = false) {
			results = MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(target, tagID, includeInactive);
		}
		#endregion

		#endregion


		#region GetMonoBehaviourTagged() ---

		#region RETURN MonoBehaviourTagged **
		/// <summary>
		/// Get the First MonoBehaviourTagged in the current scene.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The First MonoBehaviourTagged in the current scene or null if none is found.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged (bool includeInactive = false) {
			MonoBehaviourTagged result = null;

			if (includeInactive) {
				List<MonoBehaviourTagged> list = new List<MonoBehaviourTagged>();
				list.AddRange(Resources.FindObjectsOfTypeAll<MonoBehaviourTagged>());
				if( list.Count > 0) { result = list[0]; }
			} else { result = GameObject.FindObjectOfType<MonoBehaviourTagged>();}
			return result;
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the current scene wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The first MonoBehaviourTagged in the scene wich have the passed TagID.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged (TagID tagID, bool includeInactive = false) {
			List<MonoBehaviourTagged> list = MonoBehaviourTagged.GetMonoBehavioursTagged(includeInactive);	
			return list.Find(p => p.TagID == tagID);
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject. .
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <returns>The first MonoBehaviourTagged component in the target gameobject.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged (GameObject target) {			
			return target.GetComponent<MonoBehaviourTagged>();
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>The first MonoBehaviourTagged in the target gameobject wich have the passed TagID.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged (GameObject target, TagID tagID) {
			List<MonoBehaviourTagged> list = MonoBehaviourTagged.GetMonoBehavioursTagged(target);
			return  list.Find(p => p.TagID == tagID);
		}		
		#endregion
		
		#region REF MonoBehaviourTagged **
		/// <summary>
		/// Get the first MonoBehaviourTagged in the current scene.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="result">The first MonoBehaviourTagged component in the scene.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehaviourTagged (ref MonoBehaviourTagged result, bool includeInactive = false) {
			result = MonoBehaviourTagged.GetMonoBehaviourTagged(includeInactive);
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the current scene wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="result">The first MonoBehaviourTagged in the scene wich have the passed TagID.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehaviourTagged (TagID tagID, ref MonoBehaviourTagged result, bool includeInactive = false) {
			result = MonoBehaviourTagged.GetMonoBehaviourTagged(tagID, includeInactive);
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="result">The first MonoBehaviourTagged component in the target gameobject.</param>
		public static void GetMonoBehaviourTagged (GameObject target, ref MonoBehaviourTagged result) {
			result = MonoBehaviourTagged.GetMonoBehaviourTagged(target);
		}

		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject wich have the passed TagID.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="result">The first MonoBehaviourTagged in the target gameobject wich have the passed TagID.</param>
		public static void GetMonoBehaviourTagged (GameObject target, TagID tagID, ref MonoBehaviourTagged result) {
			result = MonoBehaviourTagged.GetMonoBehaviourTagged(target, tagID);			
		}		
		#endregion

		#endregion
		
		
		#region GetMonoBehaviourTaggedInChildren() ---

		#region RETURN MonoBehaviourTagged **
		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The first MonoBehaviourTagged component in the target gameobject.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTaggedInChildren (GameObject target, bool includeInactive = false) {			
			return target.GetComponentInChildren<MonoBehaviourTagged>(includeInactive);
		}
		
		/// <summary>
		/// Get the first MonoBehaviourTagged with the passed TagID in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>The first MonoBehaviourTagged component with the passed TagID in the target gameobject and it's children.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTaggedInChildren (GameObject target, TagID tagID, bool includeInactive = false) {
			List<MonoBehaviourTagged> result = MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(target, includeInactive);
			return result.Find(p => p.TagID == tagID);
		}
		#endregion
		
		#region REF MonoBehaviourTagged **
		/// <summary>
		/// Get the first MonoBehaviourTagged in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="result">The first MonoBehaviourTagged component in the target gameobject.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehaviourTaggedInChildren (GameObject target, ref MonoBehaviourTagged result, bool includeInactive = false) {
			result = MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(target, includeInactive);
		}
		
		/// <summary>
		/// Get the first MonoBehaviourTagged with the passed TagID in the target gameobject and it's children.
		/// </summary>
		/// <remarks>
		/// /!\ This function is very slow. It is not recommended to use this function every frame.
		/// </remarks>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="result">The first MonoBehaviourTagged component with the passed TagID in the target gameobject and it's children.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		public static void GetMonoBehaviourTaggedInChildren (GameObject target, TagID tagID, ref MonoBehaviourTagged result, bool includeInactive = false) {
			result = MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(target, tagID, includeInactive);
		}
		#endregion

		#endregion

		
		#region Exists() ---		
		/// <summary>
		/// Return True if at least one MonoBehaviourTagged exists in the current scene or False otherwise.
		/// </summary>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>True if at least one MonoBehaviourTagged exists in the current scene or False otherwise.</returns>
		public static bool Exists (bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(includeInactive) != null;
		}
		
		/// <summary>
		/// Return True if at least one MonoBehaviourTagged with passed TagID exists in the current scene or False otherwise.
		/// </summary>
		/// <param name="tagId">The TagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject.</param>
		/// <returns>True if at least one MonoBehaviourTagged with passed TagID exists in the current scene or False otherwise.</returns>
		public static bool Exists (TagID tagId, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(tagId, includeInactive) != null;
		}
		
		/// <summary>
		/// Return True if at least one MonoBehaviourTagged exists in the target gameobject or False otherwise.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <returns>True if at least one MonoBehaviourTagged exists in the target gameobject or False otherwise.</returns>
		public static bool Exists (GameObject target) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(target) != null;
		}
		
		/// <summary>
		/// Return True if at least one MonoBehaviourTagged with passed TagID exists in the target gameobject or False otherwise.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagId">The TagID we are looking for.</param>
		/// <returns>True if at least one MonoBehaviourTagged with passed TagID exist in the target gameobject or False otherwise.</returns>
		public static bool Exists (GameObject target, TagID tagId) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(target, tagId) != null;
		}

		#endregion


		#region ExistsInChildren() ---		

		/// <summary>
		/// Return True if at least one MonoBehaviourTagged exists in the target gameobject and its children or False otherwise.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="includeInactive">Include Inactive component.</param>
		/// <returns>True if at least one MonoBehaviourTagged exists or in the target gameobject and its children or False otherwise.</returns>
		public static bool ExistsInChildren (GameObject target, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(target, includeInactive) != null;
		}
				
		/// <summary>
		/// Return True if at least one MonoBehaviourTagged with passed TagID exists in the target gameobject and its children or False otherwise.
		/// </summary>
		/// <param name="target">The target Gameobject in wich to search.</param>
		/// <param name="tagId">The TagID we are looking for.</param>
		/// <param name="includeInactive">Include Inactive component.</param>
		/// <returns>True if at least one MonoBehaviourTagged with passed TagID exists in the target gameobject and its children or False otherwise.</returns>
		public static bool ExistsInChildren (GameObject target, TagID tagId, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(target, tagId, includeInactive) != null;
		}
		#endregion

		#endregion
	}
}

