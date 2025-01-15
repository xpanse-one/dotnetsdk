﻿using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using xpanse.sdk.Models;

namespace xpanse.sdk
{
    public static class WebhookTools
    {
        private static readonly JsonSerializerSettings JsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            NullValueHandling = NullValueHandling.Ignore,
            PreserveReferencesHandling = PreserveReferencesHandling.None,
            Formatting = Formatting.Indented,
            Converters = new List<JsonConverter> { new StringEnumConverter() }
        };

        /// <summary>
        /// Deserialize webhook transaction request body from xpanse and validate that a webhook event notification came from xpanse. Requests that fail validation
        /// will be discarded as they cannot be trusted.
        /// </summary>
        /// <param name="requestBody">HTTP request body</param>
        /// <param name="signatureHeader">Webhook signature header <code>X-Xpanse-Signature</code></param>
        /// <param name="signatureKey">Webhook signature key (from dashboard)</param>
        /// <returns>Deserialized transaction message</returns>
        /// <exception cref="ArgumentException">Throw if signature validation is failed</exception>
        public static WebhookTransaction DeserializeTransaction(string requestBody, string signatureHeader, string signatureKey)
        {
            if (!IsFormXpanse(requestBody, signatureHeader, signatureKey))
            {
                throw new ArgumentException("Request body is not from xpanse");
            }

            var transaction = JsonConvert.DeserializeObject<WebhookTransaction>(requestBody, JsonSettings);

            return transaction;
        }

        private static bool IsFormXpanse(string requestBody, string signatureHeader, string signatureKey)
        {
            var requestBytes = Encoding.UTF8.GetBytes(requestBody);
            var secret = Encoding.UTF8.GetBytes(signatureKey);

            using (var hmac = new HMACSHA256(secret))
            {
                var hash = hmac.ComputeHash(requestBytes);
                var hashString = Convert.ToBase64String(hash);

                return hashString.Equals(signatureHeader);
            }
        }
    }
}