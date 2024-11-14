using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddExpert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.AddColumn<int>(
                name: "FunderId",
                table: "Accounts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "expert_profiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Major = table.Column<string>(type: "longtext", nullable: true),
                    ExpertId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expert_profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_expert_profiles_Accounts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_FunderId",
                table: "Accounts",
                column: "FunderId");

            migrationBuilder.CreateIndex(
                name: "IX_expert_profiles_ExpertId",
                table: "expert_profiles",
                column: "ExpertId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Accounts_FunderId",
                table: "Accounts",
                column: "FunderId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId",
                principalTable: "Majors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Accounts_FunderId",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.DropTable(
                name: "expert_profiles");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_FunderId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "FunderId",
                table: "Accounts");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId",
                principalTable: "Majors",
                principalColumn: "Id");
        }
    }
}
