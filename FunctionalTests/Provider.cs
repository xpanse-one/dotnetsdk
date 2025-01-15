using System;
using System.Collections.Generic;
using xpanse.sdk;
using xpanse.sdk.Models;
using Xunit;
using Xunit.Abstractions;
using Environment = xpanse.sdk.Environment;

namespace FunctionalTests;

public class Provider : BaseTest
{
    [Fact]
    public void CreateProvider()
    {
        var provider = CreateNewProvider();

        var svc = new xpanse.sdk.Provider();
        var result = svc.Create(provider);

        Assert.NotNull(result.ProviderId);
    }

    [Fact]
    public void UpdateProvider()
    {
        var provider = CreateNewProvider();

        var svc = new xpanse.sdk.Provider();
        var result = svc.Create(provider);

        var newName = Guid.NewGuid().ToString();
        var updateProvider = new UpdateProvider()
        {
            Name = newName,
            Currency = "AUD",
            AuthenticationParameters = new Dictionary<string, string>()
            {
                ["MinMilliseconds"] = "1",
                ["MaxMilliseconds"] = "10"
            }
        };

        result = svc.Update(result.ProviderId, updateProvider);

        Assert.Equal(result.Name, newName);
    }

    [Fact]
    public void DeleteProvider()
    {
        var provider = CreateNewProvider();

        var svc = new xpanse.sdk.Provider();
        var result = svc.Create(provider);

        var deletedProvider = svc.Delete(result.ProviderId);

        Assert.Equal(result.ProviderId, deletedProvider.ProviderId);
    }

    private NewProvider CreateNewProvider()
    {
        return new NewProvider
        {
            Type = "dummy",
            Name = Guid.NewGuid().ToString(),
            Environment = "SANDBOX",
            Currency = "AUD",
            AuthenticationParameters = new Dictionary<string, string>()
            {
                ["MinMilliseconds"] = "1",
                ["MaxMilliseconds"] = "10"
            }
        };
    }
}
