using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_slofst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProcedureId",
                table: "CustomerSchedule",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_ProcedureId",
                table: "CustomerSchedule",
                column: "ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSchedule_Procedures_ProcedureId",
                table: "CustomerSchedule",
                column: "ProcedureId",
                principalTable: "Procedures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSchedule_Procedures_ProcedureId",
                table: "CustomerSchedule");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSchedule_ProcedureId",
                table: "CustomerSchedule");

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "CustomerSchedule");
        }
    }
}
