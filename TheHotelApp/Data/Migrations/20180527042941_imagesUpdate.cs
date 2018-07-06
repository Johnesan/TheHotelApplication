using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TheHotelApp.Data.Migrations
{
    public partial class imagesUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "Images",
                nullable: true);

            migrationBuilder.AddColumn<string>(
              name: "Size",
              table: "Images",
              nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ItemImageRelationships",
                columns: table => new
                {
                    ItemID = table.Column<string>(nullable: false),
                    ImageID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemImageRelationships", x => new { x.ItemID, x.ImageID });
                    table.ForeignKey(
                        name: "FK_ItemImageRelationships_Images_ImageID",
                        column: x => x.ImageID,
                        principalTable: "Images",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemImageRelationships_ImageID",
                table: "ItemImageRelationships",
                column: "ImageID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemImageRelationships");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Images");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Images",
                newName: "ImageUrl");
        }
    }
}
