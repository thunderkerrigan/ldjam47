using UnityEditor;
using UnityEngine;

namespace Nectunia.PropertyInterface {	

	/// <summary>
	/// The custom Editor for Property2 component
	/// </summary>
	[CustomEditor(typeof(Property2), true)]
	[CanEditMultipleObjects]
	public class Editor_Property2 : Editor_Property{

		#region ATTRIBUTES ______________________________________________________________________________
		protected Property2	_targetProperty2;

		// Labels
		private GUIContent  _BLabel			= new GUIContent("B",			"The Value B of the Property");
		private GUIContent  _BSourceLabel	= new GUIContent("source : B",	"The Component's field that will be used to source the value B");
		#endregion


		#region EVENTS ___________________________________________________________________________________
		public override void OnEnable () {
			if(Selection.gameObjects.Length == 1) {
				base.OnEnable();
				this._targetProperty2 = (Property2) this.target;
			}
		}		
		#endregion


		#region METHODS __________________________________________________________________________________
		/// <summary>
		/// Draw the fields of the values.
		/// </summary>
		protected override void DrawValuesField () {
			this.PropertyValueField(this._ALabel, this._targetProperty2.A);
			this.PropertyValueField(this._BLabel, this._targetProperty2.B);
		}
		
		/// <summary>
		/// Draw the fields of the values sources.
		/// </summary>
		protected override void DrawValuesSources () {						
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._ASourceLabel, this._targetProperty2.A, this._componentPropertiesNames);
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._BSourceLabel, this._targetProperty2.B, this._componentPropertiesNames);
		}

		/// <summary>
		/// Force the targetProperty to update.
		/// </summary>
		protected override void UpdateProperty () {			
			this._targetProperty2.DelayedUpdate();
		}
		
		#endregion
	}
}
