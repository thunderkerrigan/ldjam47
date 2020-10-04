/* ***
 *  Notes : source https://www.codeproject.com/Articles/23832/Implementing-Deep-Cloning-via-Serializing-objects
 *			
 * ***/
using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Nectunia.Utility {

	/// <summary>
	/// Class to deep copy an serializable object.
	/// </summary>
	public class DeepCopy {
		/// <summary>
		/// Return a deep copy of the passed object.
		/// </summary>
		/// <typeparam name="T">Type of the object.</typeparam>
		/// <param name="source">The object to clone.</param>
		/// <returns>The clone object of Type T.</returns>
		public static T Clone<T>(T source) {
			// Check the argument compatibility
			if (!typeof(T).IsSerializable){ throw new ArgumentException("The type must be serializable.", "source"); }
 
			// Don't serialize a null object, simply return the default for that object
			if (Object.ReferenceEquals(source, null)) { return default(T); }
 
			IFormatter formatter = new BinaryFormatter();
			Stream stream = new MemoryStream();

			// Release the memory allocated to the stream when
			using (stream) {
				formatter.Serialize(stream, source);
				stream.Seek(0, SeekOrigin.Begin);
				return (T)formatter.Deserialize(stream);
			}
		}
	}
}
