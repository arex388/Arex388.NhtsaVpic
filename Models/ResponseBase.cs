using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arex388.NhtsaVpic {
	public class ResponseBase {
		private const int ZeroErrorCode = 0;

		[JsonProperty("ErrorCode")]
		private int ErrorCode { get; set; }

		[JsonProperty("ErrorText")]
		private string ErrorText { get; set; }

		public string Error {
			get {
				if (Success) {
					return null;
				}

				var errorText = ErrorText.Replace($"{ErrorCode} - ", null).Trim();

				if (!errorText.HasValue()) {
					return null;
				}

				return errorText;
			}
		}

		public bool Success => ErrorCode == ZeroErrorCode;

		public static T Invalid<T>()
			where T : ResponseBase, new() => new T {
				ErrorCode = int.MaxValue,
				ErrorText = "The request is invalid."
			};
	}
}