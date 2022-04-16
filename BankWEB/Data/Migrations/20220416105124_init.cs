using Microsoft.EntityFrameworkCore.Migrations;

namespace BankWEB.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InterestRate = table.Column<double>(type: "float", nullable: false),
                    MaximumLoan = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    MinimumDownPayment = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    LoanTerm = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankModel");
        }
    }
}
