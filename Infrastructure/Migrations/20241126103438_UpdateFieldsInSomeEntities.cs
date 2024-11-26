using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFieldsInSomeEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileUrl",
                table: "award_milestone_documents");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "award_milestone_documents");

            migrationBuilder.AddColumn<int>(
                name: "NumberOfServices",
                table: "Subscriptions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfAwardMilestones",
                table: "scholarship_programs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Score",
                table: "application_reviews",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SubscriptionEndDate",
                table: "Accounts",
                type: "datetime(6)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfServices",
                table: "Subscriptions");

            migrationBuilder.DropColumn(
                name: "NumberOfAwardMilestones",
                table: "scholarship_programs");

            migrationBuilder.DropColumn(
                name: "Score",
                table: "application_reviews");

            migrationBuilder.DropColumn(
                name: "SubscriptionEndDate",
                table: "Accounts");

            migrationBuilder.AddColumn<string>(
                name: "FileUrl",
                table: "award_milestone_documents",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "award_milestone_documents",
                type: "longtext",
                nullable: true);
        }
    }
}
