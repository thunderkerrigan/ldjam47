using UnityEngine;

namespace Nectunia.PropertyInterface {
	/// <summary>
	/// Simple Player class mouvement.
	/// </summary>
	public class PlayerMouvement_Demo : MonoBehaviour{
		#region ATTRIBUTES _________________________________________________________		
		private CharacterController controller;
		public int playerSpeed = 20;
		#endregion	
		
				
		#region EVENTS _____________________________________________________________		
		private void Start() {
			controller = gameObject.GetComponent<CharacterController>();
		}

		public void Update () {			
			// Move the player
			Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			controller.Move(move * Time.deltaTime * playerSpeed);

			// Rotate the player forward
			if (move != Vector3.zero) { gameObject.transform.forward = move; }
		}
		#endregion
	}
}

