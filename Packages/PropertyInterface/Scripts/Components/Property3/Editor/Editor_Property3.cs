using UnityEditor;
using UnityEngine;

namespace Nectunia.PropertyInterface {	

	/// <summary>
	/// The custom Editor for Property3 component
	/// </summary>
	[CustomEditor(typeof(Property3), true)]
	[CanEditMultipleObjects]
	public class Editor_Property3 : Editor_Property{

		#region ATTRIBUTES ______________________________________________________________________________
		protected Property3	_targetProperty3;

		// Labels
		private GUIContent  _BLabel			= new GUIContent("B",			"The Value B of the Property");
		private GUIContent  _BSourceLabel	= new GUIContent("source : B",	"The Component's field that will be used to source the value B");
		private GUIContent  _CLabel			= new GUIContent("C",			"The Value C of the Property");
		private GUIContent  _CSourceLabel	= new GUIContent("source : C",	"The Component's field that will be used to source the value C");
		#endregion


		#region EVENTS ___________________________________________________________________________________
		public override void OnEnable () {
			if(Selection.gameObjects.Length == 1) {
				base.OnEnable();
				this._targetProperty3 = (Property3) this.target;
			}
		}		
		#endregion


		#region METHODS __________________________________________________________________________________
		/// <summary>
		/// Draw the fields of the values.
		/// </summary>
		protected override void DrawValuesField () {
			this.PropertyValueField(this._ALabel, this._targetProperty3.A);
			this.PropertyValueField(this._BLabel, this._targetProperty3.B);
			this.PropertyValueField(this._CLabel, this._targetProperty3.C);
		}
		
		/// <summary>
		/// Draw the fields of the values sources.
		/// </summary>
		protected override void DrawValuesSources () {						
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._ASourceLabel, this._targetProperty3.A, this._componentPropertiesNames);
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._BSourceLabel, this._targetProperty3.B, this._componentPropertiesNames);
			EditorGUILayout_PropertyInterface.PropertyValueSourceField(this._CSourceLabel, this._targetProperty3.C, this._componentPropertiesNames);
		}

		/// <summary>
		/// Force the targetProperty to update.
		/// </summary>
		protected override void UpdateProperty () {			
			this._targetProperty3.DelayedUpdate();
		}
		
		#endregion
	}
}
