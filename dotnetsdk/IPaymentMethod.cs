﻿using System.Threading.Tasks;
using xpanse.sdk.Models;

namespace xpanse.sdk
{
    public interface IPaymentMethod
    {
        PaymentMethodData CreatePaymentMethodWithVault(NewPaymentMethodVault newPaymentMethodVault);
        Task<PaymentMethodData> CreatePaymentMethodWithVaultAsync(NewPaymentMethodVault newPaymentMethodVault);
        PaymentMethodData CreatePaymentMethodWithCard(NewPaymentMethodCard newNewPaymentMethodCard);
        Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(NewPaymentMethodCard newNewPaymentMethodCard);
        PaymentMethodData CreatePaymentMethodWithPayto(NewPayToAgreement newNewPaymentMethodCard);
        Task<PaymentMethodData> CreatePaymentMethodWithPaytoAsync(NewPayToAgreement newNewPaymentMethodCard);
        PaymentMethodData Single(string paymentMethodId);
        Task<PaymentMethodData> SingleAsync(string paymentMethodId);
        PaymentMethodList Search(PaymentMethodSearch searchData);
        Task<PaymentMethodList> SearchAsync(PaymentMethodSearch searchData);
        PaymentMethodData CreatePaymentMethodWithSingleUseToken(NewPaymentMethodSingleUseToken newPaymentMethodSingleUseToken);
        Task<PaymentMethodData> CreatePaymentMethodWithSingleUseTokenAsync(NewPaymentMethodSingleUseToken newPaymentMethodSingleUseToken);
        PaymentMethodData CreateWithProviderToken(NewProviderToken providerToken);
        Task<PaymentMethodData> CreateWithProviderTokenAsync(NewProviderToken providerToken);
        PaymentMethodData Delete(string paymentMethodId);
        Task<PaymentMethodData> DeleteAsync(string paymentMethodId);
        PaymentMethodData UpdatePaymentMethod(string paymentMethodId, UpdatePaymentMethod updatePaymentMethod);
        Task<PaymentMethodData> UpdatePaymentMethodAsync(string paymentMethodId, UpdatePaymentMethod updatePaymentMethod);
        PaymentMethodData CreateWithToken(NewPaymentMethodToken token);
        Task<PaymentMethodData> CreateWithTokenAsync(NewPaymentMethodToken token);
    }
}
