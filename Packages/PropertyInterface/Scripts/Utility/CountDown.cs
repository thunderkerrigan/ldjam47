using UnityEngine;

namespace Nectunia.Utility {

	/// <summary>
	/// Count-down class.
	/// </summary>
	/// <remarks>
	/// Can only be used in a MonoBehaviour script because of the "Time.time" use.
	/// </remarks>
	[System.Serializable]
	public class CountDown {

		#region ATTRIBUTES _______________________________________________________________
		[SerializeField]
		private float _duration;
		[SerializeField]
		private bool _isUp = true;
		[SerializeField]
		private float _nextUp = 0f;
		   

		#region PROPERTIES _______________________________________________________________
		/// <summary>
		/// Return True if the duration of the count-down has elapsed or if the count-down has not started yet. Return False otherwise.
		/// </summary>
		public bool IsUp {
			get {
				// If the count-down is not up, we check if it should be
				if (!this._isUp && this._nextUp <= Time.time) { this._isUp = true; }

				return this._isUp;
			}
		}

		/// <summary>
		/// The count-down duration.
		/// </summary>
		public float Duration {
			get {
				return this._duration;
			}
			set {
				//	The _duration can't have a negative value
				if (value > 0) { this._duration = value; } else { this._duration = 0f; }
				this.reset();
			}
		}
		#endregion
		#endregion			
			   

		#region CONSTRUCTEURS ____________________________________________________________
		public CountDown (float duration = 0f) {
			this.Duration = duration;
		}
		#endregion
			   

		#region METHODES _________________________________________________________________

		/// <summary>
		/// Start the count-down.
		/// </summary>
		public void start () {
			if (this.IsUp && this._duration > 0f) {
				this._isUp = false;
				this._nextUp = Time.time + this._duration;
			}
		}

		/// <summary>
		/// Return the remaining number of second of the count-down.
		/// </summary>
		/// <returns>The remaining number of second.</returns>
		public float getRemainingSecond () {
			float result = 0f;
			if (!this.IsUp) { result = this._nextUp - Time.time; }
			return result;
		}

		/// <summary>
		/// Reset the count-down.
		/// </summary>
		public void reset () {
			this._isUp = true;
			this._nextUp = 0f;
		}
		#endregion
	}
}
