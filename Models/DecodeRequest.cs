namespace Arex388.NhtsaVpic {
	public sealed class DecodeRequest :
		RequestBase {
		public override string Endpoint => $"vehicles/DecodeVinValuesExtended/{Vin}?format=json";
		public string Vin { get; set; }
	}
}