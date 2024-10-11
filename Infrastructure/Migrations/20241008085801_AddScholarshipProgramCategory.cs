using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddScholarshipProgramCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scholarship_program_category");

            migrationBuilder.CreateTable(
                name: "scholarship_program_categories",
                columns: table => new
                {
                    ScholarshipProgramId = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_categories", x => new { x.ScholarshipProgramId, x.CategoryId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_categories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_scholarship_program_categories_scholarship_programs_Scholars~",
                        column: x => x.ScholarshipProgramId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_categories_CategoryId",
                table: "scholarship_program_categories",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "scholarship_program_categories");

            migrationBuilder.CreateTable(
                name: "scholarship_program_category",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "int", nullable: false),
                    ScholarshipProgramsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_scholarship_program_category", x => new { x.CategoriesId, x.ScholarshipProgramsId });
                    table.ForeignKey(
                        name: "FK_scholarship_program_category_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_scholarship_program_category_scholarship_programs_Scholarshi~",
                        column: x => x.ScholarshipProgramsId,
                        principalTable: "scholarship_programs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_scholarship_program_category_ScholarshipProgramsId",
                table: "scholarship_program_category",
                column: "ScholarshipProgramsId");
        }
    }
}
