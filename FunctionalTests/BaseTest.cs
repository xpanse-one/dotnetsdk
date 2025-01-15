using System;
using System.Linq;
using Microsoft.Extensions.Configuration;
using xpanse.sdk;
using Environment = xpanse.sdk.Environment;

namespace FunctionalTests;

public class BaseTest
{
    private readonly IConfiguration _configuration;
    public static int TokenNum = 0;
        
    public BaseTest()
    {
        _configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables() 
            .Build();

        Enum.TryParse(_configuration["Environment"]?.ToUpper() ?? "DEVELOPMENT", out Environment env);
        Config.Setup(_configuration["SecretKey"], env);
    }
    
    protected string GetProviderId()
    {
        return _configuration["ProviderId"];
    }
    
    protected string GetPaymentToken()
    {
        var tokens = _configuration.GetSection("Tokens").AsEnumerable().ToArray();
        TokenNum++;
        if (tokens.Count() <= TokenNum)
            return "";

        return tokens[TokenNum].Value;
    }
}