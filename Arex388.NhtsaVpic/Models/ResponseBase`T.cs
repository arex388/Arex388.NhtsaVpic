using System.Collections.Generic;
using System.Linq;

namespace Arex388.NhtsaVpic;

public class ResponseBase<T> :
    ResponseBase {
    public IEnumerable<T> Results { get; set; } = Enumerable.Empty<T>();
}