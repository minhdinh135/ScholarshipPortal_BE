using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMajorAndSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scholarship_program_skills");

            migrationBuilder.AddColumn<int>(
                name: "ParentMajorId",
                table: "Majors",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "major_skills",
                columns: table => new
                {
                    MajorId = table.Column<int>(type: "int", nullable: false),
                    SkillId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_major_skills", x => new { x.MajorId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_major_skills_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_major_skills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId");

            migrationBuilder.CreateIndex(
                name: "IX_major_skills_SkillId",
                table: "major_skills",
                column: "SkillId");

            migrationBuilder.AddForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors",
                column: "ParentMajorId",
                principalTable: "Majors",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Majors_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.DropTable(
                name: "major_skills");

            migrationBuilder.DropIndex(
                name: "IX_Majors_ParentMajorId",
                table: "Majors");

            migrationBuilder.DropColumn(
                name: "ParentMajorId",
                table: "Majors");

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
                name: "IX_scholarship_program_skills_SkillId",
                table: "scholarship_program_skills",
                column: "SkillId");
        }
    }
}
