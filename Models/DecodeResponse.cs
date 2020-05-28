using Humanizer;
using System.Collections.Generic;

namespace Arex388.NhtsaVpic {
    public sealed class DecodeResponse :
        ResponseBase {
        private string _make;
        private string _model;

        /// <summary>
        /// The vehicle's make.
        /// </summary>
        public string Make {
            get => _make;
            set => _make = value.Transform(To.TitleCase);
        }

        /// <summary>
        /// The vehicle's model.
        /// </summary>
        public string Model {
            get => _model;
            set => _model = value.Transform(To.TitleCase);
        }

        /// <summary>
        /// The vehicle's year.
        /// </summary>
        public short? ModelYear { get; set; }
    }

    internal sealed class DecodeResponseWrapper {
        public IEnumerable<DecodeResponse> Results { get; set; }
    }
}