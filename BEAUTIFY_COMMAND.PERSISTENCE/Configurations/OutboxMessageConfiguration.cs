using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_COMMAND.PERSISTENCE.Constants;
using BEAUTIFY_COMMAND.PERSISTENCE.Outbox;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.DoctorServices;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.EntityEvents;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MongoDB.Driver.Linq;
using Newtonsoft.Json;
using ClinicServicesDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.ClinicServices.DomainEvents;
using ProceduresDomainEvent = BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.CONTRACT.Services.Procedures.DomainEvents;
namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
internal sealed class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessage>
{
    public void Configure(EntityTypeBuilder<OutboxMessage> builder)
    {
        builder.ToTable(TableNames.OutboxMessages);

        builder.HasKey(x => x.Id);
        
        var idPhauThuatVungMat = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var idPhauThuatVungNguc = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var idPhauThuatVungBung = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var idPhauThuatVungMong = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var idPhauThuatVungChan = Guid.Parse("55555555-5555-5555-5555-555555555555");
        var idPhauThuatGiamCan = Guid.Parse("66666666-6666-6666-6666-666666666666");
        var idPhauThuatTaoHinhCoThe = Guid.Parse("77777777-7777-7777-7777-777777777777");
        var idPhauThuatTaoHinhBoPhanSinhDuc = Guid.Parse("88888888-8888-8888-8888-888888888888");
        var idPhauThuatTaoHinhDa = Guid.Parse("99999999-9999-9999-9999-999999999999");
        var idPhauThuatTaoHinhTai = Guid.Parse("10101010-1010-1010-1010-101010101010");


        var idNangMui = Guid.Parse("12121212-1212-1212-1212-121212121212");
        var idCatMiMat = Guid.Parse("13131313-1313-1313-1313-131313131313");
        var idNangCungMay = Guid.Parse("14141414-1414-1414-1414-141414141414");
        var idDonCam = Guid.Parse("15151515-1515-1515-1515-151515151515");
        var idHutMoMat = Guid.Parse("16161616-1616-1616-1616-161616161616");
        var idNangNguc = Guid.Parse("17171717-1717-1717-1717-171717171717");
        var idThuNhoNguc = Guid.Parse("18181818-1818-1818-1818-181818181818");

        var idHutMoBung = Guid.Parse("19191919-1919-1919-1919-191919191919");
        var idCangDaBung = Guid.Parse("20202020-2020-2020-2020-202020202020");
        
        var categories = new List<Category>()
        {
            new Category
            {
                Id = idPhauThuatVungMat,
                Name = "Phẫu Thuật Vùng Mặt",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mặt",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatVungNguc,
                Name = "Phẫu Thuật Vùng Ngực",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng ngực",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatVungBung,
                Name = "Phẫu Thuật Vùng Bụng",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng bụng",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatVungMong,
                Name = "Phẫu Thuật Vùng Mông",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng mông",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatVungChan,
                Name = "Phẫu Thuật Vùng Chân",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa vùng chân",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatGiamCan,
                Name = "Phẫu Thuật Giảm Cân",
                Description = "Dịch vụ phẫu thuật hỗ trợ giảm cân",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatTaoHinhCoThe,
                Name = "Phẫu Thuật Tạo Hình Cơ Thể",
                Description = "Dịch vụ phẫu thuật tạo hình cơ thể",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatTaoHinhBoPhanSinhDuc,
                Name = "Phẫu Thuật Tạo Hình Bộ Phận Sinh Dục",
                Description = "Dịch vụ phẫu thuật tạo hình bộ phận sinh dục",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatTaoHinhDa,
                Name = "Phẫu Thuật Tạo Hình Da",
                Description = "Dịch vụ phẫu thuật tạo hình da",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idPhauThuatTaoHinhTai,
                Name = "Phẫu Thuật Tạo Hình Tai",
                Description = "Dịch vụ phẫu thuật tạo hình tai",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = idNangMui,
                Name = "Nâng Mũi (Rhinoplasty)",
                Description = "Điều chỉnh hình dáng mũi để cân đối với khuôn mặt",
                IsParent = false,
                ParentId = idPhauThuatVungMat
            },
            // Danh mục con: Cắt mí mắt
            new Category
            {
                Id = idCatMiMat,
                Name = "Cắt Mí Mắt (Blepharoplasty)",
                Description = "Loại bỏ da thừa, mỡ thừa ở mí mắt, giúp mắt to và trẻ trung hơn",
                IsParent = false,
                ParentId = idPhauThuatVungMat
            },
            // Danh mục con: Nâng cung mày
            new Category
            {
                Id = idNangCungMay,
                Name = "Nâng Cung Mày (Brow Lift)",
                Description = "Cải thiện vùng trán và cung mày, giảm nếp nhăn",
                IsParent = false,
                ParentId = idPhauThuatVungMat
            },
            // Danh mục con: Độn cằm
            new Category
            {
                Id = idDonCam,
                Name = "Độn Cằm (Chin Augmentation)",
                Description = "Tạo hình cằm cân đối với khuôn mặt",
                IsParent = false,
                ParentId = idPhauThuatVungMat
            },
            // Danh mục con: Hút mỡ mặt
            new Category
            {
                Id = idHutMoMat,
                Name = "Hút Mỡ Mặt",
                Description = "Loại bỏ mỡ thừa ở vùng mặt như má, cằm",
                IsParent = false,
                ParentId = idPhauThuatVungMat
            }, new Category
            {
                Id = idNangNguc,
                Name = "Nâng Ngực (Breast Augmentation)",
                Description = "Sử dụng túi độn hoặc mỡ tự thân để tăng kích thước ngực",
                IsParent = false,
                ParentId = idPhauThuatVungNguc
            },
            // Danh mục con: Thu nhỏ ngực
            new Category
            {
                Id = idThuNhoNguc,
                Name = "Thu Nhỏ Ngực (Breast Reduction)",
                Description = "Giảm kích thước ngực quá lớn",
                IsParent = false,
                ParentId = idPhauThuatVungNguc
            },
            // Danh mục con: Hút mỡ bụng
            new Category
            {
                Id = idHutMoBung,
                Name = "Hút Mỡ Bụng",
                Description = "Loại bỏ mỡ thừa ở vùng bụng",
                IsParent = false,
                ParentId = idPhauThuatVungBung
            },
            // Danh mục con: Căng da bụng
            new Category
            {
                Id = idCangDaBung,
                Name = "Căng Da Bụng",
                Description = "Loại bỏ da thừa và mỡ, làm săn chắc vùng bụng",
                IsParent = false,
                ParentId = idPhauThuatVungBung
            }
        };

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

        var services = new List<Service>()
        {
            // Beauty Center Sài Gòn Services (5 services)
            new Service
            {
                Id = idBCSG_Service1,
                Name = "Nâng Mũi Cấu Trúc S-Line Premium",
                Description = "Thiết kế đường nét mũi S-Line chuẩn Hàn Quốc, sử dụng sụn tự thân kết hợp với sụn nhân tạo cao cấp, tạo dáng mũi thanh tú tự nhiên, phù hợp với từng khuôn mặt.",
                MaxPrice = 45000000,
                MinPrice = 30000000,
                CategoryId = idNangMui,
            },
            new Service
            {
                Id = idBCSG_Service2,
                Name = "Cắt Mí Mắt Plasma Luxury",
                Description = "Công nghệ cắt mí mắt không chạm với plasma, đem lại đôi mắt to tròn, rõ mí, giảm thiểu thời gian hồi phục với công nghệ hiện đại.",
                MaxPrice = 18000000,
                MinPrice = 12000000,
                CategoryId = idCatMiMat,
            },
            new Service
            {
                Id = idBCSG_Service3,
                Name = "Nâng Ngực Túi Độn Mentor",
                Description = "Sử dụng túi độn Mentor được FDA Hoa Kỳ chứng nhận, đảm bảo an toàn và độ bền cao, nâng ngực tạo dáng tự nhiên với kỹ thuật nội soi.",
                MaxPrice = 85000000,
                MinPrice = 65000000,
                CategoryId = idNangNguc,
            },
            new Service
            {
                Id = idBCSG_Service4,
                Name = "Độn Cằm V-Line Hàn Quốc",
                Description = "Tạo cằm V-Line thanh thoát với phương pháp độn cằm kết hợp định hình theo công nghệ Hàn Quốc, phù hợp với từng khuôn mặt Á Đông.",
                MaxPrice = 32000000,
                MinPrice = 25000000,
                CategoryId = idDonCam,
            },
            new Service
            {
                Id = idBCSG_Service5,
                Name = "Hút Mỡ Bụng VASER Lipo",
                Description = "Công nghệ hút mỡ siêu âm VASER Lipo hiện đại, loại bỏ mỡ thừa vùng bụng một cách chọn lọc, giảm thiểu tác động đến các mô liên kết, đem lại vùng bụng thon gọn tự nhiên.",
                MaxPrice = 50000000,
                MinPrice = 35000000,
                CategoryId = idHutMoBung,
            },

            // Hanoi Beauty Spa Services (5 services)
            new Service
            {
                Id = idHBS_Service1,
                Name = "Nâng Mũi Bio-Silicon Elite",
                Description = "Nâng mũi với công nghệ sử dụng Bio-Silicon cao cấp kết hợp sụn tự thân, tạo dáng mũi cao thanh tú phù hợp khuôn mặt Á Đông.",
                MaxPrice = 50000000,
                MinPrice = 35000000,
                CategoryId = idNangMui,
            },
            new Service
            {
                Id = idHBS_Service2,
                Name = "Cắt Mí Mắt Hàn Quốc Không Sẹo",
                Description = "Kỹ thuật cắt mí mắt công nghệ Hàn Quốc, tạo đường mí tự nhiên, không để lại sẹo, thời gian hồi phục nhanh.",
                MaxPrice = 20000000,
                MinPrice = 15000000,
                CategoryId = idCatMiMat,
            },
            new Service
            {
                Id = idHBS_Service3,
                Name = "Nâng Cung Mày Siêu Âm Hifu",
                Description = "Công nghệ nâng cung mày không phẫu thuật bằng siêu âm tập trung Hifu, đem lại hiệu quả nâng cung mày tự nhiên.",
                MaxPrice = 15000000,
                MinPrice = 8000000,
                CategoryId = idNangCungMay,
            },
            new Service
            {
                Id = idHBS_Service4,
                Name = "Thu Nhỏ Ngực Vertical Short-Scar",
                Description = "Thu nhỏ ngực với kỹ thuật sẹo ngắn đứng, giảm thiểu sẹo, bảo tồn chức năng và nhạy cảm của núm vú.",
                MaxPrice = 70000000,
                MinPrice = 50000000,
                CategoryId = idThuNhoNguc,
            },
            new Service
            {
                Id = idHBS_Service5,
                Name = "Hút Mỡ Mặt Precision",
                Description = "Kỹ thuật hút mỡ mặt chính xác từng vùng, tạo đường nét gương mặt thanh tú, thon gọn tự nhiên.",
                MaxPrice = 28000000,
                MinPrice = 18000000,
                CategoryId = idHutMoMat,
            },

            // Skin Care Đà Nẵng Services (5 services)
            new Service
            {
                Id = idSCDN_Service1,
                Name = "Nâng Mũi Cấu Trúc Hybrid",
                Description = "Kết hợp sụn tự thân với vật liệu nhân tạo cao cấp để tạo dáng mũi cân đối, tự nhiên, phù hợp với khuôn mặt người Việt.",
                MaxPrice = 55000000,
                MinPrice = 40000000,
                CategoryId = idNangMui,
            },
            new Service
            {
                Id = idSCDN_Service2,
                Name = "Nâng Ngực Nội Soi Ergonomix",
                Description = "Sử dụng túi ngực Ergonomix thế hệ mới với kỹ thuật nội soi, tạo dáng ngực tự nhiên, mềm mại và vững chắc.",
                MaxPrice = 90000000,
                MinPrice = 70000000,
                CategoryId = idNangNguc,
            },
            new Service
            {
                Id = idSCDN_Service3,
                Name = "Hút Mỡ Bụng 3D Hi-Definition",
                Description = "Công nghệ hút mỡ bụng 3D tạo hình cơ bụng rõ nét, không chỉ loại bỏ mỡ thừa mà còn định hình cơ bụng 6 múi cho nam giới.",
                MaxPrice = 65000000,
                MinPrice = 45000000,
                CategoryId = idHutMoBung,
            },
            new Service
            {
                Id = idSCDN_Service4,
                Name = "Cắt Mí Mắt Plasma Tech",
                Description = "Công nghệ cắt mí mắt bằng plasma tiên tiến, tạo đường mí sắc nét, đều đẹp, giảm thiểu đau đớn và thời gian hồi phục.",
                MaxPrice = 22000000,
                MinPrice = 15000000,
                CategoryId = idCatMiMat,
            },
            new Service
            {
                Id = idSCDN_Service5,
                Name = "Độn Cằm 3D Crystal",
                Description = "Sử dụng công nghệ 3D Crystal để độn cằm, tạo đường nét cằm sắc sảo, cân đối với khuôn mặt, bền vững theo thời gian.",
                MaxPrice = 35000000,
                MinPrice = 25000000,
                CategoryId = idDonCam,
            }
        };
        
        var clinics = new List<Clinic>
        {
            // Main Clinics (3) - one for each Clinic Admin
            new Clinic
            {
                Id = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                Name = "Beauty Center Sài Gòn",
                Email = "beautycenter.saigon@gmail.com",
                PhoneNumber = "0283456789",
                City = "Hồ Chí Minh",
                TaxCode = "12345678901",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-1.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-1.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "Vietcombank",
                BankAccountNumber = "1234567890123"
            },
            new Clinic
            {
                Id = new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"),
                Name = "Hanoi Beauty Spa",
                Email = "hanoi.beautyspa@gmail.com",
                PhoneNumber = "0243812345",
                City = "Hà Nội",
                TaxCode = "23456789012",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-2.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-2.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "BIDV",
                BankAccountNumber = "2345678901234"
            },
            new Clinic
            {
                Id = new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"),
                Name = "Skin Care Đà Nẵng",
                Email = "skincare.danang@gmail.com",
                PhoneNumber = "0236789123",
                City = "Đà Nẵng",
                TaxCode = "34567890123",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-3.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-3.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "Agribank",
                BankAccountNumber = "3456789012345"
            },

            // Sub Clinics for Beauty Center Sài Gòn
            new Clinic
            {
                Id = new Guid("c0b7058f-8e72-4dee-8742-0df6206d1843"),
                Name = "Beauty Center Sài Gòn - Chi nhánh Quận 1",
                Email = "beautycenter.q1@gmail.com",
                PhoneNumber = "0283456111",
                City = "Hồ Chí Minh",
                TaxCode = "12345678902",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-1-1.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-1-1.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                BankName = "Vietcombank",
                BankAccountNumber = "1234567890124"
            },
            new Clinic
            {
                Id = new Guid("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"),
                Name = "Beauty Center Sài Gòn - Chi nhánh Quận 3",
                Email = "beautycenter.q3@gmail.com",
                PhoneNumber = "0283456222",
                City = "Hồ Chí Minh",
                TaxCode = "12345678903",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-1-2.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-1-2.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                BankName = "Vietcombank",
                BankAccountNumber = "1234567890125"
            },

            // Sub Clinics for Hanoi Beauty Spa
            new Clinic
            {
                Id = new Guid("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"),
                Name = "Hanoi Beauty Spa - Chi nhánh Đống Đa",
                Email = "hanoi.dongda@gmail.com",
                PhoneNumber = "0243812111",
                City = "Hà Nội",
                TaxCode = "23456789013",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-2-1.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-2-1.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"),
                BankName = "BIDV",
                BankAccountNumber = "2345678901235"
            },
            new Clinic
            {
                Id = new Guid("c96de07e-32d7-41d5-b417-060cd95ee7ff"),
                Name = "Hanoi Beauty Spa - Chi nhánh Cầu Giấy",
                Email = "hanoi.caugiay@gmail.com",
                PhoneNumber = "0243812222",
                City = "Hà Nội",
                TaxCode = "23456789014",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-2-2.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-2-2.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"),
                BankName = "BIDV",
                BankAccountNumber = "2345678901236"
            },

            // Sub Clinics for Skin Care Đà Nẵng
            new Clinic
            {
                Id = new Guid("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"),
                Name = "Skin Care Đà Nẵng - Chi nhánh Hải Châu",
                Email = "skincare.haichau@gmail.com",
                PhoneNumber = "0236789111",
                City = "Đà Nẵng",
                TaxCode = "34567890124",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-3-1.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-3-1.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"),
                BankName = "Agribank",
                BankAccountNumber = "3456789012346"
            },
            new Clinic
            {
                Id = new Guid("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"),
                Name = "Skin Care Đà Nẵng - Chi nhánh Sơn Trà",
                Email = "skincare.sontra@gmail.com",
                PhoneNumber = "0236789222",
                City = "Đà Nẵng",
                TaxCode = "34567890125",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-3-2.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-3-2.pdf",
                Status = 1,
                TotalBranches = 0,
                IsParent = false,
                ParentId = new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"),
                BankName = "Agribank",
                BankAccountNumber = "3456789012347"
            }
        };


        // Get clinic IDs from ClinicConfiguration
        // Sub Clinics for Beauty Center Sài Gòn
        var idBCSG_Q1 = Guid.Parse("c0b7058f-8e72-4dee-8742-0df6206d1843"); // Beauty Center Sài Gòn - Chi nhánh Quận 1
        var idBCSG_Q3 = Guid.Parse("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"); // Beauty Center Sài Gòn - Chi nhánh Quận 3

        // Sub Clinics for Hanoi Beauty Spa
        var idHBS_DD = Guid.Parse("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"); // Hanoi Beauty Spa - Chi nhánh Đống Đa
        var idHBS_CG = Guid.Parse("c96de07e-32d7-41d5-b417-060cd95ee7ff"); // Hanoi Beauty Spa - Chi nhánh Cầu Giấy

        // Sub Clinics for Skin Care Đà Nẵng
        var idSCDN_HC = Guid.Parse("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"); // Skin Care Đà Nẵng - Chi nhánh Hải Châu
        var idSCDN_ST = Guid.Parse("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"); // Skin Care Đà Nẵng - Chi nhánh Sơn Trà
        
        var clinicServices = new List<ClinicService>()
        {
            // Beauty Center Sài Gòn Services at Chi nhánh Quận 1
            new ClinicService()
            {
                Id = Guid.Parse("a1000001-a000-4000-a000-000000000001"),
                ServiceId = idBCSG_Service1,
                ClinicId = idBCSG_Q1,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a1000002-a000-4000-a000-000000000002"),
                ServiceId = idBCSG_Service2,
                ClinicId = idBCSG_Q1,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a1000003-a000-4000-a000-000000000003"),
                ServiceId = idBCSG_Service3,
                ClinicId = idBCSG_Q1,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a1000004-a000-4000-a000-000000000004"),
                ServiceId = idBCSG_Service4,
                ClinicId = idBCSG_Q1,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a1000005-a000-4000-a000-000000000005"),
                ServiceId = idBCSG_Service5,
                ClinicId = idBCSG_Q1,
            },

            // Beauty Center Sài Gòn Services at Chi nhánh Quận 3
            new ClinicService()
            {
                Id = Guid.Parse("a3000001-a000-4000-a000-000000000001"),
                ServiceId = idBCSG_Service1,
                ClinicId = idBCSG_Q3,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a3000002-a000-4000-a000-000000000002"),
                ServiceId = idBCSG_Service2,
                ClinicId = idBCSG_Q3,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a3000003-a000-4000-a000-000000000003"),
                ServiceId = idBCSG_Service3,
                ClinicId = idBCSG_Q3,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a3000004-a000-4000-a000-000000000004"),
                ServiceId = idBCSG_Service4,
                ClinicId = idBCSG_Q3,
            },
            new ClinicService()
            {
                Id = Guid.Parse("a3000005-a000-4000-a000-000000000005"),
                ServiceId = idBCSG_Service5,
                ClinicId = idBCSG_Q3,
            },

            // Hanoi Beauty Spa Services at Chi nhánh Đống Đa
            new ClinicService()
            {
                Id = Guid.Parse("b1000001-b000-4000-b000-000000000001"),
                ServiceId = idHBS_Service1,
                ClinicId = idHBS_DD,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b1000002-b000-4000-b000-000000000002"),
                ServiceId = idHBS_Service2,
                ClinicId = idHBS_DD,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b1000003-b000-4000-b000-000000000003"),
                ServiceId = idHBS_Service3,
                ClinicId = idHBS_DD,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b1000004-b000-4000-b000-000000000004"),
                ServiceId = idHBS_Service4,
                ClinicId = idHBS_DD,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b1000005-b000-4000-b000-000000000005"),
                ServiceId = idHBS_Service5,
                ClinicId = idHBS_DD,
            },

            // Hanoi Beauty Spa Services at Chi nhánh Cầu Giấy
            new ClinicService()
            {
                Id = Guid.Parse("b2000001-b000-4000-b000-000000000001"),
                ServiceId = idHBS_Service1,
                ClinicId = idHBS_CG,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b2000002-b000-4000-b000-000000000002"),
                ServiceId = idHBS_Service2,
                ClinicId = idHBS_CG,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b2000003-b000-4000-b000-000000000003"),
                ServiceId = idHBS_Service3,
                ClinicId = idHBS_CG,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b2000004-b000-4000-b000-000000000004"),
                ServiceId = idHBS_Service4,
                ClinicId = idHBS_CG,
            },
            new ClinicService()
            {
                Id = Guid.Parse("b2000005-b000-4000-b000-000000000005"),
                ServiceId = idHBS_Service5,
                ClinicId = idHBS_CG,
            },

            // Skin Care Đà Nẵng Services at Chi nhánh Hải Châu
            new ClinicService()
            {
                Id = Guid.Parse("c1000001-c000-4000-c000-000000000001"),
                ServiceId = idSCDN_Service1,
                ClinicId = idSCDN_HC,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c1000002-c000-4000-c000-000000000002"),
                ServiceId = idSCDN_Service2,
                ClinicId = idSCDN_HC,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c1000003-c000-4000-c000-000000000003"),
                ServiceId = idSCDN_Service3,
                ClinicId = idSCDN_HC,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c1000004-c000-4000-c000-000000000004"),
                ServiceId = idSCDN_Service4,
                ClinicId = idSCDN_HC,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c1000005-c000-4000-c000-000000000005"),
                ServiceId = idSCDN_Service5,
                ClinicId = idSCDN_HC,
            },

            // Skin Care Đà Nẵng Services at Chi nhánh Sơn Trà
            new ClinicService()
            {
                Id = Guid.Parse("c2000001-c000-4000-c000-000000000001"),
                ServiceId = idSCDN_Service1,
                ClinicId = idSCDN_ST,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c2000002-c000-4000-c000-000000000002"),
                ServiceId = idSCDN_Service2,
                ClinicId = idSCDN_ST,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c2000003-c000-4000-c000-000000000003"),
                ServiceId = idSCDN_Service3,
                ClinicId = idSCDN_ST,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c2000004-c000-4000-c000-000000000004"),
                ServiceId = idSCDN_Service4,
                ClinicId = idSCDN_ST,
            },
            new ClinicService()
            {
                Id = Guid.Parse("c2000005-c000-4000-c000-000000000005"),
                ServiceId = idSCDN_Service5,
                ClinicId = idSCDN_ST,
            }
        };
        
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
    
        var procedures = new List<Procedure>()
        {
            // Beauty Center Sài Gòn Service 1 - Nâng Mũi Cấu Trúc S-Line Premium
            new Procedure
            {
                Id = idBCSG_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi 3D",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi 3D phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service1,
            },
            new Procedure
            {
                Id = idBCSG_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service1,
            },
            new Procedure
            {
                Id = idBCSG_S1_P3,
                Name = "Phẫu thuật nâng mũi cấu trúc S-Line",
                Description = "Thực hiện phẫu thuật với kỹ thuật cấu trúc S-Line, sử dụng sụn tự thân kết hợp sụn nhân tạo cao cấp.",
                StepIndex = 3,
                ServiceId = idBCSG_Service1,
            },
            new Procedure
            {
                Id = idBCSG_S1_P4,
                Name = "Theo dõi hậu phẫu 24h",
                Description = "Theo dõi tình trạng hậu phẫu 24h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idBCSG_Service1,
            },
            new Procedure
            {
                Id = idBCSG_S1_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và can thiệp kịp thời nếu có biến chứng.",
                StepIndex = 5,
                ServiceId = idBCSG_Service1,
            },

            // Beauty Center Sài Gòn Service 2 - Cắt Mí Mắt Plasma Luxury
            new Procedure
            {
                Id = idBCSG_S2_P1,
                Name = "Tư vấn và thiết kế mí mắt",
                Description = "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service2,
            },
            new Procedure
            {
                Id = idBCSG_S2_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description = "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idBCSG_Service2,
            },
            new Procedure
            {
                Id = idBCSG_S2_P3,
                Name = "Phẫu thuật cắt mí bằng công nghệ Plasma",
                Description = "Thực hiện phẫu thuật với công nghệ Plasma không chạm, tạo đường mí tự nhiên, giảm thiểu chảy máu và đau đớn.",
                StepIndex = 3,
                ServiceId = idBCSG_Service2,
            },
            new Procedure
            {
                Id = idBCSG_S2_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idBCSG_Service2,
            },

            // Beauty Center Sài Gòn Service 3 - Nâng Ngực Túi Độn Mentor
            new Procedure
            {
                Id = idBCSG_S3_P1,
                Name = "Tư vấn và lựa chọn kích thước túi ngực",
                Description = "Bác sĩ phân tích cấu trúc cơ thể, tư vấn và giúp khách hàng lựa chọn kích thước túi ngực phù hợp.",
                StepIndex = 1,
                ServiceId = idBCSG_Service3,
            },
            new Procedure
            {
                Id = idBCSG_S3_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service3,
            },
            new Procedure
            {
                Id = idBCSG_S3_P3,
                Name = "Phẫu thuật nâng ngực nội soi với túi Mentor",
                Description = "Thực hiện phẫu thuật nâng ngực bằng kỹ thuật nội soi, đặt túi độn Mentor được FDA Hoa Kỳ chứng nhận.",
                StepIndex = 3,
                ServiceId = idBCSG_Service3,
            },
            new Procedure
            {
                Id = idBCSG_S3_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description = "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idBCSG_Service3,
            },
            new Procedure
            {
                Id = idBCSG_S3_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idBCSG_Service3,
            },

            // Beauty Center Sài Gòn Service 4 - Độn Cằm V-Line Hàn Quốc
            new Procedure
            {
                Id = idBCSG_S4_P1,
                Name = "Tư vấn và thiết kế đường cằm V-Line",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế đường cằm V-Line phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service4,
            },
            new Procedure
            {
                Id = idBCSG_S4_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang hàm mặt, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service4,
            },
            new Procedure
            {
                Id = idBCSG_S4_P3,
                Name = "Phẫu thuật độn cằm V-Line",
                Description = "Thực hiện phẫu thuật với kỹ thuật V-Line của Hàn Quốc, tạo đường cằm thanh thoát, cân đối với khuôn mặt.",
                StepIndex = 3,
                ServiceId = idBCSG_Service4,
            },
            new Procedure
            {
                Id = idBCSG_S4_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idBCSG_Service4,
            },

            // Beauty Center Sài Gòn Service 5 - Hút Mỡ Bụng VASER Lipo
            new Procedure
            {
                Id = idBCSG_S5_P1,
                Name = "Tư vấn và đánh giá vùng mỡ bụng",
                Description = "Bác sĩ đánh giá tình trạng mỡ bụng, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service5,
            },
            new Procedure
            {
                Id = idBCSG_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu, đo chỉ số BMI và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service5,
            },
            new Procedure
            {
                Id = idBCSG_S5_P3,
                Name = "Phẫu thuật hút mỡ bụng bằng công nghệ VASER Lipo",
                Description = "Thực hiện phẫu thuật hút mỡ siêu âm VASER Lipo, loại bỏ mỡ thừa vùng bụng một cách chọn lọc.",
                StepIndex = 3,
                ServiceId = idBCSG_Service5,
            },
            new Procedure
            {
                Id = idBCSG_S5_P4,
                Name = "Massage định hình sau phẫu thuật",
                Description = "Thực hiện liệu trình massage định hình vùng bụng sau hút mỡ, giúp làm săn chắc da và đều màu.",
                StepIndex = 4,
                ServiceId = idBCSG_Service5,
            },
            new Procedure
            {
                Id = idBCSG_S5_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idBCSG_Service5,
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite
            new Procedure
            {
                Id = idHBS_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idHBS_Service1,
            },
            new Procedure
            {
                Id = idHBS_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service1,
            },
            new Procedure
            {
                Id = idHBS_S1_P3,
                Name = "Phẫu thuật nâng mũi với Bio-Silicon Elite",
                Description = "Thực hiện phẫu thuật với vật liệu Bio-Silicon cao cấp, kết hợp sụn tự thân để tạo dáng mũi tự nhiên.",
                StepIndex = 3,
                ServiceId = idHBS_Service1,
            },
            new Procedure
            {
                Id = idHBS_S1_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service1,
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo
            new Procedure
            {
                Id = idHBS_S2_P1,
                Name = "Tư vấn và thiết kế đường mí",
                Description = "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp theo công nghệ Hàn Quốc.",
                StepIndex = 1,
                ServiceId = idHBS_Service2,
            },
            new Procedure
            {
                Id = idHBS_S2_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description = "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idHBS_Service2,
            },
            new Procedure
            {
                Id = idHBS_S2_P3,
                Name = "Phẫu thuật cắt mí với kỹ thuật không sẹo",
                Description = "Thực hiện phẫu thuật với kỹ thuật Hàn Quốc tiên tiến, đảm bảo không để lại sẹo, tạo đường mí tự nhiên.",
                StepIndex = 3,
                ServiceId = idHBS_Service2,
            },
            new Procedure
            {
                Id = idHBS_S2_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service2,
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu
            new Procedure
            {
                Id = idHBS_S3_P1,
                Name = "Tư vấn và đánh giá cung mày",
                Description = "Bác sĩ phân tích đặc điểm cung mày và khuôn mặt, tư vấn phương pháp nâng cung mày phù hợp.",
                StepIndex = 1,
                ServiceId = idHBS_Service3,
            },
            new Procedure
            {
                Id = idHBS_S3_P2,
                Name = "Kiểm tra và chuẩn bị trước điều trị",
                                    Description = "Kiểm tra sức khỏe tổng quát, vệ sinh vùng điều trị và chuẩn bị các thiết bị Hifu.",
                StepIndex = 2,
                ServiceId = idHBS_Service3,
            },
            new Procedure
            {
                Id = idHBS_S3_P3,
                Name = "Điều trị nâng cung mày bằng siêu âm Hifu",
                Description = "Sử dụng công nghệ siêu âm Hifu tác động vào tầng SMAS, kích thích tăng sinh collagen, nâng cơ mày.",
                StepIndex = 3,
                ServiceId = idHBS_Service3,
            },
            new Procedure
            {
                Id = idHBS_S3_P4,
                Name = "Chăm sóc và tái khám sau điều trị",
                Description = "Hướng dẫn chăm sóc sau điều trị và lịch tái khám để đánh giá kết quả.",
                StepIndex = 4,
                ServiceId = idHBS_Service3,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar
            new Procedure
            {
                Id = idHBS_S4_P1,
                Name = "Tư vấn và đánh giá tình trạng ngực",
                Description = "Bác sĩ phân tích kích thước, hình dáng ngực hiện tại và tư vấn kích thước, hình dáng mong muốn.",
                StepIndex = 1,
                ServiceId = idHBS_Service4,
            },
            new Procedure
            {
                Id = idHBS_S4_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service4,
            },
            new Procedure
            {
                Id = idHBS_S4_P3,
                Name = "Phẫu thuật thu nhỏ ngực Vertical Short-Scar",
                Description = "Thực hiện phẫu thuật với kỹ thuật Vertical Short-Scar giúp thu nhỏ ngực và tạo hình dáng tự nhiên với sẹo tối thiểu.",
                StepIndex = 3,
                ServiceId = idHBS_Service4,
            },
            new Procedure
            {
                Id = idHBS_S4_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description = "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idHBS_Service4,
            },
            new Procedure
            {
                Id = idHBS_S4_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idHBS_Service4,
            },

            // Hanoi Beauty Spa Service 5 - Hút Mỡ Mặt Precision
            new Procedure
            {
                Id = idHBS_S5_P1,
                Name = "Tư vấn và đánh giá vùng mỡ mặt",
                Description = "Bác sĩ đánh giá tình trạng mỡ mặt, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idHBS_Service5,
            },
            new Procedure
            {
                Id = idHBS_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service5,
            },
            new Procedure
            {
                Id = idHBS_S5_P3,
                Name = "Phẫu thuật hút mỡ mặt Precision",
                Description = "Thực hiện phẫu thuật hút mỡ mặt với kỹ thuật Precision, loại bỏ mỡ thừa và tạo đường nét khuôn mặt thanh tú.",
                StepIndex = 3,
                ServiceId = idHBS_Service5,
            },
            new Procedure
            {
                Id = idHBS_S5_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service5,
            },

            // Skin Care Đà Nẵng Service 1 - Nâng Mũi Cấu Trúc Hybrid
            new Procedure
            {
                Id = idSCDN_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service1,
            },
            new Procedure
            {
                Id = idSCDN_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service1,
            },
            new Procedure
            {
                Id = idSCDN_S1_P3,
                Name = "Phẫu thuật nâng mũi cấu trúc Hybrid",
                Description = "Thực hiện phẫu thuật với kỹ thuật Hybrid kết hợp sụn tự thân và vật liệu y khoa cao cấp.",
                StepIndex = 3,
                ServiceId = idSCDN_Service1,
            },
            new Procedure
            {
                Id = idSCDN_S1_P4,
                Name = "Theo dõi hậu phẫu 24h",
                Description = "Theo dõi tình trạng hậu phẫu 24h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idSCDN_Service1,
            },
            new Procedure
            {
                Id = idSCDN_S1_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idSCDN_Service1,
            },

            // Skin Care Đà Nẵng Service 2 - Nâng Ngực Nội Soi Ergonomix
            new Procedure
            {
                Id = idSCDN_S2_P1,
                Name = "Tư vấn và lựa chọn kích thước túi ngực",
                Description = "Bác sĩ phân tích cấu trúc cơ thể, tư vấn và giúp khách hàng lựa chọn kích thước túi ngực Ergonomix phù hợp.",
                StepIndex = 1,
                ServiceId = idSCDN_Service2,
            },
            new Procedure
            {
                Id = idSCDN_S2_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service2,
            },
            new Procedure
            {
                Id = idSCDN_S2_P3,
                Name = "Phẫu thuật nâng ngực nội soi với túi Ergonomix",
                Description = "Thực hiện phẫu thuật nâng ngực bằng kỹ thuật nội soi với túi Ergonomix có hình giọt nước tự nhiên.",
                StepIndex = 3,
                ServiceId = idSCDN_Service2,
            },
            new Procedure
            {
                Id = idSCDN_S2_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description = "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idSCDN_Service2,
            },
            new Procedure
            {
                Id = idSCDN_S2_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idSCDN_Service2,
            },

            // Skin Care Đà Nẵng Service 3 - Hút Mỡ Bụng 3D Hi-Definition
            new Procedure
            {
                Id = idSCDN_S3_P1,
                Name = "Tư vấn và đánh giá vùng mỡ bụng",
                Description = "Bác sĩ đánh giá tình trạng mỡ bụng, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service3,
            },
            new Procedure
            {
                Id = idSCDN_S3_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu, đo chỉ số BMI và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service3,
            },
            new Procedure
            {
                Id = idSCDN_S3_P3,
                Name = "Phẫu thuật hút mỡ bụng 3D Hi-Definition",
                Description = "Thực hiện phẫu thuật hút mỡ bụng với công nghệ 3D Hi-Definition, tạo đường nét cơ bụng rõ ràng.",
                StepIndex = 3,
                ServiceId = idSCDN_Service3,
            },
            new Procedure
            {
                Id = idSCDN_S3_P4,
                Name = "Massage định hình sau phẫu thuật",
                Description = "Thực hiện liệu trình massage định hình vùng bụng sau hút mỡ, giúp làm săn chắc da và đều màu.",
                StepIndex = 4,
                ServiceId = idSCDN_Service3,
            },
            new Procedure
            {
                Id = idSCDN_S3_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idSCDN_Service3,
            },

            // Skin Care Đà Nẵng Service 4 - Cắt Mí Mắt Plasma Tech
            new Procedure
            {
                Id = idSCDN_S4_P1,
                Name = "Tư vấn và thiết kế đường mí",
                Description = "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service4,
            },
            new Procedure
            {
                Id = idSCDN_S4_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description = "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idSCDN_Service4,
            },
            new Procedure
            {
                Id = idSCDN_S4_P3,
                Name = "Phẫu thuật cắt mí với công nghệ Plasma Tech",
                Description = "Thực hiện phẫu thuật với công nghệ Plasma Tech hiện đại, tạo đường mí tự nhiên, giảm thiểu đau đớn và thời gian hồi phục.",
                StepIndex = 3,
                ServiceId = idSCDN_Service4,
            },
            new Procedure
            {
                Id = idSCDN_S4_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idSCDN_Service4,
            },

            // Skin Care Đà Nẵng Service 5 - Độn Cằm 3D Crystal
            new Procedure
            {
                Id = idSCDN_S5_P1,
                Name = "Tư vấn và thiết kế dáng cằm",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng cằm phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service5,
            },
            new Procedure
            {
                Id = idSCDN_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, chụp X-quang hàm mặt, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service5,
            },
            new Procedure
            {
                Id = idSCDN_S5_P3,
                Name = "Phẫu thuật độn cằm với vật liệu 3D Crystal",
                Description = "Thực hiện phẫu thuật độn cằm với vật liệu 3D Crystal cao cấp, tạo dáng cằm tự nhiên và cân đối.",
                StepIndex = 3,
                ServiceId = idSCDN_Service5,
            },
            new Procedure
            {
                Id = idSCDN_S5_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idSCDN_Service5,
            }
        };
        
        var procedurePriceTypes = new List<ProcedurePriceType>()
        {
            // Beauty Center Sài Gòn Service 1 - Nâng Mũi Cấu Trúc S-Line Premium - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn chuyên sâu",
                Price = 1000000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1001-4a2f-0003-3d05b7e1f2a3"),
                Name = "Tư vấn VIP kèm mô phỏng 3D",
                Price = 2000000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P1
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 1500000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 2500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P2
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật cơ bản",
                Price = 25000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật cao cấp",
                Price = 30000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật VIP với bác sĩ chuyên gia",
                Price = 40000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P3
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu tiêu chuẩn",
                Price = 1000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idBCSG_S1_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu VIP",
                Price = 2500000,
                Duration = 1440, // 24 hours
                IsDefault = false,
                ProcedureId = idBCSG_S1_P4
            },

            // Beauty Center Sài Gòn Service 1 - Procedure 5
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói tái khám cơ bản",
                Price = 2000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S1_P5
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a1d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói tái khám VIP",
                Price = 3500000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S1_P5
            },

            // Beauty Center Sài Gòn Service 2 - Cắt Mí Mắt Plasma Luxury - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế mí cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế mí chuyên sâu",
                Price = 900000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P1
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra tiêu chuẩn",
                Price = 800000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra nâng cao",
                Price = 1200000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P2
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma tiêu chuẩn",
                Price = 10000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma cao cấp",
                Price = 15000000,
                Duration = 150,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật cắt mí Plasma Luxury",
                Price = 18000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P3
            },

            // Beauty Center Sài Gòn Service 2 - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc hậu phẫu cơ bản",
                Price = 1500000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S2_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a2d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc hậu phẫu cao cấp",
                Price = 2500000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S2_P4
            },

            // Beauty Center Sài Gòn Service 3 - Nâng Ngực Túi Độn Mentor - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn và lựa chọn túi ngực tiêu chuẩn",
                Price = 1000000,
                Duration = 90,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn và lựa chọn túi ngực cao cấp kèm mô phỏng 3D",
                Price = 2500000,
                Duration = 120,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P1
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 2000000,
                Duration = 150,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 3500000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P2
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor cơ bản",
                Price = 65000000,
                Duration = 240,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor cao cấp",
                Price = 75000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật nâng ngực Mentor VIP",
                Price = 85000000,
                Duration = 300,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P3
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu tiêu chuẩn",
                Price = 2000000,
                Duration = 2880, // 48 hours
                IsDefault = true,
                ProcedureId = idBCSG_S3_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Theo dõi hậu phẫu VIP",
                Price = 5000000,
                Duration = 2880, // 48 hours
                IsDefault = false,
                ProcedureId = idBCSG_S3_P4
            },

            // Beauty Center Sài Gòn Service 3 - Procedure 5
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói tái khám định kỳ tiêu chuẩn",
                Price = 3000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S3_P5
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a3d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói tái khám định kỳ VIP",
                Price = 5000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S3_P5
            },

            // Beauty Center Sài Gòn Service 4 - Độn Cằm V-Line Hàn Quốc - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế cằm V-Line cơ bản",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn thiết kế cằm V-Line chuyên sâu với mô phỏng 3D",
                Price = 1500000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P1
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn",
                Price = 1200000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện",
                Price = 2000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P2
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line cơ bản",
                Price = 25000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line cao cấp",
                Price = 28000000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1003-4a2f-0003-3d05b7e1f2a3"),
                Name = "Phẫu thuật độn cằm V-Line VIP",
                Price = 32000000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P3
            },

            // Beauty Center Sài Gòn Service 4 - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám tiêu chuẩn",
                Price = 2000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S4_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám cao cấp",
                Price = 3500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S4_P4
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn đánh giá vùng mỡ bụng tiêu chuẩn",
                Price = 500000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1005-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn đánh giá vùng mỡ bụng chuyên sâu với chụp hình 3D",
                Price = 1500000,
                Duration = 90,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P1
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1006-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe tiêu chuẩn trước phẫu thuật",
                Price = 1500000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1006-4a2f-0002-3d05b7e1f2a3"),
                Name = "Kiểm tra sức khỏe toàn diện trước phẫu thuật",
                Price = 2500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P2
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0001-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo vùng nhỏ",
                Price = 30000000,
                Duration = 180,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0002-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo vùng trung bình",
                Price = 45000000,
                Duration = 210,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1007-4a2f-0003-3d05b7e1f2a3"),
                Name = "Hút mỡ bụng VASER Lipo toàn vùng bụng",
                Price = 60000000,
                Duration = 240,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P3
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1008-4a2f-0001-3d05b7e1f2a3"),
                Name = "Liệu trình massage định hình cơ bản (5 buổi)",
                Price = 5000000,
                Duration = 60,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1008-4a2f-0002-3d05b7e1f2a3"),
                Name = "Liệu trình massage định hình cao cấp (10 buổi)",
                Price = 9000000,
                Duration = 60,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P4
            },

            // Beauty Center Sài Gòn Service 5 - Procedure 5
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1009-4a2f-0001-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám tiêu chuẩn (3 tháng)",
                Price = 3000000,
                Duration = 120,
                IsDefault = true,
                ProcedureId = idBCSG_S5_P5
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("a4d3d799-1009-4a2f-0002-3d05b7e1f2a3"),
                Name = "Gói chăm sóc và tái khám cao cấp (6 tháng)",
                Price = 5500000,
                Duration = 180,
                IsDefault = false,
                ProcedureId = idBCSG_S5_P5
            },
            
            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Dáng Mũi Cá Nhân Hóa",
                Price = 12000000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P1,
                Duration = 60,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Thiết Kế Dáng Mũi 3D Chuyên Sâu",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P1,
                Duration = 90,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1113-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Quy Trình Thiết Kế VIP Cùng Chuyên Gia Hàn Quốc",
                Price = 25000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P1,
                Duration = 120,
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Đánh Giá Sức Khỏe Pre-Surgery Chuẩn Y Khoa",
                Price = 1500000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P2,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Gói Kiểm Tra Toàn Diện Với Bác Sĩ Chuyên Khoa",
                Price = 2500000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P2,
                Duration = 45,
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Natural Look Với Sụn Tự Thân",
                Price = 20000000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P3,
                Duration = 120,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Perfect Balance - Công Nghệ Kết Hợp",
                Price = 30000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P3,
                Duration = 150,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Nâng Mũi Luxury Elite - Công Nghệ Hàn Quốc Độc Quyền",
                Price = 45000000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P3,
                Duration = 180,
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Nhanh - Gói Chăm Sóc Thiết Yếu",
                Price = 500000M,
                IsDefault = true,
                ProcedureId = idHBS_S1_P4,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b1d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Golden Care - Gói Phục Hồi Toàn Diện Cao Cấp",
                Price = 1200000M,
                IsDefault = false,
                ProcedureId = idHBS_S1_P4,
                Duration = 60,
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Mí Quyến Rũ - Gói Cơ Bản",
                Price = 8000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P1,
                Duration = 60,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Mí Tự Nhiên 3D - Gói Cao Cấp",
                Price = 12000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P1,
                Duration = 75,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1113-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Celebrity Eyes - Tư Vấn VIP Cùng Chuyên Gia Hàn Quốc",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P1,
                Duration = 90,
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Sức Khỏe An Toàn Trước Phẫu Thuật",
                Price = 1000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P2,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Đánh Giá Sức Khỏe Toàn Diện Premium",
                Price = 2000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P2,
                Duration = 45,
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Mí Tự Nhiên - Công Nghệ Không Sẹo Hàn Quốc",
                Price = 15000000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P3,
                Duration = 90,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Double-Fold Premium - Kỹ Thuật Chuyên Gia",
                Price = 22000000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P3,
                Duration = 120,
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Nhanh - Chăm Sóc Sau Phẫu Thuật",
                Price = 800000M,
                IsDefault = true,
                ProcedureId = idHBS_S2_P4,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b2d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tái Sinh Siêu Tốc - Liệu Trình Phục Hồi Cao Cấp",
                Price = 1500000M,
                IsDefault = false,
                ProcedureId = idHBS_S2_P4,
                Duration = 45,
            },

            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Định Hình Cung Mày Cá Nhân Hóa",
                Price = 5000000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P1,
                Duration = 45,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phân Tích & Thiết Kế Cung Mày 3D Cao Cấp",
                Price = 8000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P1,
                Duration = 60,
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Chuẩn Bị Hifu Standard - An Toàn Tối Ưu",
                Price = 800000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P2,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Chuẩn Bị Hifu Premium - Kiểm Tra Toàn Diện",
                Price = 1200000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P2,
                Duration = 45,
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Power Lift - Liệu Trình Đơn",
                Price = 7000000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P3,
                Duration = 60,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Ultra Lift - Liệu Trình 3 Buổi Tăng Cường",
                Price = 18000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P3,
                Duration = 180,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Hifu Royal V-Shape - Liệu Trình Hoàng Gia 5 Buổi",
                Price = 28000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P3,
                Duration = 300,
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Express - Theo Dõi Kết Quả Chuẩn Y Khoa",
                Price = 600000M,
                IsDefault = true,
                ProcedureId = idHBS_S3_P4,
                Duration = 30,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b3d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Luxury - Liệu Trình Dưỡng Ẩm Chuyên Sâu",
                Price = 1000000M,
                IsDefault = false,
                ProcedureId = idHBS_S3_P4,
                Duration = 45,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 1
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1111-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Định Hình Dáng Ngực Tự Nhiên",
                Price = 40000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P1,
                Duration = 180,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1112-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Tư Vấn Thẩm Mỹ Ngực Toàn Diện Với Chuyên Gia Quốc Tế",
                Price = 65000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P1,
                Duration = 240,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 2
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1121-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Tiền Phẫu Standard - An Toàn Tối Ưu",
                Price = 2000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P2,
                Duration = 45,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1122-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Kiểm Tra Tiền Phẫu Premium - Đánh Giá Chuyên Sâu",
                Price = 3500000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P2,
                Duration = 60,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 3
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1131-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Vertical Short-Scar Chuẩn Quốc Tế",
                Price = 55000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P3,
                Duration = 180,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1132-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Short-Scar Premium - Dáng Ngực Hoàn Hảo",
                Price = 75000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P3,
                Duration = 240,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1133-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phẫu Thuật Executive Suite - Dáng Ngực Tự Nhiên Không Dấu Vết",
                Price = 100000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P3,
                Duration = 300,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 4
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1141-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "48h Recovery Suite - Chăm Sóc Phục Hồi Chuyên Nghiệp",
                Price = 5000000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P4,
                Duration = 60,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1142-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "48h VIP Recovery Suite - Phục Hồi Cao Cấp Với Y Tá Riêng",
                Price = 8000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P4,
                Duration = 90,
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar - Procedure 5
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1151-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Sau Phẫu Thuật - Gói 3 Tháng Theo Dõi",
                Price = 1500000M,
                IsDefault = true,
                ProcedureId = idHBS_S4_P5,
                Duration = 45,
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("b4d3d799-1152-4a2f-b6b5-3d05b7e1f2a3"),
                Name = "Phục Hồi Toàn Diện - Gói VIP 6 Tháng Với Bác Sĩ Chuyên Khoa",
                Price = 3000000M,
                IsDefault = false,
                ProcedureId = idHBS_S4_P5,
                Duration = 60,
            },
            
            // Hanoi Beauty Spa Service 5 - Hút Mỡ Mặt Precision
            // Procedure 1 - Tư vấn và đánh giá vùng mỡ mặt
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Đánh Giá Tạo Hình V-Line Tự Nhiên",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idHBS_S5_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư Vấn Tạo Hình Khuôn Mặt 3D Chuyên Sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P1
            },

            // Procedure 2 - Kiểm tra sức khỏe và chuẩn bị tiền phẫu
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Kiểm Tra Sức Khỏe Pre-Surgery Standard",
                Price = 1500000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Đánh Giá Tiền Phẫu Toàn Diện Premium",
                Price = 2500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P2
            },

            // Procedure 3 - Phẫu thuật hút mỡ mặt Precision
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Precision V-Shape Facial Contouring",
                Price = 8000000,
                Duration = 240, // 4 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Elite Precision 360° Facial Sculpting",
                Price = 12000000,
                Duration = 300, // 5 hours
                IsDefault = false,
                ProcedureId = idHBS_S5_P3
            },

            // Procedure 4 - Chăm sóc và tái khám sau phẫu thuật
            new ProcedurePriceType
            {
                Id = Guid.Parse("d5d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phục Hồi 24h - Chăm Sóc Hậu Phẫu Chuyên Nghiệp",
                Price = 1000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idHBS_S5_P4
            },
            new ProcedurePriceType
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
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn nâng mũi cơ bản",
                Price = 600000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S1_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn nâng mũi chuyên sâu",
                Price = 900000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P1
            },

            // Procedure 2 - Phân tích và thiết kế
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phân tích và thiết kế tiêu chuẩn",
                Price = 2000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phân tích và thiết kế 3D",
                Price = 3000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P2
            },

            // Procedure 3 - Chuẩn bị vật liệu
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Vật liệu tiêu chuẩn",
                Price = 5000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S1_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Vật liệu cao cấp nhập khẩu",
                Price = 8000000,
                Duration = 60, // 1 hour
                IsDefault = false,
                ProcedureId = idSCDN_S1_P3
            },

            // Procedure 4 - Thực hiện phẫu thuật
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật tiêu chuẩn",
                Price = 10000000,
                Duration = 180, // 3 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật kỹ thuật cao",
                Price = 15000000,
                Duration = 240, // 4 hours
                IsDefault = false,
                ProcedureId = idSCDN_S1_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new ProcedurePriceType
            {
                Id = Guid.Parse("e1d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 1500000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S1_P5
            },
            new ProcedurePriceType
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
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn nâng ngực cơ bản",
                Price = 700000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S2_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn nâng ngực chuyên sâu",
                Price = 1200000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P1
            },

            // Procedure 2 - Phân tích và thiết kế
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 3000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D Simulation",
                Price = 5000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P2
            },

            // Procedure 3 - Chuẩn bị túi độn
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Túi độn tiêu chuẩn",
                Price = 15000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S2_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Túi độn cao cấp Mentor",
                Price = 25000000,
                Duration = 60, // 1 hour
                IsDefault = false,
                ProcedureId = idSCDN_S2_P3
            },

            // Procedure 4 - Thực hiện phẫu thuật
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Phẫu thuật tiêu chuẩn",
                Price = 20000000,
                Duration = 240, // 4 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Phẫu thuật nội soi cao cấp",
                Price = 30000000,
                Duration = 300, // 5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S2_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new ProcedurePriceType
            {
                Id = Guid.Parse("e2d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 2000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S2_P5
            },
            new ProcedurePriceType
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
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn tiêu chuẩn",
                Price = 600000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S3_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn và đánh giá chuyên sâu",
                Price = 1000000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P1
            },

            // Procedure 2 - Thiết kế định hình cơ thể
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Định hình cơ bản",
                Price = 1500000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Định hình 3D precision",
                Price = 2500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P2
            },

            // Procedure 3 - Chuẩn bị trước phẫu thuật
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chuẩn bị tiêu chuẩn",
                Price = 2000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chuẩn bị cao cấp",
                Price = 3500000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P3
            },

            // Procedure 4 - Thực hiện hút mỡ
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Hút mỡ tiêu chuẩn",
                Price = 15000000,
                Duration = 180, // 3 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Hút mỡ 3D Hi-Definition",
                Price = 25000000,
                Duration = 240, // 4 hours
                IsDefault = false,
                ProcedureId = idSCDN_S3_P4
            },

            // Procedure 5 - Theo dõi và chăm sóc hậu phẫu
            new ProcedurePriceType
            {
                Id = Guid.Parse("e3d3d799-1005-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc phục hồi tiêu chuẩn",
                Price = 2000000,
                Duration = 1440, // 24 hours
                IsDefault = true,
                ProcedureId = idSCDN_S3_P5
            },
            new ProcedurePriceType
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
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn cơ bản",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S4_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn chuyên sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P1
            },

            // Procedure 2 - Thiết kế mí mắt
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 1000000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S4_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D precision",
                Price = 1800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P2
            },

            // Procedure 3 - Thực hiện cắt mí
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Cắt mí tiêu chuẩn",
                Price = 8000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S4_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Cắt mí Plasma Tech cao cấp",
                Price = 12000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S4_P3
            },

            // Procedure 4 - Theo dõi và chăm sóc
            new ProcedurePriceType
            {
                Id = Guid.Parse("e4d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc tiêu chuẩn",
                Price = 1200000,
                Duration = 720, // 12 hours
                IsDefault = true,
                ProcedureId = idSCDN_S4_P4
            },
            new ProcedurePriceType
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
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1001-4a2f-0001-3d05b7e1f2a3"),
                Name = "Tư vấn độn cằm cơ bản",
                Price = 500000,
                Duration = 60, // 1 hour
                IsDefault = true,
                ProcedureId = idSCDN_S5_P1
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1001-4a2f-0002-3d05b7e1f2a3"),
                Name = "Tư vấn độn cằm chuyên sâu",
                Price = 800000,
                Duration = 90, // 1.5 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P1
            },

            // Procedure 2 - Thiết kế và tạo hình
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1002-4a2f-0001-3d05b7e1f2a3"),
                Name = "Thiết kế tiêu chuẩn",
                Price = 1500000,
                Duration = 90, // 1.5 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P2
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1002-4a2f-0002-3d05b7e1f2a3"),
                Name = "Thiết kế 3D Crystal",
                Price = 2500000,
                Duration = 120, // 2 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P2
            },

            // Procedure 3 - Thực hiện độn cằm
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1003-4a2f-0001-3d05b7e1f2a3"),
                Name = "Độn cằm silicon tiêu chuẩn",
                Price = 10000000,
                Duration = 120, // 2 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P3
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1003-4a2f-0002-3d05b7e1f2a3"),
                Name = "Độn cằm 3D Crystal cao cấp",
                Price = 15000000,
                Duration = 180, // 3 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P3
            },

            // Procedure 4 - Theo dõi và chăm sóc
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1004-4a2f-0001-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu tiêu chuẩn",
                Price = 1200000,
                Duration = 720, // 12 hours
                IsDefault = true,
                ProcedureId = idSCDN_S5_P4
            },
            new ProcedurePriceType
            {
                Id = Guid.Parse("e5d3d799-1004-4a2f-0002-3d05b7e1f2a3"),
                Name = "Chăm sóc hậu phẫu VIP",
                Price = 2500000,
                Duration = 1440, // 24 hours
                IsDefault = false,
                ProcedureId = idSCDN_S5_P4
            }
        };
        
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
        
        var staff = new List<Staff>
            {
                // Doctors (12) - Each staff manages 2 doctors (total 12 doctors for 6 staff)
                new ()
                {
                    Id = new Guid("ab23d158-44e2-44d4-b679-d7c568993702"),
                    Email = "lethithanhdoan@gmail.com",
                    FirstName = "Lê Thị Thanh",
                    LastName = "Đoan",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("a2b21279-5bbd-40c3-8981-6821c7f6b2ea"),
                    Email = "phamminhhoang@gmail.com",
                    FirstName = "Phạm Minh",
                    LastName = "Hoàng",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("da2d6a80-75cc-4757-8ed3-e0b508ffb080"),
                    Email = "trinhthuonglam@gmail.com",
                    FirstName = "Trịnh Thượng",
                    LastName = "Lâm",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("8b0f0b23-f07f-453d-b8bc-1acb26d03d87"),
                    Email = "doanthanhtien@gmail.com",
                    FirstName = "Đoàn Thanh",
                    LastName = "Tiến",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("79f207d4-dbbf-488d-848d-fc74a7fdbb29"),
                    Email = "phanvankhoa@gmail.com",
                    FirstName = "Phan Văn",
                    LastName = "Khoa",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("32e8cfbb-d8b4-4768-8695-81b6b7e63c63"),
                    Email = "hoangminhtrang@gmail.com",
                    FirstName = "Hoàng Minh",
                    LastName = "Trang",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("c21fa13a-b2f6-4eba-8b77-d2c57854bc5f"),
                    Email = "tranthanhlong@gmail.com",
                    FirstName = "Trần Thanh",
                    LastName = "Long",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("bd3c9480-7bca-43d7-94ed-58cea8b32733"),
                    Email = "nguyenngocmaihuong@gmail.com",
                    FirstName = "Nguyễn Ngọc Mai",
                    LastName = "Hương",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("5e9bd8dd-fb53-4f74-bf59-b3aedb96aa7c"),
                    Email = "voanhquan@gmail.com",
                    FirstName = "Võ Anh",
                    LastName = "Quân",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("71ddc3c7-a3a0-4e6b-bd1d-f03d5deedbfa"),
                    Email = "lethikimhoa@gmail.com",
                    FirstName = "Lê Thị Kim",
                    LastName = "Hoa",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("e8e3f18b-9179-48a6-94bb-1e5320fb8f30"),
                    Email = "phamtuanminh@gmail.com",
                    FirstName = "Phạm Tuấn",
                    LastName = "Minh",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                },
                new ()
                {
                    Id = new Guid("f76d8ab1-c9eb-4e29-a9f1-5302b543c283"),
                    Email = "nguyenminhhieu@gmail.com",
                    FirstName = "Nguyễn Minh",
                    LastName = "Hiếu",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("b549752a-f156-4894-90ad-ab3994fd071d")
                }
            };

        var branding = new List<Clinic>
        {
            // Main Clinics (3) - one for each Clinic Admin
            new Clinic
            {
                Id = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                Name = "Beauty Center Sài Gòn",
                Email = "beautycenter.saigon@gmail.com",
                PhoneNumber = "0283456789",
                City = "Hồ Chí Minh",
                TaxCode = "12345678901",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-1.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-1.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "Vietcombank",
                BankAccountNumber = "1234567890123"
            },
            new Clinic
            {
                Id = new Guid("a96d68d9-3f28-48f3-add5-a74a6b882e93"),
                Name = "Hanoi Beauty Spa",
                Email = "hanoi.beautyspa@gmail.com",
                PhoneNumber = "0243812345",
                City = "Hà Nội",
                TaxCode = "23456789012",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-2.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-2.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "BIDV",
                BankAccountNumber = "2345678901234"
            },
            new Clinic
            {
                Id = new Guid("e5a759cd-af8d-4a1c-8c05-43cc2c95e067"),
                Name = "Skin Care Đà Nẵng",
                Email = "skincare.danang@gmail.com",
                PhoneNumber = "0236789123",
                City = "Đà Nẵng",
                TaxCode = "34567890123",
                BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-3.pdf",
                OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-3.pdf",
                Status = 1,
                TotalBranches = 2,
                IsParent = true,
                ParentId = null,
                BankName = "Agribank",
                BankAccountNumber = "3456789012345"
            }
        };
        
        var clinicDictionary = new Dictionary<Guid, Guid>
        {
            { Guid.Parse("c0b7058f-8e72-4dee-8742-0df6206d1843"), Guid.Parse("78705cfa-7097-408f-93e2-70950fc886a3") }, // Child -> Parent
            { Guid.Parse("6e7e4870-d28d-4a2d-9d0f-9e29f2930fc5"), Guid.Parse("78705cfa-7097-408f-93e2-70950fc886a3") },
            { Guid.Parse("f3e6a7ca-28f9-4c7b-a190-c065cecf7be3"), Guid.Parse("a96d68d9-3f28-48f3-add5-a74a6b882e93") },
            { Guid.Parse("c96de07e-32d7-41d5-b417-060cd95ee7ff"), Guid.Parse("a96d68d9-3f28-48f3-add5-a74a6b882e93") },
            { Guid.Parse("3c8b8f3d-2f3f-4b17-9b46-0517c0183a50"), Guid.Parse("e5a759cd-af8d-4a1c-8c05-43cc2c95e067") },
            { Guid.Parse("6ed1aefc-863e-4f2e-9c24-83eec7c0181c"), Guid.Parse("e5a759cd-af8d-4a1c-8c05-43cc2c95e067") }
        };
        
        var createServiceEvents = services.Select(se =>
        {
            var clinicOfServices = clinicServices.Where(cs => cs.ServiceId.Equals(se.Id));
            
            var parentId = clinicDictionary
                .FirstOrDefault(x => x.Key.Equals(clinicOfServices.FirstOrDefault()!.ClinicId)).Value;
            
            var brand = branding
                .FirstOrDefault(x => x.Id.Equals(parentId))!;
            
            return new ClinicServicesDomainEvent.ClinicServiceCreated(
                Guid.NewGuid(),
                new ClinicServiceEvent.CreateClinicService(
                    se.Id, se.Name, se.Description, new ClinicServiceEvent.Clinic(
                        brand.Id, brand.Name, brand.Email,
                        brand.City, brand.Address, brand.FullAddress,
                        brand.District, brand.Ward, brand.PhoneNumber,
                        brand.ProfilePictureUrl,
                        brand.IsParent, brand.ParentId),
                    [],
                    new ClinicServiceEvent.Category(
                        categories.FirstOrDefault(x => x.Id.Equals(se.CategoryId))!.Id,
                        categories.FirstOrDefault(x => x.Id.Equals(se.CategoryId))!.Name,
                        categories.FirstOrDefault(x => x.Id.Equals(se.CategoryId)).Description
                    ),
                    clinicOfServices
                        .Select(y =>
                        {
                            var x = clinics.FirstOrDefault(z => z.Id.Equals(y.ClinicId));

                            return new ClinicServiceEvent.Clinic(x.Id, x.Name, x.Email,
                                x.City, x.Address, x.FullAddress, x.District, x.Ward, x.PhoneNumber,
                                x.ProfilePictureUrl,
                                x.IsParent, x.ParentId);
                        }).ToList()
                ));
        }).ToList();
        
        var createProcedureEvents = procedures.Select(pro => new ProceduresDomainEvent.ProcedureCreated(
            Guid.NewGuid(),
            new ProcedureEvent.CreateProcedure(
                pro.Id, (Guid)pro.ServiceId!, pro.Name, pro.Description, 0, 0, 0,
                0, pro.StepIndex, procedurePriceTypes.Where(prt => prt.ProcedureId.Equals(pro.Id)).Select(
                    x => new ProcedureEvent.ProcedurePriceType(
                        x.Id, x.Name, x.Price, x.Duration, x.IsDefault
                    )).ToList()
            ))).ToList();
        
        
        var doctorServiceEventEntity = doctorServices
            .Select(x =>
            {
                var doctor = staff.FirstOrDefault(y => y.Id.Equals(x.DoctorId))!;
                return new EntityEvent.DoctorServiceEntity()
                {
                    Id = Guid.NewGuid(),
                    ServiceId = x.ServiceId,
                    Doctor = new EntityEvent.UserEntity()
                    {
                        Id = x.DoctorId,
                        FullName = doctor.FirstName + " " + doctor.LastName,
                        Email = doctor.Email,
                        PhoneNumber = doctor.PhoneNumber,
                        ProfilePictureUrl = doctor.ProfilePicture
                    }
                };
            }).ToList();

        var doctorServiceEvent = new DomainEvents.DoctorServiceCreated(Guid.NewGuid(), doctorServiceEventEntity);
        
        var outBoxCreateServices = createServiceEvents.Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();
        
        var outBoxCreateProcedures = createProcedureEvents.Select(domainEvent => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                OccurredOnUtc = DateTime.UtcNow,
                Type = domainEvent.GetType().Name,
                Content = JsonConvert.SerializeObject(
                    domainEvent,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    })
            })
            .ToList();

        var outBoxCreateDoctorServices = new OutboxMessage
        {
            Id = Guid.NewGuid(),
            OccurredOnUtc = DateTime.UtcNow,
            Type = doctorServiceEvent.GetType().Name,
            Content = JsonConvert.SerializeObject(
                doctorServiceEvent,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                })
        };
        
        var outBoxList = new List<OutboxMessage>();
        
        outBoxList.AddRange(outBoxCreateServices);
        
        outBoxList.AddRange(outBoxCreateProcedures);
        
        outBoxList.Add(outBoxCreateDoctorServices);

        builder.HasData(outBoxList);
    }
}