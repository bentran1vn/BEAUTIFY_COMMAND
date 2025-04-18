using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class ProcedurePriceTypeConfiguration : IEntityTypeConfiguration<ProcedurePriceType>
{
    public void Configure(EntityTypeBuilder<ProcedurePriceType> builder)
    {
        // Beauty Center Sài Gòn Service 1 - Nâng Mũi Cấu Trúc S-Line Premium
        var idBCSG_S1_P1 = Guid.Parse("a1d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S1_P2 = Guid.Parse("a1d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S1_P3 = Guid.Parse("a1d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S1_P4 = Guid.Parse("a1d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S1_P5 = Guid.Parse("a1d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Beauty Center Sài Gòn Service 2 - Cắt Mí Mắt Plasma Luxury
        var idBCSG_S2_P1 = Guid.Parse("a2d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S2_P2 = Guid.Parse("a2d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S2_P3 = Guid.Parse("a2d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S2_P4 = Guid.Parse("a2d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Beauty Center Sài Gòn Service 3 - Nâng Ngực Túi Độn Mentor
        var idBCSG_S3_P1 = Guid.Parse("a3d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S3_P2 = Guid.Parse("a3d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S3_P3 = Guid.Parse("a3d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S3_P4 = Guid.Parse("a3d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S3_P5 = Guid.Parse("a3d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Beauty Center Sài Gòn Service 4 - Độn Cằm V-Line Hàn Quốc
        var idBCSG_S4_P1 = Guid.Parse("a4d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S4_P2 = Guid.Parse("a4d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S4_P3 = Guid.Parse("a4d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S4_P4 = Guid.Parse("a4d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Beauty Center Sài Gòn Service 5 - Hút Mỡ Bụng VASER Lipo
        var idBCSG_S5_P1 = Guid.Parse("a5d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S5_P2 = Guid.Parse("a5d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S5_P3 = Guid.Parse("a5d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S5_P4 = Guid.Parse("a5d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_S5_P5 = Guid.Parse("a5d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite
        var idHBS_S1_P1 = Guid.Parse("b1d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S1_P2 = Guid.Parse("b1d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S1_P3 = Guid.Parse("b1d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S1_P4 = Guid.Parse("b1d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo
        var idHBS_S2_P1 = Guid.Parse("b2d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S2_P2 = Guid.Parse("b2d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S2_P3 = Guid.Parse("b2d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S2_P4 = Guid.Parse("b2d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu
        var idHBS_S3_P1 = Guid.Parse("b3d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S3_P2 = Guid.Parse("b3d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S3_P3 = Guid.Parse("b3d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S3_P4 = Guid.Parse("b3d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar
        var idHBS_S4_P1 = Guid.Parse("b4d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S4_P2 = Guid.Parse("b4d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S4_P3 = Guid.Parse("b4d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S4_P4 = Guid.Parse("b4d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S4_P5 = Guid.Parse("b4d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Service 5 - Hút Mỡ Mặt Precision
        var idHBS_S5_P1 = Guid.Parse("b5d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S5_P2 = Guid.Parse("b5d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S5_P3 = Guid.Parse("b5d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_S5_P4 = Guid.Parse("b5d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Service 1 - Nâng Mũi Cấu Trúc Hybrid
        var idSCDN_S1_P1 = Guid.Parse("c1d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S1_P2 = Guid.Parse("c1d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S1_P3 = Guid.Parse("c1d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S1_P4 = Guid.Parse("c1d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S1_P5 = Guid.Parse("c1d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Service 2 - Nâng Ngực Nội Soi Ergonomix
        var idSCDN_S2_P1 = Guid.Parse("c2d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S2_P2 = Guid.Parse("c2d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S2_P3 = Guid.Parse("c2d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S2_P4 = Guid.Parse("c2d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S2_P5 = Guid.Parse("c2d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Service 3 - Hút Mỡ Bụng 3D Hi-Definition
        var idSCDN_S3_P1 = Guid.Parse("c3d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S3_P2 = Guid.Parse("c3d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S3_P3 = Guid.Parse("c3d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S3_P4 = Guid.Parse("c3d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S3_P5 = Guid.Parse("c3d3d799-1005-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Service 4 - Cắt Mí Mắt Plasma Tech
        var idSCDN_S4_P1 = Guid.Parse("c4d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S4_P2 = Guid.Parse("c4d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S4_P3 = Guid.Parse("c4d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S4_P4 = Guid.Parse("c4d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Service 5 - Độn Cằm 3D Crystal
        var idSCDN_S5_P1 = Guid.Parse("c5d3d799-1001-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S5_P2 = Guid.Parse("c5d3d799-1002-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S5_P3 = Guid.Parse("c5d3d799-1003-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_S5_P4 = Guid.Parse("c5d3d799-1004-4a2f-b6b5-3d05b7e1f2a3");

        var procedurePriceTypes = new List<ProcedurePriceType>
        {
            // Beauty Center Sài Gòn Service 1 - Nâng Mũi Cấu Trúc S-Line Premium - Procedure 1
            new()
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P1
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn chuyên sâu",
                Price = 1000000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P1
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0003-3d05b7e1f2a3"),
                Name = "Tư vấn VIP kèm mô phỏng 3D",
                Price = 2000000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P1
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 2
            new()
            {
                Id = Guid.Parse("a1d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 1500000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P2
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 2500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P2
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 3
            new()
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật cơ bản",
                Price = 25000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P3
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật cao cấp",
                Price = 30000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P3
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật VIP với bác sĩ chuyên gia",
                Price = 40000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P3
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 4
            new()
            {
                Id = Guid.Parse("a1d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu tiêu chuẩn",
                Price = 1000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idBCSG_S1_P4
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu VIP",
                Price = 2500000,
                Duration = 1440, // 24 hours
                IsDefault = false,
                ProcedureId = idBCSG_S1_P4
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 5
            new()
            {
                Id = Guid.Parse("a1d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói tái khám cơ bản",
                Price = 2000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P5
            },
            new()
            {
                Id = Guid.Parse("a1d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói tái khám VIP",
                Price = 3500000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P5
            },

            // Beauty Center Sài Gòn Service 2 - Cắt Mí Mắt Plasma Luxury - Procedure 1
            new()
            {
                Id = Guid.Parse("a2d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế mí cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P1
            },
            new()
            {
                Id = Guid.Parse("a2d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế mí chuyên sâu",
                Price = 900000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P1
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 2
            new()
            {
                Id = Guid.Parse("a2d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra tiêu chuẩn",
                Price = 800000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P2
            },
            new()
            {
                Id = Guid.Parse("a2d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra nâng cao",
                Price = 1200000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P2
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 3
            new()
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma tiêu chuẩn",
                Price = 10000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P3
            },
            new()
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma cao cấp",
                Price = 15000000,
                Duration = 150,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P3
            },
            new()
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma Luxury",
                Price = 18000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P3
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 4
            new()
            {
                Id = Guid.Parse("a2d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc hậu phẫu cơ bản",
                Price = 1500000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P4
            },
            new()
            {
                Id = Guid.Parse("a2d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc hậu phẫu cao cấp",
                Price = 2500000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P4
            },

            // Beauty Center Sài Gòn Service 3 - Nâng Ngực Túi Độn Mentor - Procedure 1
            new()
            {
                Id = Guid.Parse("a3d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn và lựa chọn túi ngực tiêu chuẩn",
                Price = 1000000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P1
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn và lựa chọn túi ngực cao cấp kèm mô phỏng 3D",
                Price = 2500000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P1
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 2
            new()
            {
                Id = Guid.Parse("a3d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 2000000,
                Duration = 150,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P2
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 3500000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P2
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 3
            new()
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor cơ bản",
                Price = 65000000,
                Duration = 240,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P3
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor cao cấp",
                Price = 75000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P3
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor VIP",
                Price = 85000000,
                Duration = 300,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P3
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 4
            new()
            {
                Id = Guid.Parse("a3d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu tiêu chuẩn",
                Price = 2000000,
                Duration = 2880, // 48 hours
                IsDefault = true,
                ProcedureId = idBCSG_S3_P4
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu VIP",
                Price = 5000000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idBCSG_S3_P4
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 5
            new()
            {
                Id = Guid.Parse("a3d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói tái khám định kỳ tiêu chuẩn",
                Price = 3000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P5
            },
            new()
            {
                Id = Guid.Parse("a3d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói tái khám định kỳ VIP",
                Price = 5000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P5
            },

            // Beauty Center Sài Gòn Service 4 - Độn Cằm V-Line Hàn Quốc - Procedure 1
            new()
            {
                Id = Guid.Parse("a4d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế cằm V-Line cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P1
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế cằm V-Line chuyên sâu với mô phỏng 3D",
                Price = 1500000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P1
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 2
            new()
            {
                Id = Guid.Parse("a4d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 1200000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P2
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 2000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P2
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 3
            new()
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line cơ bản",
                Price = 25000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P3
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line cao cấp",
                Price = 28000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P3
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line VIP",
                Price = 32000000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P3
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 4
            new()
            {
                Id = Guid.Parse("a4d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám tiêu chuẩn",
                Price = 2000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P4
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám cao cấp",
                Price = 3500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P4
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 1
            new()
            {
                Id = Guid.Parse("a4d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn đánh giá vùng mỡ bụng tiêu chuẩn",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P1
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn đánh giá vùng mỡ bụng chuyên sâu với chụp hình 3D",
                Price = 1500000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P1
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 2
            new()
            {
                Id = Guid.Parse("a4d3d799-1006-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn trước phẫu thuật",
                Price = 1500000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P2
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1006-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện trước phẫu thuật",
                Price = 2500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P2
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 3
            new()
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0001-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo vùng nhỏ",
                Price = 30000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P3
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0002-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo vùng trung bình",
                Price = 45000000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P3
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0003-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo toàn vùng bụng",
                Price = 60000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P3
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 4
            new()
            {
                Id = Guid.Parse("a4d3d799-1008-4a2f-0001-3d05b7e1f2a3"),
                Name = "Liệu trình massage định hình cơ bản (5 buổi)",
                Price = 5000000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P4
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1008-4a2f-0002-3d05b7e1f2a3"),
                Name = "Liệu trình massage định hình cao cấp (10 buổi)",
                Price = 9000000,
                Duration = 60,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P4
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 5
            new()
            {
                Id = Guid.Parse("a4d3d799-1009-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám tiêu chuẩn (3 tháng)",
                Price = 3000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P5
            },
            new()
            {
                Id = Guid.Parse("a4d3d799-1009-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám cao cấp (6 tháng)",
                Price = 5500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P5
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 1
            new()
            {
                Id = Guid.Parse("b1d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Dáng Mũi Cá Nhân Hóa",
                Price = 12000000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P1,
                Duration = 60
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Thiết Kế Dáng Mũi 3D Chuyên Sâu",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P1,
                Duration = 90
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1113-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Quy Trình Thiết Kế VIP Cùng Chuyên Gia Hàn Quốc",
                Price = 25000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P1,
                Duration = 120
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 2
            new()
            {
                Id = Guid.Parse("b1d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Đánh Giá Sức Khỏe Pre-Surgery Chuẩn Y Khoa",
                Price = 1500000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P2,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Gói Kiểm Tra Toàn Diện Với Bác Sĩ Chuyên Khoa",
                Price = 2500000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P2,
                Duration = 45
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 3
            new()
            {
                Id = Guid.Parse("b1d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Natural Look Với Sụn Tự Thân",
                Price = 20000000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P3,
                Duration = 120
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Perfect Balance - Công Nghệ Kết Hợp",
                Price = 30000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P3,
                Duration = 150
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Luxury Elite - Công Nghệ Hàn Quốc Độc Quyền",
                Price = 45000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P3,
                Duration = 180
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 4
            new()
            {
                Id = Guid.Parse("b1d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Nhanh - Gói Chăm Sóc Thiết Yếu",
                Price = 500000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P4,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b1d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Golden Care - Gói Phục Hồi Toàn Diện Cao Cấp",
                Price = 1200000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P4,
                Duration = 60
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 1
            new()
            {
                Id = Guid.Parse("b2d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Mí Quyến Rũ - Gói Cơ Bản",
                Price = 8000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P1,
                Duration = 60
            },
            new()
            {
                Id = Guid.Parse("b2d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Mí Tự Nhiên 3D - Gói Cao Cấp",
                Price = 12000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P1,
                Duration = 75
            },
            new()
            {
                Id = Guid.Parse("b2d3d799-1113-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Celebrity Eyes - Tư Vấn VIP Cùng Chuyên Gia Hàn Quốc",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P1,
                Duration = 90
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 2
            new()
            {
                Id = Guid.Parse("b2d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Sức Khỏe An Toàn Trước Phẫu Thuật",
                Price = 1000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P2,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b2d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Đánh Giá Sức Khỏe Toàn Diện Premium",
                Price = 2000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P2,
                Duration = 45
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 3
            new()
            {
                Id = Guid.Parse("b2d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Mí Tự Nhiên - Công Nghệ Không Sẹo Hàn Quốc",
                Price = 15000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P3,
                Duration = 90
            },
            new()
            {
                Id = Guid.Parse("b2d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Double-Fold Premium - Kỹ Thuật Chuyên Gia",
                Price = 22000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P3,
                Duration = 120
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 4
            new()
            {
                Id = Guid.Parse("b2d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Nhanh - Chăm Sóc Sau Phẫu Thuật",
                Price = 800000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P4,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b2d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tái Sinh Siêu Tốc - Liệu Trình Phục Hồi Cao Cấp",
                Price = 1500000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P4,
                Duration = 45
            },

            new()
            {
                Id = Guid.Parse("b3d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Định Hình Cung Mày Cá Nhân Hóa",
                Price = 5000000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P1,
                Duration = 45
            },
            new()
            {
                Id = Guid.Parse("b3d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phân Tích & Thiết Kế Cung Mày 3D Cao Cấp",
                Price = 8000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P1,
                Duration = 60
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 2
            new()
            {
                Id = Guid.Parse("b3d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Chuẩn Bị Hifu Standard - An Toàn Tối Ưu",
                Price = 800000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P2,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b3d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Chuẩn Bị Hifu Premium - Kiểm Tra Toàn Diện",
                Price = 1200000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P2,
                Duration = 45
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 3
            new()
            {
                Id = Guid.Parse("b3d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Power Lift - Liệu Trình Đơn",
                Price = 7000000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P3,
                Duration = 60
            },
            new()
            {
                Id = Guid.Parse("b3d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Ultra Lift - Liệu Trình 3 Buổi Tăng Cường",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P3,
                Duration = 180
            },
            new()
            {
                Id = Guid.Parse("b3d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Royal V-Shape - Liệu Trình Hoàng Gia 5 Buổi",
                Price = 28000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P3,
                Duration = 300
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 4
            new()
            {
                Id = Guid.Parse("b3d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Express - Theo Dõi Kết Quả Chuẩn Y Khoa",
                Price = 600000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P4,
                Duration = 30
            },
            new()
            {
                Id = Guid.Parse("b3d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Luxury - Liệu Trình Dưỡng Ẩm Chuyên Sâu",
                Price = 1000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P4,
                Duration = 45
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 1
            new()
            {
                Id = Guid.Parse("b4d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Định Hình Dáng Ngực Tự Nhiên",
                Price = 40000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P1,
                Duration = 180
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Thẩm Mỹ Ngực Toàn Diện Với Chuyên Gia Quốc Tế",
                Price = 65000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P1,
                Duration = 240
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 2
            new()
            {
                Id = Guid.Parse("b4d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Tiền Phẫu Standard - An Toàn Tối Ưu",
                Price = 2000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P2,
                Duration = 45
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Tiền Phẫu Premium - Đánh Giá Chuyên Sâu",
                Price = 3500000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P2,
                Duration = 60
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 3
            new()
            {
                Id = Guid.Parse("b4d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Vertical Short-Scar Chuẩn Quốc Tế",
                Price = 55000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P3,
                Duration = 180
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Short-Scar Premium - Dáng Ngực Hoàn Hảo",
                Price = 75000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P3,
                Duration = 240
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Executive Suite - Dáng Ngực Tự Nhiên Không Dấu Vết",
                Price = 100000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P3,
                Duration = 300
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 4
            new()
            {
                Id = Guid.Parse("b4d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "48h Recovery Suite - Chăm Sóc Phục Hồi Chuyên Nghiệp",
                Price = 5000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P4,
                Duration = 60
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "48h VIP Recovery Suite - Phục Hồi Cao Cấp Với Y Tá Riêng",
                Price = 8000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P4,
                Duration = 90
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 5
            new()
            {
                Id = Guid.Parse("b4d3d799-1151-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Sau Phẫu Thuật - Gói 3 Tháng Theo Dõi",
                Price = 1500000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P5,
                Duration = 45
            },
            new()
            {
                Id = Guid.Parse("b4d3d799-1152-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Toàn Diện - Gói VIP 6 Tháng Với Bác Sĩ Chuyên Khoa",
                Price = 3000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P5,
                Duration = 60
            },

            // Hanoi Beauty Spa Service 5 - Hút Mỡ Mặt Precision
            // Procedure 1 - Tư vấn và đánh giá vùng mỡ mặt
            new()
            {
                Id = Guid.Parse("d5d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Đánh Giá Tạo Hình V-Line Tự Nhiên",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idHBS_S5_P1
            },
            new()
            {
                Id = Guid.Parse("d5d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư Vấn Tạo Hình Khuôn Mặt 3D Chuyên Sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P1
            },

            // Procedure 2 - Kiểm tra sức khỏe và chuẩn bị tiền phẫu
            new()
            {
                Id = Guid.Parse("d5d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm Tra Sức Khỏe Pre-Surgery Standard",
                Price = 1500000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P2
            },
            new()
            {
                Id = Guid.Parse("d5d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Đánh Giá Tiền Phẫu Toàn Diện Premium",
                Price = 2500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P2
            },

            // Procedure 3 - Phẫu thuật hút mỡ mặt Precision
            new()
            {
                Id = Guid.Parse("d5d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Precision V-Shape Facial Contouring",
                Price = 8000000,
                Duration = 240, // 4 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P3
            },
            new()
            {
                Id = Guid.Parse("d5d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Elite Precision 360° Facial Sculpting",
                Price = 12000000,
                Duration = 300, // 5 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P3
            },

            // Procedure 4 - Chăm sóc và tái khám sau phẫu thuật
            new()
            {
                Id = Guid.Parse("d5d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phục Hồi 24h - Chăm Sóc Hậu Phẫu Chuyên Nghiệp",
                Price = 1000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P4
            },
            new()
            {
                Id = Guid.Parse("d5d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phục Hồi 48h VIP - Theo Dõi Toàn Diện Với Chuyên Gia",
                Price = 2500000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P4
            },

            // Skin Care Đà Nẵng Service 1 - Nâng Mũi Cấu Trúc Hybrid
            // Procedure 1 - Tư vấn và thăm khám
            new()
            {
                Id = Guid.Parse("e1d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn nâng mũi cơ bản",
                Price = 600000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S1_P1
            },
            new()
            {
                Id = Guid.Parse("e1d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn nâng mũi chuyên sâu",
                Price = 900000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P1
            },

            // Procedure 2 - Phân tích và thiết kế
            new()
            {
                Id = Guid.Parse("e1d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phân tích và thiết kế tiêu chuẩn",
                Price = 2000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P2
            },
            new()
            {
                Id = Guid.Parse("e1d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phân tích và thiết kế 3D",
                Price = 3000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P2
            },

            // Procedure 3 - Chuẩn bị vật liệu
            new()
            {
                Id = Guid.Parse("e1d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Vật liệu tiêu chuẩn",
                Price = 5000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S1_P3
            },
            new()
            {
                Id = Guid.Parse("e1d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Vật liệu cao cấp nhập khẩu",
                Price = 8000000,
                Duration = 60, // 1 hour
                IsDefault = false,
                ProcedureId = idSCDN_S1_P3
            },

            // Procedure 4 - Thực hiện phẫu thuật
            new()
            {
                Id = Guid.Parse("e1d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật tiêu chuẩn",
                Price = 10000000,
                Duration = 180, // 3 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P4
            },
            new()
            {
                Id = Guid.Parse("e1d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật kỹ thuật cao",
                Price = 15000000,
                Duration = 240, // 4 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new()
            {
                Id = Guid.Parse("e1d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 1500000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P5
            },
            new()
            {
                Id = Guid.Parse("e1d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu VIP",
                Price = 3000000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P5
            },

            // Skin Care Đà Nẵng Service 2 - Nâng Ngực Nội Soi Ergonomix
            // Procedure 1 - Tư vấn và thăm khám
            new()
            {
                Id = Guid.Parse("e2d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn nâng ngực cơ bản",
                Price = 700000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S2_P1
            },
            new()
            {
                Id = Guid.Parse("e2d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn nâng ngực chuyên sâu",
                Price = 1200000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P1
            },

            // Procedure 2 - Phân tích và thiết kế
            new()
            {
                Id = Guid.Parse("e2d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 3000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P2
            },
            new()
            {
                Id = Guid.Parse("e2d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D Simulation",
                Price = 5000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P2
            },

            // Procedure 3 - Chuẩn bị túi độn
            new()
            {
                Id = Guid.Parse("e2d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Túi độn tiêu chuẩn",
                Price = 15000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S2_P3
            },
            new()
            {
                Id = Guid.Parse("e2d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Túi độn cao cấp Mentor",
                Price = 25000000,
                Duration = 60, // 1 hour
                IsDefault = false,
                ProcedureId = idSCDN_S2_P3
            },

            // Procedure 4 - Thực hiện phẫu thuật
            new()
            {
                Id = Guid.Parse("e2d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật tiêu chuẩn",
                Price = 20000000,
                Duration = 240, // 4 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P4
            },
            new()
            {
                Id = Guid.Parse("e2d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật nội soi cao cấp",
                Price = 30000000,
                Duration = 300, // 5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new()
            {
                Id = Guid.Parse("e2d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 2000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P5
            },
            new()
            {
                Id = Guid.Parse("e2d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu premium",
                Price = 4000000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P5
            },

            // Skin Care Đà Nẵng Service 3 - Hút Mỡ Bụng 3D Hi-Definition
            // Procedure 1 - Tư vấn và đánh giá
            new()
            {
                Id = Guid.Parse("e3d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn tiêu chuẩn",
                Price = 600000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S3_P1
            },
            new()
            {
                Id = Guid.Parse("e3d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn và đánh giá chuyên sâu",
                Price = 1000000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P1
            },

            // Procedure 2 - Thiết kế định hình cơ thể
            new()
            {
                Id = Guid.Parse("e3d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Định hình cơ bản",
                Price = 1500000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P2
            },
            new()
            {
                Id = Guid.Parse("e3d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Định hình 3D precision",
                Price = 2500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P2
            },

            // Procedure 3 - Chuẩn bị trước phẫu thuật
            new()
            {
                Id = Guid.Parse("e3d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chuẩn bị tiêu chuẩn",
                Price = 2000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P3
            },
            new()
            {
                Id = Guid.Parse("e3d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chuẩn bị cao cấp",
                Price = 3500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P3
            },

            // Procedure 4 - Thực hiện hút mỡ
            new()
            {
                Id = Guid.Parse("e3d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Hút mỡ tiêu chuẩn",
                Price = 15000000,
                Duration = 180, // 3 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P4
            },
            new()
            {
                Id = Guid.Parse("e3d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Hút mỡ 3D Hi-Definition",
                Price = 25000000,
                Duration = 240, // 4 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new()
            {
                Id = Guid.Parse("e3d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc phục hồi tiêu chuẩn",
                Price = 2000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P5
            },
            new()
            {
                Id = Guid.Parse("e3d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc phục hồi nhanh VIP",
                Price = 3500000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P5
            },

            // Skin Care Đà Nẵng Service 4 - Cắt Mí Mắt Plasma Tech
            // Procedure 1 - Tư vấn và thăm khám
            new()
            {
                Id = Guid.Parse("e4d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn cơ bản",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S4_P1
            },
            new()
            {
                Id = Guid.Parse("e4d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn chuyên sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P1
            },

            // Procedure 2 - Thiết kế mí mắt
            new()
            {
                Id = Guid.Parse("e4d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 1000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S4_P2
            },
            new()
            {
                Id = Guid.Parse("e4d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D precision",
                Price = 1800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P2
            },

            // Procedure 3 - Thực hiện cắt mí
            new()
            {
                Id = Guid.Parse("e4d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Cắt mí tiêu chuẩn",
                Price = 8000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S4_P3
            },
            new()
            {
                Id = Guid.Parse("e4d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Cắt mí Plasma Tech cao cấp",
                Price = 12000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P3
            },

            // Procedure 4 - Theo dõi và chăm sóc
            new()
            {
                Id = Guid.Parse("e4d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc tiêu chuẩn",
                Price = 1200000,
                Duration = 720, // 12 hours
                IsDefault = true,
                ProcedureId = idSCDN_S4_P4
            },
            new()
            {
                Id = Guid.Parse("e4d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc phục hồi nhanh",
                Price = 2000000,
                Duration = 1440, // 24 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P4
            },

            // Skin Care Đà Nẵng Service 5 - Độn Cằm 3D Crystal
            // Procedure 1 - Tư vấn và thăm khám
            new()
            {
                Id = Guid.Parse("e5d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn độn cằm cơ bản",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S5_P1
            },
            new()
            {
                Id = Guid.Parse("e5d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn độn cằm chuyên sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P1
            },

            // Procedure 2 - Thiết kế và tạo hình
            new()
            {
                Id = Guid.Parse("e5d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 1500000,
                Duration = 90, // 1.5 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P2
            },
            new()
            {
                Id = Guid.Parse("e5d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D Crystal",
                Price = 2500000,
                Duration = 120, // 2 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P2
            },

            // Procedure 3 - Thực hiện độn cằm
            new()
            {
                Id = Guid.Parse("e5d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Độn cằm silicon tiêu chuẩn",
                Price = 10000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P3
            },
            new()
            {
                Id = Guid.Parse("e5d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Độn cằm 3D Crystal cao cấp",
                Price = 15000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P3
            },

            // Procedure 4 - Theo dõi và chăm sóc
            new()
            {
                Id = Guid.Parse("e5d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 1200000,
                Duration = 720, // 12 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P4
            },
            new()
            {
                Id = Guid.Parse("e5d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu VIP",
                Price = 2500000,
                Duration = 1440, // 24 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P4
            }
        };

        // builder.HasData(procedurePriceTypes);
    }
}