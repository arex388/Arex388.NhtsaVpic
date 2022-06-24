using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Arex388.NhtsaVpic.Tests;

public sealed class Decode {
    private readonly IConfigurationRoot _configuration;
    private readonly INhtsaVpicClient _nhtsaVpic;

    public Decode() {
        _configuration = new ConfigurationBuilder().AddUserSecrets<Decode>().Build();
        _nhtsaVpic = new NhtsaVpicClient(new HttpClient(), true);
    }

    [Fact]
    public async Task GetAsync() {
        var vin = _configuration["Vin"];
        var response = await _nhtsaVpic.DecodeAsync(vin, CancellationToken.None).ConfigureAwait(false);

        Assert.Equal("Toyota", response.Make);
        Assert.Equal("Tacoma", response.Model);
        Assert.Equal(2006, response.ModelYear!.Value);
    }
}