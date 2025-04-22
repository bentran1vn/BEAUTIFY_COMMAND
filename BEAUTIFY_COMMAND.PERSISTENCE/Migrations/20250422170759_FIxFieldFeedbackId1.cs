using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class FIxFieldFeedbackId1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedback_CustomerSchedule_CustomerScheduleId",
                table: "Feedback");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("ec3d15b3-9db3-42b5-8c16-6d99069fe2eb"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f204c503-e850-4aa4-93d0-8e747aa20338"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProcedureId",
                table: "ProcedurePriceType",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 287, DateTimeKind.Unspecified).AddTicks(1270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 287, DateTimeKind.Unspecified).AddTicks(1270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 287, DateTimeKind.Unspecified).AddTicks(1280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 287, DateTimeKind.Unspecified).AddTicks(1280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 287, DateTimeKind.Unspecified).AddTicks(1280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("8db805e3-dbf7-47f5-8d9e-5d2343ca77b6"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("face2fc5-390c-48dd-b7d4-1d3b1da8a107"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(30), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(30), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(30), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(30), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(30), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(40), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(60), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(60), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(60), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 292, DateTimeKind.Unspecified).AddTicks(60), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9700), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 17, 7, 59, 291, DateTimeKind.Unspecified).AddTicks(9310), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_ProcedurePriceType_ProcedureId",
                table: "ProcedurePriceType",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSchedule_FeedbackId",
                table: "CustomerSchedule",
                column: "FeedbackId",
                unique: true,
                filter: "[FeedbackId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSchedule_Feedback_FeedbackId",
                table: "CustomerSchedule",
                column: "FeedbackId",
                principalTable: "Feedback",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType",
                column: "ProcedureId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType",
                column: "ProcedureId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSchedule_Feedback_FeedbackId",
                table: "CustomerSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType");

            migrationBuilder.DropForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType");

            migrationBuilder.DropIndex(
                name: "IX_ProcedurePriceType_ProcedureId",
                table: "ProcedurePriceType");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSchedule_FeedbackId",
                table: "CustomerSchedule");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8db805e3-dbf7-47f5-8d9e-5d2343ca77b6"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("face2fc5-390c-48dd-b7d4-1d3b1da8a107"));

            migrationBuilder.DropColumn(
                name: "ProcedureId",
                table: "ProcedurePriceType");

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 502, DateTimeKind.Unspecified).AddTicks(8670), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 502, DateTimeKind.Unspecified).AddTicks(8680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 502, DateTimeKind.Unspecified).AddTicks(8680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 502, DateTimeKind.Unspecified).AddTicks(8680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 502, DateTimeKind.Unspecified).AddTicks(8680), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("ec3d15b3-9db3-42b5-8c16-6d99069fe2eb"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("f204c503-e850-4aa4-93d0-8e747aa20338"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3260), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(3280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2890), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2890), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2910), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 22, 16, 57, 18, 511, DateTimeKind.Unspecified).AddTicks(2540), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_Feedback_CustomerSchedule_CustomerScheduleId",
                table: "Feedback",
                column: "CustomerScheduleId",
                principalTable: "CustomerSchedule",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProcedurePriceType_Procedure_ProcedureId",
                table: "ProcedurePriceType",
                column: "ProcedureId",
                principalTable: "Procedure",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
