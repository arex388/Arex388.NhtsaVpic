using System.Collections.Generic;
using System.Net.Http;

namespace Arex388.NhtsaVpic {
	public abstract class RequestBase {
		public abstract string Endpoint { get; }
		public virtual IEnumerable<KeyValuePair<string, string>> FormData { get; }
		public virtual HttpMethod Method { get; } = HttpMethod.Get;
	}
}