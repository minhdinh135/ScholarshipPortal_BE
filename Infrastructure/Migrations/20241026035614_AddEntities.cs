using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Accounts_ApplicantId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Services_ServiceId",
                table: "Requests");

            migrationBuilder.DropForeignKey(
                name: "FK_Skills_scholarship_programs_ScholarshipProgramId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Skills_ScholarshipProgramId",
                table: "Skills");

            migrationBuilder.DropIndex(
                name: "IX_Requests_ServiceId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ScholarshipProgramId",
                table: "Skills");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "ScholarshipProgramId",
                table: "Certificates");

            migrationBuilder.AddColumn<string>(
                name: "TransactionId",
                table: "Transactions",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Duration",
                table: "Services",
                type: "datetime(6)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Experiences",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    ApplicantProfileId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiences", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experiences_applicant_profiles_ApplicantProfileId",
                        column: x => x.ApplicantProfileId,
                        principalTable: "applicant_profiles",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "request_details",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ExpectedCompletionTime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ApplicationNotes = table.Column<string>(type: "longtext", nullable: true),
                    ScholarshipType = table.Column<string>(type: "longtext", nullable: true),
                    ApplicationFileUrl = table.Column<string>(type: "longtext", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    ServiceId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_request_details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_request_details_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_request_details_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scholarship_program_skills",
                columns: table => new
                {
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_skills", x => new { x.ScholarshipProgramId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_skills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scholarship_program_skills_scholarship_programs_ScholarshipP~",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_ApplicantProfileId",
                table: "Experiences",
                column: "ApplicantProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_request_details_RequestId",
                table: "request_details",
                column: "RequestId");

            migrationBuilder.CreateIndex(
                name: "IX_request_details_ServiceId",
                table: "request_details",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_skills_SkillId",
                table: "scholarship_program_skills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Accounts_ApplicantId",
                table: "Requests",
                column: "ApplicantId",
                principalTable: "Accounts",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Requests_Accounts_ApplicantId",
                table: "Requests");

            migrationBuilder.DropTable(
                name: "Experiences");

            migrationBuilder.DropTable(
                name: "request_details");

            migrationBuilder.DropTable(
                name: "scholarship_program_skills");

            migrationBuilder.DropColumn(
                name: "TransactionId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Services");

            migrationBuilder.AddColumn<int>(
                name: "ScholarshipProgramId",
                table: "Skills",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ScholarshipProgramId",
                table: "Certificates",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skills_ScholarshipProgramId",
                table: "Skills",
                column: "ScholarshipProgramId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_ServiceId",
                table: "Requests",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Accounts_ApplicantId",
                table: "Requests",
                column: "ApplicantId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Requests_Services_ServiceId",
                table: "Requests",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Skills_scholarship_programs_ScholarshipProgramId",
                table: "Skills",
                column: "ScholarshipProgramId",
                principalTable: "scholarship_programs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
