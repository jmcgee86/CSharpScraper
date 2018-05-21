using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ScraperDb.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Portfolio",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DatePulled = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DayGain = table.Column<double>(type: "REAL", nullable: false),
                    DayGainPercentage = table.Column<double>(type: "REAL", nullable: false),
                    NetWorth = table.Column<double>(type: "REAL", nullable: false),
                    TotalGain = table.Column<double>(type: "REAL", nullable: false),
                    TotalGainPercentage = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Portfolio", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CostBasis = table.Column<double>(type: "REAL", nullable: false),
                    CurrentPrice = table.Column<double>(type: "REAL", nullable: false),
                    DayGain = table.Column<double>(type: "REAL", nullable: false),
                    DayGainPercentage = table.Column<double>(type: "REAL", nullable: false),
                    Lots = table.Column<int>(type: "INTEGER", nullable: false),
                    MarketValue = table.Column<double>(type: "REAL", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: true),
                    PortfolioInfoID = table.Column<int>(type: "INTEGER", nullable: true),
                    PriceChange = table.Column<double>(type: "REAL", nullable: false),
                    PriceChangePercentage = table.Column<double>(type: "REAL", nullable: false),
                    Shares = table.Column<double>(type: "REAL", nullable: false),
                    StockSymbol = table.Column<string>(type: "TEXT", nullable: true),
                    TotalGain = table.Column<double>(type: "REAL", nullable: false),
                    TotalGainPercentage = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Stocks_Portfolio_PortfolioInfoID",
                        column: x => x.PortfolioInfoID,
                        principalTable: "Portfolio",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_PortfolioInfoID",
                table: "Stocks",
                column: "PortfolioInfoID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "Portfolio");
        }
    }
}
