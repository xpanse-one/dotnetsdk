﻿using xpanse.sdk.Models;
using System.Threading.Tasks;
using Xunit;

namespace FunctionalTests
{
    public class Vault : BaseTest
    {

        private static readonly NewVault NewVault = new()
        {
            CardNumber = "4111111111111111",
            Ccv = "123"
        };

        [Fact]
        public void Create()
        {
            Assert.NotNull(CreateVault().VaultId);
        }

        [Fact]
        public async Task CreateAsync()
        {
            var result = await CreateVaultAsync();
            Assert.NotNull(result.VaultId);
        }

        [Fact]
        public void Single()
        {
            var vault = CreateVault();
            var svc = new xpanse.sdk.Vault();
            var result = svc.Single(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact]
        public async Task SingleAsync()
        {
            var vault = await CreateVaultAsync();
            var svc = new xpanse.sdk.Vault();
            var result = await svc.SingleAsync(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact] public void Delete()
        {
            var vault = CreateVault();
            var svc = new xpanse.sdk.Vault();
            var result = svc.Delete(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        [Fact]
        public async Task DeleteAsync()
        {
            var vault = await CreateVaultAsync();
            var svc = new xpanse.sdk.Vault();
            var result = await svc.DeleteAsync(vault.VaultId);

            Assert.Equal(result.VaultId, vault.VaultId);
        }

        private VaultData CreateVault()
        {
            var svc = new xpanse.sdk.Vault();
            return svc.Create(NewVault);
        }

        private async Task<VaultData> CreateVaultAsync()
        {
            var svc = new xpanse.sdk.Vault();
            return await svc.CreateAsync(NewVault);
        }
    }
}
