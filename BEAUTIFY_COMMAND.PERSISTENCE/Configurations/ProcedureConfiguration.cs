using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;
public class ProcedureConfiguration : IEntityTypeConfiguration<Procedure>
{
    public void Configure(EntityTypeBuilder<Procedure> builder)
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

        // Get service IDs from ServiceConfiguration
        var idBCSG_Service1 = Guid.Parse("a1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service2 = Guid.Parse("a2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service3 = Guid.Parse("a3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service4 = Guid.Parse("a4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idBCSG_Service5 = Guid.Parse("a5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service1 = Guid.Parse("b1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service2 = Guid.Parse("b2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service3 = Guid.Parse("b3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service4 = Guid.Parse("b4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idHBS_Service5 = Guid.Parse("b5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service1 = Guid.Parse("c1d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service2 = Guid.Parse("c2d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service3 = Guid.Parse("c3d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service4 = Guid.Parse("c4d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");
        var idSCDN_Service5 = Guid.Parse("c5d3d799-3f62-4a2f-b6b5-3d05b7e1f2a3");

        var procedures = new List<Procedure>
        {
            // Beauty Center Sài Gòn Service 1 - Nâng Mũi Cấu Trúc S-Line Premium
            new()
            {
                Id = idBCSG_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi 3D",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi 3D phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service1
            },
            new()
            {
                Id = idBCSG_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service1
            },
            new()
            {
                Id = idBCSG_S1_P3,
                Name = "Phẫu thuật nâng mũi cấu trúc S-Line",
                Description =
                    "Thực hiện phẫu thuật với kỹ thuật cấu trúc S-Line, sử dụng sụn tự thân kết hợp sụn nhân tạo cao cấp.",
                StepIndex = 3,
                ServiceId = idBCSG_Service1
            },
            new()
            {
                Id = idBCSG_S1_P4,
                Name = "Theo dõi hậu phẫu 24h",
                Description =
                    "Theo dõi tình trạng hậu phẫu 24h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idBCSG_Service1
            },
            new()
            {
                Id = idBCSG_S1_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description =
                    "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và can thiệp kịp thời nếu có biến chứng.",
                StepIndex = 5,
                ServiceId = idBCSG_Service1
            },

            // Beauty Center Sài Gòn Service 2 - Cắt Mí Mắt Plasma Luxury
            new()
            {
                Id = idBCSG_S2_P1,
                Name = "Tư vấn và thiết kế mí mắt",
                Description =
                    "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service2
            },
            new()
            {
                Id = idBCSG_S2_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description =
                    "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idBCSG_Service2
            },
            new()
            {
                Id = idBCSG_S2_P3,
                Name = "Phẫu thuật cắt mí bằng công nghệ Plasma",
                Description =
                    "Thực hiện phẫu thuật với công nghệ Plasma không chạm, tạo đường mí tự nhiên, giảm thiểu chảy máu và đau đớn.",
                StepIndex = 3,
                ServiceId = idBCSG_Service2
            },
            new()
            {
                Id = idBCSG_S2_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idBCSG_Service2
            },

            // Beauty Center Sài Gòn Service 3 - Nâng Ngực Túi Độn Mentor
            new()
            {
                Id = idBCSG_S3_P1,
                Name = "Tư vấn và lựa chọn kích thước túi ngực",
                Description =
                    "Bác sĩ phân tích cấu trúc cơ thể, tư vấn và giúp khách hàng lựa chọn kích thước túi ngực phù hợp.",
                StepIndex = 1,
                ServiceId = idBCSG_Service3
            },
            new()
            {
                Id = idBCSG_S3_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service3
            },
            new()
            {
                Id = idBCSG_S3_P3,
                Name = "Phẫu thuật nâng ngực nội soi với túi Mentor",
                Description =
                    "Thực hiện phẫu thuật nâng ngực bằng kỹ thuật nội soi, đặt túi độn Mentor được FDA Hoa Kỳ chứng nhận.",
                StepIndex = 3,
                ServiceId = idBCSG_Service3
            },
            new()
            {
                Id = idBCSG_S3_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description =
                    "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idBCSG_Service3
            },
            new()
            {
                Id = idBCSG_S3_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description =
                    "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idBCSG_Service3
            },

            // Beauty Center Sài Gòn Service 4 - Độn Cằm V-Line Hàn Quốc
            new()
            {
                Id = idBCSG_S4_P1,
                Name = "Tư vấn và thiết kế đường cằm V-Line",
                Description =
                    "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế đường cằm V-Line phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service4
            },
            new()
            {
                Id = idBCSG_S4_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang hàm mặt, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service4
            },
            new()
            {
                Id = idBCSG_S4_P3,
                Name = "Phẫu thuật độn cằm V-Line",
                Description =
                    "Thực hiện phẫu thuật với kỹ thuật V-Line của Hàn Quốc, tạo đường cằm thanh thoát, cân đối với khuôn mặt.",
                StepIndex = 3,
                ServiceId = idBCSG_Service4
            },
            new()
            {
                Id = idBCSG_S4_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idBCSG_Service4
            },

            // Beauty Center Sài Gòn Service 5 - Hút Mỡ Bụng VASER Lipo
            new()
            {
                Id = idBCSG_S5_P1,
                Name = "Tư vấn và đánh giá vùng mỡ bụng",
                Description =
                    "Bác sĩ đánh giá tình trạng mỡ bụng, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idBCSG_Service5
            },
            new()
            {
                Id = idBCSG_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, xét nghiệm máu, đo chỉ số BMI và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idBCSG_Service5
            },
            new()
            {
                Id = idBCSG_S5_P3,
                Name = "Phẫu thuật hút mỡ bụng bằng công nghệ VASER Lipo",
                Description =
                    "Thực hiện phẫu thuật hút mỡ siêu âm VASER Lipo, loại bỏ mỡ thừa vùng bụng một cách chọn lọc.",
                StepIndex = 3,
                ServiceId = idBCSG_Service5
            },
            new()
            {
                Id = idBCSG_S5_P4,
                Name = "Massage định hình sau phẫu thuật",
                Description =
                    "Thực hiện liệu trình massage định hình vùng bụng sau hút mỡ, giúp làm săn chắc da và đều màu.",
                StepIndex = 4,
                ServiceId = idBCSG_Service5
            },
            new()
            {
                Id = idBCSG_S5_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idBCSG_Service5
            },

            // Hanoi Beauty Spa Service 1 - Nâng Mũi Bio-Silicon Elite
            new()
            {
                Id = idHBS_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idHBS_Service1
            },
            new()
            {
                Id = idHBS_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service1
            },
            new()
            {
                Id = idHBS_S1_P3,
                Name = "Phẫu thuật nâng mũi với Bio-Silicon Elite",
                Description =
                    "Thực hiện phẫu thuật với vật liệu Bio-Silicon cao cấp, kết hợp sụn tự thân để tạo dáng mũi tự nhiên.",
                StepIndex = 3,
                ServiceId = idHBS_Service1
            },
            new()
            {
                Id = idHBS_S1_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service1
            },

            // Hanoi Beauty Spa Service 2 - Cắt Mí Mắt Hàn Quốc Không Sẹo
            new()
            {
                Id = idHBS_S2_P1,
                Name = "Tư vấn và thiết kế đường mí",
                Description =
                    "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp theo công nghệ Hàn Quốc.",
                StepIndex = 1,
                ServiceId = idHBS_Service2
            },
            new()
            {
                Id = idHBS_S2_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description =
                    "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idHBS_Service2
            },
            new()
            {
                Id = idHBS_S2_P3,
                Name = "Phẫu thuật cắt mí với kỹ thuật không sẹo",
                Description =
                    "Thực hiện phẫu thuật với kỹ thuật Hàn Quốc tiên tiến, đảm bảo không để lại sẹo, tạo đường mí tự nhiên.",
                StepIndex = 3,
                ServiceId = idHBS_Service2
            },
            new()
            {
                Id = idHBS_S2_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service2
            },

            // Hanoi Beauty Spa Service 3 - Nâng Cung Mày Siêu Âm Hifu
            new()
            {
                Id = idHBS_S3_P1,
                Name = "Tư vấn và đánh giá cung mày",
                Description =
                    "Bác sĩ phân tích đặc điểm cung mày và khuôn mặt, tư vấn phương pháp nâng cung mày phù hợp.",
                StepIndex = 1,
                ServiceId = idHBS_Service3
            },
            new()
            {
                Id = idHBS_S3_P2,
                Name = "Kiểm tra và chuẩn bị trước điều trị",
                Description = "Kiểm tra sức khỏe tổng quát, vệ sinh vùng điều trị và chuẩn bị các thiết bị Hifu.",
                StepIndex = 2,
                ServiceId = idHBS_Service3
            },
            new()
            {
                Id = idHBS_S3_P3,
                Name = "Điều trị nâng cung mày bằng siêu âm Hifu",
                Description =
                    "Sử dụng công nghệ siêu âm Hifu tác động vào tầng SMAS, kích thích tăng sinh collagen, nâng cơ mày.",
                StepIndex = 3,
                ServiceId = idHBS_Service3
            },
            new()
            {
                Id = idHBS_S3_P4,
                Name = "Chăm sóc và tái khám sau điều trị",
                Description = "Hướng dẫn chăm sóc sau điều trị và lịch tái khám để đánh giá kết quả.",
                StepIndex = 4,
                ServiceId = idHBS_Service3
            },

            // Hanoi Beauty Spa Service 4 - Thu Nhỏ Ngực Vertical Short-Scar
            new()
            {
                Id = idHBS_S4_P1,
                Name = "Tư vấn và đánh giá tình trạng ngực",
                Description =
                    "Bác sĩ phân tích kích thước, hình dáng ngực hiện tại và tư vấn kích thước, hình dáng mong muốn.",
                StepIndex = 1,
                ServiceId = idHBS_Service4
            },
            new()
            {
                Id = idHBS_S4_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service4
            },
            new()
            {
                Id = idHBS_S4_P3,
                Name = "Phẫu thuật thu nhỏ ngực Vertical Short-Scar",
                Description =
                    "Thực hiện phẫu thuật với kỹ thuật Vertical Short-Scar giúp thu nhỏ ngực và tạo hình dáng tự nhiên với sẹo tối thiểu.",
                StepIndex = 3,
                ServiceId = idHBS_Service4
            },
            new()
            {
                Id = idHBS_S4_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description =
                    "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idHBS_Service4
            },
            new()
            {
                Id = idHBS_S4_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description =
                    "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idHBS_Service4
            },

            // Hanoi Beauty Spa Service 5 - Hút Mỡ Mặt Precision
            new()
            {
                Id = idHBS_S5_P1,
                Name = "Tư vấn và đánh giá vùng mỡ mặt",
                Description =
                    "Bác sĩ đánh giá tình trạng mỡ mặt, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idHBS_Service5
            },
            new()
            {
                Id = idHBS_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idHBS_Service5
            },
            new()
            {
                Id = idHBS_S5_P3,
                Name = "Phẫu thuật hút mỡ mặt Precision",
                Description =
                    "Thực hiện phẫu thuật hút mỡ mặt với kỹ thuật Precision, loại bỏ mỡ thừa và tạo đường nét khuôn mặt thanh tú.",
                StepIndex = 3,
                ServiceId = idHBS_Service5
            },
            new()
            {
                Id = idHBS_S5_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idHBS_Service5
            },

            // Skin Care Đà Nẵng Service 1 - Nâng Mũi Cấu Trúc Hybrid
            new()
            {
                Id = idSCDN_S1_P1,
                Name = "Tư vấn và thiết kế dáng mũi",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng mũi phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service1
            },
            new()
            {
                Id = idSCDN_S1_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description = "Thăm khám, kiểm tra tổng quát, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service1
            },
            new()
            {
                Id = idSCDN_S1_P3,
                Name = "Phẫu thuật nâng mũi cấu trúc Hybrid",
                Description =
                    "Thực hiện phẫu thuật với kỹ thuật Hybrid kết hợp sụn tự thân và vật liệu y khoa cao cấp.",
                StepIndex = 3,
                ServiceId = idSCDN_Service1
            },
            new()
            {
                Id = idSCDN_S1_P4,
                Name = "Theo dõi hậu phẫu 24h",
                Description =
                    "Theo dõi tình trạng hậu phẫu 24h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idSCDN_Service1
            },
            new()
            {
                Id = idSCDN_S1_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idSCDN_Service1
            },

            // Skin Care Đà Nẵng Service 2 - Nâng Ngực Nội Soi Ergonomix
            new()
            {
                Id = idSCDN_S2_P1,
                Name = "Tư vấn và lựa chọn kích thước túi ngực",
                Description =
                    "Bác sĩ phân tích cấu trúc cơ thể, tư vấn và giúp khách hàng lựa chọn kích thước túi ngực Ergonomix phù hợp.",
                StepIndex = 1,
                ServiceId = idSCDN_Service2
            },
            new()
            {
                Id = idSCDN_S2_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang ngực, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service2
            },
            new()
            {
                Id = idSCDN_S2_P3,
                Name = "Phẫu thuật nâng ngực nội soi với túi Ergonomix",
                Description =
                    "Thực hiện phẫu thuật nâng ngực bằng kỹ thuật nội soi với túi Ergonomix có hình giọt nước tự nhiên.",
                StepIndex = 3,
                ServiceId = idSCDN_Service2
            },
            new()
            {
                Id = idSCDN_S2_P4,
                Name = "Theo dõi hậu phẫu 48h",
                Description =
                    "Theo dõi tình trạng hậu phẫu 48h tại phòng hồi sức với sự chăm sóc của đội ngũ y tá chuyên nghiệp.",
                StepIndex = 4,
                ServiceId = idSCDN_Service2
            },
            new()
            {
                Id = idSCDN_S2_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description =
                    "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục dài hạn.",
                StepIndex = 5,
                ServiceId = idSCDN_Service2
            },

            // Skin Care Đà Nẵng Service 3 - Hút Mỡ Bụng 3D Hi-Definition
            new()
            {
                Id = idSCDN_S3_P1,
                Name = "Tư vấn và đánh giá vùng mỡ bụng",
                Description =
                    "Bác sĩ đánh giá tình trạng mỡ bụng, vẽ và đánh dấu vùng cần hút mỡ phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service3
            },
            new()
            {
                Id = idSCDN_S3_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, xét nghiệm máu, đo chỉ số BMI và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service3
            },
            new()
            {
                Id = idSCDN_S3_P3,
                Name = "Phẫu thuật hút mỡ bụng 3D Hi-Definition",
                Description =
                    "Thực hiện phẫu thuật hút mỡ bụng với công nghệ 3D Hi-Definition, tạo đường nét cơ bụng rõ ràng.",
                StepIndex = 3,
                ServiceId = idSCDN_Service3
            },
            new()
            {
                Id = idSCDN_S3_P4,
                Name = "Massage định hình sau phẫu thuật",
                Description =
                    "Thực hiện liệu trình massage định hình vùng bụng sau hút mỡ, giúp làm săn chắc da và đều màu.",
                StepIndex = 4,
                ServiceId = idSCDN_Service3
            },
            new()
            {
                Id = idSCDN_S3_P5,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 5,
                ServiceId = idSCDN_Service3
            },

            // Skin Care Đà Nẵng Service 4 - Cắt Mí Mắt Plasma Tech
            new()
            {
                Id = idSCDN_S4_P1,
                Name = "Tư vấn và thiết kế đường mí",
                Description =
                    "Bác sĩ phân tích đặc điểm mắt và khuôn mặt, thiết kế đường mí phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service4
            },
            new()
            {
                Id = idSCDN_S4_P2,
                Name = "Kiểm tra trước phẫu thuật",
                Description =
                    "Kiểm tra sức khỏe tổng quát, thực hiện các xét nghiệm cần thiết để đảm bảo an toàn cho khách hàng.",
                StepIndex = 2,
                ServiceId = idSCDN_Service4
            },
            new()
            {
                Id = idSCDN_S4_P3,
                Name = "Phẫu thuật cắt mí với công nghệ Plasma Tech",
                Description =
                    "Thực hiện phẫu thuật với công nghệ Plasma Tech hiện đại, tạo đường mí tự nhiên, giảm thiểu đau đớn và thời gian hồi phục.",
                StepIndex = 3,
                ServiceId = idSCDN_Service4
            },
            new()
            {
                Id = idSCDN_S4_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc vết mổ và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idSCDN_Service4
            },

            // Skin Care Đà Nẵng Service 5 - Độn Cằm 3D Crystal
            new()
            {
                Id = idSCDN_S5_P1,
                Name = "Tư vấn và thiết kế dáng cằm",
                Description = "Bác sĩ phân tích cấu trúc khuôn mặt, thiết kế dáng cằm phù hợp với từng khách hàng.",
                StepIndex = 1,
                ServiceId = idSCDN_Service5
            },
            new()
            {
                Id = idSCDN_S5_P2,
                Name = "Kiểm tra sức khỏe và chuẩn bị tiền phẫu",
                Description =
                    "Thăm khám, kiểm tra tổng quát, chụp X-quang hàm mặt, xét nghiệm máu và đánh giá tính khả thi của phẫu thuật.",
                StepIndex = 2,
                ServiceId = idSCDN_Service5
            },
            new()
            {
                Id = idSCDN_S5_P3,
                Name = "Phẫu thuật độn cằm với vật liệu 3D Crystal",
                Description =
                    "Thực hiện phẫu thuật độn cằm với vật liệu 3D Crystal cao cấp, tạo dáng cằm tự nhiên và cân đối.",
                StepIndex = 3,
                ServiceId = idSCDN_Service5
            },
            new()
            {
                Id = idSCDN_S5_P4,
                Name = "Chăm sóc và tái khám sau phẫu thuật",
                Description = "Lịch tái khám định kỳ, hướng dẫn chăm sóc hậu phẫu và theo dõi quá trình hồi phục.",
                StepIndex = 4,
                ServiceId = idSCDN_Service5
            }
        };

        // builder.HasData(procedures);

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(p => p.Description)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(p => p.StepIndex)
            .IsRequired();
    }
}