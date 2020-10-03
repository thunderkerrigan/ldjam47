using System;
using UnityEngine;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Class used by MonoBehaviourTagged component to know wich other MonoBehaviourTagged component it can interact with.
	/// </summary>
	/// <remarks>
	/// Because of how the Tag are stored, their ref can change between two frame. You shouldn't refer a Tag by it's reference. Use TagID instead.
	/// </remarks>
	[Serializable]
	public class Tag{
		#region CONSTANTES _________________________________________________________
		/// <summary>
		/// Default ValueType set to a Tag.
		/// </summary>
		public const ValueType DEFAULT_VALUESTYPE = ValueType.Float;
		#endregion


		#region ENUM _______________________________________________________________
		/// <summary>
		/// List all Type that can be assigned to values of a MonoBehaviourTagged component.
		/// </summary>
		public enum ValueType {
			Boolean, // Use &= operator
			Color,
			Double,
			Float,
			Int,
			Long,
			String,
			Vector2,
			Vector2Int,
			Vector3,
			Vector3Int,
			Vector4
		}

		#endregion


		#region ATTRIBUTES _________________________________________________________
		/// <summary>
		/// The TagID of the Tag.
		/// </summary>
		/// <remarks>
		/// TagID should be use to refer a Tag instead of the Tag's reference.
		/// </remarks>
		[SerializeField]
		public TagID			Id;

		/// <summary>
		/// The name of the tag.
		/// </summary>
		public string			Name;

		/// <summary>
		/// The icon assigned to a Tag
		/// </summary>
		public Texture2D		Icon;
		[SerializeField]
		private Tag.ValueType	_valuesType = ValueType.Float;

		/// <summary>
		/// The Type of the values that will be used by all MonoBehaviourTagged component wich refer this Tag.
		/// </summary>
		public Tag.ValueType ValuesType {
			get { return this._valuesType; }
			set {
				if(this._valuesType != value) { this._valuesType = value; }
			}
		}
		#endregion


		#region METHODS ____________________________________________________________		
		/// <summary>
		/// Return the System.Type of the current Tag.ValueType.
		/// </summary>
		/// <returns>The System.Type of the current Tag.ValueType.</returns>
		public System.Type GetSelectedType () {
			return GetSystemType(this.ValuesType);
		}

		/// <summary>
		/// Return the Type of the Tag.ValueType passed.
		/// </summary>
		/// <param name="type">The Tag.ValueType to return the system Type.</param>
		/// <returns>The system type of the Tag.ValueType passed.</returns>
		public static System.Type GetSystemType (Tag.ValueType type) {
			return type.GetSystemType();
		}

		#endregion
	}
}
