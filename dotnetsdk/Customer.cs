﻿using xpanse.sdk.Models;
using xpanse.sdk.Tools;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using xpanse.sdk.Helpers;

namespace xpanse.sdk
{
    public class Customer : ICustomer
    {
        public CustomerData CreateWithCard(NewCustomerCard newCustomer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewCustomerCard, CustomerData>("/customer/card", Method.POST, newCustomer));
        }

        public async Task<CustomerData> CreateWithCardAsync(NewCustomerCard newCustomer)
        {
            return await HttpWrapper.CallAsync<NewCustomerCard, CustomerData>("/customer/card", Method.POST,
                newCustomer);
        }

        public CustomerData CreateWithToken(NewCustomerToken newCustomer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewCustomerToken, CustomerData>("/customer/token", Method.POST, newCustomer));
        }

        public async Task<CustomerData> CreateWithTokenAsync(NewCustomerToken newCustomer)
        {
            return await HttpWrapper.CallAsync<NewCustomerToken, CustomerData>("/customer/token", Method.POST,
                newCustomer);
        }
        
        public CustomerData CreateWithSingleUseToken(NewCustomerSingleUseToken newCustomerSingleUseToken)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewCustomerSingleUseToken, CustomerData>("/customer/provider_single_use_token", Method.POST, newCustomerSingleUseToken));
        }

        public async Task<CustomerData> CreateWithSingleUseTokenAsync(NewCustomerSingleUseToken newCustomerSingleUseToken)
        {
            return await HttpWrapper.CallAsync<NewCustomerSingleUseToken, CustomerData>("/customer/provider_single_use_token", Method.POST,
                newCustomerSingleUseToken);
        }

        public CustomerData CreateWithProviderToken(NewCustomerProviderToken newCustomer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewCustomerProviderToken, CustomerData>("/customer/provider_token", Method.POST, newCustomer));
        }

        public async Task<CustomerData> CreateWithProviderTokenAsync(NewCustomerProviderToken newCustomer)
        {
            return await HttpWrapper.CallAsync<NewCustomerProviderToken, CustomerData>("/customer/provider_token", Method.POST,
                newCustomer);
        }

        public PaymentMethodData CreatePaymentMethodWithCard(string customerId, NewPaymentMethodCard newPaymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodCard, PaymentMethodData>(
                    "/customer/" + customerId + "/payment_method/card", Method.POST, newPaymentMethod));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithCardAsync(string customerId,
            NewPaymentMethodCard newPaymentMethod)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodCard, PaymentMethodData>(
                "/customer/" + customerId + "/payment_method/card", Method.POST, newPaymentMethod);
        }

        public PaymentMethodData CreatePaymentMethodWithToken(string customerId, NewPaymentMethodToken newPaymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodToken, PaymentMethodData>(
                    "/customer/" + customerId + "/payment_method/token", Method.POST, newPaymentMethod));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithTokenAsync(string customerId,
            NewPaymentMethodToken newPaymentMethod)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodToken, PaymentMethodData>(
                "/customer/" + customerId + "/payment_method/token", Method.POST, newPaymentMethod);
        }
        
        public PaymentMethodData CreatePaymentMethodWithPayTo(string customerId, NewPayToAgreement newPayToAgreement)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPayToAgreement, PaymentMethodData>(
                    "/customer/" + customerId + "/payment_method/payto", Method.POST, newPayToAgreement));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithPayToAsync(string customerId,
            NewPayToAgreement newPayToAgreement)
        {
            return await HttpWrapper.CallAsync<NewPayToAgreement, PaymentMethodData>(
                "/customer/" + customerId + "/payment_method/payto", Method.POST, newPayToAgreement);
        }

        public List<PaymentMethodData> GetPaymentMethods(string customerId)
        {
            customerId = HttpUtility.UrlEncode(customerId);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, List<PaymentMethodData>>(
                    "/customer/" + HttpUtility.UrlEncode(customerId) + "/payment_method", Method.GET, null));
        }

        public async Task<List<PaymentMethodData>> GetPaymentMethodsAsync(string customerId)
        {
            customerId = HttpUtility.UrlEncode(customerId);

            return await HttpWrapper.CallAsync<string, List<PaymentMethodData>>(
                "/customer/" + HttpUtility.UrlEncode(customerId) + "/payment_method", Method.GET, null);
        }

        public CustomerData Single(string customerId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, CustomerData>("/customer/" + customerId, Method.GET, null));
        }

        public async Task<CustomerData> SingleAsync(string customerId)
        {
            return await HttpWrapper.CallAsync<string, CustomerData>("/customer/" + customerId, Method.GET, null);
        }

        public CustomerList Search(CustomerSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, CustomerList>("/customer" + queryString, Method.GET, null));
        }

        public async Task<CustomerList> SearchAsync(CustomerSearch searchData)
        {
            var queryString = BuildSearchQueryString(searchData);

            return await HttpWrapper.CallAsync<string, CustomerList>("/customer" + queryString, Method.GET, null);
        }

        public CustomerData Delete(string customerId)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<string, CustomerData>("/customer/" + customerId, Method.DELETE, null));
        }

        public async Task<CustomerData> DeleteAsync(string customerId)
        {
            return await HttpWrapper.CallAsync<string, CustomerData>("/customer/" + customerId, Method.DELETE, null);
        }

        public CustomerData Update(string customerId, UpdateCustomer updateCustomer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<UpdateCustomer, CustomerData>(
                    "/customer/" + customerId, Method.PUT, updateCustomer));
        }

        public async Task<CustomerData> UpdateAsync(string customerId, UpdateCustomer updateCustomer)
        {
            return await HttpWrapper.CallAsync<UpdateCustomer, CustomerData>(
                "/customer/" + customerId, Method.PUT, updateCustomer);
        }
        
        public CustomerData CreateWithBankAccount(NewCustomerBankPayment newCustomer)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewCustomerBankPayment, CustomerData>("/customer/bank_account", Method.POST, newCustomer));
        }

        public async Task<CustomerData> CreateWithBankAccountAsync(NewCustomerBankPayment newCustomer)
        {
            return await HttpWrapper.CallAsync<NewCustomerBankPayment, CustomerData>("/customer/bank_account", Method.POST,
                newCustomer);
        }

        public PaymentMethodData CreatePaymentMethodWithBankAccount(string customerId, NewPaymentMethodBankPayment newPaymentMethod)
        {
            return AsyncHelper.RunSync(() =>
                HttpWrapper.CallAsync<NewPaymentMethodBankPayment, PaymentMethodData>("/customer/" + customerId + "/bank_account", Method.POST, newPaymentMethod));
        }

        public async Task<PaymentMethodData> CreatePaymentMethodWithBankAccountAsync(string customerId, NewPaymentMethodBankPayment newPaymentMethod)
        {
            return await HttpWrapper.CallAsync<NewPaymentMethodBankPayment, PaymentMethodData>("/customer/" + customerId + "/bank_account", Method.POST,
                newPaymentMethod);
        }

        private static string BuildSearchQueryString(CustomerSearch searchData)
        {
            // TODO: move into a shared class
            var queryString = new List<string>();

            if (searchData.Skip.HasValue)
                queryString.Add("Skip=" + searchData.Skip.Value);

            if (searchData.Limit.HasValue)
                queryString.Add("Limit=" + searchData.Limit.Value);
            
            if (!string.IsNullOrWhiteSpace(searchData.Reference))
                queryString.Add("Reference=" + HttpUtility.UrlEncode(searchData.Reference));

            if (!string.IsNullOrWhiteSpace(searchData.Email))
                queryString.Add("Email=" + HttpUtility.UrlEncode(searchData.Email));

            if (!string.IsNullOrWhiteSpace(searchData.Search))
                queryString.Add("Search=" + HttpUtility.UrlEncode(searchData.Search));

            if (searchData.AddedAfter.HasValue)
                queryString.Add("AddedAfter=" +
                                HttpUtility.UrlEncode(searchData.AddedAfter.Value.ToString("yyyy-MM-dd HH: mm:ss")));

            if (searchData.AddedBefore.HasValue)
                queryString.Add("AddedBefore=" +
                                HttpUtility.UrlEncode(searchData.AddedBefore.Value.ToString("yyyy-MM-dd HH: mm:ss")));
            
            if (!string.IsNullOrWhiteSpace(searchData.SortBy))
                queryString.Add("SortBy=" + HttpUtility.UrlEncode(searchData.SortBy));
            
            if (searchData.IncludeRemoved != null)
                queryString.Add("IncludeRemoved=" + searchData.IncludeRemoved);

            var result = "";
            if (queryString.Count > 0)
                result = "?" + string.Join("&", queryString);
            
            return result;
        }
    }
}