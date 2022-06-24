using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Arex388.NhtsaVpic;

/// <summary>
/// The request base.
/// </summary>
public abstract class RequestBase {
    internal const string EndpointRoot = "https://vpic.nhtsa.dot.gov/api";

    internal abstract string Endpoint { get; }
    internal virtual IEnumerable<KeyValuePair<string, string>> FormData { get; } = Enumerable.Empty<KeyValuePair<string, string>>();
    internal abstract HttpMethod Method { get; }
}