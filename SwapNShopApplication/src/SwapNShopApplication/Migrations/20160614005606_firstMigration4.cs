using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SwapNShopApplication.Migrations
{
    public partial class firstMigration4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    IdCategory = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    description = table.Column<string>(nullable: true),
                    title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Comment",
                columns: table => new
                {
                    IdComment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    date = table.Column<DateTime>(nullable: false),
                    message = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comment", x => x.IdComment);
                });

            migrationBuilder.CreateTable(
                name: "CommentList",
                columns: table => new
                {
                    IdCommentList = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdComment = table.Column<int>(nullable: false),
                    IdMusician = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentList", x => x.IdCommentList);
                });

            migrationBuilder.CreateTable(
                name: "Equipment",
                columns: table => new
                {
                    IdEquipment = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdCategory = table.Column<int>(nullable: false),
                    IdMusician = table.Column<int>(nullable: false),
                    condition = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    pricePerDay = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipment", x => x.IdEquipment);
                });

            migrationBuilder.CreateTable(
                name: "Musician",
                columns: table => new
                {
                    IdMusician = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    city = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    email = table.Column<string>(nullable: true),
                    rating = table.Column<int>(nullable: false),
                    state = table.Column<string>(nullable: true),
                    userName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Musician", x => x.IdMusician);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    IdPicture = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.IdPicture);
                });

            migrationBuilder.CreateTable(
                name: "PictureList",
                columns: table => new
                {
                    IdPictureList = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEquipment = table.Column<int>(nullable: false),
                    IdPicture = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PictureList", x => x.IdPictureList);
                });

            migrationBuilder.CreateTable(
                name: "RentalDates",
                columns: table => new
                {
                    IdRentalDates = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdEquipment = table.Column<int>(nullable: false),
                    IdMusician = table.Column<int>(nullable: false),
                    checkInDates = table.Column<DateTime>(nullable: false),
                    checkOutDates = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RentalDates", x => x.IdRentalDates);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Comment");

            migrationBuilder.DropTable(
                name: "CommentList");

            migrationBuilder.DropTable(
                name: "Equipment");

            migrationBuilder.DropTable(
                name: "Musician");

            migrationBuilder.DropTable(
                name: "Picture");

            migrationBuilder.DropTable(
                name: "PictureList");

            migrationBuilder.DropTable(
                name: "RentalDates");
        }
    }
}
