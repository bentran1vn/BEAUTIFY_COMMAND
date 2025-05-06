using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BEAUTIFY_COMMAND.PERSISTENCE.Migrations
{
    /// <inheritdoc />
    public partial class seed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedOnUtc", "Description", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình tai", false, true, null, "Phẫu Thuật Tạo Hình Tai", null },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mặt", false, true, null, "Phẫu Thuật Vùng Mặt", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng ngực", false, true, null, "Phẫu Thuật Vùng Ngực", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng bụng", false, true, null, "Phẫu Thuật Vùng Bụng", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mông", false, true, null, "Phẫu Thuật Vùng Mông", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng chân", false, true, null, "Phẫu Thuật Vùng Chân", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật hỗ trợ giảm cân", false, true, null, "Phẫu Thuật Giảm Cân", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình cơ thể", false, true, null, "Phẫu Thuật Tạo Hình Cơ Thể", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình bộ phận sinh dục", false, true, null, "Phẫu Thuật Tạo Hình Bộ Phận Sinh Dục", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Dịch vụ phẫu thuật tạo hình da", false, true, null, "Phẫu Thuật Tạo Hình Da", null }
                });

            migrationBuilder.InsertData(
                table: "Clinic",
                columns: new[] { "Id", "AdditionBranches", "AdditionLivestreams", "Address", "Balance", "BankAccountNumber", "BankName", "BusinessLicenseUrl", "City", "CreatedOnUtc", "District", "Email", "IsActivated", "IsDeleted", "IsFirstLogin", "IsParent", "ModifiedOnUtc", "Name", "Note", "OperatingLicenseExpiryDate", "OperatingLicenseUrl", "ParentId", "PhoneNumber", "ProfilePictureUrl", "Status", "TaxCode", "TotalApply", "TotalBranches", "Ward", "WorkingTimeEnd", "WorkingTimeStart" },
                values: new object[,]
                {
                    { new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), 0, 0, null, 0m, "1234567890123", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.saigon@gmail.com", false, false, null, true, null, "Beauty Center Sài Gòn", null, null, "https://storage.googleapis.com/licenses/operating-license-1.pdf", null, "0283456789", "https://res.cloudinary.com/dmiueqpah/image/upload/v1744138052/1-1711946463238508154235_eakppa.jpg", 1, "12345678901", 0, 2, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), 0, 0, null, 0m, "2345678901234", "BIDV", "https://storage.googleapis.com/licenses/business-license-2.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.beautyspa@gmail.com", false, false, null, true, null, "Hanoi Beauty Spa", null, null, "https://storage.googleapis.com/licenses/operating-license-2.pdf", null, "0243812345", null, 1, "23456789012", 0, 2, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), 0, 0, null, 0m, "3456789012345", "Agribank", "https://storage.googleapis.com/licenses/business-license-3.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.danang@gmail.com", false, false, null, true, null, "Skin Care Đà Nẵng", null, null, "https://storage.googleapis.com/licenses/operating-license-3.pdf", null, "0236789123", null, 1, "34567890123", 0, 2, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Staff",
                columns: new[] { "Id", "Address", "City", "CreatedOnUtc", "DateOfBirth", "District", "Email", "FirstName", "IsDeleted", "LastName", "ModifiedOnUtc", "Password", "PhoneNumber", "ProfilePicture", "Rating", "RefreshToken", "RoleId", "Status", "Ward" },
                values: new object[,]
                {
                    { new Guid("20825745-09d5-4900-b6ee-fa68bb340b4a"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "staff@gmail.com", "System", false, "Staff", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), 1, null },
                    { new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hoangminhtrang@gmail.com", "Hoàng Minh", false, "Trang", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "voanhquan@gmail.com", "Võ Anh", false, "Quân", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("6f8bb800-0594-4389-9749-f214ef855bdc"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenvananh@gmail.com", "Nguyễn Văn", false, "Anh", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "lethikimhoa@gmail.com", "Lê Thị Kim", false, "Hoa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phanvankhoa@gmail.com", "Phan Văn", false, "Khoa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "doanthanhtien@gmail.com", "Đoàn Thanh", false, "Tiến", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "lethihuong@gmail.com", "Lê Thị", false, "Hương", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "skincare.danang@gmail.com", "Skin Care Đà Nẵng", false, "Skin Care Đà Nẵng", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "trandinhthientan@gmail.com", "Trần Đình Thiên", false, "Tân", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamthiha@gmail.com", "Phạm Thị", false, "Hà", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("ab23d158-44e2-44d4-b679-d7c568993702"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamphucnghi@gmail.com", "Phạm Phúc", false, "Nghị", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("b02a28f3-f1a7-4fd7-bcb1-53be587be9f9"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "caoanhdung@gmail.com", "Cao Anh", false, "Dũng", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "vuthithu@gmail.com", "Vũ Thị", false, "Thu", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenngocmaihuong@gmail.com", "Nguyễn Ngọc Mai", false, "Hương", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "tranthanhlong@gmail.com", "Trần Thanh", false, "Long", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "beautycenter.saigon@gmail.com", "Beauty Center Sài Gòn", false, "Beauty Center Sài Gòn", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "dothanhsonquan1@gmail.com", "Đỗ Thanh", false, "Sơn Quận 1", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "dothanhsonquan3@gmail.com", "Đỗ Thanh", false, "Sơn Quận 3", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), 1, null },
                    { new Guid("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "trinhthuonglam@gmail.com", "Trịnh Thượng", false, "Lâm", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("de36112f-49d4-4e7a-9960-00b0a919fed0"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "admin@gmail.com", "System", false, "Admin", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), 1, null },
                    { new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "phamtuanminh@gmail.com", "Phạm Tuấn", false, "Minh", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null },
                    { new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "hanoi.beautyspa@gmail.com", "Hanoi Beauty Spa", false, "Hanoi Beauty Spa", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), 1, null },
                    { new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), null, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, null, "nguyenminhhieu@gmail.com", "Nguyễn Minh", false, "Hiếu", null, "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=", null, null, 4, null, new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), 1, null }
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "CreatedOnUtc", "Description", "IsDeleted", "IsParent", "ModifiedOnUtc", "Name", "ParentId" },
                values: new object[,]
                {
                    { new Guid("12121212-1212-1212-1212-121212121212"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Điều chỉnh hình dáng mũi để cân đối với khuôn mặt", false, false, null, "Nâng Mũi (Rhinoplasty)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("13131313-1313-1313-1313-131313131313"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ da thừa, mỡ thừa ở mí mắt, giúp mắt to và trẻ trung hơn", false, false, null, "Cắt Mí Mắt (Blepharoplasty)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("14141414-1414-1414-1414-141414141414"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Cải thiện vùng trán và cung mày, giảm nếp nhăn", false, false, null, "Nâng Cung Mày (Brow Lift)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("15151515-1515-1515-1515-151515151515"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Tạo hình cằm cân đối với khuôn mặt", false, false, null, "Độn Cằm (Chin Augmentation)", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("16161616-1616-1616-1616-161616161616"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ mỡ thừa ở vùng mặt như má, cằm", false, false, null, "Hút Mỡ Mặt", new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("17171717-1717-1717-1717-171717171717"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Sử dụng túi độn hoặc mỡ tự thân để tăng kích thước ngực", false, false, null, "Nâng Ngực (Breast Augmentation)", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("18181818-1818-1818-1818-181818181818"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Giảm kích thước ngực quá lớn", false, false, null, "Thu Nhỏ Ngực (Breast Reduction)", new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("19191919-1919-1919-1919-191919191919"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ mỡ thừa ở vùng bụng", false, false, null, "Hút Mỡ Bụng", new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("20202020-2020-2020-2020-202020202020"), new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), "Loại bỏ da thừa và mỡ, làm săn chắc vùng bụng", false, false, null, "Căng Da Bụng", new Guid("33333333-3333-3333-3333-333333333333") }
                });

            migrationBuilder.InsertData(
                table: "Clinic",
                columns: new[] { "Id", "AdditionBranches", "AdditionLivestreams", "Address", "Balance", "BankAccountNumber", "BankName", "BusinessLicenseUrl", "City", "CreatedOnUtc", "District", "Email", "IsActivated", "IsDeleted", "IsFirstLogin", "IsParent", "ModifiedOnUtc", "Name", "Note", "OperatingLicenseExpiryDate", "OperatingLicenseUrl", "ParentId", "PhoneNumber", "ProfilePictureUrl", "Status", "TaxCode", "TotalApply", "TotalBranches", "Ward", "WorkingTimeEnd", "WorkingTimeStart" },
                values: new object[,]
                {
                    { new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), 0, 0, null, 0m, "3456789012346", "Agribank", "https://storage.googleapis.com/licenses/business-license-3-1.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.haichau@gmail.com", false, false, null, false, null, "Skin Care Đà Nẵng - Chi nhánh Hải Châu", null, null, "https://storage.googleapis.com/licenses/operating-license-3-1.pdf", new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), "0236789111", null, 1, "34567890124", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), 0, 0, null, 0m, "1234567890125", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1-2.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.q3@gmail.com", false, false, null, false, null, "Beauty Center Sài Gòn - Chi nhánh Quận 3", null, null, "https://storage.googleapis.com/licenses/operating-license-1-2.pdf", new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), "0283456222", null, 1, "12345678903", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), 0, 0, null, 0m, "3456789012347", "Agribank", "https://storage.googleapis.com/licenses/business-license-3-2.pdf", "Đà Nẵng", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "skincare.sontra@gmail.com", false, false, null, false, null, "Skin Care Đà Nẵng - Chi nhánh Sơn Trà", null, null, "https://storage.googleapis.com/licenses/operating-license-3-2.pdf", new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), "0236789222", null, 1, "34567890125", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), 0, 0, null, 0m, "1234567890124", "Vietcombank", "https://storage.googleapis.com/licenses/business-license-1-1.pdf", "Hồ Chí Minh", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "beautycenter.q1@gmail.com", false, false, null, false, null, "Beauty Center Sài Gòn - Chi nhánh Quận 1", null, null, "https://storage.googleapis.com/licenses/operating-license-1-1.pdf", new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), "0283456111", null, 1, "12345678902", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), 0, 0, null, 0m, "2345678901236", "BIDV", "https://storage.googleapis.com/licenses/business-license-2-2.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.caugiay@gmail.com", false, false, null, false, null, "Hanoi Beauty Spa - Chi nhánh Cầu Giấy", null, null, "https://storage.googleapis.com/licenses/operating-license-2-2.pdf", new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), "0243812222", "https://res.cloudinary.com/dmiueqpah/image/upload/v1744138051/hinh-AA-Clinic-lgo-moi-1-1_smg56o.jpg", 1, "23456789014", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) },
                    { new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), 0, 0, null, 0m, "2345678901235", "BIDV", "https://storage.googleapis.com/licenses/business-license-2-1.pdf", "Hà Nội", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "hanoi.dongda@gmail.com", false, false, null, false, null, "Hanoi Beauty Spa - Chi nhánh Đống Đa", null, null, "https://storage.googleapis.com/licenses/operating-license-2-1.pdf", new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), "0243812111", "https://res.cloudinary.com/dvadlh7ah/image/upload/v1744178257/ty7jok5ooenrha5aydid.jpg", 1, "23456789013", 0, 0, null, new TimeSpan(0, 20, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Surveys",
                columns: new[] { "Id", "CategoryId", "CreatedOnUtc", "Description", "IsDeleted", "ModifiedOnUtc", "Name" },
                values: new object[] { new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("20202020-2020-2020-2020-202020202020"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(8114), new TimeSpan(0, 0, 0, 0, 0)), "Nhận biết loại da", false, null, "Khảo sát da" });

            migrationBuilder.InsertData(
                table: "SurveyQuestions",
                columns: new[] { "Id", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "Question", "QuestionType", "SurveyId" },
                values: new object[,]
                {
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9060), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Sau khi rửa mặt (không bôi kem) da bạn thường cảm thấy thế nào?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9064), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Vào giữa ngày da bạn trông thế nào (nếu không thấm dầu)?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9065), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Tần suất bong tróc hoặc khô mảng?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9067), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Mức độ nhìn thấy lỗ chân lông?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9068), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Bạn có thường bị mụn hoặc tắc nghẽn lỗ chân lông?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9070), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Da bạn có khi nào vừa khô ở vài chỗ vừa dầu ở chỗ khác?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9073), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Phản ứng da khi dùng sản phẩm mới hoặc thời tiết thay đổi?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9075), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Nếu bỏ qua kem dưỡng một ngày da bạn thế nào?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9076), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Khi trang điểm lớp nền giữ trên da ra sao?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") },
                    { new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9077), new TimeSpan(0, 0, 0, 0, 0)), false, null, "Tổng quát, câu mô tả nào hợp nhất với da bạn?", "Multiple Choice", new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345") }
                });

            migrationBuilder.InsertData(
                table: "ClassificationRules",
                columns: new[] { "Id", "ClassificationLabel", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "OptionValue", "Points", "SurveyId", "SurveyQuestionId" },
                values: new object[,]
                {
                    { new Guid("33333333-1111-1111-1111-111111111111"), "Da khô", new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 40, DateTimeKind.Unspecified).AddTicks(768), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-2222-1111-1111-111111111111"), "Da thường", new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 40, DateTimeKind.Unspecified).AddTicks(774), new TimeSpan(0, 0, 0, 0, 0)), false, null, "B", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-3333-1111-1111-111111111111"), "Da hỗn hợp", new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 40, DateTimeKind.Unspecified).AddTicks(778), new TimeSpan(0, 0, 0, 0, 0)), false, null, "C", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-4444-1111-1111-111111111111"), "Da dầu", new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 40, DateTimeKind.Unspecified).AddTicks(779), new TimeSpan(0, 0, 0, 0, 0)), false, null, "D", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("33333333-5555-1111-1111-111111111111"), "Da nhạy cảm", new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 40, DateTimeKind.Unspecified).AddTicks(781), new TimeSpan(0, 0, 0, 0, 0)), false, null, "E", 2, new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"), new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") }
                });

            migrationBuilder.InsertData(
                table: "SurveyQuestionOption",
                columns: new[] { "Id", "CreatedOnUtc", "IsDeleted", "ModifiedOnUtc", "Option", "SurveyQuestionId" },
                values: new object[,]
                {
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9964), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất căng khô hoặc bong tróc; B) Khá cân bằng không quá khô hay dầu; C) Hơi bóng ở vùng chữ T; D) Bóng dầu toàn mặt; E) Đỏ hoặc châm chích", new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9967), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Không hầu như chỉ khô; B) Không khá đồng đều; C) Thường khô ở má nhưng dầu vùng chữ T; D) Oily toàn mặt; E) Thay đổi theo độ nhạy cảm", new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9970), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất nhỏ hoặc gần như không thấy; B) Thấy ở mức vừa phải; C) Rõ hơn ở vùng chữ T; D) To và dễ thấy toàn mặt; E) Rõ hơn khi da ửng đỏ hoặc kích ứng", new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9972), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất căng và khó chịu; B) Khá bình thường; C) T-zone bóng má bình thường; D) Rất bóng hoặc nhờn; E) Đỏ hoặc ngứa", new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9974), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Khô hơn hoặc bong tróc; B) Thích nghi khá ổn; C) Có vùng dầu vùng không; D) Tăng tiết dầu nổi mụn; E) Kích ứng ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9984), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Thường xuyên; B) Hầu như không bao giờ; C) Thỉnh thoảng ở một số vùng; D) Rất hiếm; E) Do nhạy cảm với sản phẩm hoặc thời tiết", new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9986), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Dễ bám vào vùng khô; B) Khá đều cần ít dặm lại; C) Xuống tông hoặc bóng ở chữ T; D) Trôi hoặc bóng dầu gần như toàn mặt; E) Kích ứng hoặc ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9988), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất hiếm; B) Đôi khi; C) Chủ yếu ở vùng chữ T; D) Thường xuyên hoặc toàn mặt; E) Phụ thuộc độ nhạy cảm với sản phẩm", new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9989), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Rất khô và hay căng; B) Cân bằng không quá khô dầu; C) Vừa dầu vừa khô da hỗn hợp; D) Dầu toàn mặt; E) Rất nhạy cảm hoặc dễ kích ứng", new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999") },
                    { new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"), new DateTimeOffset(new DateTime(2025, 5, 5, 14, 1, 26, 46, DateTimeKind.Unspecified).AddTicks(9992), new TimeSpan(0, 0, 0, 0, 0)), false, null, "A) Vẫn khô hoặc căng; B) Khá cân bằng ít bóng; C) Có chút bóng ở vùng chữ T; D) Bóng dầu toàn khuôn mặt; E) Dễ kích ứng hoặc ửng đỏ", new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("12121212-1212-1212-1212-121212121212"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("13131313-1313-1313-1313-131313131313"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("14141414-1414-1414-1414-141414141414"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("15151515-1515-1515-1515-151515151515"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("16161616-1616-1616-1616-161616161616"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("17171717-1717-1717-1717-171717171717"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("18181818-1818-1818-1818-181818181818"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("19191919-1919-1919-1919-191919191919"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-2222-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-4444-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "ClassificationRules",
                keyColumn: "Id",
                keyValue: new Guid("33333333-5555-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("20825745-09d5-4900-b6ee-fa68bb340b4a"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("6f8bb800-0594-4389-9749-f214ef855bdc"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("ab23d158-44e2-44d4-b679-d7c568993702"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b02a28f3-f1a7-4fd7-bcb1-53be587be9f9"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("da2d6a80-75cc-4757-8ed3-e0b508ffb080"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("de36112f-49d4-4e7a-9960-00b0a919fed0"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92"));

            migrationBuilder.DeleteData(
                table: "Staff",
                keyColumn: "Id",
                keyValue: new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a11"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a12"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a13"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a14"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a15"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a16"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a17"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a18"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a19"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestionOption",
                keyColumn: "Id",
                keyValue: new Guid("a0eebc99-9c0b-4ef8-bb6d-6bb9bd380a1a"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("78705cfa-7097-408f-93e2-70950fc886a3"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"));

            migrationBuilder.DeleteData(
                table: "Clinic",
                keyColumn: "Id",
                keyValue: new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-111111111111"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-222222222222"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-333333333333"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-444444444444"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-555555555555"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-666666666666"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-777777777777"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-888888888888"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-999999999999"));

            migrationBuilder.DeleteData(
                table: "SurveyQuestions",
                keyColumn: "Id",
                keyValue: new Guid("d1a2c3b4-e5f6-4789-abcd-aaaaaaaaaaaa"));

            migrationBuilder.DeleteData(
                table: "Surveys",
                keyColumn: "Id",
                keyValue: new Guid("f3e2c9f4-9d1b-4d3f-98a6-2a7fabc12345"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("20202020-2020-2020-2020-202020202020"));

            migrationBuilder.DeleteData(
                table: "Category",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));
        }
    }
}
