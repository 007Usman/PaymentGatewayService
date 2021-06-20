using System;
using Microsoft.EntityFrameworkCore;
using PaymentGatway.Core.Models;

namespace PaymentGatway.Infrastructure.Data
{
	public class PaymentGatewayDBContext : DbContext
	{
		#region Properties

		public DbSet<Merchant> Merchants { get; set; }
		public DbSet<Bank> Banks { get; set; }
		public DbSet<Transaction> Transactions { get; set; }
		public DbSet<BankCard> BankCards { get; set; }
		public DbSet<Currency> Currencies { get; set; }

		#endregion

		#region Constructor

		public PaymentGatewayDBContext(DbContextOptions options) : base(options)
		{
			InstantiateDatabase();
		}


		#endregion

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			#region SeedData

			modelBuilder.Entity<Merchant>().ToTable("Merchants").HasData(
				new Merchant { MerchantId = Guid.NewGuid(), MerchantName = "Apple", Description = "Makes Tech Product", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Merchant { MerchantId = Guid.NewGuid(), MerchantName = "Amazon", Description = "Online Retaier and Distributer", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Merchant { MerchantId = Guid.NewGuid(), MerchantName = "Sainsburys", Description = "Groceries Retailer", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Merchant { MerchantId = Guid.NewGuid(), MerchantName = "Deliveroo", Description = "Online food delivery service", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now }
				);

			modelBuilder.Entity<Bank>().ToTable("Banks").HasData(
				new Bank { BankId = Guid.NewGuid(), BankName = "Barclays", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Bank { BankId = Guid.NewGuid(), BankName = "American Express", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Bank { BankId = Guid.NewGuid(), BankName = "Halifax", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Bank { BankId = Guid.NewGuid(), BankName = "HSBC", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now }
				);

			modelBuilder.Entity<Currency>().ToTable("Currencies").HasData(
				new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = "GBP", CurrencyDescription = "British Pound", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = "USD", CurrencyDescription = "United States Doller", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = "TRY", CurrencyDescription = "Turkish Lira", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = "AED", CurrencyDescription = "United Arab Emirates Dirham", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now },
				new Currency { CurrencyId = Guid.NewGuid(), CurrencyCode = "EUR", CurrencyDescription = "Euro", CreatedTimestamp = DateTimeOffset.Now, ModifiedTimestamp = DateTimeOffset.Now }
				);

			#endregion
		}

		/// <summary>
		/// Resovles Unit Testing Migrations Issue
		/// </summary>
		private void InstantiateDatabase()
		{
			if (Database.IsRelational())
				Database.Migrate();
			else
				Database.EnsureCreated();
		}
	}
}