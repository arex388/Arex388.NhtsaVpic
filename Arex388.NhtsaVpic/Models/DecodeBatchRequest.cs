using System.Collections.Generic;
using System.Net.Http;

namespace Arex388.NhtsaVpic;

/// <summary>
/// The batch decode request.
/// </summary>
public sealed class DecodeBatchRequest :
    RequestBase {
    internal override string Endpoint => $"{EndpointRoot}/vehicles/DecodeVINValuesBatch/";
    internal override IEnumerable<KeyValuePair<string, string>> FormData => new[] {
        new KeyValuePair<string, string>("DATA", Vins.StringJoin(";")),
        new KeyValuePair<string, string>("format", "JSON")
    };
    internal override HttpMethod Method => HttpMethod.Post;

    //	============================================================================

    /// <summary>
    /// The vehicle identification numbers to decode.
    /// </summary>
    public IEnumerable<string> Vins { get; set; }
}