using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAssignmentAndProfileRelatedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Assignment");

            migrationBuilder.DropColumn(
                name: "AchievedDate",
                table: "applicant_certificates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Reviews",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedDate",
                table: "Reviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DeadlineDate",
                table: "Reviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "FromYear",
                table: "applicant_skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ToYear",
                table: "applicant_skills",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "AchievedYear",
                table: "applicant_certificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "expert_for_programs",
                columns: table => new
                {
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    ExpertId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expert_for_programs", x => new { x.ScholarshipProgramId, x.ExpertId });
                    table.ForeignKey(
                        name: "FK_expert_for_programs_Accounts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_expert_for_programs_scholarship_programs_ScholarshipProgramId",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_expert_for_programs_ExpertId",
                table: "expert_for_programs",
                column: "ExpertId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expert_for_programs");

            migrationBuilder.DropColumn(
                name: "AssignedDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "DeadlineDate",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "FromYear",
                table: "applicant_skills");

            migrationBuilder.DropColumn(
                name: "ToYear",
                table: "applicant_skills");

            migrationBuilder.DropColumn(
                name: "AchievedYear",
                table: "applicant_certificates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReviewDate",
                table: "Reviews",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "AchievedDate",
                table: "applicant_certificates",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Assignment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ExpertId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Status = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assignment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assignment_Accounts_ExpertId",
                        column: x => x.ExpertId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Assignment_scholarship_programs_ScholarshipProgramId",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_ExpertId",
                table: "Assignment",
                column: "ExpertId");

            migrationBuilder.CreateIndex(
                name: "IX_Assignment_ScholarshipProgramId",
                table: "Assignment",
                column: "ScholarshipProgramId");
        }
    }
}
