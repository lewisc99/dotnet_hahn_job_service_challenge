﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LifeMin = table.Column<int>(type: "int", nullable: false),
                    LifeMax = table.Column<int>(type: "int", nullable: false),
                    MaleWeightMin = table.Column<int>(type: "int", nullable: false),
                    MaleWeightMax = table.Column<int>(type: "int", nullable: false),
                    FemaleWeightMin = table.Column<int>(type: "int", nullable: false),
                    FemaleWeightMax = table.Column<int>(type: "int", nullable: false),
                    Hypoallergenic = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Breeds");
        }
    }
}
