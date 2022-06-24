using Newtonsoft.Json;
using System.Collections.Generic;

namespace Arex388.NhtsaVpic;

/// <summary>
/// The response base.
/// </summary>
public class ResponseBase {
    [JsonProperty("ErrorCode")]
    private string ErrorCode { get; set; }

    [JsonProperty("ErrorText")]
    internal string ErrorText { get; set; }

    /// <summary>
    /// The response error if the request failed.
    /// </summary>
    public string ResponseError {
        get {
            if (ResponseStatus == ResponseStatus.Success) {
                return null;
            }

            var errorText = ErrorText.Replace($"{ErrorCode} - ", null).Trim();

            return errorText.HasValue()
                ? errorText
                : null;
        }
    }

    /// <summary>
    /// The response JSON if debugging is enabled.
    /// </summary>
    [JsonIgnore]
    public string ResponseJson { get; set; }

    /// <summary>
    /// The response status.
    /// </summary>
    public ResponseStatus ResponseStatus { get; set; } = ResponseStatus.Success;
}