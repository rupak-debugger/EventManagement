using EventManagement.Areas.Identity.Data;
using EventManagement.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EventManagement.Areas.Identity.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
        builder.ApplyConfiguration(new ApplicationUserEntityConfiguration());

        const string ADMIN_ID = "ecdb55c7-b097-43bf-90a7-b493f29805e5";
        const string ROLE_ID = "17e97036-0e6a-4bfc-975e-5c3b99257728";
        //adding the identity roles
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = ROLE_ID,
            Name = "admin",
            NormalizedName = "ADMIN",
            ConcurrencyStamp = "981a65b5-1525-48fd-929f-8b4b3039e950"
        });
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "14e51365-4f18-46e5-a3f9-4009dc8ea389",
            Name = "manager",
            NormalizedName = "MANAGER",
            ConcurrencyStamp = "e0143d74-ebfa-49f0-a959-e76cef198ec5"
        });
        builder.Entity<IdentityRole>().HasData(new IdentityRole
        {
            Id = "254f0ae8-9530-4a1c-ab89-1149159bac10",
            Name = "user",
            NormalizedName = "USER",
            ConcurrencyStamp = "67560632-481d-4eb6-9d1a-74666831042e"
        });

        //adding the admin account
        builder.Entity<ApplicationUser>().HasData(new ApplicationUser
        {
            Id = ADMIN_ID,
            FullName= "Admin Admin",
            UserName = "admin@gmail.com",
            NormalizedUserName = "ADMIN@GMAIL.COM",
            Email = "admin@gmail.com",
            NormalizedEmail = "ADMIN@GMAIL.COM",
            EmailConfirmed = false,
            PasswordHash = "AQAAAAEAACcQAAAAEAiCOqRFc3+nhMuP+sAuf+zLMKxUgQ7S+XggcrDeSGMo9sKnUTAjRcI9UtcWz3X/eA==",
            SecurityStamp = "PYWJWP4GVEGIQZAGDCOBXRVND3E5N2O6",
            ConcurrencyStamp = "4968302b-2c1e-4369-ba91-edef3d345847"
        });
        //adding the relationship
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
        {
            RoleId = ROLE_ID,
            UserId = ADMIN_ID
        });
    }
    public DbSet<Venue> Venues { get; set; }
    public DbSet<EventCategory> EventCategories { get; set; }
    public DbSet<EventInformation> EventInformations { get; set; }
}
