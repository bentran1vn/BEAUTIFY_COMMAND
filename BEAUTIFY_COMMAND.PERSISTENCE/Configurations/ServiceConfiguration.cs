using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        // Get category IDs from CategoryConfiguration
        var idNangMui = Guid.Parse("12121212-1212-1212-1212-121212121212");
        var idCatMiMat = Guid.Parse("13131313-1313-1313-1313-131313131313");
        var idNangCungMay = Guid.Parse("14141414-1414-1414-1414-141414141414");
        var idDonCam = Guid.Parse("15151515-1515-1515-1515-151515151515");
        var idHutMoMat = Guid.Parse("16161616-1616-1616-1616-161616161616");
        var idNangNguc = Guid.Parse("17171717-1717-1717-1717-171717171717");
        var idThuNhoNguc = Guid.Parse("18181818-1818-1818-1818-181818181818");
        var idHutMoBung = Guid.Parse("19191919-1919-1919-1919-191919191919");
        var idCangDaBung = Guid.Parse("20202020-2020-2020-2020-202020202020");

        // Generate Service IDs
        // Beauty Center Sài Gòn Services
        var idBCSG_Service1 = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service2 = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service3 = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service4 = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service5 = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");

        // Hanoi Beauty Spa Services
        var idHBS_Service1 = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service2 = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service3 = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service4 = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service5 = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");

        // Skin Care Đà Nẵng Services
        var idSCDN_Service1 = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service2 = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service3 = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service4 = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service5 = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");

        var services = new List<Service>
        {
            // Beauty Center Sài Gòn Services (5 services)
            new()
            {
                Id = idBCSG_Service1,
                Name = "Nâng Mũi Cấu Trúc S-Line Premium",
                Description =
                    "Thiết kế đường nét mũi S-Line chuẩn Hàn Quốc, sử dụng sụn tự thân kết hợp với sụn nhân tạo cao cấp, tạo dáng mũi thanh tú tự nhiên, phù hợp với từng khuôn mặt.",
                MaxPrice = 45000000,
                MinPrice = 30000000,
                CategoryId = idNangMui,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idBCSG_Service2,
                Name = "Cắt Mí Mắt Plasma Luxury",
                Description =
                    "Công nghệ cắt mí mắt không chạm với plasma, đem lại đôi mắt to tròn, rõ mí, giảm thiểu thời gian hồi phục với công nghệ hiện đại.",
                MaxPrice = 18000000,
                MinPrice = 12000000,
                CategoryId = idCatMiMat,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idBCSG_Service3,
                Name = "Nâng Ngực Túi Độn Mentor",
                Description =
                    "Sử dụng túi độn Mentor được FDA Hoa Kỳ chứng nhận, đảm bảo an toàn và độ bền cao, nâng ngực tạo dáng tự nhiên với kỹ thuật nội soi.",
                MaxPrice = 85000000,
                MinPrice = 65000000,
                CategoryId = idNangNguc,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idBCSG_Service4,
                Name = "Độn Cằm V-Line Hàn Quốc",
                Description =
                    "Tạo cằm V-Line thanh thoát với phương pháp độn cằm kết hợp định hình theo công nghệ Hàn Quốc, phù hợp với từng khuôn mặt Á Đông.",
                MaxPrice = 32000000,
                MinPrice = 25000000,
                CategoryId = idDonCam,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idBCSG_Service5,
                Name = "Hút Mỡ Bụng VASER Lipo",
                Description =
                    "Công nghệ hút mỡ siêu âm VASER Lipo hiện đại, loại bỏ mỡ thừa vùng bụng một cách chọn lọc, giảm thiểu tác động đến các mô liên kết, đem lại vùng bụng thon gọn tự nhiên.",
                MaxPrice = 50000000,
                MinPrice = 35000000,
                CategoryId = idHutMoBung,
                DepositPercent = 0,
                IsRefundable = false
            },

            // Hanoi Beauty Spa Services (5 services)
            new()
            {
                Id = idHBS_Service1,
                Name = "Nâng Mũi Bio-Silicon Elite",
                Description =
                    "Nâng mũi với công nghệ sử dụng Bio-Silicon cao cấp kết hợp sụn tự thân, tạo dáng mũi cao thanh tú phù hợp khuôn mặt Á Đông.",
                MaxPrice = 50000000,
                MinPrice = 35000000,
                CategoryId = idNangMui,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idHBS_Service2,
                Name = "Cắt Mí Mắt Hàn Quốc Không Sẹo",
                Description =
                    "Kỹ thuật cắt mí mắt công nghệ Hàn Quốc, tạo đường mí tự nhiên, không để lại sẹo, thời gian hồi phục nhanh.",
                MaxPrice = 20000000,
                MinPrice = 15000000,
                CategoryId = idCatMiMat,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idHBS_Service3,
                Name = "Nâng Cung Mày Siêu Âm Hifu",
                Description =
                    "Công nghệ nâng cung mày không phẫu thuật bằng siêu âm tập trung Hifu, đem lại hiệu quả nâng cung mày tự nhiên.",
                MaxPrice = 15000000,
                MinPrice = 8000000,
                CategoryId = idNangCungMay,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idHBS_Service4,
                Name = "Thu Nhỏ Ngực Vertical Short-Scar",
                Description =
                    "Thu nhỏ ngực với kỹ thuật sẹo ngắn đứng, giảm thiểu sẹo, bảo tồn chức năng và nhạy cảm của núm vú.",
                MaxPrice = 70000000,
                MinPrice = 50000000,
                CategoryId = idThuNhoNguc,
                DepositPercent = 0,
                IsRefundable = false,
                ServiceMedias =
                [
                    new ServiceMedia
                    {
                        Id = Guid.NewGuid(),
                        ImageUrl =
                            "https://res.cloudinary.com/dvadlh7ah/image/upload/v1744178015/b8pbl60kjocx61cyizto.png"
                    }
                ]
            },
            new()
            {
                Id = idHBS_Service5,
                Name = "Hút Mỡ Mặt Precision",
                Description =
                    "Kỹ thuật hút mỡ mặt chính xác từng vùng, tạo đường nét gương mặt thanh tú, thon gọn tự nhiên.",
                MaxPrice = 28000000,
                MinPrice = 18000000,
                CategoryId = idHutMoMat,
                DepositPercent = 0,
                IsRefundable = false
            },

            // Skin Care Đà Nẵng Services (5 services)
            new()
            {
                Id = idSCDN_Service1,
                Name = "Nâng Mũi Cấu Trúc Hybrid",
                Description =
                    "Kết hợp sụn tự thân với vật liệu nhân tạo cao cấp để tạo dáng mũi cân đối, tự nhiên, phù hợp với khuôn mặt người Việt.",
                MaxPrice = 55000000,
                MinPrice = 40000000,
                CategoryId = idNangMui,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idSCDN_Service2,
                Name = "Nâng Ngực Nội Soi Ergonomix",
                Description =
                    "Sử dụng túi ngực Ergonomix thế hệ mới với kỹ thuật nội soi, tạo dáng ngực tự nhiên, mềm mại và vững chắc.",
                MaxPrice = 90000000,
                MinPrice = 70000000,
                CategoryId = idNangNguc,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idSCDN_Service3,
                Name = "Hút Mỡ Bụng 3D Hi-Definition",
                Description =
                    "Công nghệ hút mỡ bụng 3D tạo hình cơ bụng rõ nét, không chỉ loại bỏ mỡ thừa mà còn định hình cơ bụng 6 múi cho nam giới.",
                MaxPrice = 65000000,
                MinPrice = 45000000,
                CategoryId = idHutMoBung,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idSCDN_Service4,
                Name = "Cắt Mí Mắt Plasma Tech",
                Description =
                    "Công nghệ cắt mí mắt bằng plasma tiên tiến, tạo đường mí sắc nét, đều đẹp, giảm thiểu đau đớn và thời gian hồi phục.",
                MaxPrice = 22000000,
                MinPrice = 15000000,
                CategoryId = idCatMiMat,
                DepositPercent = 0,
                IsRefundable = false
            },
            new()
            {
                Id = idSCDN_Service5,
                Name = "Độn Cằm 3D Crystal",
                Description =
                    "Sử dụng công nghệ 3D Crystal để độn cằm, tạo đường nét cằm sắc sảo, cân đối với khuôn mặt, bền vững theo thời gian.",
                MaxPrice = 35000000,
                MinPrice = 25000000,
                CategoryId = idDonCam,
                DepositPercent = 0,
                IsRefundable = false
            }
        };

        // builder.HasData(services);
    }
}