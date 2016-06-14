using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SwapNShopApplication.Models;

namespace SwapNShopApplication.Migrations
{
    [DbContext(typeof(SwapNShopDbContext))]
    [Migration("20160614005606_firstMigration4")]
    partial class firstMigration4
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rc2-20901")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SwapNShopApplication.Models.Category", b =>
                {
                    b.Property<int>("IdCategory")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("description");

                    b.Property<string>("title");

                    b.HasKey("IdCategory");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.Comment", b =>
                {
                    b.Property<int>("IdComment")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("date");

                    b.Property<string>("message");

                    b.HasKey("IdComment");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.CommentList", b =>
                {
                    b.Property<int>("IdCommentList")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdComment");

                    b.Property<int>("IdMusician");

                    b.HasKey("IdCommentList");

                    b.ToTable("CommentList");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.Equipment", b =>
                {
                    b.Property<int>("IdEquipment")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdCategory");

                    b.Property<int>("IdMusician");

                    b.Property<string>("condition");

                    b.Property<string>("description");

                    b.Property<string>("name");

                    b.Property<double>("pricePerDay");

                    b.HasKey("IdEquipment");

                    b.ToTable("Equipment");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.Musician", b =>
                {
                    b.Property<int>("IdMusician")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("city");

                    b.Property<string>("description");

                    b.Property<string>("email");

                    b.Property<int>("rating");

                    b.Property<string>("state");

                    b.Property<string>("userName");

                    b.HasKey("IdMusician");

                    b.ToTable("Musician");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.Picture", b =>
                {
                    b.Property<int>("IdPicture")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("image");

                    b.HasKey("IdPicture");

                    b.ToTable("Picture");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.PictureList", b =>
                {
                    b.Property<int>("IdPictureList")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEquipment");

                    b.Property<int>("IdPicture");

                    b.HasKey("IdPictureList");

                    b.ToTable("PictureList");
                });

            modelBuilder.Entity("SwapNShopApplication.Models.RentalDates", b =>
                {
                    b.Property<int>("IdRentalDates")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IdEquipment");

                    b.Property<int>("IdMusician");

                    b.Property<DateTime>("checkInDates");

                    b.Property<DateTime>("checkOutDates");

                    b.HasKey("IdRentalDates");

                    b.ToTable("RentalDates");
                });
        }
    }
}
