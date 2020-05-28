using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arex388.NhtsaVpic {
    public sealed class NhtsaVpicClient {
        /// <summary>
        /// Is debugging enabled.
        /// </summary>
        private readonly bool _debug;

        /// <summary>
        /// An instance of HttpClient.
        /// </summary>
        private readonly HttpClient _httpClient;

        /// <summary>
        /// NHTSA vPIC API client.
        /// </summary>
        /// <param name="httpClient">An instance of HttpClient.</param>
        /// <param name="debug">Toggle capturing the raw JSON response from NHTSA vPIC and returning it as part of the deserialized response object.</param>
        public NhtsaVpicClient(
            HttpClient httpClient,
            bool debug = false) {
            _debug = debug;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Decode a VIN.
        /// </summary>
        /// <param name="vin">The VIN to decode.</param>
        /// <returns>A DecodeResponse.</returns>
        public Task<DecodeResponse> DecodeAsync(
            string vin) => DecodeAsync(new DecodeRequest {
                Vin = vin
            });

        /// <summary>
        /// Decode a VIN.
        /// </summary>
        /// <param name="request">A DecodeRequest instance.</param>
        /// <returns>A DecodeResponse.</returns>
        public async Task<DecodeResponse> DecodeAsync(
            DecodeRequest request) {
            if (request is null) {
                return NullRequestResponse<DecodeResponse>();
            }

            var response = await GetResponseAsync(request).ConfigureAwait(false);
            var responseObj = JsonConvert.DeserializeObject<DecodeResponseWrapper>(response).Results.Single();

            if (_debug) {
                responseObj.Json = response;
            }

            return responseObj;
        }

        /// <summary>
        /// Decode a batch of VINs.
        /// </summary>
        /// <param name="vins">The VINs to decode.</param>
        /// <returns>A DecodeResponse collection.</returns>
        public Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
            IEnumerable<string> vins) => DecodeBatchAsync(new DecodeBatchRequest {
                Vins = vins
            });

        /// <summary>
        /// Decode a batch of VINs.
        /// </summary>
        /// <param name="request">A DecodeBatchRequest instance.</param>
        /// <returns>A DecodeResponse collection.</returns>
        public async Task<IEnumerable<DecodeResponse>> DecodeBatchAsync(
            DecodeBatchRequest request) {
            if (request is null) {
                return new[] {
                    NullRequestResponse<DecodeResponse>()
                };
            }

            var response = await GetResponseAsync(request).ConfigureAwait(false);
            var responseObjs = JsonConvert.DeserializeObject<DecodeResponseWrapper>(response).Results.ToList();

            if (_debug) {
                foreach (var responseObj in responseObjs) {
                    responseObj.Json = response;
                }
            }

            return responseObjs;
        }

        //	========================================================================
        //  Response
        //  ========================================================================

        private async Task<string> GetResponseAsync(
            RequestBase request) {
            var endpoint = $"https://vpic.nhtsa.dot.gov/api/{request.Endpoint}";

            try {
                if (request.Method == HttpMethod.Get) {
                    return await GetGetResponseAsync(endpoint).ConfigureAwait(false);
                }

                return await GetPostResponseAsync(request, endpoint).ConfigureAwait(false);
            } catch (HttpRequestException e) {
                var error = $"{e.Message}\n{e.InnerException?.Message}".Trim();

                return $"{{\"ErrorCode\":\"2147483647\",\"ErrorText\":\"{error}\"}}";
            } catch (TaskCanceledException) {
                return "{{\"ErrorCode\":\"2147483647\",\"ErrorText\":\"The request has timed out.\"}}";
            }
        }

        private async Task<string> GetGetResponseAsync(
            string endpoint) {
            var response = await _httpClient.GetAsync(endpoint).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        private async Task<string> GetPostResponseAsync(
            RequestBase request,
            string endpoint) {
            using var content = new FormUrlEncodedContent(request.FormData);
            using var response = await _httpClient.PostAsync(endpoint, content).ConfigureAwait(false);

            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }

        //  ========================================================================
        //  Utilities
        //  ========================================================================

        /// <summary>
        /// A failure due to a null request.
        /// </summary>
        /// <typeparam name="T">The response type.</typeparam>
        /// <returns>T</returns>
        private static T NullRequestResponse<T>()
            where T : ResponseBase, new() => ResponseBase.Invalid<T>();
    }
}