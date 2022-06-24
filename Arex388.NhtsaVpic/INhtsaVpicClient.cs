using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.NhtsaVpic;

/// <summary>
/// NHTSA vPIC API client.
/// </summary>
public interface INhtsaVpicClient {
    /// <summary>
    /// Decode a VIN.
    /// </summary>
    /// <param name="vin">The VIN to decode.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>DecodeRequest</returns>
    Task<DecodeResponse> DecodeAsync(
        string vin,
        CancellationToken cancellationToken);

    /// <summary>
    /// Decode a VIN.
    /// </summary>
    /// <param name="request">The DecodeRequest instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>DecodeResponse</returns>
    Task<DecodeResponse> DecodeAsync(
        DecodeRequest request,
        CancellationToken cancellationToken);

    /// <summary>
    /// Decode a batch of VINs.
    /// </summary>
    /// <param name="vins">The VINs to decode.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>IEnumerable&lt;DecodeResponse&gt;</returns>
    Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
        IEnumerable<string> vins,
        CancellationToken cancellationToken);

    /// <summary>
    /// Decode a batch of VINs.
    /// </summary>
    /// <param name="request">The DecodeBatchRequest instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>IEnumerable&lt;DecodeResponse&gt;</returns>
    Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
        DecodeBatchRequest request,
        CancellationToken cancellationToken);
}