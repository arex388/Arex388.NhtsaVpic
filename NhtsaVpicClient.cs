using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Arex388.NhtsaVpic {
	public sealed class NhtsaVpicClient {
		private HttpClient HttpClient { get; }

		public NhtsaVpicClient(
			HttpClient httpClient) => HttpClient = httpClient;

		public Task<DecodeResponse> GetDecodeAsync(
			string vin) => GetDecodeAsync(new DecodeRequest {
				Vin = vin
			});

		public async Task<DecodeResponse> GetDecodeAsync(
			DecodeRequest request) {
			if (request is null) {
				return ResponseBase.Invalid<DecodeResponse>();
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<DecodeResponseWrapper>(response).Results.SingleOrDefault();
		}

		public Task<IEnumerable<DecodeResponse>> GetDecodeBatchAsync(
			IEnumerable<string> vins) => GetDecodeBatchAsync(new DecodeBatchRequest {
				Vins = vins
			});

		public async Task<IEnumerable<DecodeResponse>> GetDecodeBatchAsync(
			DecodeBatchRequest request) {
			if (request is null) {
				return new[] {
					ResponseBase.Invalid<DecodeResponse>()
				};
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<DecodeResponseWrapper>(response).Results;
		}

		//public async Task<object> GetMakes() {
		//	var response = await GetResponseAsync(new GetMakesRequest());

		//	Console.Write(response);

		//	return null;
		//}

		//public async Task<IEnumerable<GetManufacturersResponse>> GetManufacturers() {
		//	var response = await GetResponseAsync(new GetManufacturersRequest());

		//	return JsonConvert.DeserializeObject<GetManufacturersResponseWrapper>(response).Results;
		//}

		//	========================================================================

		private async Task<string> GetResponseAsync(
			RequestBase request) {
			var endpoint = $"https://vpic.nhtsa.dot.gov/api/{request.Endpoint}";

			try {
				if (request.Method == HttpMethod.Get) {
					return await GetGetResponseAsync(endpoint);
				}

				return await GetPostResponseAsync(request, endpoint);
			} catch (HttpRequestException) {
				return null;
			}
		}

		private async Task<string> GetGetResponseAsync(
			string endpoint) {
			var response = await HttpClient.GetAsync(endpoint);

#if DEBUG
			var responseContent = await response.Content.ReadAsStringAsync();

			Console.Write(responseContent);

			return responseContent;
#else
			return await response.Content.ReadAsStringAsync();
#endif
		}

		private async Task<string> GetPostResponseAsync(
			RequestBase request,
			string endpoint) {
			using var content = new FormUrlEncodedContent(request.FormData);
			using var response = await HttpClient.PostAsync(endpoint, content);

#if DEBUG
			var responseContent = await response.Content.ReadAsStringAsync();

			Console.Write(responseContent);

			return responseContent;
#else
			return await response.Content.ReadAsStringAsync();
#endif
		}
	}
}