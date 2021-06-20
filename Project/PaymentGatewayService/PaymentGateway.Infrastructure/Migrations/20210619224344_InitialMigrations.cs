using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGateway.Infrastructure.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankCards",
                columns: table => new
                {
                    BankCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExpiryMonth = table.Column<int>(type: "int", nullable: false),
                    ExpiryYear = table.Column<int>(type: "int", nullable: false),
                    CardHolderName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CVV = table.Column<int>(type: "int", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankCards", x => x.BankCardId);
                });

            migrationBuilder.CreateTable(
                name: "Banks",
                columns: table => new
                {
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Banks", x => x.BankId);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                columns: table => new
                {
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrencyDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "Merchants",
                columns: table => new
                {
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Merchants", x => x.MerchantId);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TransactionStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankReferenceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MerchantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BankCardId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CurrencyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    ModifiedTimestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                });

            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "BankId", "BankName", "CreatedTimestamp", "ModifiedTimestamp" },
                values: new object[,]
                {
                    { new Guid("5f2e3fd1-9b46-4b04-bca4-6f785674c172"), "Barclays", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(5891), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(7006), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("57da71b7-67ca-480b-87cf-004a6e482688"), "American Express", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8110), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8140), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("ffd1f0f2-8dd9-4d1e-94ba-1befe4e3f6b9"), "Halifax", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8159), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8172), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("4a096637-c9e6-4e49-b703-d516420e7726"), "HSBC", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8190), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 749, DateTimeKind.Unspecified).AddTicks(8204), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CreatedTimestamp", "CurrencyCode", "CurrencyDescription", "ModifiedTimestamp" },
                values: new object[,]
                {
                    { new Guid("3a7d34d8-3e66-40d7-a0e2-016ee64effc9"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(3376), new TimeSpan(0, 1, 0, 0, 0)), "GBP", "British Pound", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(4441), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("15cc6063-ebd1-4c0b-8ab6-6b6f84244538"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5492), new TimeSpan(0, 1, 0, 0, 0)), "USD", "United States Doller", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5521), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("9e7a8cfd-ebb4-44ff-abb6-f3caf390c927"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5542), new TimeSpan(0, 1, 0, 0, 0)), "TRY", "Turkish Lira", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5556), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("cab83ba1-2c84-4009-8e9f-ee611900c9c0"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5574), new TimeSpan(0, 1, 0, 0, 0)), "AED", "United Arab Emirates Dirham", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5588), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("fda8030d-42dc-455c-96a6-fdec0f2483b1"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5606), new TimeSpan(0, 1, 0, 0, 0)), "EUR", "Euro", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 750, DateTimeKind.Unspecified).AddTicks(5620), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "MerchantId", "CreatedTimestamp", "Description", "MerchantName", "ModifiedTimestamp" },
                values: new object[,]
                {
                    { new Guid("40b972b9-3457-4262-be9a-c4f109a0a5a1"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 726, DateTimeKind.Unspecified).AddTicks(3514), new TimeSpan(0, 1, 0, 0, 0)), "Makes Tech Product", "Apple", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(3120), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("9cf3ac43-9d21-4101-8828-1959d3de005f"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4629), new TimeSpan(0, 1, 0, 0, 0)), "Online Retaier and Distributer", "Amazon", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4660), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("31b5b9d7-f23b-4cb2-8d4b-5da0e256216a"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4682), new TimeSpan(0, 1, 0, 0, 0)), "Groceries Retailer", "Sainsburys", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4697), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("2828ded6-0eff-423f-bf12-648c1f55c838"), new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4715), new TimeSpan(0, 1, 0, 0, 0)), "Online food delivery service", "Deliveroo", new DateTimeOffset(new DateTime(2021, 6, 19, 23, 43, 42, 746, DateTimeKind.Unspecified).AddTicks(4730), new TimeSpan(0, 1, 0, 0, 0)) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankCards");

            migrationBuilder.DropTable(
                name: "Banks");

            migrationBuilder.DropTable(
                name: "Currencies");

            migrationBuilder.DropTable(
                name: "Merchants");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
