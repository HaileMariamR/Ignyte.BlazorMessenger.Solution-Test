using Ignyte.BlazorMessenger.DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ignyte.BlazorMessenger.DataLayer.DatabaseContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        SetUserData(builder);
    }
    private void SetUserData(EntityTypeBuilder<User> builder)
    {
        builder.HasData(new User { Id = "779fea26-3ddf-4d66-abc9-47d87fbbcc81", DisplayName = "userone", Password = "one1234", Email = "one@gmail.com" });
        builder.HasData(new User { Id = "cb6ca4bb-dadc-4c20-adfe-c96c6f02234c", DisplayName = "usertwo", Password = "two1234", Email = "two@gmail.com" });
        builder.HasData(new User { Id = "6af6bfc6-56ea-403c-a6c3-5dd040d79229", DisplayName = "userthree", Password = "three1234", Email = "three@gmail.com" });

    }
}
