using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations
{
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            var clinics = new List<Clinic>
            {
                // Main Clinics (3) - one for each Clinic Admin
                new Clinic
                {
                    Id = new Guid("78705cfa-7097-408f-93e2-70950fc886a3"),
                    Name = "Beauty Center Sài Gòn",
                    Email = "beautycenter.saigon@gmail.com",
                    WorkingTimeStart = new TimeSpan(8, 0, 0),
                    WorkingTimeEnd = new TimeSpan(20, 0, 0),
                    PhoneNumber = "0283456789",
                    City = "Hồ Chí Minh",
                    TaxCode = "12345678901",
                    BusinessLicenseUrl = "https://storage.googleapis.com/licenses/business-license-1.pdf",
                    OperatingLicenseUrl = "https://storage.googleapis.com/licenses/operating-license-1.pdf",
                    ProfilePictureUrl =
                        "https://res.cloudinary.com/dmiueqpah/image/upload/v1744138052/1-1711946463238508154235_eakppa.jpg",
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
                    WorkingTimeStart = new TimeSpan(8, 0, 0),
                    WorkingTimeEnd = new TimeSpan(20, 0, 0),
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
                    WorkingTimeStart = new TimeSpan(8, 0, 0),
                    WorkingTimeEnd = new TimeSpan(20, 0, 0),
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
                    WorkingTimeStart = new TimeSpan(8, 0, 0),
                    WorkingTimeEnd = new TimeSpan(20, 0, 0),
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
                    ProfilePictureUrl = "https://res.cloudinary.com/dvadlh7ah/image/upload/v1744178257/ty7jok5ooenrha5aydid.jpg",
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
                    ProfilePictureUrl ="https://res.cloudinary.com/dmiueqpah/image/upload/v1744138051/hinh-AA-Clinic-lgo-moi-1-1_smg56o.jpg",
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

            builder.HasData(clinics);
        }
    }
}