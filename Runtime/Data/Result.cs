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
	}
}