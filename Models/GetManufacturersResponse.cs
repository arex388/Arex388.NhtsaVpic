//using Newtonsoft.Json;
//using System.Collections.Generic;

//namespace Arex388.NhtsaVpic {
//	public sealed class GetManufacturersResponse :
//		ResponseBase {
//		[JsonProperty("Mfr_CommonName")]
//		public string CommonName { get; set; }

//		public string Country { get; set; }

//		[JsonProperty("Mfr_ID")]
//		public int Id { get; set; }

//		[JsonProperty("Mfr_Name")]
//		public string Name { get; set; }
//	}

//	internal sealed class GetManufacturersResponseWrapper {
//		public IEnumerable<GetManufacturersResponse> Results { get; set; }
//	}
//}