using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations
{
    public class ClinicServiceConfiguration : IEntityTypeConfiguration<ClinicService>
    {
        public void Configure(EntityTypeBuilder<ClinicService> builder)
        {
            // Get service IDs from ServiceConfiguration
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
            
            // builder.HasData(clinicServices);
        }
    }
}