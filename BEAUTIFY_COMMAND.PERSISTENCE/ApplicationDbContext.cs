using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BEAUTIFY_COMMAND.PERSISTENCE;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TriggerOutbox> TriggerOutboxs { get; set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(AssemblyReference.Assembly);
        builder.Entity<CustomerSchedule>()
            .HasOne(cs => cs.Customer)
            .WithMany(u => u.CustomerSchedules)
            .HasForeignKey(cs => cs.CustomerId)
            .OnDelete(DeleteBehavior.NoAction); // Prevent cascade delete
        
        builder.Entity<User>()
            .HasIndex(x => x.Email)
            .IsUnique()
            .HasAnnotation("SqlServer:CaseSensitive", true);
        builder.Entity<User>()
            .HasIndex(x => x.PhoneNumber)
            .IsUnique();

        builder.Entity<SubscriptionPackage>()
            .HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<Role>()
            .HasData(
                new Role { Id = Guid.NewGuid(), Name = "Admin" },
                new Role { Id = Guid.NewGuid(), Name = "Doctor" },
                new Role { Id = Guid.NewGuid(), Name = "Customer" },
                new Role { Id = Guid.NewGuid(), Name = "Staff" }
            );
        
        var idPhauThuatThamMy = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var idNangMui = Guid.Parse("11111111-1111-1111-1111-111111111112");
        var idThamMyMat = Guid.Parse("11111111-1111-1111-1111-111111111113");
        var idTreoChanMay = Guid.Parse("11111111-1111-1111-1111-111111111114");
        var idCangDaMat = Guid.Parse("11111111-1111-1111-1111-111111111115");
        var idThamMyGoMa = Guid.Parse("11111111-1111-1111-1111-111111111116");
        var idThamMyMoi = Guid.Parse("11111111-1111-1111-1111-111111111117");
        var idNangNguc = Guid.Parse("11111111-1111-1111-1111-111111111118");
        var idHutMoTayChan = Guid.Parse("11111111-1111-1111-1111-111111111119");

        var idThamMyKoPhauThuat = Guid.Parse("22222222-2222-2222-2222-222222222221");
        var idTiemFiller = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var idTiemBotox = Guid.Parse("22222222-2222-2222-2222-222222222223");
        var idTiemTanMo = Guid.Parse("22222222-2222-2222-2222-222222222224");

        var idSpa = Guid.Parse("33333333-3333-3333-3333-333333333331");
        var idChamSocDaMat = Guid.Parse("33333333-3333-3333-3333-333333333332");
        var idChamSocCoThe = Guid.Parse("33333333-3333-3333-3333-333333333333");
        var idTrietLong = Guid.Parse("33333333-3333-3333-3333-333333333334");

        var idChamSocDaChuyenSau = Guid.Parse("44444444-4444-4444-4444-444444444441");
        var idTriMun = Guid.Parse("44444444-4444-4444-4444-444444444442");
        var idTriNamTanNhang = Guid.Parse("44444444-4444-4444-4444-444444444443");
        var idTriSeoRo = Guid.Parse("44444444-4444-4444-4444-444444444444");
        var idTaiTaoDa = Guid.Parse("44444444-4444-4444-4444-444444444445");
        
        builder.Entity<Category>()
            .HasData(
                // ------------------------ Top-level 1: Phẫu Thuật Thẩm Mỹ ------------------------
            new Category
            {
                Id = idPhauThuatThamMy,
                Name = "Phẫu Thuật Thẩm Mỹ",
                Description = "Dịch vụ phẫu thuật can thiệp ngoại khoa",
                IsParent = true,
                ParentId = null
            },
            // ------------ Phẫu Thuật Thẩm Mỹ -> Nâng mũi ------------
            new Category
            {
                Id = idNangMui,
                Name = "Nâng mũi",
                Description = "Nhóm dịch vụ thẩm mỹ nâng mũi",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111120"),
                Name = "Nâng mũi Hàn Quốc S Line",
                Description = null,
                IsParent = false,
                ParentId = idNangMui
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111121"),
                Name = "Thu gọn cánh mũi",
                Description = null,
                IsParent = false,
                ParentId = idNangMui
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111122"),
                Name = "Nâng mũi sụn Surgiform",
                Description = null,
                IsParent = false,
                ParentId = idNangMui
            },
            // ------------ Phẫu Thuật Thẩm Mỹ -> Thẩm mỹ mắt ------------
            new Category
            {
                Id = idThamMyMat,
                Name = "Thẩm mỹ mắt",
                Description = "Nhóm dịch vụ làm đẹp cho mắt",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111123"),
                Name = "Cắt mắt 2 mí Hàn Quốc",
                Description = null,
                IsParent = false,
                ParentId = idThamMyMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111124"),
                Name = "Bấm mí Hàn Quốc (Nhấn mí mắt)",
                Description = null,
                IsParent = false,
                ParentId = idThamMyMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111125"),
                Name = "Cắt mí dưới",
                Description = null,
                IsParent = false,
                ParentId = idThamMyMat
            },
            // ------------ Phẫu Thuật Thẩm Mỹ -> Treo Chân Mày ------------
            new Category
            {
                Id = idTreoChanMay,
                Name = "Treo Chân Mày",
                Description = "Nhóm dịch vụ nâng cung chân mày",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111126"),
                Name = "Treo chân mày",
                Description = null,
                IsParent = false,
                ParentId = idTreoChanMay
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111127"),
                Name = "Trẻ hóa vùng mắt bằng phương pháp nâng cung chân mày",
                Description = null,
                IsParent = false,
                ParentId = idTreoChanMay
            },
            // ------------ Phẫu Thuật Thẩm Mỹ -> Căng da mặt ------------
            new Category
            {
                Id = idCangDaMat,
                Name = "Căng da mặt",
                Description = "Nhóm dịch vụ căng/trẻ hóa da mặt",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111128"),
                Name = "Căng da mặt bằng chỉ",
                Description = null,
                IsParent = false,
                ParentId = idCangDaMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111129"),
                Name = "Căng da mặt toàn phần",
                Description = null,
                IsParent = false,
                ParentId = idCangDaMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111130"),
                Name = "Căng da mặt nội soi",
                Description = null,
                IsParent = false,
                ParentId = idCangDaMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111131"),
                Name = "Căng da trán mổ hở",
                Description = null,
                IsParent = false,
                ParentId = idCangDaMat
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111132"),
                Name = "Căng da trán nội soi",
                Description = null,
                IsParent = false,
                ParentId = idCangDaMat
            },
            // ------------ Phẫu Thuật Thẩm Mỹ -> Độn cằm V Line, Thẩm mỹ gò má, Hút mỡ bụng ------------
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111133"),
                Name = "Độn cằm V Line – nâng cằm",
                Description = null,
                IsParent = false,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111134"),
                Name = "Hút mỡ bụng",
                Description = null,
                IsParent = false,
                ParentId = idPhauThuatThamMy
            },
            // Thẩm mỹ gò má (có 2 dịch vụ con)
            new Category
            {
                Id = idThamMyGoMa,
                Name = "Thẩm mỹ gò má",
                Description = "Dịch vụ chỉnh hình gò má",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111135"),
                Name = "Nâng cao gò má",
                Description = null,
                IsParent = false,
                ParentId = idThamMyGoMa
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111136"),
                Name = "Hạ thấp gò má",
                Description = null,
                IsParent = false,
                ParentId = idThamMyGoMa
            },
            // Thẩm mỹ môi (có 2 dịch vụ con)
            new Category
            {
                Id = idThamMyMoi,
                Name = "Thẩm mỹ môi",
                Description = "Nhóm dịch vụ tạo hình môi",
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111137"),
                Name = "Tạo hình môi dày thành môi mỏng",
                Description = null,
                IsParent = false,
                ParentId = idThamMyMoi
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111138"),
                Name = "Tạo hình môi trái tim, môi cười, môi hạt lựu",
                Description = null,
                IsParent = false,
                ParentId = idThamMyMoi
            },
            // Nâng ngực (có 3 dịch vụ con)
            new Category
            {
                Id = idNangNguc,
                Name = "Nâng ngực",
                Description = null,
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111139"),
                Name = "Đặt túi ngực (silicone)",
                Description = null,
                IsParent = false,
                ParentId = idNangNguc
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111140"),
                Name = "Treo ngực sa trễ",
                Description = null,
                IsParent = false,
                ParentId = idNangNguc
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111141"),
                Name = "Thu gọn quầng vú",
                Description = null,
                IsParent = false,
                ParentId = idNangNguc
            },
            // Hút mỡ tay/chân (có 2 dịch vụ con)
            new Category
            {
                Id = idHutMoTayChan,
                Name = "Hút mỡ tay/chân",
                Description = null,
                IsParent = true,
                ParentId = idPhauThuatThamMy
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111142"),
                Name = "Chỉnh hình cánh tay (cắt da thừa)",
                Description = null,
                IsParent = false,
                ParentId = idHutMoTayChan
            },
            new Category
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111143"),
                Name = "Chỉnh hình đùi (cắt da thừa)",
                Description = null,
                IsParent = false,
                ParentId = idHutMoTayChan
            },

            // ------------------------ Top-level 2: Thẩm mỹ không phẫu thuật ------------------------
            new Category
            {
                Id = idThamMyKoPhauThuat,
                Name = "Thẩm mỹ không phẫu thuật",
                Description = "Dịch vụ thẩm mỹ ít xâm lấn",
                IsParent = true,
                ParentId = null
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222225"),
                Name = "Bấm má lúm đồng tiền",
                Description = null,
                IsParent = false,
                ParentId = idThamMyKoPhauThuat
            },
            // Tiêm filler (có 3 dịch vụ con)
            new Category
            {
                Id = idTiemFiller,
                Name = "Tiêm filler",
                Description = null,
                IsParent = true,
                ParentId = idThamMyKoPhauThuat
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222226"),
                Name = "Tiêm filler môi",
                Description = null,
                IsParent = false,
                ParentId = idTiemFiller
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222227"),
                Name = "Tiêm filler cằm",
                Description = null,
                IsParent = false,
                ParentId = idTiemFiller
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222228"),
                Name = "Tiêm filler mũi",
                Description = null,
                IsParent = false,
                ParentId = idTiemFiller
            },
            // Tiêm botox (có 2 dịch vụ con)
            new Category
            {
                Id = idTiemBotox,
                Name = "Tiêm botox",
                Description = null,
                IsParent = true,
                ParentId = idThamMyKoPhauThuat
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222229"),
                Name = "Xóa nếp nhăn trán",
                Description = null,
                IsParent = false,
                ParentId = idTiemBotox
            },
            new Category
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222230"),
                Name = "Thon gọn góc hàm",
                Description = null,
                IsParent = false,
                ParentId = idTiemBotox
            },
            // Tiêm tan mỡ
            new Category
            {
                Id = idTiemTanMo,
                Name = "Tiêm tan mỡ",
                Description = null,
                IsParent = false,
                ParentId = idThamMyKoPhauThuat
            },

            // ------------------------ Top-level 3: Dịch vụ Spa ------------------------
            new Category
            {
                Id = idSpa,
                Name = "Dịch vụ Spa",
                Description = "Nhóm dịch vụ chăm sóc thư giãn, spa",
                IsParent = true,
                ParentId = null
            },
            // Chăm sóc da mặt (3 dịch vụ con)
            new Category
            {
                Id = idChamSocDaMat,
                Name = "Chăm sóc da mặt",
                Description = null,
                IsParent = true,
                ParentId = idSpa
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333335"),
                Name = "Lấy nhân mụn",
                Description = null,
                IsParent = false,
                ParentId = idChamSocDaMat
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333336"),
                Name = "Massage mặt",
                Description = null,
                IsParent = false,
                ParentId = idChamSocDaMat
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333337"),
                Name = "Điện di tinh chất",
                Description = null,
                IsParent = false,
                ParentId = idChamSocDaMat
            },
            // Chăm sóc cơ thể (3 dịch vụ con)
            new Category
            {
                Id = idChamSocCoThe,
                Name = "Chăm sóc cơ thể",
                Description = null,
                IsParent = true,
                ParentId = idSpa
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333338"),
                Name = "Tẩy tế bào chết toàn thân",
                Description = null,
                IsParent = false,
                ParentId = idChamSocCoThe
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333339"),
                Name = "Massage body",
                Description = null,
                IsParent = false,
                ParentId = idChamSocCoThe
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333340"),
                Name = "Xông hơi thư giãn",
                Description = null,
                IsParent = false,
                ParentId = idChamSocCoThe
            },
            // Triệt lông (3 dịch vụ con)
            new Category
            {
                Id = idTrietLong,
                Name = "Triệt lông",
                Description = null,
                IsParent = true,
                ParentId = idSpa
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333341"),
                Name = "Triệt lông nách",
                Description = null,
                IsParent = false,
                ParentId = idTrietLong
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333342"),
                Name = "Triệt lông bikini",
                Description = null,
                IsParent = false,
                ParentId = idTrietLong
            },
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333343"),
                Name = "Triệt lông chân",
                Description = null,
                IsParent = false,
                ParentId = idTrietLong
            },
            // Tắm trắng
            new Category
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333344"),
                Name = "Tắm trắng",
                Description = null,
                IsParent = false,
                ParentId = idSpa
            },

            // ------------------------ Top-level 4: Chăm sóc da chuyên sâu ------------------------
            new Category
            {
                Id = idChamSocDaChuyenSau,
                Name = "Chăm sóc da chuyên sâu",
                Description = "Nhóm dịch vụ điều trị & cải thiện da",
                IsParent = true,
                ParentId = null
            },
            // Trị mụn (2 dịch vụ con)
            new Category
            {
                Id = idTriMun,
                Name = "Trị mụn",
                Description = null,
                IsParent = true,
                ParentId = idChamSocDaChuyenSau
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444446"),
                Name = "Trị mụn bọc, mụn viêm",
                Description = null,
                IsParent = false,
                ParentId = idTriMun
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444447"),
                Name = "Trị mụn đầu đen, sợi bã nhờn",
                Description = null,
                IsParent = false,
                ParentId = idTriMun
            },
            // Trị nám, tàn nhang (2 dịch vụ con)
            new Category
            {
                Id = idTriNamTanNhang,
                Name = "Trị nám, tàn nhang",
                Description = null,
                IsParent = true,
                ParentId = idChamSocDaChuyenSau
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444448"),
                Name = "Laser trị nám",
                Description = null,
                IsParent = false,
                ParentId = idTriNamTanNhang
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444449"),
                Name = "Laser trị tàn nhang",
                Description = null,
                IsParent = false,
                ParentId = idTriNamTanNhang
            },
            // Trị sẹo rỗ (2 dịch vụ con)
            new Category
            {
                Id = idTriSeoRo,
                Name = "Trị sẹo rỗ",
                Description = null,
                IsParent = true,
                ParentId = idChamSocDaChuyenSau
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444450"),
                Name = "Lăn kim (Microneedling)",
                Description = null,
                IsParent = false,
                ParentId = idTriSeoRo
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444451"),
                Name = "Laser CO2 fractional",
                Description = null,
                IsParent = false,
                ParentId = idTriSeoRo
            },
            // Tái tạo da (2 dịch vụ con)
            new Category
            {
                Id = idTaiTaoDa,
                Name = "Tái tạo da",
                Description = null,
                IsParent = true,
                ParentId = idChamSocDaChuyenSau
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444452"),
                Name = "PRP (Huyết tương giàu tiểu cầu)",
                Description = null,
                IsParent = false,
                ParentId = idTaiTaoDa
            },
            new Category
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444453"),
                Name = "Mesotherapy (Mesotherapy da mặt)",
                Description = null,
                IsParent = false,
                ParentId = idTaiTaoDa
            }
        );
    }
}