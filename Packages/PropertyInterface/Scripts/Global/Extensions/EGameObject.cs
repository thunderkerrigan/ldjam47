using UnityEngine;
using System.Collections.Generic;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Extension class to allow to used Property and MonoBehaviourTagged static functions directly from a GameObject instance.
	/// </summary>
	public static class EGameObject{
		#region PROPERTY ***
		#region GetProperties() ---
		/// <summary>
		/// Return all Nectunia.PropertyInterface.Property in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <returns>All Nectunia.PropertyInterface.Property in the gameObject.</returns>
		public static List<Property> GetProperties(this GameObject gameObject) {
			return Property.GetProperties(gameObject);
		}
		
		/// <summary>
		/// Return all Nectunia.PropertyInterface.Property with passed TagID in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>All Nectunia.PropertyInterface.Property with passed TagID in the gameObject.</returns>
		public static List<Property> GetProperties(this GameObject gameObject, TagID tagID) {
			return Property.GetProperties(gameObject, tagID);
		}
		#endregion
		
		#region GetPropertiesInChildren() ---
		/// <summary>
		/// Return all Nectunia.PropertyInterface.Property in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>All Nectunia.PropertyInterface.Property in the gameObject or in its children.</returns>
		public static List<Property> GetPropertiesInChildren(this GameObject gameObject, bool includeInactive = false) {
			return Property.GetPropertiesInChildren(gameObject, includeInactive);
		}
		
		/// <summary>
		/// Return all Nectunia.PropertyInterface.Property with passed TagID in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>All Nectunia.PropertyInterface.Property with passed TagID in the gameObject or in its children.</returns>
		public static List<Property> GetPropertiesInChildren(this GameObject gameObject, TagID tagID, bool includeInactive = false) {
			return Property.GetPropertiesInChildren(gameObject, tagID, includeInactive);
		}
		#endregion
		
		#region GetProperty() ---
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.Property in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <returns>The first Nectunia.PropertyInterface.Property in the gameObject.</returns>
		public static Property GetProperty(this GameObject gameObject) {
			return Property.GetProperty(gameObject);
		}
		
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.Property with passed TagID in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>The first Nectunia.PropertyInterface.Property with passed TagID in the gameObject.</returns>
		public static Property GetProperty(this GameObject gameObject, TagID tagID) {
			return Property.GetProperty(gameObject, tagID);
		}
		#endregion
		
		#region GetPropertyInChildren() ---
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.Property in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>the first Nectunia.PropertyInterface.Property in the gameObject or in its children.</returns>
		public static Property GetPropertyInChildren(this GameObject gameObject, bool includeInactive = false) {
			return Property.GetPropertyInChildren(gameObject, includeInactive);
		}
		
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.Property with passed TagID in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>the first Nectunia.PropertyInterface.Property with passed TagID in the gameObject or in its children.</returns>
		public static Property GetPropertyInChildren(this GameObject gameObject, TagID tagID, bool includeInactive = false) {
			return Property.GetPropertyInChildren(gameObject, tagID, includeInactive);
		}
		#endregion
		#endregion


		#region MONOBEHAVIOURTAGGED ***
		#region GetMonoBehavioursTagged() ---
		/// <summary>
		/// Return all Nectunia.PropertyInterface.MonoBehaviourTagged in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <returns>All Nectunia.PropertyInterface.MonoBehaviourTagged in the gameObject.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged(this GameObject gameObject) {
			return MonoBehaviourTagged.GetMonoBehavioursTagged(gameObject);
		}
		
		/// <summary>
		/// Return all Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>All Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the gameObject.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTagged(this GameObject gameObject, TagID tagID) {
			return MonoBehaviourTagged.GetMonoBehavioursTagged(gameObject, tagID);
		}
		#endregion
		
		#region GetMonoBehavioursTaggedInChildren() ---
		/// <summary>
		/// Return all Nectunia.PropertyInterface.MonoBehaviourTagged in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>All Nectunia.PropertyInterface.MonoBehaviourTagged in the gameObject or in its children.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTaggedInChildren(this GameObject gameObject, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(gameObject, includeInactive);
		}
		
		/// <summary>
		/// Return all Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>All Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the gameObject or in its children.</returns>
		public static List<MonoBehaviourTagged> GetMonoBehavioursTaggedInChildren(this GameObject gameObject, TagID tagID, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehavioursTaggedInChildren(gameObject, tagID, includeInactive);
		}
		#endregion
		
		#region GetMonoBehaviourTagged() ---
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.MonoBehaviourTagged in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <returns>The first Nectunia.PropertyInterface.MonoBehaviourTagged in the gameObject.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged(this GameObject gameObject) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(gameObject);
		}
		
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the current GameObject.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <returns>The first Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the gameObject.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTagged(this GameObject gameObject, TagID tagID) {
			return MonoBehaviourTagged.GetMonoBehaviourTagged(gameObject, tagID);
		}
		#endregion
		
		#region GetMonoBehaviourTaggedInChildren() ---
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.MonoBehaviourTagged in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>the first Nectunia.PropertyInterface.MonoBehaviourTagged in the gameObject or in its children.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTaggedInChildren(this GameObject gameObject, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(gameObject, includeInactive);
		}
		
		/// <summary>
		/// Return the first Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the current GameObject or in its children.
		/// </summary>
		/// <param name="gameObject">The actual GameObject.</param>
		/// <param name="tagID">The tagID we are looking for.</param>
		/// <param name="includeInactive">Include inactive GameObject ?</param>
		/// <returns>the first Nectunia.PropertyInterface.MonoBehaviourTagged with passed TagID in the gameObject or in its children.</returns>
		public static MonoBehaviourTagged GetMonoBehaviourTaggedInChildren(this GameObject gameObject, TagID tagID, bool includeInactive = false) {
			return MonoBehaviourTagged.GetMonoBehaviourTaggedInChildren(gameObject, tagID, includeInactive);
		}
		#endregion
		#endregion
	}
}
