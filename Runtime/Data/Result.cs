using System;

namespace Foundation {
	public readonly struct Result<Success, Failure> where Failure : Exception {
		public readonly Success success;
		public readonly Failure failure;

		public Result(Success success) {
			this.success = success;
			this.failure = default;
		}

		public Result(Failure failure) {
			this.success = default;
			this.failure = failure;
		}

		// MARK: - Overrides

		public override readonly string ToString()
			=> string.Format(STRING_FORMAT, typeof(Success), typeof(Failure), success, failure);

		// MARK: - Constants

		private const string STRING_FORMAT = "Result<{0}, {1}>(success: {2}, failure: {3})";
	}
}