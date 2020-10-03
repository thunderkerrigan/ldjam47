using UnityEngine;
using UnityEngine.UI;

namespace Nectunia.PropertyInterface{

	/// <summary>
	/// Link a Slider to a Property3
	/// </summary>
	public class PropertyToSlider : MonoBehaviourTagged{
		#region ATTRIBUTS ___________________________________________________________
		public Slider	_targetSlider;
		public Image    _targetImage;
		public bool     _getPropertyInChildren;

		private Property _attachedProperty;
		#endregion


		#region EVENTS _______________________________________________________________
		public void OnEnable () {
			// get the first Property in the GameObject
			if (this._getPropertyInChildren) { this._attachedProperty = this.gameObject.GetPropertyInChildren(this.TagID); }
			else{ this._attachedProperty = this.gameObject.GetProperty(this.TagID); }

			// Define the Image with the icon attached to the Tag
			if(this._targetImage != null && this._tagID != null) {
				Texture2D tagIcon = Tags.GetTagByID(this._tagID).Icon;
				Sprite tagSprite = Sprite.Create(tagIcon, new Rect(0.0f, 0.0f, tagIcon.width, tagIcon.height), Vector2.zero);
				this._targetImage.sprite = tagSprite; 
			}
		}

		public void Update () {
			// Update the Slider
			if(this._targetSlider != null && this._attachedProperty != null && this._attachedProperty.ValuesType == Tag.ValueType.Float) {
				// Property3
				if(this._attachedProperty is Property3) {
					this._targetSlider.minValue = (float)((Property3)this._attachedProperty).A.Value;
					this._targetSlider.value = (float)((Property3)this._attachedProperty).B.Value;
					this._targetSlider.maxValue = (float)((Property3)this._attachedProperty).C.Value;				
				// Property 2
				}else if(this._attachedProperty is Property2) {
					this._targetSlider.value = (float)((Property2)this._attachedProperty).A.Value;
					this._targetSlider.maxValue = (float)((Property2)this._attachedProperty).B.Value;				
				// Property
				}else { this._targetSlider.value = (float)this._attachedProperty.A.Value; }
			}
		}
		#endregion
	}
}
