﻿using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using xpanse.sdk.Models;
using Xunit;
using Xunit.Abstractions;

namespace FunctionalTests
{
    public class WebhookTools : BaseTest
    {
        [Fact]
        public async Task DeserializeWebhookTransactionWithValidSignature()
        {
            const string webhookSignatureKey = "dCM6l9ngZMJXVappk73yS607k1K7byfyzTTdToaKMa8=";

            var request = new HttpRequestMessage(HttpMethod.Get, "https://webhook.com");
            request.Headers.Add("X-Xpanse-Signature", "rDYP2MxMKvvmoV2KrbOi4pnelHnVJoFYdBegvCK7IQk=");
            request.Content = new StringContent("{\"data\":{\"chargeId\":\"3f83ab8fdf624c649bc70bbba81d6c2b\",\"providerChargeId\":\"ch_3MYd2tE9mXU4onpB0r5iTsiL\",\"amount\":20,\"providerId\":\"a26c371f-94f6-40da-add2-28ec8e9da8ed\",\"paymentInformation\":{\"paymentMethodId\":\"80da8c2d674b4d2e8c65a6520e89d070\",\"card\":{\"cardNumber\":\"4111********1111\",\"expiryDate\":\"12/25\",\"type\":\"VISA\",\"cardType\":\"CREDIT\",\"cardIin\":\"411\"},\"type\":\"CARD\"},\"customerId\":\"025c73d9cd0540e9a5a997f8ba97c732\",\"status\":\"SUCCESS\",\"dateAdded\":\"2023-02-06T22:20:19.0461561Z\",\"successDate\":\"2023-02-06T22:20:20.8655832Z\",\"estimatedCost\":0.20,\"estimatedCostCurrency\":\"AUD\",\"currency\":\"Aud\",\"refunds\":[],\"threeDsVerified\":false},\"meta\":{\"messageId\":\"bc4f056315d6e0205ab085dde45c4a46\",\"timestamp\":\"2023-01-19T20:37:12.8456589Z\",\"type\":\"transaction\",\"eventType\":\"transaction.status.changed\"}}");

            var signatureHeader = request.Headers.GetValues("X-Xpanse-Signature").FirstOrDefault();
            var requestBody = await request.Content.ReadAsStringAsync();

            var transaction = xpanse.sdk.WebhookTools.DeserializeTransaction(requestBody, signatureHeader, webhookSignatureKey);
            
            Assert.NotNull(transaction);
            Assert.Equal(typeof(WebhookTransaction), transaction.GetType());
        }

        [Fact]
        public void DeserializeWebhookTransactionWithInvalidSignature()
        {
            const string webhookSignatureKey = "dCM6l9ngZMJXVappk73yS607k1K7byfyzTTdToaKMa8=";

            var ex = Assert.Throws<ArgumentException>(() => xpanse.sdk.WebhookTools.DeserializeTransaction("", "InvalidSignature", webhookSignatureKey));

            Assert.Equal("Request body is not from xpanse", ex.Message);
        }
    }
}
