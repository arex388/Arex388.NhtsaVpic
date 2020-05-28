using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arex388.NhtsaVpic {
    public class ResponseBase {
        [JsonProperty("ErrorCode")]
        private string ErrorCode { get; set; }

        [JsonProperty("ErrorText")]
        private string ErrorText { get; set; }

        /// <summary>
        /// The error message if the response failed.
        /// </summary>
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

        /// <summary>
        /// The raw JSON of the response for debugging.
        /// </summary>
        [JsonIgnore]
        public string Json { get; set; }

        /// <summary>
        /// Was the request successful or not.
        /// </summary>
        public bool Success => ErrorCode == "0";

        /// <summary>
        /// An invalid response instance with an optinal error message.
        /// </summary>
        /// <typeparam name="T">The response type.</typeparam>
        /// <param name="error">The error message (optional).</param>
        /// <returns>T</returns>
        public static T Invalid<T>(
            string error = null)
            where T : ResponseBase, new() => new T {
                ErrorCode = "2147483647",
                ErrorText = error ?? "The request is invalid."
            };
    }
}