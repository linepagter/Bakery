﻿// <auto-generated />
using System;
using Bakery.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Bakery.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Bakery.Models.BakingGood", b =>
                {
                    b.Property<int>("BakingGoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BakingGoodId"));

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BakingGoodId");

                    b.ToTable("Bakinggood");
                });

            modelBuilder.Entity("Bakery.Models.BakingGoodOrder", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("BakingGoodId")
                        .HasColumnType("int");

                    b.HasKey("OrderId", "BakingGoodId");

                    b.HasIndex("BakingGoodId");

                    b.ToTable("BakingGoodOrders");
                });

            modelBuilder.Entity("Bakery.Models.Batch", b =>
                {
                    b.Property<int>("BatchId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BatchId"));

                    b.Property<DateTime>("FinishTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasMaxLength(200)
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("TargetFinishTime")
                        .HasColumnType("datetime2");

                    b.HasKey("BatchId");

                    b.ToTable("Batch");
                });

            modelBuilder.Entity("Bakery.Models.BatchIngredient", b =>
                {
                    b.Property<int>("IngredientsId")
                        .HasColumnType("int");

                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("IngredientsId", "BatchId");

                    b.HasIndex("BatchId");

                    b.ToTable("BatchIngredient");
                });

            modelBuilder.Entity("Bakery.Models.Ingredient", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IngredientId"));

                    b.Property<string>("IngredientName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("StockQuantity")
                        .HasColumnType("int");

                    b.HasKey("IngredientId");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Bakery.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<DateTime>("DeliveryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DeliveryPlace")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OrderId");

                    b.ToTable("Order");
                });

            modelBuilder.Entity("Bakery.Models.Package", b =>
                {
                    b.Property<int>("TrackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TrackId"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("TrackId");

                    b.HasIndex("OrderId");

                    b.ToTable("Package");
                });

            modelBuilder.Entity("BatchOrder", b =>
                {
                    b.Property<int>("BatchId")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.HasKey("BatchId", "OrderId");

                    b.HasIndex("OrderId");

                    b.ToTable("BatchOrder");
                });

            modelBuilder.Entity("Bakery.Models.BakingGoodOrder", b =>
                {
                    b.HasOne("Bakery.Models.BakingGood", "BakingGoods")
                        .WithMany("BakingGoodOrders")
                        .HasForeignKey("BakingGoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Models.Order", "Order")
                        .WithMany("BakingGoodOrders")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BakingGoods");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("Bakery.Models.BatchIngredient", b =>
                {
                    b.HasOne("Bakery.Models.Batch", "Batch")
                        .WithMany("BatchIngredient")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Models.Ingredient", "Ingredients")
                        .WithMany("BatchIngredient")
                        .HasForeignKey("IngredientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Batch");

                    b.Navigation("Ingredients");
                });

            modelBuilder.Entity("Bakery.Models.Package", b =>
                {
                    b.HasOne("Bakery.Models.Order", "Order")
                        .WithMany("Packages")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");
                });

            modelBuilder.Entity("BatchOrder", b =>
                {
                    b.HasOne("Bakery.Models.Batch", null)
                        .WithMany()
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bakery.Models.Order", null)
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bakery.Models.BakingGood", b =>
                {
                    b.Navigation("BakingGoodOrders");
                });

            modelBuilder.Entity("Bakery.Models.Batch", b =>
                {
                    b.Navigation("BatchIngredient");
                });

            modelBuilder.Entity("Bakery.Models.Ingredient", b =>
                {
                    b.Navigation("BatchIngredient");
                });

            modelBuilder.Entity("Bakery.Models.Order", b =>
                {
                    b.Navigation("BakingGoodOrders");

                    b.Navigation("Packages");
                });
#pragma warning restore 612, 618
        }
    }
}
