using UnityEngine;
using System;

namespace Nectunia.Utility {

	/// <summary>
	/// Class to calculate if an action is critical
	/// </summary>
	[Serializable]
	public class CriticalChance {
		#region ATTRIBUTS ________________________________________________________________
		[SerializeField]
		private float _coefficient;
		[NonSerialized]private System.Random _random = new System.Random();
		  

		#region PROPERTIES _______________________________________________________________

		/// <summary>
		/// The coefficient of chance to have a critic.
		/// </summary>
		public float Coefficient {
			get { return this._coefficient; }
			set {
				if(value < 0) { value = 0; }
				this._coefficient = value;
			}
		}
		#endregion
		#endregion
			 			   

		#region CONSTRUCTEURS ____________________________________________________________
		public CriticalChance (float coefficient) {
			this.Coefficient = coefficient;
		}

		#endregion

			   
		#region METHODES _________________________________________________________________
		/// <summary>
		/// Is the current chance Critical ?
		/// </summary>
		/// <returns>True if it's a critical, false otherwise.</returns>
		public bool isCritical () {
			// get a random float between 0.01 and 1.00
			// we don't start with 0.00 because with a coefficient = 0 with still could have some critical when randomFloat = 0.00
			float randomFloat = this._random.Next(1,101)/100f;
			return (randomFloat <= this._coefficient);
		}

		/// <summary>
		/// Is the current chance Critical ?
		/// </summary>
		/// <param name="coefficient">The coefficient of chance to have a critic.</param>
		/// <returns>True if it's a critical, false otherwise.</returns>
		public static bool isCritical (float coefficient) {
			if(coefficient < 0f) { coefficient = 0f; }
			System.Random random = new System.Random();
			// get a random float between 0.01 and 1.00
			// we don't start with 0.00 because with a coefficient = 0 with still could have some critical when randomFloat = 0.00
			float randomFloat = random.Next(1,101)/100f;
			return (randomFloat <= coefficient);
		}
	
		#endregion
	}
}
