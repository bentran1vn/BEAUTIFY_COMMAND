using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_12343 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("78cb9c49-7abb-4400-9788-d8f32aef715d"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c865c191-4aa1-47de-a7ac-3f2e60695332"));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 273, DateTimeKind.Unspecified).AddTicks(196), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 273, DateTimeKind.Unspecified).AddTicks(202), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 273, DateTimeKind.Unspecified).AddTicks(204), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 273, DateTimeKind.Unspecified).AddTicks(205), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 273, DateTimeKind.Unspecified).AddTicks(207), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("a1a0954a-f93e-49f0-9b7d-77aaa02a66b7"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("c61cbfdd-e1cf-457d-86f6-bf2b7dedda12"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6744), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6747), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6749), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6751), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6752), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6755), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6756), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6757), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6762), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(6765), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5985), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5987), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5988), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5990), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5991), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5993), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5994), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5995), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5997), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5998), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 34, 19, 279, DateTimeKind.Unspecified).AddTicks(5229), new TimeSpan(0, 0, 0, 0, 0)));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a1a0954a-f93e-49f0-9b7d-77aaa02a66b7"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c61cbfdd-e1cf-457d-86f6-bf2b7dedda12"));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 91, DateTimeKind.Unspecified).AddTicks(6951), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 91, DateTimeKind.Unspecified).AddTicks(6955), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 91, DateTimeKind.Unspecified).AddTicks(6957), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 91, DateTimeKind.Unspecified).AddTicks(6959), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 91, DateTimeKind.Unspecified).AddTicks(6961), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("78cb9c49-7abb-4400-9788-d8f32aef715d"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("c865c191-4aa1-47de-a7ac-3f2e60695332"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2910), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2913), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2915), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2918), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2920), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2922), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2924), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2925), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2926), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2929), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2100), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2102), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2104), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2105), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2107), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2111), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2112), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2114), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2115), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(2117), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 18, 16, 29, 15, 98, DateTimeKind.Unspecified).AddTicks(1350), new TimeSpan(0, 0, 0, 0, 0)));
        }
    }
}
