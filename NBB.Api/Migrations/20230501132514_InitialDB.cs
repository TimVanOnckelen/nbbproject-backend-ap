using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NBB.Api.Migrations
{
    public partial class InitialDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Address",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Box = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CountryCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Number = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Address", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "FinancialData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Year = table.Column<short>(type: "smallint", nullable: false),
                    Revenue = table.Column<int>(type: "int", nullable: false),
                    Profit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserLogin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enterprises",
                columns: table => new
                {
                    EnterpriseNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    startDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    endDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepositType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Language = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Currency = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EnterpriseName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Addressid = table.Column<int>(type: "int", nullable: false),
                    LegalForm = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LegalSituation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullFillLegalValidation = table.Column<bool>(type: "bit", nullable: false),
                    ActivityCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneralAssemblyDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountingDataURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImprovementDate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CorrectedData = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinancialDataId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enterprises", x => x.EnterpriseNumber);
                    table.ForeignKey(
                        name: "FK_Enterprises_Address_Addressid",
                        column: x => x.Addressid,
                        principalTable: "Address",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enterprises_FinancialData_FinancialDataId",
                        column: x => x.FinancialDataId,
                        principalTable: "FinancialData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_Addressid",
                table: "Enterprises",
                column: "Addressid");

            migrationBuilder.CreateIndex(
                name: "IX_Enterprises_FinancialDataId",
                table: "Enterprises",
                column: "FinancialDataId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enterprises");

            migrationBuilder.DropTable(
                name: "UserLogin");

            migrationBuilder.DropTable(
                name: "Address");

            migrationBuilder.DropTable(
                name: "FinancialData");
        }
    }
}
