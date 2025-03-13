using System.Reflection;
using BEAUTIFY_COMMAND.DOMAIN.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Abstractions.Entities;
using BEAUTIFY_PACKAGES.BEAUTIFY_PACKAGES.DOMAIN.Constrants;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.PERSISTENCE;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<TriggerOutbox> TriggerOutboxs { get; set; }

    public virtual DbSet<Survey> Surveys { get; set; }
    public virtual DbSet<SurveyQuestion> SurveyQuestions { get; set; }
    public virtual DbSet<SurveyAnswer> SurveyAnswers { get; set; }
    public virtual DbSet<SurveyResponse> SurveyResponses { get; set; }
    public virtual DbSet<ClassificationRule> ClassificationRules { get; set; }

    private static void SetSoftDeleteFilter<T>(ModelBuilder modelBuilder) where T : Entity<T>
    {
        modelBuilder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        #region LinhTinh

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
        builder.Entity<Category>()
            .HasQueryFilter(x => !x.IsDeleted);
        builder.Entity<SubscriptionPackage>()
            .HasQueryFilter(x => !x.IsDeleted);
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.ClrType.BaseType is not { IsGenericType: true } ||
                entityType.ClrType.BaseType.GetGenericTypeDefinition() != typeof(Entity<>)) continue;
            var method = typeof(ApplicationDbContext)
                .GetMethod(nameof(SetSoftDeleteFilter), BindingFlags.NonPublic | BindingFlags.Static)
                ?.MakeGenericMethod(entityType.ClrType);

            method.Invoke(null, [builder]);
        }

        #endregion

        #region Subscription

        builder.Entity<SubscriptionPackage>()
            .HasData(
                new SubscriptionPackage
                {
                    Id = new Guid("4b7171f4-3219-4688-9f7c-625687a95867"),
                    Name = "Trial",
                    Description = "Trial",
                    Price = 0,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 0,
                    LimitLiveStream = 1,
                    EnhancedViewer = 0
                },
                new SubscriptionPackage
                {
                    Id = new Guid("248bf96b-9782-4011-8bb0-b26e66658090"),
                    Name = "Bronze",
                    Description = "Bronze",
                    Price = 3000000,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 1,
                    LimitLiveStream = 5,
                    EnhancedViewer = 0
                },
                new SubscriptionPackage
                {
                    Id = new Guid("b549752a-f156-4894-90ad-ab3994fd071d"),
                    Name = "Silver",
                    Description = "Silver",
                    Price = 5200000,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 5,
                    LimitLiveStream = 10,
                    EnhancedViewer = 100
                },
                new SubscriptionPackage
                {
                    Id = new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"),
                    Name = "Gold",
                    Description = "Gold",
                    Price = 9000000,
                    Duration = 30,
                    IsActivated = true,
                    LimitBranch = 10,
                    LimitLiveStream = 20,
                    EnhancedViewer = 200
                }
            );

        #endregion

        #region Role

        builder.Entity<Role>()
            .HasData(
                new Role { Id = new Guid("4b7171f4-3219-4688-9f7c-625687a95867"), Name = "System Admin" },
                new Role { Id = new Guid("248bf96b-9782-4011-8bb0-b26e66658090"), Name = "System Staff" },
                new Role { Id = new Guid("b549752a-f156-4894-90ad-ab3994fd071d"), Name = "Doctor" },
                new Role { Id = new Guid("b5db3ea1-f81c-465e-a23b-da7d6d361930"), Name = "Customer" },
                new Role { Id = new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b"), Name = "Clinic Admin" },
                new Role { Id = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3"), Name = "Clinic Staff" }
            );

        #endregion

        #region Category

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
        builder.Entity<Category>().HasData(
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
            }
        );
        builder.Entity<Category>().HasData(
            // Danh mục con: Nâng mũi
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
        );

/*

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
*/

        #endregion


        var surveyDaId = Guid.Parse("11111111-1111-1111-1111-111111111111");
        Guid[] questionIds =
        [
            Guid.Parse("22222222-1111-1111-1111-111111111111"), // Q1
            Guid.Parse("22222222-2222-1111-1111-111111111111"), // Q2
            Guid.Parse("22222222-3333-1111-1111-111111111111"), // Q3
            Guid.Parse("22222222-4444-1111-1111-111111111111"), // Q4
            Guid.Parse("22222222-5555-1111-1111-111111111111"), // Q5
            Guid.Parse("22222222-6666-1111-1111-111111111111"), // Q6
            Guid.Parse("22222222-7777-1111-1111-111111111111"), // Q7
            Guid.Parse("22222222-8888-1111-1111-111111111111"), // Q8
            Guid.Parse("22222222-9999-1111-1111-111111111111"), // Q9
            Guid.Parse("22222222-aaaa-1111-1111-111111111111") // Q10
        ];
        builder.Entity<SurveyQuestionOption>().HasData(
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[0],
                Option =
                    "A) Rất căng khô hoặc bong tróc; B) Khá cân bằng không quá khô hay dầu; C) Hơi bóng ở vùng chữ T; D) Bóng dầu toàn mặt; E) Đỏ hoặc châm chích",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[1],
                Option =
                    "A) Không hầu như chỉ khô; B) Không khá đồng đều; C) Thường khô ở má nhưng dầu vùng chữ T; D) Oily toàn mặt; E) Thay đổi theo độ nhạy cảm",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[2],
                Option =
                    "A) Rất nhỏ hoặc gần như không thấy; B) Thấy ở mức vừa phải; C) Rõ hơn ở vùng chữ T; D) To và dễ thấy toàn mặt; E) Rõ hơn khi da ửng đỏ hoặc kích ứng",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[3],
                Option =
                    "A) Rất căng và khó chịu; B) Khá bình thường; C) T-zone bóng má bình thường; D) Rất bóng hoặc nhờn; E) Đỏ hoặc ngứa",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[4],
                Option =
                    "A) Khô hơn hoặc bong tróc; B) Thích nghi khá ổn; C) Có vùng dầu vùng không; D) Tăng tiết dầu nổi mụn; E) Kích ứng ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[5],
                Option =
                    "A) Thường xuyên; B) Hầu như không bao giờ; C) Thỉnh thoảng ở một số vùng; D) Rất hiếm; E) Do nhạy cảm với sản phẩm hoặc thời tiết",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[6],
                Option =
                    "A) Dễ bám vào vùng khô; B) Khá đều cần ít dặm lại; C) Xuống tông hoặc bóng ở chữ T; D) Trôi hoặc bóng dầu gần như toàn mặt; E) Kích ứng hoặc ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[7],
                Option =
                    "A) Rất hiếm; B) Đôi khi; C) Chủ yếu ở vùng chữ T; D) Thường xuyên hoặc toàn mặt; E) Phụ thuộc độ nhạy cảm với sản phẩm",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[8],
                Option =
                    "A) Rất khô và hay căng; B) Cân bằng không quá khô dầu; C) Vừa dầu vừa khô da hỗn hợp; D) Dầu toàn mặt; E) Rất nhạy cảm hoặc dễ kích ứng",
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestionOption
            {
                Id = Guid.NewGuid(),
                SurveyQuestionId = questionIds[9],
                Option =
                    "A) Vẫn khô hoặc căng; B) Khá cân bằng ít bóng; C) Có chút bóng ở vùng chữ T; D) Bóng dầu toàn khuôn mặt; E) Dễ kích ứng hoặc ửng đỏ",
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
        builder.Entity<Survey>().HasData(
            new Survey
            {
                Id = surveyDaId,
                Name = "Khảo sát da",
                Description = "Nhận biết loại da",
                CategoryId = Guid.Parse("20202020-2020-2020-2020-202020202020"), // CategoryId
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
        builder.Entity<SurveyQuestion>().HasData(
            new SurveyQuestion
            {
                Id = questionIds[0],
                SurveyId = surveyDaId,
                Question = "Sau khi rửa mặt (không bôi kem) da bạn thường cảm thấy thế nào?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[1],
                SurveyId = surveyDaId,
                Question = "Vào giữa ngày da bạn trông thế nào (nếu không thấm dầu)?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[2],
                SurveyId = surveyDaId,
                Question = "Tần suất bong tróc hoặc khô mảng?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[3],
                SurveyId = surveyDaId,
                Question = "Mức độ nhìn thấy lỗ chân lông?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[4],
                SurveyId = surveyDaId,
                Question = "Bạn có thường bị mụn hoặc tắc nghẽn lỗ chân lông?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[5],
                SurveyId = surveyDaId,
                Question = "Da bạn có khi nào vừa khô ở vài chỗ vừa dầu ở chỗ khác?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[6],
                SurveyId = surveyDaId,
                Question = "Phản ứng da khi dùng sản phẩm mới hoặc thời tiết thay đổi?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[7],
                SurveyId = surveyDaId,
                Question = "Nếu bỏ qua kem dưỡng một ngày da bạn thế nào?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[8],
                SurveyId = surveyDaId,
                Question = "Khi trang điểm lớp nền giữ trên da ra sao?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            new SurveyQuestion
            {
                Id = questionIds[9],
                SurveyId = surveyDaId,
                Question = "Tổng quát, câu mô tả nào hợp nhất với da bạn?",
                QuestionType = Constant.SurveyQuestionType.MULTIPLE_CHOICE,
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
        builder.Entity<ClassificationRule>().HasData(
            // Q1, Option A => Da khô
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-1111-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "A",
                ClassificationLabel = "Da khô",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option B => Da thường
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-2222-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "B",
                ClassificationLabel = "Da thường",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option C => Da hỗn hợp
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-3333-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "C",
                ClassificationLabel = "Da hỗn hợp",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option D => Da dầu
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-4444-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "D",
                ClassificationLabel = "Da dầu",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            },
            // Q1, Option E => Da nhạy cảm
            new ClassificationRule
            {
                Id = Guid.Parse("33333333-5555-1111-1111-111111111111"),
                SurveyId = surveyDaId,
                SurveyQuestionId = questionIds[0],
                OptionValue = "E",
                ClassificationLabel = "Da nhạy cảm",
                Points = 2,
                CreatedOnUtc = DateTimeOffset.UtcNow
            }
        );
    }
}