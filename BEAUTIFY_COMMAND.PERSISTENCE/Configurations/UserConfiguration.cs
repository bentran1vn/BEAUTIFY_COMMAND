using BEAUTIFY_COMMAND.DOMAIN.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BEAUTIFY_COMMAND.PERSISTENCE.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        string[] userIds = 
            [
                "cb275f6c-f9c2-443a-acc0-c09c95a8026a",
                "c5acdf32-ed7d-4cf9-9e78-cc661ebae1d4",
                "509b6308-213c-4eca-a9d2-2021b2bf4c62",
                "ebaa0ba1-0191-45ce-8b46-eb686f3336ee",
                "e5b67051-d1b4-423f-865e-80322cf456aa"
            ];
        
        var user = userIds.Select((id, index) => new User
        {
            Id = new Guid(id),
            Email = index == 0 ?  "admin@gmail.com" : $"systemStaff{index}@gmail.com",
            FirstName = $"systemStaff{index}",
            LastName = $"systemStaff{index}",
            Password = "QFUsZBROui+rvdxQ0JkaJg==:6z/WlleDL/PeFU/GLZ3ZHy50E8GTUNzv0mRqB77oE8w=",
            Status = 1,
            RoleId = index == 0 ? 
                new Guid("4b7171f4-3219-4688-9f7c-625687a95867") :
                new Guid("248bf96b-9782-4011-8bb0-b26e66658090")
        }).ToList();
        
        builder.HasData(user);
    }
}