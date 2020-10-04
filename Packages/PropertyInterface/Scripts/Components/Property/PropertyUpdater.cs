using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nectunia.PropertyInterface{

	/// <summary>
	/// Singleton used to group Property component Update for performance reasons.
	/// </summary>
	/// <remarks>
	/// Should not be add manualy as a component of your scene. It will be automaticaly created by Property components.
	/// </remarks>
	[AddComponentMenu("")] // With this it's not possible to add this component on the a GameObject with the menu or the "Add component" button.
	[Serializable]
	public class PropertyUpdater : MonoBehaviour{
		
		/// <summary>
		/// The list of all enabled Property components in the scene.
		/// </summary>
		/// <remarks>
		/// Marked as public for ease of control when running in the editor.
		/// </remarks>
		[SerializeField]
		public List<Property> _properties = new List<Property>();

		/// <summary>
		/// The list of all enabled Property components in the scene.
		/// </summary>
		public List<Property> Properties {
			get{ return this._properties; }
		}

		#region SINGLETON ____________________________________________________________
		private static PropertyUpdater _instance;
		private static bool applicationIsQuitting = false;

		/// <summary>
		/// Get the PropertyUpdater instance.
		/// </summary>
		/// <remarks>
		/// This should be the only way to access PropertyUpdater.
		/// </remarks>
		public static PropertyUpdater Instance {
			get {
				if (applicationIsQuitting) { return null; }
				else if (_instance == null ) {
					GameObject gameObject = new GameObject("PropertyUpdater" + DateTime.Now.ToFileTime());
					_instance = gameObject.AddComponent<PropertyUpdater>();
				}
				return _instance;
			}
		}

		private void Awake(){
			// If a new instance is create, keep the first singleton and destroye the new one if one already exists.
			if (_instance == null && !applicationIsQuitting){ _instance = this; } 
			else { Destroy(this); }
		}
		#endregion

		private void OnDisable () {
			this._properties.Clear();
		}

		private void OnDestroy () {
			applicationIsQuitting = true;
		}
		// Update is called once per frame
		void Update () {
			foreach(Property currentProperty in this._properties) {
				currentProperty?.DelayedUpdate();
			}
		}
	}
}
