using System;
using UnityEngine;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Class used to identify and refer a Tag.
	/// </summary>
	[Serializable]
	public struct TagID : IEquatable<TagID>{
		[SerializeField]
		private long _value;

		/// <summary>
		/// The ID of a Tag.
		/// </summary>
		public long Value {
			get => this._value;
			set => this._value = value;
		}

		#region IMPLICITE OPERATOR ______________________________________________________
		public static implicit operator TagID (long value) {
			return new TagID { _value = value };
		}

		public static implicit operator long (TagID value) {
			return value._value;
		}
		/*
		public static implicit operator string (TagID value) {
			return value._value.ToString();
		}*/

		/*public static implicit operator TagID (string value) {
			TagID result;
			if(value != null) { result = new TagID { _value = long.Parse(value) }; }
			else{ result = null; }
			return result;
		}*/

		public static bool operator == (TagID t1, TagID t2) {
			return t1.Equals(t2);
		}

		public static bool operator != (TagID t1, TagID t2) {
			return !t1.Equals(t2);
		}

		public bool Equals (TagID tag) {
			return tag._value == this._value;
		}

		public override int GetHashCode () {
			return this._value.GetHashCode();
		}

		public override bool Equals (object obj) {
			return base.Equals(obj);
		}

		public override string ToString () {
			return this._value.ToString();
		}
		#endregion
	}
}
