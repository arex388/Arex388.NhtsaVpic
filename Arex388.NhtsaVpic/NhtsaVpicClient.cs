using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.NhtsaVpic;

/// <summary>
/// NHTSA vPIC API client.
/// </summary>
public sealed class NhtsaVpicClient :
    INhtsaVpicClient {
    private readonly bool _debug;
    private readonly HttpClient _httpClient;

    /// <summary>
    /// Create an instance of the NHTSA vPIC API client.
    /// </summary>
    /// <param name="httpClient">An instance of HttpClient.</param>
    /// <param name="debug">Toggle response debugging. Disabled by default.</param>
    public NhtsaVpicClient(
        HttpClient httpClient,
        bool debug = false) {
        _debug = debug;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    //  ============================================================================
    //  Actions
    //  ============================================================================

    /// <summary>
    /// Decode a VIN.
    /// </summary>
    /// <param name="vin">The VIN to decode.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>DecodeRequest</returns>
    public async Task<DecodeResponse> DecodeAsync(
        string vin,
        CancellationToken cancellationToken) => await DecodeAsync(new DecodeRequest {
            Vin = vin
        }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Decode a VIN.
    /// </summary>
    /// <param name="request">The DecodeRequest instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>DecodeRequest</returns>
    public async Task<DecodeResponse> DecodeAsync(
        DecodeRequest request,
        CancellationToken cancellationToken) {
        if (request is null) {
            return InvalidResponse<DecodeResponse>();
        }

        return cancellationToken.IsCancellationRequested
            ? CancelledResponse<DecodeResponse>()
            : (await RequestAsync<ResponseBase<DecodeResponse>>(request, cancellationToken).ConfigureAwait(false)).Results.FirstOrDefault();
    }

    /// <summary>
    /// Decode a batch of VINs.
    /// </summary>
    /// <param name="vins">The VINs to decode.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>IEnumerable&lt;DecodeResponse&gt;</returns>
    public async Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
        IEnumerable<string> vins,
        CancellationToken cancellationToken) => await DecodeBatchAsync(new DecodeBatchRequest {
            Vins = vins
        }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Decode a batch of VINs.
    /// </summary>
    /// <param name="request">The DecodeBatchRequest instance.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>IEnumerable&lt;DecodeResponse&gt;</returns>
    public async Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
        DecodeBatchRequest request,
        CancellationToken cancellationToken) {
        if (request is null) {
            return new[] {
                InvalidResponse<DecodeResponse>()
            };
        }

        if (cancellationToken.IsCancellationRequested) {
            return new[] {
                CancelledResponse<DecodeResponse>()
            };
        }

        return (await RequestAsync<ResponseBase<DecodeResponse>>(request, cancellationToken).ConfigureAwait(false)).Results;
    }

    //	============================================================================
    //  Response
    //  ============================================================================

    private T Deserialize<T>(
        ResponseResult result)
        where T : ResponseBase, new() {
        var json = result.Json;

        if (!json.HasValue()) {
            return FailureResponse<T>();
        }

        var response = JsonConvert.DeserializeObject<T>(json);

        if (response is null) {
            return FailureResponse<T>();
        }

        response.ResponseJson = _debug
            ? json
            : null;
        response.ResponseStatus = result.Success
            ? ResponseStatus.Success
            : ResponseStatus.Failure;

        return response;
    }

    private async Task<HttpResponseMessage> GetAsync(
        RequestBase request,
        CancellationToken cancellationToken) => await _httpClient.GetAsync(request.Endpoint, cancellationToken).ConfigureAwait(false);

    private async Task<HttpResponseMessage> PostAsync(
        RequestBase request,
        CancellationToken cancellationToken) {
        using var content = new FormUrlEncodedContent(request.FormData);

        return await _httpClient.PostAsync(request.Endpoint, content, cancellationToken).ConfigureAwait(false);
    }

    private async Task<T> RequestAsync<T>(
        RequestBase request,
        CancellationToken cancellationToken)
        where T : ResponseBase, new() {
        try {
            if (cancellationToken.IsCancellationRequested) {
                return CancelledResponse<T>();
            }

            var response = request.Method == HttpMethod.Get
                ? await GetAsync(request, cancellationToken).ConfigureAwait(false)
                : await PostAsync(request, cancellationToken).ConfigureAwait(false);

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var success = response.StatusCode == HttpStatusCode.OK;

            return Deserialize<T>(new ResponseResult(json, success));
        } catch (HttpRequestException) {
            return FailureResponse<T>();
        } catch (TaskCanceledException) {
            return TimeOutResponse<T>();
        }
    }

    //  ============================================================================
    //  Utilities
    //  ============================================================================

    private static T CancelledResponse<T>()
        where T : ResponseBase, new() => new() {
            ErrorText = "The request was cancelled.",
            ResponseStatus = ResponseStatus.Cancelled
        };

    private static T FailureResponse<T>()
        where T : ResponseBase, new() => new() {
            ErrorText = "The request failed.",
            ResponseStatus = ResponseStatus.Failure
        };

    private static T InvalidResponse<T>()
        where T : ResponseBase, new() => new() {
            ErrorText = "The request is invalid.",
            ResponseStatus = ResponseStatus.Invalid
        };

    private static T TimeOutResponse<T>()
        where T : ResponseBase, new() => new() {
            ErrorText = "The request timed out.",
            ResponseStatus = ResponseStatus.TimeOut
        };
}