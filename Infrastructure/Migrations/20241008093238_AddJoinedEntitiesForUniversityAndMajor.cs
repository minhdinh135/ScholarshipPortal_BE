using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddJoinedEntitiesForUniversityAndMajor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scholarship_program_major");

            migrationBuilder.DropTable(
                name: "scholarship_program_university");

            migrationBuilder.CreateTable(
                name: "scholarship_program_majors",
                columns: table => new
                {
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    MajorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_majors", x => new { x.ScholarshipProgramId, x.MajorId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_majors_Majors_MajorId",
                        column: x => x.MajorId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scholarship_program_majors_scholarship_programs_ScholarshipP~",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scholarship_program_universities",
                columns: table => new
                {
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    UniversityId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_universities", x => new { x.ScholarshipProgramId, x.UniversityId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_universities_Universities_UniversityId",
                        column: x => x.UniversityId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scholarship_program_universities_scholarship_programs_Schola~",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_majors_MajorId",
                table: "scholarship_program_majors",
                column: "MajorId");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_universities_UniversityId",
                table: "scholarship_program_universities",
                column: "UniversityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scholarship_program_majors");

            migrationBuilder.DropTable(
                name: "scholarship_program_universities");

            migrationBuilder.CreateTable(
                name: "scholarship_program_major",
                columns: table => new
                {
                    MajorsId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipProgramsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_major", x => new { x.MajorsId, x.ScholarshipProgramsId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_major_Majors_MajorsId",
                        column: x => x.MajorsId,
                        principalTable: "Majors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scholarship_program_major_scholarship_programs_ScholarshipPr~",
                        column: x => x.ScholarshipProgramsId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "scholarship_program_university",
                columns: table => new
                {
                    ScholarshipProgramsId = table.Column<int>(type: "int", nullable: false),
                    UniversitiesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_university", x => new { x.ScholarshipProgramsId, x.UniversitiesId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_university_Universities_UniversitiesId",
                        column: x => x.UniversitiesId,
                        principalTable: "Universities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scholarship_program_university_scholarship_programs_Scholars~",
                        column: x => x.ScholarshipProgramsId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_major_ScholarshipProgramsId",
                table: "scholarship_program_major",
                column: "ScholarshipProgramsId");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_university_UniversitiesId",
                table: "scholarship_program_university",
                column: "UniversitiesId");
        }
    }
}
