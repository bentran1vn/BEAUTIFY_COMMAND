using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class IsFirstLoginClinicField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b0592960-5292-4658-b2cf-37cf6e7738ca"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e7b587df-b0ae-4efd-be64-ca097c121bb9"));

            migrationBuilder.AddColumn<bool>(
                name: "IsFirstLogin",
                table: "Clinic",
                type: "bit",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 196, DateTimeKind.Unspecified).AddTicks(6310), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 196, DateTimeKind.Unspecified).AddTicks(6310), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 196, DateTimeKind.Unspecified).AddTicks(6310), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 196, DateTimeKind.Unspecified).AddTicks(6320), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 196, DateTimeKind.Unspecified).AddTicks(6320), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.UpdateData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"),
                column: "IsFirstLogin",
                value: null);

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("e78fb289-abb1-4409-943e-09a0f2e225bd"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("fd1be8ad-9f94-473c-be3a-347b01e2cc02"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8230), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8240), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8240), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8240), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(8240), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7850), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7850), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7860), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7870), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7870), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7870), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 7, 32, 29, 201, DateTimeKind.Unspecified).AddTicks(7520), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e78fb289-abb1-4409-943e-09a0f2e225bd"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("fd1be8ad-9f94-473c-be3a-347b01e2cc02"));

            migrationBuilder.DropColumn(
                name: "IsFirstLogin",
                table: "Clinic");

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 646, DateTimeKind.Unspecified).AddTicks(5081), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 646, DateTimeKind.Unspecified).AddTicks(5087), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 646, DateTimeKind.Unspecified).AddTicks(5090), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 646, DateTimeKind.Unspecified).AddTicks(5093), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 646, DateTimeKind.Unspecified).AddTicks(5096), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("b0592960-5292-4658-b2cf-37cf6e7738ca"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("e7b587df-b0ae-4efd-be64-ca097c121bb9"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6869), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6873), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6876), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6879), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6881), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6884), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6886), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6889), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6892), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(6894), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5721), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5725), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5727), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5729), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5731), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5733), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5735), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5737), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5738), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(5740), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 17, 6, 38, 30, 656, DateTimeKind.Unspecified).AddTicks(4546), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
