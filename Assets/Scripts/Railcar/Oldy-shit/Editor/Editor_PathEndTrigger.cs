using UnityEngine;
using UnityEditor;
using Nectunia.Utility;
using Nectunia.PropertyInterface;
using UnityEditorInternal;
using PathCreation;

//[CustomEditor(typeof(PathEndTrigger))]
public class Editor_PathEndTrigger : Editor{	
	/*private const float C_ELEMENT_BOX_PADDING	= 3f;
	private const float C_ELEMENT_BOX_MARGIN	= 5f;
	private const float C_FIELD_HEIGHT			= 21f;
	private const float C_LABEL_PATH_WIDTH		= 40f;

    private PathEndTrigger _pathEndTrigger;
	private AdvancedReorderableList _pathList;

	private void OnEnable () {
		this._pathEndTrigger = (PathEndTrigger) target;
		this._pathList = new AdvancedReorderableList (this._pathEndTrigger.paths, typeof(PathTile), this._pathEndTrigger, "PathList");
		EnableList();
	}

	private void OnDisable () {
		DisableList();
	}

	public override void OnInspectorGUI () {
		Undo.RecordObject(this._pathEndTrigger, "PathEndTrigger");
		this._pathList.DoLayoutList();
	}

	/// <summary>
	/// Enable the List.
	/// </summary>
	public void EnableList () {
		this._pathList.drawElementCallback	+= this.DrawElement;
		this._pathList.onAddCallback		+= this.AddItem;
		this._pathList.onRemoveCallback		+= this.RemoveItem;
	}

	/// <summary>
	/// Disable the list.
	/// </summary>
	public void  DisableList () {
		this._pathList.drawElementCallback	-= this.DrawElement;
		this._pathList.onAddCallback		-= this.AddItem;
		this._pathList.onRemoveCallback		-= this.RemoveItem;
	}

	/// <summary>
	/// Add a new empty element.
	/// </summary>
	/// <param name="list">The list to update.</param>
	protected void AddItem(ReorderableList list){
		Undo.RecordObject(this._pathEndTrigger, "Add in reorderable list");
		PathTile newItem = new PathTile();
		list.list.Add(newItem);
		// Focus the new element
		this._pathList.Focus(list.list.Count - 1);
	}
	
	/// <summary>
	/// Delete the selected element after a warning message.
	/// </summary>
	/// <param name="list">The list to update.</param>
	public void RemoveItem(ReorderableList list) {
		if(list.count > 0) {
			EditorWindow currentWindows = EditorWindow.focusedWindow;
			bool warningResult = EditorUtility.DisplayDialog("Warning!", "Are you sure you want to delete the element?", "Yes", "No");
			// Get back the focus to the editor Windows because the warning popup changed the focus.
			currentWindows.Focus();
			if (warningResult) {				
				// Say to Unity's Undo system to record all change done in the Tags object after this
				Undo.RecordObject(this._pathEndTrigger, "Delete in " + this._pathList.ListName + " list");
				ReorderableList.defaultBehaviours.DoRemoveButton(list);
				// If we keep the focus on the deleted  element, the content won't be refreshed. So we set actual control focus to nothing to force current element redraw
				GUI.FocusControl(null);
			} 
		}
	}
			
	/// <summary>
	/// Draws one element of the list.
	/// </summary>
	/// <param name="rect">The actual position in the editor.</param>
	/// <param name="index">The index element of the list.</param>
	/// <param name="active">Is the element active ?</param>
	/// <param name="focused">Is the element focused ?</param>
	protected void DrawElement (Rect rect, int index, bool active, bool focused) {
		PathTile currentItem = (PathTile) this._pathList.list[index];
			
		if (currentItem != null) {
			// Calc dynamique field width
			float startX = rect.x + C_ELEMENT_BOX_PADDING;
			// Set rect for all fields
			// Path ---
			Rect pathLabelRect	= new Rect(startX, rect.y + C_ELEMENT_BOX_PADDING, C_LABEL_PATH_WIDTH, C_FIELD_HEIGHT);
			Rect pathRect		= new Rect(startX + C_LABEL_PATH_WIDTH, rect.y + C_ELEMENT_BOX_PADDING, 40f, C_FIELD_HEIGHT);
			// way ---
			Rect wayRect		= new Rect(startX + 85f, rect.y + C_ELEMENT_BOX_PADDING, 40, C_FIELD_HEIGHT);

			// ElementBox
			Rect elementBox		= new Rect(rect.x, rect.y, rect.width, this._pathList.elementHeight - C_ELEMENT_BOX_MARGIN);

			//Draw element border
			EditorGUI.HelpBox(elementBox, "", MessageType.None);

			// Path Field
			GUIContent pathLabel = new GUIContent("PathCreator", "The PathCreator");
			EditorGUI.LabelField(pathLabelRect, pathLabel, CustomStyle.TagsList.Label());
			this._pathList.NameNextControl(index, "Path");
			currentItem._path = (PathCreator)EditorGUI.ObjectField(pathRect, currentItem._path, typeof(PathCreator), true);

			// Way Field
			currentItem._pathWay = (PathTile.PathWay)EditorGUI.EnumPopup(wayRect, currentItem._pathWay);
			this._pathList.NameNextControl(index, "Way");

		// Add en warning in the debug log if no element exist in the list at this index
		} else { Debug.Log("No Element found at the index : " + index.ToString() + " in the List"); }
	}		
	*/
}
