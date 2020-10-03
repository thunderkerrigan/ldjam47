using System.Collections.Generic;
using UnityEngine;
using System;

namespace Nectunia.PropertyInterface{

	/// <summary>
	/// Singleton used to group PropertyOptim component Update for performance reasons.
	/// </summary>
	/// <remarks>
	/// Should not be add manualy as a component of your scene. It will be automaticaly created by PropertyOptim components.
	/// </remarks>
	[AddComponentMenu("")] // With this it's not possible to add this component on the a GameObject with the menu or the "Add component" button.
	[Serializable]
	public class PropertyOptimUpdater : MonoBehaviour{
		
		/// <summary>
		/// The list of all enabled PropertyOptim components in the scene.
		/// </summary>
		/// <remarks>
		/// Marked as public for ease of control when running in the editor.
		/// </remarks>
		[SerializeField]
		public List<PropertyOptim> _properties = new List<PropertyOptim>();

		/// <summary>
		/// The list of all enabled PropertyOptim components in the scene.
		/// </summary>
		public List<PropertyOptim> Properties {
			get{ return this._properties; }
		}

		#region SINGLETON ____________________________________________________________
		private static PropertyOptimUpdater _instance;

		/// <summary>
		/// Get the PropertyOptimUpdater instance.
		/// </summary>
		/// <remarks>
		/// This should be the only way to access PropertyOptimUpdater.
		/// </remarks>
		public static PropertyOptimUpdater Instance {
			get {
				if (_instance == null) {
					GameObject gameObject = new GameObject("PropertyOptimUpdater" + DateTime.Now.ToFileTime());
					_instance = gameObject.AddComponent<PropertyOptimUpdater>();
				}
				return _instance;
			}
		}

		private void Awake(){
			// If a new instance is create, keep the first singleton and destroye the new one if one already exists.
			if (_instance == null){ _instance = this; } 
			else { Destroy(this); }
		}
		#endregion

		private void OnDisable () {
			this._properties.Clear();
		}

		// Update is called once per frame
		void Update () {
			foreach(PropertyOptim currentProperty in this._properties) {
				currentProperty?.DelayedUpdate();
			}
		}
	}
}
