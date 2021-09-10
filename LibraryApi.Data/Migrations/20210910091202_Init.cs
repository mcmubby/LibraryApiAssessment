using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LibraryApi.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PublishYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverPrice = table.Column<decimal>(type: "Money", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Checkouts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NationalIdentificationNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkouts_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LateCheckIns",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CheckoutId = table.Column<int>(type: "int", nullable: false),
                    CheckoutDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NumberOfDaysLate = table.Column<int>(type: "int", nullable: false),
                    PenaltyFees = table.Column<decimal>(type: "Money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LateCheckIns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LateCheckIns_Checkouts_CheckoutId",
                        column: x => x.CheckoutId,
                        principalTable: "Checkouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "CoverPrice", "ISBN", "IsAvailable", "PublishYear", "Title" },
                values: new object[,]
                {
                    { 1, 20m, "9476028470", true, "2013", "Things Fall Apart" },
                    { 2, 12m, "9476028470", true, "2010", "Half of a Yellow Sun" },
                    { 3, 50m, "9476028470", true, "2018", "There Was A Country" },
                    { 4, 15m, "9476028470", true, "2005", "Worm Hole" },
                    { 5, 10m, "9476028470", true, "2000", "Never Man" }
                });

            migrationBuilder.InsertData(
                table: "Checkouts",
                columns: new[] { "Id", "BookId", "CheckoutDate", "Email", "ExpectedReturnDate", "FullName", "NationalIdentificationNumber", "PhoneNumber" },
                values: new object[] { 1, 3, new DateTime(2021, 8, 20, 10, 12, 1, 324, DateTimeKind.Local).AddTicks(8875), "jd@mail.com", new DateTime(2021, 9, 3, 10, 12, 1, 333, DateTimeKind.Local).AddTicks(5018), "John Doe", "9458678475959547", "08012398765" });

            migrationBuilder.CreateIndex(
                name: "IX_Checkouts_BookId",
                table: "Checkouts",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_LateCheckIns_CheckoutId",
                table: "LateCheckIns",
                column: "CheckoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LateCheckIns");

            migrationBuilder.DropTable(
                name: "Checkouts");

            migrationBuilder.DropTable(
                name: "Books");
        }
    }
}
