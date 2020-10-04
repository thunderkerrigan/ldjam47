using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using System;

namespace Nectunia.Utility {

	/// <summary>
	/// ReaorderableList with advanced options.
	/// </summary>
	public class AdvancedReorderableList : ReorderableList {
		#region CONSTANTS __________________________________________________________
		private const string C_ELEMENT_DEFAULT_NAME = "element";
		#endregion


		#region ATTRIBUTES _________________________________________________________
		/// <summary>
		/// Name of the list.
		/// </summary>
		protected string				_listName;

		/// <summary>
		/// The Object wich own the list.
		/// </summary>
		protected UnityEngine.Object	_owner;

		/// <summary>
		/// Base name of the element.
		/// </summary>
		protected string                _baseElementName = "";

		/// <summary>
		/// Base name for the first field of the list.
		/// </summary>
		protected string                _baseFirstFieldName = "";

		/// <summary>
		/// Name of the next element to focus.
		/// </summary>
		protected string                _nextFocusName = "";

		/// <summary>
		/// Have the fields been named.
		/// </summary>
		protected bool                  _fieldHasBeenNamed = false;

		/// <summary>
		/// Say if allowed input are held or not.
		/// </summary>
		protected  bool					_keyHeld = false;


		#region PROPERTIES ----
		/// <summary>
		/// Name of the list.
		/// </summary>
		public string ListName {
			get{ return this._listName; }
			set {
				// we reset the base element names to be able to get new ones corresponding to the new list name
				this._baseElementName = ""; 
				this._baseFirstFieldName = "";
				this._listName = value;
			}
		}
		#endregion
		#endregion


		#region CONSTRUCTOR ________________________________________________________
		/// <summary>
		/// Constructor with a list not serialized.
		/// </summary>
		/// <param name="list">The list to draw.</param>
		/// <param name="elementsType">Type of the list elements.</param>
		/// <param name="owner">The Object in wich the list is.</param>
		/// <param name="listName">The name of the list.<remarks>"-" is forbiden for this value.</remarks></param>
		public AdvancedReorderableList (IList list, Type elementsType, UnityEngine.Object owner, string listName = "AdvancedReorderableList")
			:base (list, elementsType, true, true, true, true) {
			this._owner = owner;
			if (listName.IndexOf('-') == -1) { this.ListName = listName; }
			else{
				this.ListName = "AdvancedReorderableList";
				Debug.Log("AdvancedReorderableList.ListName can't contain the char '-'. Name has been set to it's default value 'AdvancedReorderableList'. You may have element focus issues.");
			}
		}
		#endregion
		

		#region HANDLER MANAGER ____________________________________________________
		/// <summary>
		/// Handler to input.Have to be call in an update function.
		/// </summary>
		/// <remarks>
		/// Ctrl+ => Add an new item in the list.<br>
		/// Ctrl- => Delete the selected item.
		/// </remarks>
		protected void OnInputHandler () {
			Event e = Event.current;
			// Check pressed keys
			if (e.type == EventType.KeyDown && !this._keyHeld) {

				// "CTRL" + "+" => onAddCallback()
				if (e.control && e.keyCode == KeyCode.KeypadPlus) {
					this._keyHeld = true;
					if(this.onAddCallback != null) { this.onAddCallback(this); }
					else{ ReorderableList.defaultBehaviours.DoAddButton(this); }
					e.Use();
				}
				
				// "CTRL" + "-" => onRemoveCallback()
				if (e.control && e.keyCode == KeyCode.KeypadMinus) {
					if(this.index < this.list.Count && this.index > -1) {
						// We don't need to set "this._keyHeld = true" because warning message of deletion lose windows focus
						if( this.onRemoveCallback != null ) { this.onRemoveCallback(this); }	
						else{ ReorderableList.defaultBehaviours.DoRemoveButton(this); }
						e.Use();
					}
				}				
			}

			// Check released key
			if (e.type == EventType.KeyUp && this._keyHeld) {
				// Key : +
				if (e.keyCode == KeyCode.KeypadPlus) {
					this._keyHeld = false;
					e.Use();
				}
			}
		}

		/// <summary>
		/// According list index to the focused element.
		/// </summary>
		protected void OnElementFocusedHandler () {
			string currentFocusedName = GUI.GetNameOfFocusedControl();
			// Check if the current focused element is in our list
			if (currentFocusedName.Contains( this._baseElementName)) {
				// Change the index if the current focused element have an one
				string[] controlNameElements = currentFocusedName.Split(new char[]{'-'});
				if(controlNameElements.Length > 1) { this.index = Int32.Parse(controlNameElements[1]); }				
			}
		}

		/// <summary>
		/// If a new focus has been defined by the function Focus(), update the Controle Focus in the list.
		/// </summary>
		protected void OnFocusHasToChangedHandler () {
			if(this._nextFocusName != "") {
				GUI.FocusControl(this._nextFocusName);
				// If the control exists and has been focused, we don't need to try again later
				if(GUI.GetNameOfFocusedControl() == this._nextFocusName) { this._nextFocusName = ""; }
			}
		}
		#endregion
		

		#region OVERRIDE / HIDDING METHODES ________________________________________
		/// <summary>
		/// Call base DoLayoutList() with new handler.
		/// </summary>
		public new void DoLayoutList () {
			base.DoLayoutList();
			this.OnInputHandler();
			this.OnElementFocusedHandler();
			this.OnFocusHasToChangedHandler();
		}

		/// <summary>
		/// Call base DoList() with new handler.
		/// </summary>
		public new void DoList (Rect rect) {
			base.DoList(rect);
			this.OnInputHandler();
			this.OnElementFocusedHandler();
			this.OnFocusHasToChangedHandler();
		}
		
		#endregion


		#region UTILITY CALLBACK ___________________________________________________
		
		 /// <summary>
		 /// Add a new empty element.
		 /// </summary>
		 /// <param name="list">The list to update.</param>
		public void AddItemWithUndo(ReorderableList list){
			Undo.RecordObject(this._owner, "Add in " + this._listName + " list");
			ReorderableList.defaultBehaviours.DoAddButton(this);
			// Focus the new element
			this.Focus(list.list.Count - 1);
			
		}
	
		 /// <summary>
		 /// Delete the selected element after a warning message.
		 /// </summary>
		 /// <param name="list">The list to update.</param>
		public void RemoveItemWithUndoAndWarning(ReorderableList list) {
			if(list.count > 0) {
				EditorWindow currentWindows = EditorWindow.focusedWindow;
				bool warningResult = EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the element?", "Yes", "No");
				// Get back the focus to the editor Windows because the warning popup changed the focus.
				currentWindows.Focus();
				if (warningResult) {				
					// Say to Unity's Undo system to record all change done in the Tags object after this
					Undo.RecordObject(this._owner, "Delete in " + this._listName + " list");
					ReorderableList.defaultBehaviours.DoRemoveButton(list);
					// If we keep the focus on the deleted  element, the content won't be refreshed. So we set actual control focus to nothing to force current element redraw
					GUI.FocusControl(null);
				} 
			}
		}
		#endregion


		#region ELEMENT MANIPULATION METHODES ______________________________________
		/// <summary>
		/// Return the given index element name formatted to be read by the OnElementFocusedHandler().
		/// </summary>
		/// <param name="index">The element index to return the name.</param>
		/// <returns>The given index element name formatted.</returns>
		protected string ElementName (int index) {
			if(this._baseElementName == "") { this._baseElementName = this._listName + "." + C_ELEMENT_DEFAULT_NAME + "-"; }
			return this._baseElementName + index.ToString();
		}

		/// <summary>
		/// Set next control name with a formatted name used in OnElementFocusedHandler().
		/// </summary>
		/// <param name="index">The element index to return the name.</param>
		/// <param name="fieldName">Current field name for the element.</param>
		public void NameNextControl (int index, string fieldName = "") {
			this._fieldHasBeenNamed = true; // We check if at least one field has been named.
			GUI.SetNextControlName(fieldName + this.ElementName(index));
			if(this._baseFirstFieldName == "") { this._baseFirstFieldName = fieldName + this._baseElementName; }
		}

		/// <summary>
		/// Set the focus of the window to the first element's field of the list if one have been Named.
		/// </summary>
		/// <param name="index">The element index to focus.</param>
		public void Focus (int index = 0) {
			// Delay the focus change to the OnFocusHasToChangedHandler() to set the new focus after the list has been updated and drawn
			if(this._fieldHasBeenNamed && this._baseFirstFieldName != "" && index > -1) {
				this._nextFocusName = this._baseFirstFieldName + index.ToString();
			}
		}		
		#endregion
	}
}
