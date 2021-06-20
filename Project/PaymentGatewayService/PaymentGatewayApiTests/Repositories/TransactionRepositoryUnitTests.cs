using System;
using System.Threading.Tasks;
using PaymentGatway.Core.Models;
using PaymentGatway.Infrastructure.Repositories;
using PaymentGatwayApiTests.Data;
using Xunit;

namespace PaymentGatwayApiTests.Repositories
{
	public class TransactionRepositoryUnitTests
	{
		private readonly Transaction _transaction;

		#region Constructor

		public TransactionRepositoryUnitTests()
		{
			_transaction = new Transaction
			{
				TransactionId = Guid.Parse("507AA3AD-C95C-405E-5EAC-08D92F9C8E27"),
				BankId = Guid.Parse("F2C37F9A-1154-4830-BB49-AABD1624B276"),
				BankReferenceId = Guid.Parse("E1957647-EE5E-481F-9E11-DE9845A87F5B"),
				Amount = 5000
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
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("AddTransactionEntity");
			var repository = new TransactionRepository(dBContext);

			// Act
			var actual = await repository.AddAsync(_transaction);

			// Assert
			Assert.Equal(_transaction.TransactionId, actual.TransactionId);
			Assert.Equal(_transaction.BankReferenceId, actual.BankReferenceId);
		}

		/// <summary>
		/// Get an Entity UnitTests
		/// </summary>
		[Fact]
		public async Task GetEntity_FilteredDataset_ReturnEntityAsync()
		{
			// Arrange 
			var dbContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("GetTransactionEntity");
			var repository = new TransactionRepository(dbContext);

			// Act
			var addData = await repository.AddAsync(_transaction);
			var result = repository.GetEntity(s => s.TransactionId == _transaction.TransactionId);

			// Assert
			Assert.NotNull(result);
			Assert.Equal(_transaction.TransactionId, result.TransactionId);
			Assert.Equal(_transaction.Amount, result.Amount);
		}

		/// <summary>
		/// Get an Entity by Id UnitTests
		/// </summary>
		/// <returns></returns>
		[Fact]
		public async Task GetByIdAsync_FilteredById_ReturnEntityAsync()
		{
			// Arrange 
			var dBContext = PaymentGatewayDBContextUnitTests.SetupInMemoryDatabase("GetTransactionEntityById");
			var repository = new TransactionRepository(dBContext);

			// Act
			await repository.AddAsync(_transaction);
			var transactionById = await repository.GetByIdAsync(_transaction.TransactionId);
			var bankCardById = await repository.GetByIdAsync(transactionById.TransactionId);

			// Assert
			Assert.NotNull(transactionById);
			Assert.NotNull(bankCardById);
			Assert.Equal(_transaction.TransactionId, transactionById.TransactionId);
			Assert.Equal(_transaction.BankCardId, bankCardById.BankCardId);
		}


	}
}
