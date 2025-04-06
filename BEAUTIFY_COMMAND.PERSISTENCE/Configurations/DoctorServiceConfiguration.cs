using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class DoctorServiceConfiguration: IEntityTypeConfiguration<DoctorService>
{
    public void Configure(EntityTypeBuilder<DoctorService> builder)
    {
        var doctorServices = new List<DoctorService>()
        {
            // Beauty Center Sài Gòn - Chi nhánh Quận 1
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), // Phạm Minh Hoàng
                ServiceId = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), // Phạm Minh Hoàng
                ServiceId = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 5
            },

            // Beauty Center Sài Gòn - Chi nhánh Quận 3
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), // Đoàn Thanh Tiến
                ServiceId = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), // Đoàn Thanh Tiến
                ServiceId = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 5
            },

            // Hanoi Beauty Spa - Chi nhánh Đống Đa
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), // Hoàng Minh Trang
                ServiceId = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), // Hoàng Minh Trang
                ServiceId = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 5
            },

            // Hanoi Beauty Spa - Chi nhánh Cầu Giấy
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("bd3c9480-7bca-43d7-94ed-58cea8b32733"), // Nguyễn Ngọc Mai Hương
                ServiceId = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("bd3c9480-7bca-43d7-94ed-58cea8b32733"), // Nguyễn Ngọc Mai Hương
                ServiceId = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 5
            },

            // Skin Care Đà Nẵng - Chi nhánh Hải Châu
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), // Lê Thị Kim Hoa
                ServiceId = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), // Lê Thị Kim Hoa
                ServiceId = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 5
            },

            // Skin Care Đà Nẵng - Chi nhánh Sơn Trà
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 1
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 2
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), // Nguyễn Minh Hiếu
                ServiceId = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 3
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), // Nguyễn Minh Hiếu
                ServiceId = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 4
            },
            new DoctorService()
            {
                Id = Guid.NewGuid(),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 5
            }
        };

        builder.HasData(doctorServices);
        
    }
}