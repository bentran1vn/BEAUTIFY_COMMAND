using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_live_entity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LiveStreamDetail_LivestreamRoom_LivestreamRoomId",
                table: "LiveStreamDetail");

            migrationBuilder.DropIndex(
                name: "IX_LiveStreamDetail_LivestreamRoomId",
                table: "LiveStreamDetail");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("252d7744-8d17-4d37-bd42-711320401541"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c0544450-1007-41f6-b29f-aa8d8a39e2f9"));

            migrationBuilder.DropColumn(
                name: "LivestreamRoomId",
                table: "LiveStreamDetail");

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 126, DateTimeKind.Unspecified).AddTicks(9027), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 126, DateTimeKind.Unspecified).AddTicks(9032), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 126, DateTimeKind.Unspecified).AddTicks(9034), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 126, DateTimeKind.Unspecified).AddTicks(9036), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 126, DateTimeKind.Unspecified).AddTicks(9038), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("e3ee6e26-face-4801-b55a-1928fa6e8330"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("fb5cda98-59d3-486b-ab80-baba4ade7635"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6917), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6922), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6923), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6926), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6927), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6929), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6931), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6932), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(6935), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5443), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5446), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5448), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5449), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5501), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5503), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5504), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5506), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5507), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(5508), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 18, 58, 133, DateTimeKind.Unspecified).AddTicks(4645), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_LivestreamRoom_LiveStreamDetailId",
                table: "LivestreamRoom",
                column: "LiveStreamDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_LivestreamRoom_LiveStreamDetail_LiveStreamDetailId",
                table: "LivestreamRoom",
                column: "LiveStreamDetailId",
                principalTable: "LiveStreamDetail",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LivestreamRoom_LiveStreamDetail_LiveStreamDetailId",
                table: "LivestreamRoom");

            migrationBuilder.DropIndex(
                name: "IX_LivestreamRoom_LiveStreamDetailId",
                table: "LivestreamRoom");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e3ee6e26-face-4801-b55a-1928fa6e8330"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("fb5cda98-59d3-486b-ab80-baba4ade7635"));

            migrationBuilder.AddColumn<Guid>(
                name: "LivestreamRoomId",
                table: "LiveStreamDetail",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 560, DateTimeKind.Unspecified).AddTicks(4586), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 560, DateTimeKind.Unspecified).AddTicks(4595), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 560, DateTimeKind.Unspecified).AddTicks(4597), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 560, DateTimeKind.Unspecified).AddTicks(4599), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 560, DateTimeKind.Unspecified).AddTicks(4601), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("252d7744-8d17-4d37-bd42-711320401541"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("c0544450-1007-41f6-b29f-aa8d8a39e2f9"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(424), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(427), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(430), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(432), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(435), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(436), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(438), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(440), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(441), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 582, DateTimeKind.Unspecified).AddTicks(444), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8601), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8604), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8606), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8607), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8609), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8610), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8612), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8613), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8614), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(8616), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 13, 10, 13, 34, 581, DateTimeKind.Unspecified).AddTicks(7507), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_LiveStreamDetail_LivestreamRoomId",
                table: "LiveStreamDetail",
                column: "LivestreamRoomId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_LiveStreamDetail_LivestreamRoom_LivestreamRoomId",
                table: "LiveStreamDetail",
                column: "LivestreamRoomId",
                principalTable: "LivestreamRoom",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
