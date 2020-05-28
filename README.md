# Arex388.NhtsaVpic

A VIN decoding library using the National Highway Traffic Safety Administration (NHTSA) vPIC API.

> **NOTE:** The API is only minimally implemented because the project that spawned this library does not have a need for anything but the year, make and model of a vehicle. I will revisit and complete this library in the future, I just wanted to separate it out as a NuGet package for now.

To use create a new instance of `NhtsaVpicClient` and pass in an instance of `HttpClient`. The original API documentation can be found [here](https://vpic.nhtsa.dot.gov/api/).

Available as a NuGet package [here]().

```c#
var nhtsaVpic = new NhtsaVpicClient(
    httpClient,
    //	debug = true/false
);
```

#### Decode

```c#
var response = await nhtsaVpic.DecodeAsync(
    "VIN"
);
```

#### Decode Batch

```C#
var response = await nhtsaVpic.DecodeBatchAsync(
    "VIN1",
    "VIN2",
    ...
);
```

