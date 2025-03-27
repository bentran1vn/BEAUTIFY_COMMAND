using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations
{
    public class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            var staff = new List<Staff>
            {
                // Clinic Admins (3)
                new ()
                {
                    Id = new Guid("c5514d77-31b0-4c07-b1fe-bf3219e249db"),
                    Email = "nguyenquocbao@gmail.com",
                    FirstName = "Nguyễn Quốc",
                    LastName = "Bảo",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b")
                },
                new ()
                {
                    Id = new Guid("f3850b25-56de-4e0d-8e66-d46617cc6f92"),
                    Email = "hoangquangduy@gmail.com",
                    FirstName = "Hoàng Quang",
                    LastName = "Duy",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b")
                },
                new ()
                {
                    Id = new Guid("a283eb13-8d68-46c9-8a1d-450e0cc7ad13"),
                    Email = "phamducnguyen@gmail.com",
                    FirstName = "Phạm Đức",
                    LastName = "Nguyên",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("c6d93b8c-f509-4498-abbb-fe63edc66f2b")
                },

                // Clinic Staff (6) - Each admin manages 2 staff
                new ()
                {
                    Id = new Guid("d2b2d4b8-c22c-4fcb-96c4-85ccfa378b32"),
                    Email = "ngothimanh@gmail.com",
                    FirstName = "Ngô Thị",
                    LastName = "Mạnh",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },
                new ()
                {
                    Id = new Guid("b02a28f3-f1a7-4fd7-bcb1-53be587be9f9"),
                    Email = "tranthithao@gmail.com",
                    FirstName = "Trần Thị",
                    LastName = "Thảo",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },
                new ()
                {
                    Id = new Guid("b9ab6eb6-5953-455e-8d53-5ec345f8649e"),
                    Email = "vuthithu@gmail.com",
                    FirstName = "Vũ Thị",
                    LastName = "Thu",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },
                new ()
                {
                    Id = new Guid("9dde4ec6-b02f-419a-900b-5c42f1a6c863"),
                    Email = "lethihuong@gmail.com",
                    FirstName = "Lê Thị",
                    LastName = "Hương",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },
                new ()
                {
                    Id = new Guid("6f8bb800-0594-4389-9749-f214ef855bdc"),
                    Email = "nguyenvananh@gmail.com",
                    FirstName = "Nguyễn Văn",
                    LastName = "Anh",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },
                new ()
                {
                    Id = new Guid("a73d00ac-00c4-456e-ab2e-dd184f8681dd"),
                    Email = "phamthiha@gmail.com",
                    FirstName = "Phạm Thị",
                    LastName = "Hà",
                    Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
                    Status = 1,
                    RoleId = new Guid("2e25e963-c03f-45e6-a29d-f22c08e117b3")
                },

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

            builder.HasData(staff);
        }
    }
}