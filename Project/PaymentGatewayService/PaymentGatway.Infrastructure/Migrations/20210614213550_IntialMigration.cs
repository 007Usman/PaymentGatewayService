using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentGatway.Infrastructure.Migrations
{
    public partial class IntialMigration : Migration
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
                    { new Guid("f9d49fd0-77d1-4a31-af08-3c1b28f6d850"), "Barclays", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(7070), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(8090), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("e31095f2-faa8-4ea7-ae3a-16b14a62190c"), "American Express", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(8971), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(8993), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("7b4d71ed-785a-4136-a08d-67d9fc038912"), "Halifax", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(9002), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(9009), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("f2bbbf01-1574-4e03-ab77-ccd760b8618c"), "HSBC", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(9017), new TimeSpan(0, 1, 0, 0, 0)), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 105, DateTimeKind.Unspecified).AddTicks(9024), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Currencies",
                columns: new[] { "CurrencyId", "CreatedTimestamp", "CurrencyCode", "CurrencyDescription", "ModifiedTimestamp" },
                values: new object[,]
                {
                    { new Guid("e5d15d55-5c68-4d90-829b-4ccbe10f337a"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(3952), new TimeSpan(0, 1, 0, 0, 0)), "GPB", "British Pound", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(4833), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("b7fbb50c-87e3-4ccb-ab63-9893ee9470af"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5762), new TimeSpan(0, 1, 0, 0, 0)), "USD", "United States Doller", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5787), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("a7c1dc99-2be8-4891-807c-e0471602795d"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5797), new TimeSpan(0, 1, 0, 0, 0)), "TRY", "Turkish Lira", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5803), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("78301e89-e4e3-4bc3-a36b-c6471370e349"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5812), new TimeSpan(0, 1, 0, 0, 0)), "AED", "United Arab Emirates Dirham", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5819), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("31be5f1d-97ec-4d8c-8d35-e7cc98a28fee"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5832), new TimeSpan(0, 1, 0, 0, 0)), "EUR", "Euro", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 106, DateTimeKind.Unspecified).AddTicks(5838), new TimeSpan(0, 1, 0, 0, 0)) }
                });

            migrationBuilder.InsertData(
                table: "Merchants",
                columns: new[] { "MerchantId", "CreatedTimestamp", "Description", "MerchantName", "ModifiedTimestamp" },
                values: new object[,]
                {
                    { new Guid("490c4922-7123-4004-9c80-4adb2d5661e3"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 85, DateTimeKind.Unspecified).AddTicks(5260), new TimeSpan(0, 1, 0, 0, 0)), "Makes Tech Product", "Apple", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(3854), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("3a517723-3344-4858-9262-2a07927c2ee5"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5123), new TimeSpan(0, 1, 0, 0, 0)), "Online Retaier and Distributer", "Amazon", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5151), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("bdcdef89-06ff-4d2f-a8e3-2dab99278155"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5172), new TimeSpan(0, 1, 0, 0, 0)), "Groceries Retailer", "Sainsburys", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5186), new TimeSpan(0, 1, 0, 0, 0)) },
                    { new Guid("1f948c80-68f4-4f7e-942f-161b99d28d12"), new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5204), new TimeSpan(0, 1, 0, 0, 0)), "Online food delivery service", "Deliveroo", new DateTimeOffset(new DateTime(2021, 6, 14, 22, 35, 48, 102, DateTimeKind.Unspecified).AddTicks(5217), new TimeSpan(0, 1, 0, 0, 0)) }
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
