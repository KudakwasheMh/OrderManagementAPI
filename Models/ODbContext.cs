using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace OrderManagementAPI.Models
{
    public class ODbContext: IdentityDbContext<User>
    {

        public ODbContext(DbContextOptions<ODbContext> options) : base(options)
        {
        
        }

        public  DbSet<Client> Clients { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //inheritance
            modelBuilder.Entity<User>()
                .HasDiscriminator<string>("UserType")
                .HasValue<User>("User")
                .HasValue<Vendor>("Vendor")
                .HasValue<Client>("Client");

            // Seed roles if needed (example roles: Client, Vendor)
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = "Vendors", Name = "Vendor", NormalizedName = "VENDOR" },
                new IdentityRole { Id = "Clients", Name = "Client", NormalizedName = "CLIENT" }
                );            

            //seeding user-role associations
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                 new IdentityUserRole<string> { UserId = "1", RoleId = "Clients" }, 
                 new IdentityUserRole<string> { UserId = "301", RoleId = "Vendors" },
                 new IdentityUserRole<string> { UserId = "302", RoleId = "Vendors" },
                 new IdentityUserRole<string> { UserId = "303", RoleId = "Vendors" },
                 new IdentityUserRole<string> { UserId = "304", RoleId = "Vendors" }
            );

            // Seed Client with hashed password
            SeedClient(modelBuilder, "User1", "User1@user.com", "User@321");

            // Seed Vendor with hashed password
            SeedVendor(modelBuilder);


            //Product Seed data
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    ProdId = 1,
                    PName = "Cap",
                    PDesc = "A kind of soft, flat hat, typically with a peak.",
                    Image = "https://cdn11.bigcommerce.com/s-hsi95a83fz/images/stencil/3000w/products/129/16227/1100_STOCK_CAP_COAL_SIDE__38602.1716854434.jpg?c=1",
                    Price = 185,
                    QuantityIS = 5000,
                    VendorId = "301"
                },
                new Product
                {
                    ProdId = 2,
                    PName = "T-Shirt",
                    PDesc = "A style of fabric shirt named after the T shape of its body and sleeves.",
                    Image = "https://media.takealot.com/covers_tsins/59243529/59243529-1-pdpxl.jpeg",
                    Price = 250,
                    QuantityIS = 10000,
                    VendorId = "304"
                },
                new Product
                {
                    ProdId = 3,
                    PName = "Jacket",
                    PDesc = "An outer garment extending either to the waist or the hips, typically having sleeves and a fastening down the front.",
                    Image = "https://m.media-amazon.com/images/I/71E7c09iTdL._AC_UY1000_.jpg",
                    Price = 700,
                    QuantityIS = 3000,
                    VendorId = "302"
                },
                new Product
                {
                    ProdId = 4,
                    PName = "Cargo Pants",
                    PDesc = "An article of clothing that extends from the waist to around the ankles and fits around each leg",
                    Image = "https://www.boerboelwear.co.za/wp-content/uploads/2019/06/Adjustable-Cargo-Pants_Putty-FT-min.jpg",
                    Price = 700,
                    QuantityIS = 6500,
                    VendorId = "303"
                }

            );

            //Order Seed data
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    OrderId = 1,
                    OrderDate = DateTime.Now,
                    ClientId = "1",
                    OrderTotal = 1055
                }
            );

            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem
                {
                    OrderItemID = 1,
                    OrderId = 1,
                    ProdId = 1,
                    Quantity = 3,
                    Total = 555
                },
                new OrderItem
                {
                    OrderItemID = 2,
                    OrderId = 1,
                    ProdId = 2,
                    Quantity = 2,
                    Total = 500
                }
            );
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProdId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents deleting Product when OrderItem is deleted

            // Order-OrderItem relationship
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade); // Deletes related OrderItems when Order is deleted

            // Vendor-Product relationship
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Vendor)
                .WithMany(v => v.Products)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Restrict); // Prevents deleting Vendor when Product is deleted

            // Client-Order relationship
            modelBuilder.Entity<Order>()
    .HasOne(o => o.Client)            // Specifies that the Order entity has a navigation property to the Client entity.
    .WithMany(c => c.Orders)          // Specifies that the Client entity has a collection of Orders.
    .HasForeignKey(o => o.ClientId)  // Specifies that the ClientId property in the Order entity is the foreign key.
    .OnDelete(DeleteBehavior.Cascade); // Specifies that when a Client is deleted, all related Orders will be deleted.
        }
        private void SeedClient(ModelBuilder modelBuilder, string userName, string email, string password)
        {
            var client = new Client
            {

                Id = "1",
                UserName = userName,
                NormalizedUserName = "USER1",
                Email = email,
                NormalizedEmail = email.ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "0654567489",
                ClientId = "1",
                ClientName = "User",
                LastName = "1"
            };

            client.PasswordHash = new PasswordHasher<Client>().HashPassword(client, password);

            modelBuilder.Entity<Client>().HasData(client);
        }

        private void SeedVendor(ModelBuilder modelBuilder)
        {
            var vendor1 = new Vendor
            {
                Id = "301",
                UserName = "TheCapist",
                NormalizedUserName = "THECAPIST",
                Email = "TheCapist@gmail.com",
                NormalizedEmail = "THECAPIST@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0654567489",
                VendorId = "301",
                VendorName = "The Capist",
                VDescrip = "Cap Seller"
            };

            var vendor2 = new Vendor
            {
                Id = "302",
                UserName = "JacketCo",
                NormalizedUserName = "JACKETCO",
                Email = "JacketCo@gmail.com",
                NormalizedEmail = "JACKETCO@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0654567489",
                VendorId = "302",
                VendorName = "JacketCo",
                VDescrip = "Jacket Seller"
            };

            var vendor3 = new Vendor
            {
                Id = "303",
                UserName = "PantsLtd",
                NormalizedUserName = "PANTSLTD",
                Email = "PantsLtd@gmail.com",
                NormalizedEmail = "PANTSLTD@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0654567489",
                VendorId = "303",
                VendorName = "PantsLtd",
                VDescrip = "Pants Seller"
            };

            var vendor4 = new Vendor
            {
                Id = "304",
                UserName = "TorsoProtectors",
                NormalizedUserName = "TORSOPROTECTORS",
                Email = "TorsoProtectors@gmail.com",
                NormalizedEmail = "TORSOPROTECTORS@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "0654567489",
                VendorId = "304",
                VendorName = "TorsoProtectors",
                VDescrip = "T-Shirt Seller"
            };

            vendor1.PasswordHash = new PasswordHasher<Vendor>().HashPassword(vendor1, "Cap@the321");
            vendor2.PasswordHash = new PasswordHasher<Vendor>().HashPassword(vendor2, "Jacket@123");
            vendor3.PasswordHash = new PasswordHasher<Vendor>().HashPassword(vendor3, "Pants@456");
            vendor4.PasswordHash = new PasswordHasher<Vendor>().HashPassword(vendor4, "Tshirt@789");

            modelBuilder.Entity<Vendor>().HasData(vendor1, vendor2, vendor3, vendor4);

        }
    }

}

