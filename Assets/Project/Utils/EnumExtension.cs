using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace Project.Utils {
	public static class EnumExtension {
		/// <summary>
		/// HasFlag()
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum value (flag)</param>
		/// <returns>Enum value cast to int</returns>
		public static bool Contains<T>(this T container, T value) where T : Enum => container.HasFlag(value);

		/// <summary>
		/// Set flag value(s) 1
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum flag value(s)</param>
		/// <returns>Enum value cast to int</returns>
		public static T On<T>(this ref T container, T value) where T : struct, Enum {
			if (Enum.GetUnderlyingType(typeof(T)) == typeof(int)) {
				container = (T) (object) ((int) (object) container | (int) (object) value);
			} else { container = (T) (object) ((long) (object) container | (long) (object) value); }
			return container;
		}

		/// <summary>
		/// Set flag value(s) 1 and set other values 0
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum flag value(s)</param>
		/// <returns>Enum value cast to int</returns>
		public static T OnOnly<T>(this ref T container, T value) where T : struct, Enum {
			container.OffAll();
			return container.On(value);
		}

		/// <summary>
		/// Set all flag values 1
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <returns>Enum value cast to int</returns>
		public static T OnAll<T>(this ref T container) where T : struct, Enum {
			if (Enum.GetUnderlyingType(typeof(T)) == typeof(int)) { container = (T) (object) ~(int) 0; } else {
				container = (T) (object) ~(long) 0;
			}
			return container;
		}

		/// <summary>
		/// Set flag value(s) 0
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum flag value(s)</param>
		/// <returns>Enum value cast to int</returns>
		public static T Off<T>(this ref T container, T value) where T : struct, Enum {
			if (Enum.GetUnderlyingType(typeof(T)) == typeof(int)) {
				container = (T) (object) ((int) (object) container & ~(int) (object) value);
			} else { container = (T) (object) ((long) (object) container & ~(long) (object) value); }
			return container;
		}

		/// <summary>
		/// Set all flag values 0
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <returns>Enum value cast to int</returns>
		public static T OffAll<T>(this ref T container) where T : struct, Enum {
			if (Enum.GetUnderlyingType(typeof(T)) == typeof(int)) { container = (T) (object) (int) 0; } else {
				container = (T) (object) (long) 0;
			}
			return container;
		}

		/// <summary>
		/// Get flag value(s). Alternative HasFlag()
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum flag value(s)</param>
		/// <returns>Enum value cast to int</returns>
		public static bool Get<T>(this T container, T value) where T : struct, Enum => container.Contains(value);

		/// <summary>
		/// Set indicated value to flag value(s)
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <param name="value">Enum flag value(s)</param>
		/// <returns>Enum value cast to int</returns>
		public static T Set<T>(this ref T container, T value, bool state) where T : unmanaged, Enum {
			container = state ? container.On(value) : container.Off(value);
			return container;
		}

		/// <summary>
		/// Cast to int
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <returns>Enum value cast to int</returns>
		public static int TI<T>(this T container) where T : Enum => (int) (object) container;

		/// <summary>
		/// Cast to long
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <returns>Enum value cast to long</returns>
		public static long TL<T>(this T container) where T : Enum => (long) (object) container;

		/// <summary>
		/// ToString()
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="container">Enum ref</param>
		/// <returns>Enum value to string</returns>
		public static string TS<T>(this T container) where T : Enum => container.ToString();

		/// <summary>
		/// Get description of enum value
		/// </summary>
		/// <typeparam name="T">Enum type</typeparam>
		/// <param name="e">Enum ref</param>
		/// <returns>Return description attribute value.</returns>
		public static string GetDescription<T>(this T e) where T : Enum, IConvertible {
			var type = e.GetType();
			var values = Enum.GetValues(type);

			foreach (int val in values) {
				if (val != e.ToInt32(CultureInfo.InvariantCulture)) { continue; }
				var memInfo = type.GetMember(((T) (object) val).ToString());
				var first = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault();
				if (first == null) { return string.Empty; }
				if (first.GetType() == typeof(DescriptionAttribute)) { return ((DescriptionAttribute) first).Description; }
			}
			return string.Empty;
		}
	}
}