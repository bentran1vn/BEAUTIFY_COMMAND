using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class fix_sl123ot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderFeedback_OrderFeedbackId1",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderFeedbackId1",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("064d144d-f2e6-4ac9-9816-04104d32a58d"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("344ff3b8-6ba2-4d74-b892-674b2c42d7ca"));

            migrationBuilder.DropColumn(
                name: "OrderFeedbackId1",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "Staff",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 978, DateTimeKind.Unspecified).AddTicks(1059), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 978, DateTimeKind.Unspecified).AddTicks(1063), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 978, DateTimeKind.Unspecified).AddTicks(1065), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 978, DateTimeKind.Unspecified).AddTicks(1067), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 978, DateTimeKind.Unspecified).AddTicks(1069), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("6f8bb800-0594-4389-9749-f214ef855bdc"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("ab23d158-44e2-44d4-b679-d7c568993702"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b02a28f3-f1a7-4fd7-bcb1-53be587be9f9"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("da2d6a80-75cc-4757-8ed3-e0b508ffb080"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92"),
                column: "Rating",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"),
                column: "Rating",
                value: 4);

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("20193167-f1dc-4172-a4aa-a9fbe4db8660"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("abcc7758-9d65-4905-8391-58056060e272"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8681), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8684), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8686), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8688), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8690), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8693), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8695), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8697), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8699), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(8701), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7895), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7897), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7899), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7900), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7901), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7903), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7904), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7906), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7907), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7909), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 45, 30, 984, DateTimeKind.Unspecified).AddTicks(7183), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderFeedbackId",
                table: "Order",
                column: "OrderFeedbackId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderFeedback_OrderFeedbackId",
                table: "Order",
                column: "OrderFeedbackId",
                principalTable: "OrderFeedback",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_OrderFeedback_OrderFeedbackId",
                table: "Order");

            migrationBuilder.DropIndex(
                name: "IX_Order_OrderFeedbackId",
                table: "Order");

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("20193167-f1dc-4172-a4aa-a9fbe4db8660"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("abcc7758-9d65-4905-8391-58056060e272"));

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Staff");

            migrationBuilder.AddColumn<Guid>(
                name: "OrderFeedbackId1",
                table: "Order",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 960, DateTimeKind.Unspecified).AddTicks(323), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 960, DateTimeKind.Unspecified).AddTicks(329), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 960, DateTimeKind.Unspecified).AddTicks(331), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 960, DateTimeKind.Unspecified).AddTicks(333), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 960, DateTimeKind.Unspecified).AddTicks(336), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("064d144d-f2e6-4ac9-9816-04104d32a58d"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("344ff3b8-6ba2-4d74-b892-674b2c42d7ca"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null }
                });

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1346), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1350), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1353), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1355), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1357), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1362), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1365), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1369), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1374), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(1381), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(266), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(268), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(270), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(271), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(274), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(275), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(277), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(278), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(280), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 971, DateTimeKind.Unspecified).AddTicks(281), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.UpdateData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"),
                column: "CreatedOnUtc",
                value: new DateTimeOffset(new DateTime(2025, 4, 15, 16, 38, 7, 970, DateTimeKind.Unspecified).AddTicks(9181), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateIndex(
                name: "IX_Order_OrderFeedbackId1",
                table: "Order",
                column: "OrderFeedbackId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_OrderFeedback_OrderFeedbackId1",
                table: "Order",
                column: "OrderFeedbackId1",
                principalTable: "OrderFeedback",
                principalColumn: "Id");
        }
    }
}
