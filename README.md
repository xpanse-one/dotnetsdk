# xpanse .NET SDK

Library for integrating with xpanse payments in your app. It includes the following APIs:

1. Charge API
2. Customer API
3. PaymentMethod API
4. Transfer API
5. Vault API
6. Token API
7. Provider API
8. Info API
9. Batch API

## 📄 Requirements

Use of the xpanse .NET SDK requires:

* .NET Standard 2.0/2.1 or .NET Core 2.1 or .NET 4.5/4.8

## 🧰 Installation

To use the xpanse .NET SDK in your project please do the following step:

```shell
dotnet add package XpanseSdk --version <version>
```

or 

```shell
NuGet\Install-Package XpanseSdk -Version <version>
```

## 👷 Usage

An example of creating a xpanse client with **development** environment.
FunctionalTests folder must contain **appsettings.json** file with following contents:

```json
{
  "Environment": "Development",
  "SecretKey": "XPANSE_SECRET_KEY",
  "ProviderId": "DUMMY_PROVIDER_ID",
  "Tokens": ["PAYMENT_TOKEN1","PAYMENT_TOKEN2"]
}
```
We recommend to have 8 payment tokens to make all tests passed.

## 🔨 Tests

Clone the repo locally, `cd` into the directory and run `dotnet test`
