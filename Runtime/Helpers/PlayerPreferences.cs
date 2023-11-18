using UnityEditor;
using UnityEngine;

namespace Foundation {
	public sealed class PlayerPreferences {
		public static String strings { get; set; }
		public static Int ints { get; set; }
		public static Float floats { get; set; }

		[InitializeOnEnterPlayMode]
		private static void Initialize() {
			strings = new String();
			ints = new Int();
			floats = new Float();
		}

		public sealed class String {
			public string this[in string key] {
				get => Get(key);
				set => Set(key, value);
			}

			internal String() { }

			public string Get(in string key)
				=> PlayerPrefs.GetString(key);

			public string Get(in string key, in string defaultValue)
				=> PlayerPrefs.GetString(key, defaultValue);

			public void Set(in string key, in string value)
				=> PlayerPrefs.SetString(key, value);
		}

		public sealed class Int {
			public int this[in string key] {
				get => Get(key);
				set => Set(key, value);
			}

			internal Int() { }

			public int Get(in string key)
				=> PlayerPrefs.GetInt(key);

			public int Get(in string key, int defaultValue)
				=> PlayerPrefs.GetInt(key, defaultValue);

			public void Set(in string key, int value)
				=> PlayerPrefs.SetInt(key, value);
		}

		public sealed class Float {
			public float this[in string key] {
				get => Get(key);
				set => Set(key, value);
			}

			internal Float() { }

			public float Get(in string key)
				=> PlayerPrefs.GetFloat(key);

			public float Get(in string key, float defaultValue)
				=> PlayerPrefs.GetFloat(key, defaultValue);

			public void Set(in string key, float value)
				=> PlayerPrefs.SetFloat(key, value);
		}

		public static void DeleteAll()
			=> PlayerPrefs.DeleteAll();

		public static void Delete(in string key)
			=> PlayerPrefs.DeleteKey(key);
	}
}