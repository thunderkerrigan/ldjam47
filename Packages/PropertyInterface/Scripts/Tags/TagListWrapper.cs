using System;
using System.Collections.Generic;
using UnityEngine;

namespace Nectunia.PropertyInterface {

	/// <summary>
	/// Wrapper for Tag list to allow Easiest serialisation.
	/// </summary>
	/// <remarks>
	/// Used for JSON export from Unity Windows menu.
	/// </remarks>
	[Serializable]
	public class TagListWrapper {

		/// <summary>
		/// The Tag list.
		/// </summary>
		[SerializeField]
		public List<Tag> _tagsList = new List<Tag>();

		/// <summary>
		/// Instanciate the wrapper.
		/// </summary>
		/// <param name="tasgList">The Tag list to instanciate.</param>
		public TagListWrapper (List<Tag> tasgList) {
			this._tagsList = tasgList;
		}
	}
}
