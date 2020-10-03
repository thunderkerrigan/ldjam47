using System.Collections.Generic;
using System;
using System.Reflection;

namespace Nectunia.Utility {

	/// <summary>
	/// Extension class for Object
	/// </summary>
	public static class EObject {
		#region UnityEngine.Object _______________________________________		
		#region GetFieldsNames() ---
		/// <summary>
		/// Return a string list of all fields in the UnityEngine.Object. 
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this UnityEngine.Object obj) {
			return obj.GetFieldsNames(null, BindingFlags.Default);
		}

		/// <summary>
		/// Return a string list of all fields of the passed type in the UnityEngine.Object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this UnityEngine.Object obj, Type type) {
			return obj.GetFieldsNames(type, BindingFlags.Default);
		}
		
		/// <summary>
		/// Return a string list of all fields wich match BindingFlags in the UnityEngine.Object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this UnityEngine.Object obj, BindingFlags bindingFlags) {
			return obj.GetFieldsNames(null, bindingFlags);
		}

		/// <summary>
		/// Return a string list of all fields of the passed type and wich match BindingFlags in the UnityEngine.Object
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this UnityEngine.Object obj, Type type, BindingFlags bindingFlags) {
			List<string> result = new List<string>();
			if(obj != null) {
				FieldInfo[] fields;
				if (bindingFlags == BindingFlags.Default) { fields = obj.GetType().GetFields(); }
				else { fields = obj.GetType().GetFields(bindingFlags); }
				foreach(FieldInfo currentField in fields) {
					if ((type != null && currentField.FieldType == type) || type == null) { result.Add(currentField.Name); }
				}
			}

			return result;
		}
		#endregion

		#region GetPropertiesNames() ---
		/// <summary>
		/// Return a string list of all properties in the UnityEngine.Object. 
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this UnityEngine.Object obj) {
			return obj.GetPropertiesNames(null, BindingFlags.Default);
		}

		/// <summary>
		/// Return a string list of all properties of the passed type in the UnityEngine.Object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this UnityEngine.Object obj, Type type) {
			return obj.GetPropertiesNames(type, BindingFlags.Default);
		}
		
		/// <summary>
		/// Return a string list of all properties wich match BindingFlags in the UnityEngine.Object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this UnityEngine.Object obj, BindingFlags bindingFlags) {
			return obj.GetPropertiesNames(null, bindingFlags);
		}

		/// <summary>
		/// Return a string list of all properties of the passed type and wich match BindingFlags in the UnityEngine.Object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this UnityEngine.Object obj, Type type, BindingFlags bindingFlags) {
			List<string> result = new List<string>();
			if(obj != null) {
				PropertyInfo[] properties;
				if (bindingFlags == BindingFlags.Default) { properties = obj.GetType().GetProperties(); }
				else { properties = obj.GetType().GetProperties(bindingFlags); }
				foreach(PropertyInfo currentProperty in properties) {
					if ((type != null && currentProperty.PropertyType == type) || type == null) { result.Add(currentProperty.Name); }
				}
			}

			return result;
		}
		#endregion
		#endregion
		
		#region System.Object ______________________________________________		
		#region GetFieldsNames() ---
		/// <summary>
		/// Return a string list of all fields in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this object obj) {
			return obj.GetFieldsNames(null, BindingFlags.Default);
		}

		/// <summary>
		/// Return a string list of all fields of the passed type in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this object obj, Type type) {
			return obj.GetFieldsNames(type, BindingFlags.Default);
		}
		
		/// <summary>
		/// Return a string list of all fields wich match BindingFlags in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this object obj, BindingFlags bindingFlags) {
			return obj.GetFieldsNames(null, bindingFlags);
		}

		/// <summary>
		/// Return a string list of all fields of the passed type and wich match BindingFlags in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The fields names list.</returns>
		public static List<string> GetFieldsNames (this object obj, Type type, BindingFlags bindingFlags) {
			List<string> result = new List<string>();
			if(obj != null) {
				FieldInfo[] fields;
				if (bindingFlags == BindingFlags.Default) { fields = obj.GetType().GetFields(); }
				else { fields = obj.GetType().GetFields(bindingFlags); }
				foreach(FieldInfo currentField in fields) {
					if ((type != null && currentField.FieldType == type) || type == null) { result.Add(currentField.Name); }
				}
			}

			return result;
		}
		#endregion

		#region GetPropertiesNames() ---
		/// <summary>
		/// Return a string list of all properties in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this object obj) {
			return obj.GetPropertiesNames(null, BindingFlags.Default);
		}

		/// <summary>
		/// Return a string list of all properties of the passed type in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this object obj, Type type) {
			return obj.GetPropertiesNames(type, BindingFlags.Default);
		}
		
		/// <summary>
		/// Return a string list of all properties wich match BindingFlags in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this object obj, BindingFlags bindingFlags) {
			return obj.GetPropertiesNames(null, bindingFlags);
		}

		/// <summary>
		/// Return a string list of all properties of the passed type and wich match BindingFlags in the object.
		/// </summary>
		/// <param name="obj">The Object source.</param>
		/// <param name="type">The Type to filter on.</param>
		/// <param name="bindingFlags">List of BindingFlags to specify search criteria.</param>
		/// <returns>The properties names list.</returns>
		public static List<string> GetPropertiesNames (this object obj, Type type, BindingFlags bindingFlags) {
			List<string> result = new List<string>();
			if(obj != null) {
				PropertyInfo[] properties;
				if (bindingFlags == BindingFlags.Default) { properties = obj.GetType().GetProperties(); }
				else { properties = obj.GetType().GetProperties(bindingFlags); }
				foreach(PropertyInfo currentProperty in properties) {
					if ((type != null && currentProperty.PropertyType == type) || type == null) { result.Add(currentProperty.Name); }
				}
			}

			return result;
		}
		#endregion
		#endregion
	}
}
