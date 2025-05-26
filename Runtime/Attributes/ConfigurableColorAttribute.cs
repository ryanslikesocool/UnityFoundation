// Developed With Love by Ryan Boyer https://ryanjboyer.com <3

using System;
using System.Diagnostics;
using UnityEngine;

namespace Foundation {
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class ConfigurableColorAttribute : PropertyAttribute {
		public Options options;

		// MARK: - Lifecycle

		public ConfigurableColorAttribute(Options options = Options.Eyedropper) {
			this.options = options;
		}

		// MARK: - Supporting Data

		public enum Options : byte {
			Eyedropper = 1 << 0,
			Alpha = 1 << 1,
			HDR = 1 << 2,
		}
	}
}