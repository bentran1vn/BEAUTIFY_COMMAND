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
                Id = Guid.Parse("d23e467d-d315-44de-bdb0-49a78a520fe5"),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("b5c39c4b-4a90-47ea-bdb7-87edcfcff0e9"),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("bd7be97f-c632-45de-8d4a-b0a38cd04335"),
                DoctorId = Guid.Parse("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), // Phạm Minh Hoàng
                ServiceId = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("6bca7894-b94d-4840-a218-56fcd0803a8b"),
                DoctorId = Guid.Parse("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"), // Phạm Minh Hoàng
                ServiceId = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("38b2c6de-e4eb-4f33-bf66-626563349b87"),
                DoctorId = Guid.Parse("ab23d158-44e2-44d4-b679-d7c568993702"), // Lê Thị Thanh Đoan
                ServiceId = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 5
            },

            // Beauty Center Sài Gòn - Chi nhánh Quận 3
            new DoctorService()
            {
                Id = Guid.Parse("4c0288d1-0289-4e0c-b8b4-b68ad25d713e"),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("c3d459ff-0f1f-4208-8cc3-45c4b8a0c2a3"),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("48f8d39c-44e1-47e3-907d-91c1bc81acb1"),
                DoctorId = Guid.Parse("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), // Đoàn Thanh Tiến
                ServiceId = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("d8c1f4f3-e92e-41db-b8b0-48a4ff5b82e5"),
                DoctorId = Guid.Parse("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"), // Đoàn Thanh Tiến
                ServiceId = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("90f89c34-b439-4874-91f6-7d4e5975b2a7"),
                DoctorId = Guid.Parse("da2d6a80-75cc-4757-8ed3-e0b508ffb080"), // Trịnh Thượng Lâm
                ServiceId = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // BCSG Service 5
            },

            // Hanoi Beauty Spa - Chi nhánh Đống Đa
            new DoctorService()
            {
                Id = Guid.Parse("679d6015-d626-44c9-826d-5b508e13de68"),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("61c90551-2202-4d71-872f-75c9d6e93f07"),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("d43105a5-8252-4b71-b227-f3422261c99d"),
                DoctorId = Guid.Parse("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), // Hoàng Minh Trang
                ServiceId = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("3db20969-d29c-44d9-9395-ff775d82c9a5"),
                DoctorId = Guid.Parse("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"), // Hoàng Minh Trang
                ServiceId = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("c9ad65b7-3e2e-4d07-b5f9-76e5c846c8b0"),
                DoctorId = Guid.Parse("79f207d4-dbbf-488d-848d-fc74a7fdbb29"), // Phan Văn Khoa
                ServiceId = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 5
            },

            // Hanoi Beauty Spa - Chi nhánh Cầu Giấy
            new DoctorService()
            {
                Id = Guid.Parse("50b60f10-1c51-4d9f-8cfb-5abfda601c07"),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("6181eeb3-fb27-4568-8c3e-9db2c50028d0"),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("16b828a7-d18c-4a58-9a61-179a7d8fa1a9"),
                DoctorId = Guid.Parse("bd3c9480-7bca-43d7-94ed-58cea8b32733"), // Nguyễn Ngọc Mai Hương
                ServiceId = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("e8c6a7a2-ea73-433f-888d-b100f8edb3fa"),
                DoctorId = Guid.Parse("bd3c9480-7bca-43d7-94ed-58cea8b32733"), // Nguyễn Ngọc Mai Hương
                ServiceId = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("7cf6932d-f327-46eb-83a1-7745035cbebd"),
                DoctorId = Guid.Parse("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"), // Trần Thanh Long
                ServiceId = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // HBS Service 5
            },

            // Skin Care Đà Nẵng - Chi nhánh Hải Châu
            new DoctorService()
            {
                Id = Guid.Parse("24d7e8bc-7558-4875-bc38-91b26df3de36"),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("4655940b-1167-4f64-8be6-243dcb7b4311"),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("b4a09f65-5b68-4796-9ea6-b07f82574bb6"),
                DoctorId = Guid.Parse("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), // Lê Thị Kim Hoa
                ServiceId = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("6d27ad69-d68b-4977-95d6-487982c87bc3"),
                DoctorId = Guid.Parse("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"), // Lê Thị Kim Hoa
                ServiceId = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("8b9a08e3-cda4-4292-b21c-cd92d1e20719"),
                DoctorId = Guid.Parse("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"), // Võ Anh Quân
                ServiceId = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 5
            },

            // Skin Care Đà Nẵng - Chi nhánh Sơn Trà
            new DoctorService()
            {
                Id = Guid.Parse("be83c87c-7b4b-4fd4-8a88-18bcf6b8e09e"),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 1
            },
            new DoctorService()
            {
                Id = Guid.Parse("24d67f77-5106-4e56-92d4-97d8d17c680d"),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 2
            },
            new DoctorService()
            {
                Id = Guid.Parse("e3d7125f-f2f0-4df0-b6c3-e828b0e37a8e"),
                DoctorId = Guid.Parse("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), // Nguyễn Minh Hiếu
                ServiceId = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 3
            },
            new DoctorService()
            {
                Id = Guid.Parse("1cd46c70-1c69-4188-b3be-5f35c62798cc"),
                DoctorId = Guid.Parse("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"), // Nguyễn Minh Hiếu
                ServiceId = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 4
            },
            new DoctorService()
            {
                Id = Guid.Parse("7a84f0b0-e4da-4935-9e63-b35e5a82d9e4"),
                DoctorId = Guid.Parse("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"), // Phạm Tuấn Minh
                ServiceId = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3"), // SCDN Service 5
            }
        };
        
        builder.HasData(doctorServices);
        
    }
}