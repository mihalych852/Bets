using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BetsService.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedBets : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsCanceled",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "IsOver",
                table: "Events");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Events",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentOdd",
                table: "EventOutcomes",
                type: "numeric(5,3)",
                precision: 5,
                scale: 3,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric");

            migrationBuilder.AddColumn<decimal>(
                name: "Commision",
                table: "EventOutcomes",
                type: "numeric(3,3)",
                precision: 3,
                scale: 3,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Bets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    State = table.Column<int>(type: "integer", nullable: false),
                    BettorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    EventOutcomesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "character varying(60)", maxLength: 60, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bets_EventOutcomes_EventOutcomesId",
                        column: x => x.EventOutcomesId,
                        principalTable: "EventOutcomes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bets_EventOutcomesId",
                table: "Bets",
                column: "EventOutcomesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bets");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Events");

            migrationBuilder.DropColumn(
                name: "Commision",
                table: "EventOutcomes");

            migrationBuilder.AddColumn<bool>(
                name: "IsCanceled",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsOver",
                table: "Events",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentOdd",
                table: "EventOutcomes",
                type: "numeric",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "numeric(5,3)",
                oldPrecision: 5,
                oldScale: 3);
        }
    }
}
