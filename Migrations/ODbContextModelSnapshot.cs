﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using OrderManagementAPI.Models;

#nullable disable

namespace OrderManagementAPI.Migrations
{
    [DbContext(typeof(ODbContext))]
    partial class ODbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "Vendors",
                            Name = "Vendor",
                            NormalizedName = "VENDOR"
                        },
                        new
                        {
                            Id = "Clients",
                            Name = "Client",
                            NormalizedName = "CLIENT"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "1",
                            RoleId = "Clients"
                        },
                        new
                        {
                            UserId = "301",
                            RoleId = "Vendors"
                        },
                        new
                        {
                            UserId = "302",
                            RoleId = "Vendors"
                        },
                        new
                        {
                            UserId = "303",
                            RoleId = "Vendors"
                        },
                        new
                        {
                            UserId = "304",
                            RoleId = "Vendors"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OrderTotal")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("ClientId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            OrderId = 1,
                            ClientId = "1",
                            OrderDate = new DateTime(2024, 7, 31, 19, 52, 35, 179, DateTimeKind.Local).AddTicks(246),
                            OrderTotal = 1055
                        });
                });

            modelBuilder.Entity("OrderManagementAPI.Models.OrderItem", b =>
                {
                    b.Property<int>("OrderItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderItemID"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProdId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("Total")
                        .HasColumnType("int");

                    b.HasKey("OrderItemID");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProdId");

                    b.ToTable("OrderItems");

                    b.HasData(
                        new
                        {
                            OrderItemID = 1,
                            OrderId = 1,
                            ProdId = 1,
                            Quantity = 3,
                            Total = 555
                        },
                        new
                        {
                            OrderItemID = 2,
                            OrderId = 1,
                            ProdId = 2,
                            Quantity = 2,
                            Total = 500
                        });
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Product", b =>
                {
                    b.Property<int>("ProdId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProdId"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<int>("QuantityIS")
                        .HasColumnType("int");

                    b.Property<string>("VendorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("ProdId");

                    b.HasIndex("VendorId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ProdId = 1,
                            Image = "https://cdn11.bigcommerce.com/s-hsi95a83fz/images/stencil/3000w/products/129/16227/1100_STOCK_CAP_COAL_SIDE__38602.1716854434.jpg?c=1",
                            PDesc = "A kind of soft, flat hat, typically with a peak.",
                            PName = "Cap",
                            Price = 185,
                            QuantityIS = 5000,
                            VendorId = "301"
                        },
                        new
                        {
                            ProdId = 2,
                            Image = "https://media.takealot.com/covers_tsins/59243529/59243529-1-pdpxl.jpeg",
                            PDesc = "A style of fabric shirt named after the T shape of its body and sleeves.",
                            PName = "T-Shirt",
                            Price = 250,
                            QuantityIS = 10000,
                            VendorId = "304"
                        },
                        new
                        {
                            ProdId = 3,
                            Image = "https://m.media-amazon.com/images/I/71E7c09iTdL._AC_UY1000_.jpg",
                            PDesc = "An outer garment extending either to the waist or the hips, typically having sleeves and a fastening down the front.",
                            PName = "Jacket",
                            Price = 700,
                            QuantityIS = 3000,
                            VendorId = "302"
                        },
                        new
                        {
                            ProdId = 4,
                            Image = "https://www.boerboelwear.co.za/wp-content/uploads/2019/06/Adjustable-Cargo-Pants_Putty-FT-min.jpg",
                            PDesc = "An article of clothing that extends from the waist to around the ankles and fits around each leg",
                            PName = "Cargo Pants",
                            Price = 700,
                            QuantityIS = 6500,
                            VendorId = "303"
                        });
                });

            modelBuilder.Entity("OrderManagementAPI.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("UserType")
                        .IsRequired()
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator<string>("UserType").HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Client", b =>
                {
                    b.HasBaseType("OrderManagementAPI.Models.User");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Client");

                    b.HasData(
                        new
                        {
                            Id = "1",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "9e2f4fb5-41d9-476a-962f-c4c9db234e70",
                            Email = "User1@user.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "USER1@USER.COM",
                            NormalizedUserName = "USER1",
                            PasswordHash = "AQAAAAIAAYagAAAAEKXHKxVuX7FWX7ELJf5O+9BJU+NBpJYtQVQRdX7JHNtILcZaD/sLVpuxAa4diJfE2Q==",
                            PhoneNumber = "0654567489",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "5d50dab1-d777-4432-a538-c95fb91d856d",
                            TwoFactorEnabled = false,
                            UserName = "User1",
                            ClientId = "1",
                            ClientName = "User",
                            LastName = "1"
                        });
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Vendor", b =>
                {
                    b.HasBaseType("OrderManagementAPI.Models.User");

                    b.Property<string>("VDescrip")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VendorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Vendor");

                    b.HasData(
                        new
                        {
                            Id = "301",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "07b7c25e-bfca-4c4e-85fc-eb51a28b54ea",
                            Email = "TheCapist@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "THECAPIST@GMAIL.COM",
                            NormalizedUserName = "THECAPIST",
                            PasswordHash = "AQAAAAIAAYagAAAAEExdESRW3cud70nEkjrltYwckKX2IHHPU16Ht4+V+zcbIHq3VeI3BRQRXQbVHbhGdQ==",
                            PhoneNumber = "0654567489",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "9c26f6bb-8949-4604-85bb-b7b1ff265a37",
                            TwoFactorEnabled = false,
                            UserName = "TheCapist",
                            VDescrip = "Cap Seller",
                            VendorId = "301",
                            VendorName = "The Capist"
                        },
                        new
                        {
                            Id = "302",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d4ae4368-15ee-4b10-8d50-061ae849f00c",
                            Email = "JacketCo@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "JACKETCO@GMAIL.COM",
                            NormalizedUserName = "JACKETCO",
                            PasswordHash = "AQAAAAIAAYagAAAAEGtPnnfIF3MtkumLgW2Z4yp0Sq0rdiaxxCHEbMSPZIR4b1Hqbf7Y8yHSDR9wDh7Kmw==",
                            PhoneNumber = "0654567489",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "809e4134-71d9-4cc4-a9e5-e0ac5104c5c2",
                            TwoFactorEnabled = false,
                            UserName = "JacketCo",
                            VDescrip = "Jacket Seller",
                            VendorId = "302",
                            VendorName = "JacketCo"
                        },
                        new
                        {
                            Id = "303",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4fc5a008-395c-4f99-897d-a4ec8ef20d22",
                            Email = "PantsLtd@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "PANTSLTD@GMAIL.COM",
                            NormalizedUserName = "PANTSLTD",
                            PasswordHash = "AQAAAAIAAYagAAAAEMbuwxOHCEtwroWmNpXdf912Da5usx/E8Nd0pSlSlwA93GUxTpd8RQmOYgTduDFT5A==",
                            PhoneNumber = "0654567489",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "99b38bf0-a3ff-42fc-a278-b9083328ea0d",
                            TwoFactorEnabled = false,
                            UserName = "PantsLtd",
                            VDescrip = "Pants Seller",
                            VendorId = "303",
                            VendorName = "PantsLtd"
                        },
                        new
                        {
                            Id = "304",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1506e8df-de4c-4244-a543-79fde760228e",
                            Email = "TorsoProtectors@gmail.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "TORSOPROTECTORS@GMAIL.COM",
                            NormalizedUserName = "TORSOPROTECTORS",
                            PasswordHash = "AQAAAAIAAYagAAAAEJb98g4hUV48Q1qKDUZRSMdhaX+QWeZPp23nzC76DGbSHB0YnrB7Bj85psaZ/1+RJw==",
                            PhoneNumber = "0654567489",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "360ceac6-6d41-4042-a295-a6f4a1ced2cc",
                            TwoFactorEnabled = false,
                            UserName = "TorsoProtectors",
                            VDescrip = "T-Shirt Seller",
                            VendorId = "304",
                            VendorName = "TorsoProtectors"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderManagementAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Order", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.OrderItem", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("OrderManagementAPI.Models.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProdId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Product", b =>
                {
                    b.HasOne("OrderManagementAPI.Models.Vendor", "Vendor")
                        .WithMany("Products")
                        .HasForeignKey("VendorId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Vendor");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Product", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("OrderManagementAPI.Models.Vendor", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
