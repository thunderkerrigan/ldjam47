using UnityEngine;

namespace Nectunia.Utility {

	/// <summary>
	/// Component to force a GameObect to follow a target Gameobject.
	/// </summary>
	[RequireComponent(typeof(Transform))]
	public class FollowTarget : MonoBehaviour {
		#region PROPERTIES ________________________________________________________
		/// <summary>
		/// The target GameObject to follow.
		/// </summary>
		public GameObject   _target;

		/// <summary>
		/// Gap between the parent GameObject and it's Target.
		/// </summary>
		public Vector3      _gap = Vector3.zero;

		/// <summary>
		/// Has the parent GameObject to follow the target on the X axis.
		/// </summary>
		public bool         _followOnX = true;

		/// <summary>
		/// Has the parent GameObject to follow the target on the Y axis.
		/// </summary>
		public bool         _followOnY = false;

		/// <summary>
		/// Has the parent GameObject to follow the target on the Z axis.
		/// </summary>
		public bool         _followOnZ = true;

		private Transform   _targetTransform;
		private Vector3     _targetLastPosition;

		/// <summary>
		/// Lerp speed of the parent GameObject.
		/// </summary>
		[Range(0.01f,1f)]
		public float    _lerpSpeed = 0.2f;
		#endregion


		#region EVENT _____________________________________________________________
		void Start () {
			// Check if the _target have a transform component
			if (this._target != null) {
				this._targetTransform = this._target.GetComponent<Transform>();
				if (this._targetTransform == null) {
					this._target = null;
					throw new System.Exception("The GameObject '" + this._target.name + "' need a transform to be assigned to a FollowTarget");
				// Initialise the current position as the last one if the target have a Transform component
				} else { this._targetLastPosition = this._targetTransform.position; }
			}
		}

		void LateUpdate () {
			if (this._target != null) { this.moveToTarget(); }
		}
		#endregion
			   

		#region METHODES __________________________________________________________
		/// <summary>
		/// Move the parent GameObject to the Target position.
		/// </summary>
		private void moveToTarget () {
			if (this._target != null && this._targetTransform != null && this._targetLastPosition != this._targetTransform.position) {

				// Set the new position of the actual GameObject
				Vector3 positionToTarget = new Vector3();
				if (this._followOnX) { positionToTarget.x = this._targetTransform.position.x + this._gap.x; } else { positionToTarget.x = this.transform.position.x; }
				if (this._followOnY) { positionToTarget.y = this._targetTransform.position.y + this._gap.y; } else { positionToTarget.y = this.transform.position.y; }
				if (this._followOnZ) { positionToTarget.z = this._targetTransform.position.z + this._gap.z; } else { positionToTarget.z = this.transform.position.z; }
				this.transform.position = Vector3.Lerp(this.transform.position, positionToTarget, this._lerpSpeed);

				// Set the current target position as the last one
				this._targetLastPosition = this._targetTransform.position;
			}
		}
		#endregion
	}
}
