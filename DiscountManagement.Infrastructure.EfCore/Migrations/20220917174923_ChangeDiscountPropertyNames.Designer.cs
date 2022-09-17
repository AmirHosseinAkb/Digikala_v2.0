﻿// <auto-generated />
using System;
using DiscountManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DiscountManagement.Infrastructure.EfCore.Migrations
{
    [DbContext(typeof(DiscountContext))]
    [Migration("20220917174923_ChangeDiscountPropertyNames")]
    partial class ChangeDiscountPropertyNames
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("DiscountManagement.Domain.OrderDiscountAgg.OrderDiscount", b =>
                {
                    b.Property<long>("DiscountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("DiscountId"), 1L, 1);

                    b.Property<string>("DiscountCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("DiscountRate")
                        .HasColumnType("int");

                    b.Property<DateTime?>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<DateTime?>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("UsableCount")
                        .HasColumnType("int");

                    b.HasKey("DiscountId");

                    b.ToTable("OrderDiscounts", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}