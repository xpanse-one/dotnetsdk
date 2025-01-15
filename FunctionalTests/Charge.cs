﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using xpanse.sdk.Models;
using Xunit;

namespace FunctionalTests
{
    public class Charge : BaseTest
    {
        private const string SuccessResponseValue = "SUCCESS";
        private const string PendingResponseValue = "PENDING";

        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "02/32",
            Ccv = "987",
            Cardholder = "John Smith"
        };

        private NewChargeCard GetChargeData()
        {
            return new NewChargeCard
            {
                Amount = 20,
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }
        
        private NewChargeBankPayment GetChargeDataWithBankAccount()
        {
            return new NewChargeBankPayment
            {
                Amount = 20,
                Currency = "AUD",
                ProviderId = GetProviderId(),
                BankPaymentInformation = new BankPaymentInformationData()
                {
                    BankCode = "123-456",
                    AccountNumber = "123456",
                    AccountName = "Bank Account"
                }
            };
        }

        private readonly NewChargeCardLeastCost _newChargeCardLeastCost = new()
        {
            Amount = 20,
            Currency = "AUD",
            PaymentInformation = CardRequestInformation
        };

        private NewCustomerCard GetNewCustomer()
        {
            return new NewCustomerCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }

        [Fact]
        public void ChargeWithValidCard()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreateWithCard(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void ChargeWithValidCardWithWebhook()
        {
            var svc = new xpanse.sdk.Charge();

            var chargeData = GetChargeData();
            chargeData.Webhook = new WebhookConfig
            {
                Url = "https://webhook.site/1da8cac9-fef5-47bf-a276-81856f73d7ca",
                Authorization = "Basic user:password"
            };

            var result = svc.CreateWithCard(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithValidCardAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            var result = await svc.CreateWithCardAsync(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void ChargeWithValidCardLeastCost()
        {
            var svc = new xpanse.sdk.Charge();
            var result = svc.CreateWithCardLeastCost(_newChargeCardLeastCost);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithValidCardLeastCostAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var result = await svc.CreateWithCardLeastCostAsync(_newChargeCardLeastCost);

            Assert.Equal(SuccessResponseValue, result.Status);
        }


        [Fact]
        public void Search()
        {
            var svc = new xpanse.sdk.Charge();
            var result = svc.Search(new ChargeSearch());

            Assert.Equal(0, result.Skip);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var result = await svc.SearchAsync(new ChargeSearch());

            Assert.Equal(0, result.Skip);
        }

        [Fact]
        public void ChargePaymentMethod()
        {
            var custSvc = new xpanse.sdk.Customer();
            var newCustomer = GetNewCustomer();
            var createdCustomer = custSvc.CreateWithCard(newCustomer);

            var createdPaymentMethod = custSvc.GetPaymentMethods(createdCustomer.CustomerId);

            var chargeSvc = new xpanse.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };

            var result = chargeSvc.CreateWithPaymentMethod(charge);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargePaymentMethodAsync()
        {
            var custSvc = new xpanse.sdk.Customer();
            var newCustomer = GetNewCustomer();
            var createdCustomer = await custSvc.CreateWithCardAsync(newCustomer);

            var createdPaymentMethod = await custSvc.GetPaymentMethodsAsync(createdCustomer.CustomerId);

            var chargeSvc = new xpanse.sdk.Charge();
            var charge = new NewChargePaymentMethod
            {
                Amount = 5,
                PaymentMethodId = createdPaymentMethod[0].PaymentMethodId
            };

            var result = await chargeSvc.CreateWithPaymentMethodAsync(charge);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public void Refund()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            var chargeResult = svc.CreateWithCard(chargeData);

            var refundResult = svc.Refund(new NewRefund
                { ChargeId = chargeResult.ChargeId, Comment = "Refund comment" });

            Assert.Equal(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact]
        public async Task RefundAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            var chargeResult = await svc.CreateWithCardAsync(chargeData);

            var refundResult = await svc.RefundAsync(new NewRefund
                { ChargeId = chargeResult.ChargeId, Comment = "Refund comment" });

            Assert.Equal(chargeData.Amount, refundResult.RefundedAmount);
        }

        [Fact]
        public void ChargeWithValidToken()
        {
            var chargeData = new NewChargeToken
            {
                Amount = 20,
                Token = GetPaymentToken()
            };

            var svc = new xpanse.sdk.Charge();
            var result = svc.CreateWithToken(chargeData);

            Assert.Equal(SuccessResponseValue, result.Status);
        }

        [Fact]
        public async Task ChargeWithInvalidCard()
        {
            var svc = new xpanse.sdk.Charge();

            Task Act() => svc.CreateWithCardAsync(new NewChargeCard
            {
                Amount = 20,
                ProviderId = GetProviderId()
            });
            var ex = await Assert.ThrowsAsync<ApiException>(Act);
            
            Assert.Equal(81, (int)ex.Code);
        }
        
        [Fact]
        public void ChargeWithValidBankAccount()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeDataWithBankAccount();
            chargeData.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreateWithBankAccount(chargeData);

            Assert.Equal(PendingResponseValue, result.Status);
        }
        
        [Fact]
        public async Task ChargeWithValidBankAccountAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeDataWithBankAccount();
            chargeData.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = await svc.CreateWithBankAccountAsync(chargeData);

            Assert.Equal(PendingResponseValue, result.Status);
        }

        [Fact]
        public void AuthoriseAndCapture()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Capture = false;

            var chargeResult = svc.CreateWithCard(chargeData);

            Assert.Equal("AUTHORISE", chargeResult.Status);
            Assert.Equal(chargeData.Amount, chargeResult.AuthorisationAmount);

            var captureResult = svc.Capture(chargeResult.ChargeId, new NewChargeCapture {Amount = chargeResult.Amount});

            Assert.Equal("SUCCESS", captureResult.Status);
        }

        [Fact]
        public async Task AuthoriseAndCaptureAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Capture = false;

            var chargeResult = await svc.CreateWithCardAsync(chargeData);

            Assert.Equal("AUTHORISE", chargeResult.Status);
            Assert.Equal(chargeData.Amount, chargeResult.AuthorisationAmount);

            var captureResult = await svc.CaptureAsync(chargeResult.ChargeId, new NewChargeCapture { Amount = chargeResult.Amount });

            Assert.Equal("SUCCESS", captureResult.Status);
        }

        [Fact]
        public void Void()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Capture = false;

            var chargeResult = svc.CreateWithCard(chargeData);

            Assert.Equal("AUTHORISE", chargeResult.Status);
            Assert.Equal(chargeData.Amount, chargeResult.AuthorisationAmount);

            var voidResult = svc.Void(chargeResult.ChargeId);

            Assert.Equal("AUTHORISE_CANCELLED", voidResult.Status);
        }

        [Fact]
        public async Task VoidAsync()
        {
            var svc = new xpanse.sdk.Charge();
            var chargeData = GetChargeData();
            chargeData.Capture = false;

            var chargeResult = await svc.CreateWithCardAsync(chargeData);

            Assert.Equal("AUTHORISE", chargeResult.Status);
            Assert.Equal(chargeData.Amount, chargeResult.AuthorisationAmount);

            var voidResult = await svc.VoidAsync(chargeResult.ChargeId);

            Assert.Equal("AUTHORISE_CANCELLED", voidResult.Status);
        }
        
        [Fact]
        public void SearchByCard()
        {
            var svc = new xpanse.sdk.Charge();

            var chargeData = GetChargeData();
            chargeData.PaymentInformation.Cardholder = Guid.NewGuid().ToString("N");
            svc.CreateWithCard(chargeData);

            var result = svc.Search(new ChargeSearch
            {
                Cardholder = chargeData.PaymentInformation.Cardholder,
                CardNumber = CardRequestInformation.CardNumber.Substring(0, 6),
                CardType = "VISA"
            });

            Assert.Equal(1, result.Count);
        }

    }
}
