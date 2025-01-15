﻿using xpanse.sdk.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests
{
    public class PaymentMethod : BaseTest
    {
        private static readonly CardRequestInformation CardRequestInformation = new()
        {
            CardNumber = "4111111111111111",
            ExpiryDate = "12/35",
            Ccv = "123"
        };

        private NewPaymentMethodCard GetNewPaymentMethod()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }

        private NewCustomerCard GetNewCustomer()
        {
            return new NewCustomerCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation,
                VaultCard = true
            };
        }

        private NewPaymentMethodCard GetNewPaymentMethodCard()
        {
            return new NewPaymentMethodCard
            {
                ProviderId = GetProviderId(),
                PaymentInformation = CardRequestInformation
            };
        }
        
        [Fact]
        public void CreatePaymentMethodWithCard()
        {

            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod(); 
            newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreatePaymentMethodWithCard(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithCardAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethod());

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void Single()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var paymentMethodWithCard = svc.CreatePaymentMethodWithCard(GetNewPaymentMethodCard());

            var result = svc.Single(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var paymentMethodWithCard = await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethodCard());

            var result = await svc.SingleAsync(paymentMethodWithCard.PaymentMethodId);

            Assert.Equal(paymentMethodWithCard.PaymentMethodId, result.PaymentMethodId);
        }

        [Fact]
        public void CreatePaymentMethodWithVault()
        {
            var custSvc = new xpanse.sdk.Customer();
            var customer = custSvc.CreateWithCard(GetNewCustomer());

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = GetProviderId(),
                PaymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId
            };

            var svc = new xpanse.sdk.PaymentMethod();
            var result = svc.CreatePaymentMethodWithVault(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithVaultAsync()
        {
            var custSvc = new xpanse.sdk.Customer();
            var customer = await custSvc.CreateWithCardAsync(GetNewCustomer());

            var newPaymentMethod = new NewPaymentMethodVault
            {
                ProviderId = GetProviderId(),
                PaymentMethodId = customer.DefaultPaymentMethod.PaymentMethodId
            };

            var svc = new xpanse.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithVaultAsync(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void Search()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            svc.CreatePaymentMethodWithCard(GetNewPaymentMethod());

            var result = svc.Search(new PaymentMethodSearch { ProviderId = GetProviderId()});

            Assert.False(result.Count == 0);
        }

        [Fact]
        public async Task SearchAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            await svc.CreatePaymentMethodWithCardAsync(GetNewPaymentMethod());

            var result = svc.Search(new PaymentMethodSearch { ProviderId = GetProviderId() });

            Assert.False(result.Count == 0);
        }
        
        [Fact]
        public void CreateWithProviderToken()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var paymentMethod = svc.CreateWithProviderToken(new NewProviderToken
            {
                ProviderId = GetProviderId(),
                ProviderToken = "test"
            });

            Assert.NotNull(paymentMethod);
            Assert.Null(paymentMethod.CustomerId);
            Assert.Equal("CARD", paymentMethod.Type);
        }

        [Fact]
        public async Task CreateWithProviderTokenAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var paymentMethod = await svc.CreateWithProviderTokenAsync(new NewProviderToken
            {
                ProviderId = GetProviderId(),
                ProviderToken = "test"
            });

            Assert.NotNull(paymentMethod);
            Assert.Null(paymentMethod.CustomerId);
            Assert.Equal("CARD", paymentMethod.Type);
        }
        
        [Fact]
        public void CreatePaymentMethodWithBankAccount()
        {

            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetPaymentMethodWithBankAccount(); 
            newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreatePaymentMethodWithBankAccount(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public async Task CreatePaymentMethodWithBankAccountAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var result = await svc.CreatePaymentMethodWithBankAccountAsync(GetPaymentMethodWithBankAccount());

            Assert.NotNull(result.PaymentMethodId);
        }

        [Fact]
        public void DeletePaymentMethod()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod();
            var result = svc.CreatePaymentMethodWithCard(newPaymentMethod);

            var deletedPaymentMethod = svc.Delete(result.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, deletedPaymentMethod.PaymentMethodId);
        }

        [Fact]
        public async Task DeletePaymentMethodAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod();
            var result = await svc.CreatePaymentMethodWithCardAsync(newPaymentMethod);

            var deletedPaymentMethod = await svc.DeleteAsync(result.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, deletedPaymentMethod.PaymentMethodId);
        }
        
        [Fact]
        public void UpdatePaymentMethod()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod(); 
            newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = svc.CreatePaymentMethodWithCard(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);

            var newExpiryDate = "04/31";
            var newCardholderName = "UpdatedCardholderName";
            
            var updateResult = svc.UpdatePaymentMethod(result.PaymentMethodId, new UpdatePaymentMethod()
            {
                Card = new UpdatePaymentMethodCardRequestInformation()
                {
                    ExpiryDate = newExpiryDate,
                    Cardholder = newCardholderName
                }
            });
            
            Assert.NotNull(updateResult.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updateResult.PaymentMethodId);
            Assert.Equal(newExpiryDate, updateResult.Card.ExpiryDate);
            Assert.Equal(newCardholderName, updateResult.Card.Cardholder);
        }

        [Fact]
        public async Task UpdatePaymentMethodAsync()
        {
            var svc = new xpanse.sdk.PaymentMethod();
            var newPaymentMethod = GetNewPaymentMethod(); 
            newPaymentMethod.Metadata = new Dictionary<string, string>{ { "merchant_id", "value1" } };
            var result = await svc.CreatePaymentMethodWithCardAsync(newPaymentMethod);

            Assert.NotNull(result.PaymentMethodId);

            var newExpiryDate = "04/31";
            var newCardholderName = "UpdatedCardholderName";
            
            var updateResult = await svc.UpdatePaymentMethodAsync(result.PaymentMethodId, new UpdatePaymentMethod()
            {
                Card = new UpdatePaymentMethodCardRequestInformation()
                {
                    ExpiryDate = newExpiryDate,
                    Cardholder = newCardholderName
                }
            });
            
            Assert.NotNull(updateResult.PaymentMethodId);
            Assert.Equal(result.PaymentMethodId, updateResult.PaymentMethodId);
            Assert.Equal(newExpiryDate, updateResult.Card.ExpiryDate);
            Assert.Equal(newCardholderName, updateResult.Card.Cardholder);
        }
        
        [Fact]
        public void CreateWithToken()
        {
            var tokenData = new NewPaymentMethodToken
            {
                Token = GetPaymentToken()
            };

            var svc = new xpanse.sdk.PaymentMethod();
            var result = svc.CreateWithToken(tokenData);

            Assert.NotNull(result.PaymentMethodId);
        }


        private NewPaymentMethodBankPayment GetPaymentMethodWithBankAccount()
        {
            return new NewPaymentMethodBankPayment
            {
                ProviderId = GetProviderId(),
                BankPaymentInformation = new NewBankPayment()
                {
                    BankCode = "123-456",
                    AccountNumber = "123456",
                    AccountName = "Bank Account"
                }
            };
        }
    }
}
