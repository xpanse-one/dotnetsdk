﻿using xpanse.sdk;
using Xunit;

namespace FunctionalTests;

public class ConfigTest
{
    [Theory]
    [InlineData("", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("", Environment.DEVELOPMENT, "https://develop-api.xpanse.one")]
    [InlineData("", Environment.SANDBOX, "https://sandbox-api.xpanse.one")]
    [InlineData("", Environment.PRODUCTION, "https://api.xpanse.one")]
    [InlineData("", Environment.PROD, "https://api.xpanse.one")]
    [InlineData("secretKey1-au", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("secretKey1-jp", Environment.LOCAL, "https://localhost:5001")]
    [InlineData("secretKey2-au", Environment.DEVELOPMENT, "https://develop-api-au.xpanse.one")]
    [InlineData("secretKey2-us", Environment.DEVELOPMENT, "https://develop-api-us.xpanse.one")]
    [InlineData("secretKey2-jp", Environment.DEVELOPMENT, "https://develop-api-jp.xpanse.one")]
    [InlineData("secretKey2-au", Environment.SANDBOX, "https://sandbox-api-au.xpanse.one")]
    [InlineData("secretKey2-us", Environment.SANDBOX, "https://sandbox-api-us.xpanse.one")]
    [InlineData("secretKey2-au", Environment.PRODUCTION, "https://api-au.xpanse.one")]
    [InlineData("secretKey2-us", Environment.PRODUCTION, "https://api-us.xpanse.one")]
    [InlineData("secretKey2-aU", Environment.PRODUCTION, "https://api-au.xpanse.one")]
    [InlineData("secretKey2-jp", Environment.PRODUCTION, "https://api.xpanse.one")]
    [InlineData("secretKey2-123", Environment.PRODUCTION, "https://api.xpanse.one")]
    [InlineData("secretKey2-au", Environment.PROD, "https://api-au.xpanse.one")]
    [InlineData("secretKey2-us", Environment.PROD, "https://api-us.xpanse.one")]
    [InlineData("secretKey2-aU", Environment.PROD, "https://api-au.xpanse.one")]
    [InlineData("secretKey2-jp", Environment.PROD, "https://api.xpanse.one")]
    [InlineData("secretKey2-123", Environment.PROD, "https://api.xpanse.one")]
    public void TestSetup(string secretKey, Environment environment, string expectedBaseUrl)
    {
        // Arrange
        // Act
        Config.Setup(secretKey, environment);

        // Assert
        Assert.Equal(expectedBaseUrl, Config.BaseUrl);
    }
}
