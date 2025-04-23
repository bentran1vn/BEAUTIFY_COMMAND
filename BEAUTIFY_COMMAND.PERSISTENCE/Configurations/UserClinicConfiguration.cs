using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class UserClinicConfiguration : IEntityTypeConfiguration<UserClinic>
{
    public void Configure(EntityTypeBuilder<UserClinic> builder)
    {
        var userClinics = new List<UserClinic>
        {
            // Clinic Admins - Each Admin manages 1 Main Clinic
            new()
            {
                Id = new Guid("6c330a77-5168-49f3-98ad-b06a25a9c814"),
                ClinicId = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"), // Beauty Center Sài Gòn
                UserId = new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db") // Nguyễn Quốc Bảo
            },
            new()
            {
                Id = new Guid("f3f9e5a7-d0b1-4f8a-8a45-b37a07178e4b"),
                ClinicId = new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"), // Hanoi Beauty Spa
                UserId = new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92") // Hoàng Quang Duy
            },
            new()
            {
                Id = new Guid("d58a7c2d-a9f2-4c9b-bd3e-32126a76f2a5"),
                ClinicId = new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"), // Skin Care Đà Nẵng
                UserId = new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13") // Phạm Đức Nguyên
            },

            // Clinic Staff - Each Clinic Staff manages 2 doctors at Sub Clinics

            // Staff for Beauty Center Sài Gòn (managed by Nguyễn Quốc Bảo)
            new()
            {
                Id = new Guid("b2e0cbc8-1f29-45a1-b0d6-1f67d83c0a7d"),
                ClinicId = new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"), // Beauty Center Sài Gòn - Chi nhánh Quận 1
                UserId = new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32") // ĐỖ Thanh SƠn Quận 1
            },
            new()
            {
                Id = new Guid("7f0c57c5-632a-4241-8425-95e8d1c5bd5a"),
                ClinicId = new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), // Beauty Center Sài Gòn - Chi nhánh Quận 3
                UserId = new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b33") // ĐỖ Thanh SƠn Quận 3
            },

            // Staff for Hanoi Beauty Spa (managed by Hoàng Quang Duy)
            new()
            {
                Id = new Guid("c7b4a5e6-d879-4b5a-9f3e-95e8d1c5bd5b"),
                ClinicId = new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), // Hanoi Beauty Spa - Chi nhánh Đống Đa
                UserId = new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e") // Vũ Thị Thu
            },
            new()
            {
                Id = new Guid("e8d9c5a3-b4d7-4f5a-9e3c-1a2b3c4d5e6f"),
                ClinicId = new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"), // Hanoi Beauty Spa - Chi nhánh Cầu Giấy
                UserId = new Guid(
                    "9dde4ec6-b02f-419a-900b-5c42f1a6c863") // Lê Thị Hương (originally had same name as Ngô Thị Mạnh)
            },

            // Staff for Skin Care Đà Nẵng (managed by Phạm Đức Nguyên)
            new()
            {
                Id = new Guid("f9e8d7c6-b5a4-4c3b-9d2e-1a2b3c4d5e6f"),
                ClinicId = new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), // Skin Care Đà Nẵng - Chi nhánh Hải Châu
                UserId = new Guid("6f8bb800-0594-4389-9749-f214ef855bdc") // Nguyễn Văn Anh
            },
            new()
            {
                Id = new Guid("01b2c3d4-e5f6-4a5b-8c7d-9e8f7a6b5c4d"),
                ClinicId = new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), // Skin Care Đà Nẵng - Chi nhánh Sơn Trà
                UserId = new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd") // Phạm Thị Hà
            },

            // Doctors - Assigned to sub-clinics (2 doctors per staff, 4 per clinic admin, 12 total)

            // Doctors at Beauty Center Sài Gòn - Chi nhánh Quận 1 (managed by Ngô Thị Mạnh)
            new()
            {
                Id = new Guid("06a7b8c9-d0e1-4f2a-3b4c-5d6e7f8a9b0c"),
                ClinicId = new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"),
                UserId = new Guid("ab23d158-44e2-44d4-b679-d7c568993702") // Lê Thị Thanh Đoan
            },
            new()
            {
                Id = new Guid("07b8c9d0-e1f2-4a3b-4c5d-6e7f8a9b0c1d"),
                ClinicId = new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"),
                UserId = new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea") // Phạm Minh Hoàng
            },

            // Doctors at Beauty Center Sài Gòn - Chi nhánh Quận 3 (managed by Trần Thị Thảo)
            new()
            {
                Id = new Guid("08c9d0e1-f2a3-4b4c-5d6e-7f8a9b0c1d2e"),
                ClinicId = new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"),
                UserId = new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea") // Trịnh Thượng Lâm
            },
            new()
            {
                Id = new Guid("09d0e1f2-a3b4-4c5d-6e7f-8a9b0c1d2e3f"),
                ClinicId = new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"),
                UserId = new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87") // Đoàn Thanh Tiến
            },

            // Doctors at Hanoi Beauty Spa - Chi nhánh Đống Đa (managed by Vũ Thị Thu)
            new()
            {
                Id = new Guid("16e7f8a9-b0c1-4d2e-3f4a-5b6c7d8e9f0a"),
                ClinicId = new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"),
                UserId = new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29") // Phan Văn Khoa
            },
            new()
            {
                Id = new Guid("17f8a9b0-c1d2-4e3f-4a5b-6c7d8e9f0a1b"),
                ClinicId = new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"),
                UserId = new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63") // Hoàng Minh Trang
            },

            // Doctors at Hanoi Beauty Spa - Chi nhánh Cầu Giấy (managed by Lê Thị Hương)
            new()
            {
                Id = new Guid("18a9b0c1-d2e3-4f4a-5b6c-7d8e9f0a1b2c"),
                ClinicId = new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"),
                UserId = new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f") // Trần Thanh Long
            },
            new()
            {
                Id = new Guid("19b0c1d2-e3f4-4a5b-6c7d-8e9f0a1b2c3d"),
                ClinicId = new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"),
                UserId = new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733") // Nguyễn Ngọc Mai Hương
            },

            // Doctors at Skin Care Đà Nẵng - Chi nhánh Hải Châu (managed by Nguyễn Văn Anh)
            new()
            {
                Id = new Guid("26c7d8e9-f0a1-4b2c-3d4e-5f6a7b8c9d0e"),
                ClinicId = new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"),
                UserId = new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c") // Võ Anh Quân
            },
            new()
            {
                Id = new Guid("27d8e9f0-a1b2-4c3d-4e5f-6a7b8c9d0e1f"),
                ClinicId = new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"),
                UserId = new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa") // Lê Thị Kim Hoa
            },

            // Doctors at Skin Care Đà Nẵng - Chi nhánh Sơn Trà (managed by Phạm Thị Hà)
            new()
            {
                Id = new Guid("28e9f0a1-b2c3-4d4e-5f6a-7b8c9d0e1f2a"),
                ClinicId = new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"),
                UserId = new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30") // Phạm Tuấn Minh
            },
            new()
            {
                Id = new Guid("29f0a1b2-c3d4-4e5f-6a7b-8c9d0e1f2a3b"),
                ClinicId = new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"),
                UserId = new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283") // Nguyễn Minh Hiếu
            }
        };

        // builder.HasData(userClinics);
    }
}