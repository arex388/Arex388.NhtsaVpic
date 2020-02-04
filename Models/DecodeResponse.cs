using System.Collections.Generic;
using Humanizer;

namespace Arex388.NhtsaVpic {
	public sealed class DecodeResponse :
		ResponseBase {
		//	ABS
		//	ActiveSafetySysNote
		//	AdaptiveCruiseControl
		//	AdaptiveDrivingBeam
		//	AdaptiveHeadlights
		//	AdditionalErrorText
		//	AirBagLocCurtain
		//	AirBagLocFront
		//	AirBagLocKnee
		//	AirBagLocSeatCushion
		//	AirBagLocSide
		//	AutoReverseSystem
		//	AutomaticPedestrianAlertingSound
		//	AxleConfiguration
		//	Axles
		//	BasePrice
		//	BatteryA
		//	BatteryA_to
		//	BatteryCells
		//	BatteryInfo
		//	BatteryKWh
		//	BatteryKWh_to
		//	BatteryModules
		//	BatteryPacks
		//	BatteryType
		//	BatteryV
		//	BatteryV_to
		//	BedLengthIN
		//	BedType
		//	BlindSpotMon
		//	BodyCabType
		//	BodyClass
		//	BrakeSystemDesc
		//	BrakeSystemType
		//	BusFloorConfigType
		//	BusLength
		//	BusType
		//	CAN_AACN
		//	CIB
		//	CashForClunkers
		//	ChargerLevel
		//	ChargerPowerKW
		//	CoolingType
		//	CurbWeightLB
		//	CustomMotorcycleType
		//	DaytimeRunningLight
		//	DestinationMarket
		//	DisplacementCC
		//	DisplacementCI
		//	DisplacementL
		//	Doors
		//	DriveType
		//	DriverAssist
		//	DynamicBrakeSupport
		//	EDR
		//	ESC
		//	EVDriveUnit
		//	ElectrificationLevel
		//	EngineConfiguration
		//	EngineCycles
		//	EngineCylinders
		//	EngineHP
		//	EngineHP_to
		//	EngineKW
		//	EngineManufacturer
		//	EngineModel
		//	EntertainmentSystem
		//	ForwardCollisionWarning
		//	FuelInjectionType
		//	FuelTypePrimary
		//	FuelTypeSecondary
		//	GCWR
		//	GCWR_to
		//	GVWR
		//	GVWR_to
		//	KeylessIgnition
		//	LaneDepartureWarning
		//	LaneKeepSystem
		//	LowerBeamHeadlampLightSource

		private string _make;

		public string Make {
			get => _make;
			set => _make = value.Transform(To.TitleCase);
		}

		//	Manufacturer
		//	ManufacturerId

		private string _model;

		public string Model {
			get => _model;
			set => _model = value.Transform(To.TitleCase);
		}
		public short ModelYear { get; set; }

		//	MotorcycleChassisType
		//	MotorcycleSuspensionType
		//	NCSABodyType
		//	NCSAMake
		//	NCSAMapExcApprovedBy
		//	NCSAMapExcApprovedOn
		//	NCSAMappingException
		//	NCSAModel
		//	NCSANote
		//	Note
		//	OtherBusInfo
		//	OtherEngineInfo
		//	OtherMotorcycleInfo
		//	OtherRestraintSystemInfo
		//	OtherTrailerInfo
		//	ParkAssist
		//	PedestrianAutomaticEmergencyBraking
		//	PlantCity
		//	PlantCompanyName
		//	PlantCountry
		//	PlantState
		//	PossibleValues
		//	Pretensioner
		//	RearCrossTrafficAlert
		//	RearVisibilitySystem
		//	SAEAutomationLevel
		//	SAEAutomationLevel_to
		//	SeatBeltsAll
		//	SeatRows
		//	Seats
		//	SemiautomaticHeadlampBeamSwitching
		//	Series
		//	Series2
		//	SteeringLocation
		//	SuggestedVIN
		//	TPMS
		//	TopSpeedMPH
		//	TrackWidth
		//	TractionControl
		//	TrailerBodyType
		//	TrailerLength
		//	TrailerType
		//	TransmissionSpeeds
		//	TransmissionStyle
		//	Trim
		//	Trim2
		//	Turbo
		//	VIN
		//	ValveTrainDesign
		//	VehicleType
		//	WheelBaseLong
		//	WheelBaseShort
		//	WheelBaseType
		//	WheelSizeFront
		//	WheelSizeRear
		//	Wheels
		//	Windows
	}

	internal sealed class DecodeResponseWrapper {
		public IEnumerable<DecodeResponse> Results { get; set; }
	}
}