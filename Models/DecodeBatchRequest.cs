using System.Collections.Generic;
using System.Net.Http;

namespace Arex388.NhtsaVpic {
	public sealed class DecodeBatchRequest :
		RequestBase {
		public override string Endpoint => "vehicles/DecodeVINValuesBatch/";
		public override IEnumerable<KeyValuePair<string, string>> FormData => new[] {
			new KeyValuePair<string, string>("DATA", Vins.StringJoin(";")),
			new KeyValuePair<string, string>("format", "JSON")
		};

		public override HttpMethod Method => HttpMethod.Post;
		public IEnumerable<string> Vins { get; set; }
	}
}