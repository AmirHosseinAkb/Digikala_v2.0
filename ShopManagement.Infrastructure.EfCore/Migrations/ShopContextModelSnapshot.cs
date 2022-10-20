﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShopManagement.Infrastructure.EfCore;

#nullable disable

namespace ShopManagement.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(ShopContext))]
    partial class ShopContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ShopManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.Property<long>("InventoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("InventoryId"), 1L, 1);

                    b.Property<int>("ProductCount")
                        .HasColumnType("int");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("InventoryId");

                    b.HasIndex("ProductId")
                        .IsUnique();

                    b.ToTable("Inventories", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.InventoryAgg.InventoryHistory", b =>
                {
                    b.Property<long>("InventoryHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("InventoryHistoryId"), 1L, 1);

                    b.Property<int>("Count")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<long>("OperatorId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("InventoryHistoryId");

                    b.HasIndex("ProductId");

                    b.ToTable("InventoryHistories", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.Order", b =>
                {
                    b.Property<long>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderId"), 1L, 1);

                    b.Property<long?>("AddressId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("DiscountId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsClosed")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OrderDiscount")
                        .HasColumnType("int");

                    b.Property<int>("OrderSum")
                        .HasColumnType("int");

                    b.Property<int>("PaidPrice")
                        .HasColumnType("int");

                    b.Property<byte>("PaymentType")
                        .HasColumnType("tinyint");

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint");

                    b.Property<string>("TrackingNumber")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("UserId")
                        .HasColumnType("bigint");

                    b.HasKey("OrderId");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.OrderItem", b =>
                {
                    b.Property<long>("OrderItemId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("OrderItemId"), 1L, 1);

                    b.Property<long?>("ColorId")
                        .HasColumnType("bigint");

                    b.Property<byte>("Count")
                        .HasColumnType("tinyint");

                    b.Property<long>("OrderId")
                        .HasColumnType("bigint");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("int");

                    b.HasKey("OrderItemId");

                    b.HasIndex("ColorId");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderItems", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.Product", b =>
                {
                    b.Property<long>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ProductId"), 1L, 1);

                    b.Property<long>("BrandId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("OtherLangTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<long?>("PrimaryGroupId")
                        .HasColumnType("bigint");

                    b.Property<long?>("SecondaryGroupId")
                        .HasColumnType("bigint");

                    b.Property<string>("Tags")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandId");

                    b.HasIndex("GroupId");

                    b.HasIndex("PrimaryGroupId");

                    b.HasIndex("SecondaryGroupId");

                    b.ToTable("Products", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.ProductDetail", b =>
                {
                    b.Property<long>("PD_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("PD_Id"), 1L, 1);

                    b.Property<long>("DetailId")
                        .HasColumnType("bigint");

                    b.Property<string>("DetailValue")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("PD_Id");

                    b.HasIndex("DetailId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductDetails", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductBrandAgg.ProductBrand", b =>
                {
                    b.Property<long>("BrandId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("BrandId"), 1L, 1);

                    b.Property<string>("BrandTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("OtherLangTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("BrandId");

                    b.ToTable("ProductBrands", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductColorAgg.ProductColor", b =>
                {
                    b.Property<long>("ColorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ColorId"), 1L, 1);

                    b.Property<string>("ColorCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ColorName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("ColorId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductColors", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.GroupDetail", b =>
                {
                    b.Property<long>("DetailId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DetailId"), 1L, 1);

                    b.Property<string>("DetailTitle")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<long>("GroupId")
                        .HasColumnType("bigint");

                    b.HasKey("DetailId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupDetails", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.ProductGroup", b =>
                {
                    b.Property<long>("GroupId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("GroupId"), 1L, 1);

                    b.Property<string>("GroupTitle")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("ImageName")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("GroupId");

                    b.HasIndex("ParentId");

                    b.ToTable("ProductGroups", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductImageAgg.ProductImage", b =>
                {
                    b.Property<long>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("ImageId"), 1L, 1);

                    b.Property<string>("ImageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("ProductId")
                        .HasColumnType("bigint");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages", (string)null);
                });

            modelBuilder.Entity("ShopManagement.Domain.InventoryAgg.Inventory", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithOne("Inventory")
                        .HasForeignKey("ShopManagement.Domain.InventoryAgg.Inventory", "ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopManagement.Domain.InventoryAgg.InventoryHistory", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("InventoryHistories")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.OrderItem", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductColorAgg.ProductColor", "ProductColor")
                        .WithMany("OrderItems")
                        .HasForeignKey("ColorId");

                    b.HasOne("ShopManagement.Domain.OrderAgg.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("OrderItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");

                    b.Navigation("ProductColor");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.Product", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductBrandAgg.ProductBrand", "ProductBrand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.ProductGroup", "ProductGroup")
                        .WithMany("Products")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.ProductGroup", "PrimaryProductGroup")
                        .WithMany("PrimaryProducts")
                        .HasForeignKey("PrimaryGroupId");

                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.ProductGroup", "SecondaryProductGroup")
                        .WithMany("SecondaryProducts")
                        .HasForeignKey("SecondaryGroupId");

                    b.Navigation("PrimaryProductGroup");

                    b.Navigation("ProductBrand");

                    b.Navigation("ProductGroup");

                    b.Navigation("SecondaryProductGroup");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.ProductDetail", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.GroupDetail", "GroupDetail")
                        .WithMany("ProductDetails")
                        .HasForeignKey("DetailId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("ProductDetails")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("GroupDetail");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductColorAgg.ProductColor", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("ProductColors")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.GroupDetail", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.ProductGroup", "ProductGroup")
                        .WithMany("GroupDetails")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductGroup");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.ProductGroup", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductGroupAgg.ProductGroup", null)
                        .WithMany("ProductGroups")
                        .HasForeignKey("ParentId");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductImageAgg.ProductImage", b =>
                {
                    b.HasOne("ShopManagement.Domain.ProductAgg.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("ShopManagement.Domain.OrderAgg.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductAgg.Product", b =>
                {
                    b.Navigation("Inventory")
                        .IsRequired();

                    b.Navigation("InventoryHistories");

                    b.Navigation("OrderItems");

                    b.Navigation("ProductColors");

                    b.Navigation("ProductDetails");

                    b.Navigation("ProductImages");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductBrandAgg.ProductBrand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductColorAgg.ProductColor", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.GroupDetail", b =>
                {
                    b.Navigation("ProductDetails");
                });

            modelBuilder.Entity("ShopManagement.Domain.ProductGroupAgg.ProductGroup", b =>
                {
                    b.Navigation("GroupDetails");

                    b.Navigation("PrimaryProducts");

                    b.Navigation("ProductGroups");

                    b.Navigation("Products");

                    b.Navigation("SecondaryProducts");
                });
#pragma warning restore 612, 618
        }
    }
}
