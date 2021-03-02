using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Identity
{
    public class AppIdentityDbContent : IdentityDbContext<AppUser>
    {
        public AppIdentityDbContent(DbContextOptions<AppIdentityDbContent> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
