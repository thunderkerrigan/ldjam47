using UnityEditor;
using UnityEditorInternal;
using UnityEngine;
using System.Collections;
using System;
using Nectunia.Utility;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Class used to draw a Tags list.
	/// </summary>
	public class Drawer_TagsList : AdvancedReorderableList {
		#region CONSTANTS __________________________________________________________
		private const string C_LIST_HEADER_TEXT		= "List of Tags available in the project";
		private const string C_LIST_NAME			= "TagsList";
		private const float	C_ELEMENT_HEIGHT		= 55f;
		private const float C_ELEMENT_BOX_PADDING	= 3f;
		private const float C_ELEMENT_BOX_MARGIN	= 5f;
		private const float C_FIELD_HEIGHT			= 21f;
		private const float C_FIELD_MARGIN			= 3f;
		private const float C_LABEL_NAME_WIDTH		= 40f;
		private const float C_LABEL_ICON_WIDTH		= 30f;
		private const float C_NAME_RECT_MIN_WIDTH	= 70f;
		#endregion
		

		#region CONSTRUCTOR ________________________________________________________
		/// <summary>
		/// Instanciate the Tags list drawer.
		/// </summary>
		/// <param name="list">The list of Tags.</param>
		/// <param name="elementsType">The Type of Tags.</param>
		/// <param name="owner">The Object wich own the Tags list. Used to enable Undo/Redo. If null Undo/Redo won't be available.</param>
		/// <param name="listName">The name to draw in the header of the list.</param>
		public Drawer_TagsList (IList list, Type elementsType, UnityEngine.Object owner, string listName = C_LIST_NAME)
			:base (list, elementsType, owner, listName) {
			this.Enable();
			this.elementHeight = C_ELEMENT_HEIGHT;			
		}

		~Drawer_TagsList () {
			// To avoid memories leaks
			this.Disable();
		}
		#endregion
		
		
		#region METHODES ___________________________________________________________	
		 /// <summary>
		 /// Enable the List.
		 /// </summary>
		public void Enable () {
			this.drawHeaderCallback		+= this.DrawHeader;
			this.drawElementCallback	+= this.DrawElement;
			this.onAddCallback			+= this.AddItem;
			this.onRemoveCallback		+= this.RemoveItem;
			this.onChangedCallback		+= this.OnChangedTagList; // ReorderableList.onChangedCallback is call only when items are added or deleted from the list
		}

		 /// <summary>
		 /// Disable the list.
		 /// </summary>
		public void  Disable () {
			this.drawHeaderCallback		-= this.DrawHeader;
			this.drawElementCallback	-= this.DrawElement;
			this.onAddCallback			-= this.AddItem;
			this.onRemoveCallback		-= this.RemoveItem;
			this.onChangedCallback		-= this.OnChangedTagList;
		}

		/// <summary>
		/// Draw the header of the list.
		/// </summary>
		/// <param name="rect">The actual position.</param>
		protected virtual void DrawHeader(Rect rect){
			GUI.Label(rect, C_LIST_HEADER_TEXT);
		}

		 /// <summary>
		 /// Add a new empty element.
		 /// </summary>
		 /// <param name="list">The list to update.</param>
		protected void AddItem(ReorderableList list){
			Undo.RecordObject(this._owner, "Add in reorderable list");
			Tag newItem = new Tag();
			newItem.Id = DateTime.Now.ToFileTime();
			list.list.Add(newItem);
			// Focus the new element
			this.Focus(list.list.Count - 1);
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
					Undo.RecordObject(this._owner, "Delete in " + this._listName + " list");
					Tag deletedTag = (Tag)list.list[list.index];
					ReorderableList.defaultBehaviours.DoRemoveButton(list);
					// If we keep the focus on the deleted  element, the content won't be refreshed. So we set actual control focus to nothing to force current element redraw
					GUI.FocusControl(null);
					Tags.Instance.OnChangedTag(deletedTag);
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
			Tag currentItem = (Tag) this.list[index];
			
			if (currentItem != null) {
				EditorGUI.BeginChangeCheck();
				// Calc dynamique field width
				float startX = rect.x + C_ELEMENT_BOX_PADDING;
				float iconRectWidth = rect.height - C_ELEMENT_BOX_MARGIN - (C_ELEMENT_BOX_PADDING * 3); // Padding * 3 is for better readability
				float nameRectWith = rect.width - (C_ELEMENT_BOX_MARGIN * 2) - (C_ELEMENT_BOX_PADDING * 2) - C_LABEL_NAME_WIDTH - (C_FIELD_MARGIN * 3) - C_LABEL_ICON_WIDTH - iconRectWidth;
				if( nameRectWith < C_NAME_RECT_MIN_WIDTH) { nameRectWith = C_NAME_RECT_MIN_WIDTH; }
				// Set rect for all fields
				// Name ---
				Rect nameLabelRect	= new Rect(startX, rect.y + C_ELEMENT_BOX_PADDING, C_LABEL_NAME_WIDTH, C_FIELD_HEIGHT);
				Rect nameRect		= new Rect(startX + C_LABEL_NAME_WIDTH, rect.y + C_ELEMENT_BOX_PADDING, nameRectWith, C_FIELD_HEIGHT);
				// Icon ---
				Rect iconLabelRect	= new Rect(startX + nameRectWith + C_LABEL_NAME_WIDTH + C_FIELD_MARGIN, rect.y + C_ELEMENT_BOX_PADDING, C_LABEL_ICON_WIDTH, rect.height);
				Rect iconRect		= new Rect(startX + nameRectWith + C_LABEL_NAME_WIDTH + (C_FIELD_MARGIN *2) + C_LABEL_ICON_WIDTH, rect.y + C_ELEMENT_BOX_PADDING, iconRectWidth, iconRectWidth);
				// Type ---
				Rect TypeLabelRect	= new Rect(startX, rect.y + C_ELEMENT_BOX_PADDING + 23, 40, C_FIELD_HEIGHT);
				Rect TypeRect		= new Rect(startX + 40, rect.y + C_ELEMENT_BOX_PADDING + 23, 100, C_FIELD_HEIGHT);
				// ElementBox
				Rect elementBox		= new Rect(rect.x, rect.y, rect.width, this.elementHeight - C_ELEMENT_BOX_MARGIN);

				//Draw element border
				EditorGUI.HelpBox(elementBox, "", MessageType.None);

				// Name Field
				GUIContent nameLabel = new GUIContent("Name", "The name of the tag");
				EditorGUI.LabelField(nameLabelRect, nameLabel, CustomStyle.TagsList.Label());
				this.NameNextControl(index, "Name");
				string newName = EditorGUI.TextField(nameRect, currentItem.Name);
				
				// Type Field
				GUIContent typeLabel = new GUIContent("Type", "The Type of values that Property using this tag will use");
				EditorGUI.LabelField(TypeLabelRect, typeLabel, CustomStyle.TagsList.Label());
				this.NameNextControl(index, "Type");
				Tag.ValueType newType = (Tag.ValueType)EditorGUI.EnumPopup(TypeRect, currentItem.ValuesType);

				// Icon Label
				GUIContent iconLabel = new GUIContent("Icon", "The icon to show in the inspector for Property wich refer this Tag");
				EditorGUI.LabelField(iconLabelRect, iconLabel, CustomStyle.TagsList.IconLabel());

				// Icon Field
				this.NameNextControl(index, "Icon");
				Texture2D newIcon = (Texture2D)EditorGUI.ObjectField(iconRect, currentItem.Icon, typeof(Texture2D), false);
				
				if (EditorGUI.EndChangeCheck()) {
					// Say to Unity's Undo system to record all change done in the Tags object after this
					Undo.RecordObject(this._owner, "Tag modification in reorderable list");
					if (newName != currentItem.Name) { currentItem.Name = newName; } // We don't call OnChangedTag event for the name because it's too heavy and not realy usefull
					if (newIcon != currentItem.Icon) { currentItem.Icon = newIcon; }
					if (newType != currentItem.ValuesType) {						
						bool warningResult = EditorUtility.DisplayDialog("Warning!", "Some Components may link this Tag. If you continue you will have to modify all concerned Components in all scenes. Do you want to continue?", "Yes", "No");
						if (warningResult) {
							currentItem.ValuesType = newType;							
							Tags.Instance.OnChangedTag(currentItem);
						}									
					}
					this.OnChangedTagList(this);
				}
			// Add en warning in the debug log if no element exist in the list at this index
			} else { Debug.Log("No Element found at the index : " + index.ToString() + " in the Tag reorderable List : " + this.serializedProperty.ToString()); }
		}		
		
		 /// <summary>
		 /// Call the Onchanged call back of the Tags List.
		 /// </summary>
		 /// <param name="list">The list that was changed.</param>
		protected void OnChangedTagList (ReorderableList list) {
			Tags.Instance.OnChangedTagList();
		}
		#endregion
	}
}
