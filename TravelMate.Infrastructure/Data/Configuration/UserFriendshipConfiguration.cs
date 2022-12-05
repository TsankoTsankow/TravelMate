using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMate.Infrastructure.Data.Configuration
{
    internal class UserFriendshipConfiguration : IEntityTypeConfiguration<UserFriendship>
    {
        public void Configure(EntityTypeBuilder<UserFriendship> builder)
        {
            builder
                .HasKey(k => new { k.UserId, k.UserFriendId });

            builder
                .HasOne(x => x.User)
                .WithMany(x => x.Friends)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasOne(x => x.UserFriend)
                .WithMany()
                .HasForeignKey(x => x.UserFriendId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
