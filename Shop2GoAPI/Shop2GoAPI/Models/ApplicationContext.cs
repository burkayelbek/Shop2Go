using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shop2GoAPI.Models
{
    public class ApplicationContext : IdentityDbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {

        }

        public DbSet<Products> Products { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Favorites> Favorites { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<ProductImage> ProductImage { get; set; }
        public DbSet<Conversation> Conversation { get; set; }
        public DbSet<ReviewRating> ReviewRating { get; set; }
        public DbSet<RestrictedUsers> RestrictedUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Category>().HasData(new Category
            {
                Id = 1,
                Name = "Electronics"
            });

            builder.Entity<Products>().HasData(new Products
            {
                Id=1,
                Title="Test Title",
                Price=1000,
                Description="This is a test description",
                PublishedDate = DateTime.Now,
                CategoryId=1,
                UserID = "1",
                isApproved=1,
                RejectedMessage=null,
                isSold=0
            });

            builder.Entity<IdentityRole>().HasData(
            new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id ="1",
                    Name = "Admin",
                    NormalizedName = "Admin"
                },
                 new IdentityRole
                {
                    Id ="2",
                    Name = "User",
                    NormalizedName = "User"
                }

            });

            var hasher = new PasswordHasher<User>();

            builder.Entity<User>().HasData(
                new User
                {
                    Id = "1",
                    FirstName="Burkay",
                    LastName="Elbek",
                    UserName = "testuser",
                    NormalizedUserName= "testuser".ToUpper(),
                    Email="burkay.elbek@gmail.com",
                    NormalizedEmail= "burkay.elbek@gmail.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null,"123456"),
                    ProfilePicture=null
                
                },
                new User
                {
                    Id = "2",
                    FirstName = "AdminName",
                    LastName = "AdminSurname",
                    UserName = "testadmin",
                    NormalizedUserName = "testadmin".ToUpper(),
                    Email = "auspiece.gamer@gmail.com",
                    NormalizedEmail = "auspiece.gamer@gmail.com".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "123456"),
                    ProfilePicture = null
                }
                );

            builder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string> 
                {
                    UserId="1",
                    RoleId = "2"
                },
                new IdentityUserRole<string>
                {
                    UserId = "2",
                    RoleId = "1"
                });

        }
    }
}
