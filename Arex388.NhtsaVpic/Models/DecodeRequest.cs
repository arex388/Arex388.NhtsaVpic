using System.Net.Http;

namespace Arex388.NhtsaVpic;

/// <summary>
/// The decode request.
/// </summary>
public sealed class DecodeRequest :
    RequestBase {
    internal override string Endpoint => $"{EndpointRoot}/vehicles/DecodeVinValuesExtended/{Vin}?format=json";
    internal override HttpMethod Method => HttpMethod.Get;

    //	============================================================================

    /// <summary>
    /// The vehicle identification number to decode.
    /// </summary>
    public string Vin { get; set; }
}