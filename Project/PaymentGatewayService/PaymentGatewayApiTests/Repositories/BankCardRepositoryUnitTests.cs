using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Repositories;
using PaymentGatwayApiTests.Data;
using Xunit;

namespace PaymentGatwayApiTests.Repositories
{
	public class BankCardRepositoryUnitTests
	{
		private readonly BankCard _card;

		#region Constructor

		public BankCardRepositoryUnitTests()
		{
			_card = new BankCard
			{
				CardNumber = "3456673673683467",
				CVV = 123,
				ExpiryYear = DateTimeOffset.Now.Day,
				ExpiryMonth = DateTimeOffset.Now.Month,
				CardHolderName = "Joe Bloggs",
				BankCardId = Guid.Parse("F2C37F9A-1154-4830-BB49-AABD1624B276")
			};
		}

		#endregion

		/// <summary>
		/// Add Entity UnitTests
		/// </summary>
		[Fact]
		public async Task AddAsync_CreatesEntity_ReturnsEntityAsync()
		{
			// Arrange
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("AddBankEntity");
			var repository = new BankCardRepository(dBContext);

			// Act
			var actual = await repository.AddAsync(_card);

			// Assert
			Assert.Equal(_card.CardNumber, actual.CardNumber);
		}

		/// <summary>
		/// Delete Entity UnitTests
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task DeleteAsync_RemovesEntity_SuccussfullyRemovedAsync()
		{
			// Arrange
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("DeleteBankEntity");
			var repository = new BankCardRepository(dBContext);

			// Act
			await repository.AddAsync(_card);
			var actual = repository.DeleteAsync(_card);

			// Assert
			Assert.True(actual.IsCompletedSuccessfully);
		}

		/// <summary>
		/// Get All Entities UnitTests
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task GetAllAsync_FindAllData_ReturnEntityAsync()
		{
			// Arrange 
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("GetAllBankEntity");
			IEnumerable<BankCard> creditCards = new List<BankCard>
			{
				_card,
				new BankCard
				{
					CardNumber = "3423154521454123",
					CVV = 321,
					ExpiryYear = 03,
					ExpiryMonth = 04
				}
			};
			var repository = new BankCardRepository(dBContext);

			// Act
			await repository.AddAsync(creditCards);
			var result = repository.GetAllAsync();

			// Assert
			Assert.NotNull(result);
			Assert.True(result.IsCompletedSuccessfully);
			Assert.True(result.IsCompleted);
		}

		/// <summary>
		/// Get an Entity UnitTests
		/// </summary>
		[Fact]
		public async Task GetEntity_FilteredDataset_ReturnEntityAsync()
		{
			// Arrange 
			var dbContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("GetBankEntity");
			IEnumerable<BankCard> creditCards = new List<BankCard>
			{
				_card,
				new BankCard
				{
					CardNumber = "3423154521454123",
					CVV = 321,
					ExpiryYear = 03,
					ExpiryMonth = 04
				},
				new BankCard
				{
					CardNumber = "1235654376357763",
					CVV = 456,
					ExpiryYear = 23,
					ExpiryMonth = 12
				}
			};
			var repository = new BankCardRepository(dbContext);

			// Act
			var addData = await repository.AddAsync(creditCards);
			var result = repository.GetEntity(s => s.CardNumber == "3423154521454123");

			// Assert
			Assert.NotNull(result);
			Assert.Equal("3423154521454123", result.CardNumber);
		}

		/// <summary>
		/// Get an Entity by Id UnitTests
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task GetByIdAsync_FilteredById_ReturnEntityAsync()
		{
			// Arrange 
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("GetBankEntityById");
			IEnumerable<BankCard> creditCards = new List<BankCard>
			{
				_card,
				new BankCard
				{
					CardNumber = "3423154521454123",
					CVV = 321,
					ExpiryYear = 03,
					ExpiryMonth = 04
				},
				new BankCard
				{
					CardNumber = "1235654376357763",
					CVV = 456,
					ExpiryYear = 23,
					ExpiryMonth = 12
				}
			};
			var repository = new BankCardRepository(dBContext);

			// Act
			await repository.AddAsync(creditCards);
			var result = await repository.GetByIdAsync(_card.BankCardId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(Guid.Parse("F2C37F9A-1154-4830-BB49-AABD1624B276"), result.BankCardId);
			Assert.Equal(_card.CardNumber, result.CardNumber);
		}

		/// <summary>
		/// Update an Entity UnitTests
		/// </summary>
		[Fact]
		public async Task UpdateAsync_RecordUpdated_ReturnEntityAsync()
		{
			//Arrange 
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("UpdateBank");
			var repository = new BankCardRepository(dBContext);
			Guid id = Guid.Parse("F2C37F9A-1154-4830-BB49-AABD1624B276");


			//Act
			var addData = await repository.AddAsync(_card);
			var result = await repository.UpdateAsync(id, addData);

			//Assert
			Assert.Equal(_card.CardNumber, result.CardNumber);

		}


	}
}
