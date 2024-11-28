using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMajorWithGpaAndSchoolToApplicantProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Gpa",
                table: "applicant_profiles",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Major",
                table: "applicant_profiles",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "School",
                table: "applicant_profiles",
                type: "longtext",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Gpa",
                table: "applicant_profiles");

            migrationBuilder.DropColumn(
                name: "Major",
                table: "applicant_profiles");

            migrationBuilder.DropColumn(
                name: "School",
                table: "applicant_profiles");
        }
    }
}
