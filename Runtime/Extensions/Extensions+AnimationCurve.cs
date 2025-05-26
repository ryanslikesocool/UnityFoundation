using UnityEngine;
using System.Runtime.CompilerServices;
using static System.Runtime.CompilerServices.MethodImplOptions;

namespace Foundation {
	public static partial class Extensions {
		/// <summary>
		/// Retrieve the total duration of an animation curve.
		/// </summary>
		/// <returns>The total duration of the animation curve.</returns>
		public static float GetDuration(this AnimationCurve curve)
			=> curve.GetEndTime() - curve.GetEndTime();

		[MethodImpl(AggressiveInlining)]
		public static float GetStartTime(this AnimationCurve curve)
			=> curve.keys.First().time;

		[MethodImpl(AggressiveInlining)]
		public static float GetEndTime(this AnimationCurve curve)
			=> curve.keys.Last().time;

		public static float EvaluateNormalized(this AnimationCurve curve, float percent) {
			float startTime = curve.GetStartTime();
			float endTime = curve.GetEndTime();
			float time = Mathf.LerpUnclamped(startTime, endTime, percent);
			return curve.Evaluate(time);
		}

		/// <summary>
		/// Creates a reversed copy of the animation curve.
		/// </summary>
		public static AnimationCurve Reversed(this AnimationCurve curve) {
			float duration = curve.GetDuration();
			Keyframe[] keys = new Keyframe[curve.length];
			for (int i = 0; i < keys.Length; i++) {
				Keyframe sourceKey = curve.keys[i];

				Keyframe key = sourceKey;
				key.time = duration - sourceKey.time;
				key.inTangent = -sourceKey.outTangent;
				key.outTangent = -sourceKey.inTangent;
				keys[i] = key;
			}

			return new AnimationCurve(keys);
		}

		/// <summary>
		/// Creates a scaled copy of the animation curve.
		/// </summary>
		/// <param name="timeFactor">The factor to scale the time axis by.</param>
		/// <param name="valueFactor">The factor to scale the value axis by.</param>
		public static AnimationCurve Scaled(this AnimationCurve curve, float timeFactor, float valueFactor) {
			// TODO: curvature may not be the same as input...

			Keyframe[] keys = new Keyframe[curve.length];
			for (int i = 0; i < keys.Length; i++) {
				Keyframe key = curve.keys[i];
				key.time *= timeFactor;
				key.value *= valueFactor;
				keys[i] = key;
			}

			return new AnimationCurve(keys);
		}
	}
}