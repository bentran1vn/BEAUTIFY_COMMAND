using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDateTypeOfEventEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Event_LivestreamRoom_LivestreamRoomId",
                table: "Event");

            migrationBuilder.DropIndex(
                name: "IX_Event_LivestreamRoomId",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Event");

            migrationBuilder.DropColumn(
                name: "LivestreamRoomId",
                table: "Event");

            migrationBuilder.AddColumn<Guid>(
                name: "EventId",
                table: "LivestreamRoom",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "StartDate",
                table: "Event",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "EndDate",
                table: "Event",
                type: "datetimeoffset",
                nullable: true,
                oldClrType: typeof(TimeOnly),
                oldType: "time",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRoom_EventId",
                table: "LivestreamRoom",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivestreamRoom_Event_EventId",
                table: "LivestreamRoom",
                column: "EventId",
                principalTable: "Event",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivestreamRoom_Event_EventId",
                table: "LivestreamRoom");

            migrationBuilder.DropIndex(
                name: "IX_LivestreamRoom_EventId",
                table: "LivestreamRoom");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "LivestreamRoom");

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "StartDate",
                table: "Event",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeOnly>(
                name: "EndDate",
                table: "Event",
                type: "time",
                nullable: true,
                oldClrType: typeof(DateTimeOffset),
                oldType: "datetimeoffset",
                oldNullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "Date",
                table: "Event",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LivestreamRoomId",
                table: "Event",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Event_LivestreamRoomId",
                table: "Event",
                column: "LivestreamRoomId",
                unique: true,
                filter: "[LivestreamRoomId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Event_LivestreamRoom_LivestreamRoomId",
                table: "Event",
                column: "LivestreamRoomId",
                principalTable: "LivestreamRoom",
                principalColumn: "Id");
        }
    }
}
