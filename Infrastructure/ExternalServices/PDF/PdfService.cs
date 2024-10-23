using Application.Interfaces.IServices;
using Domain.DTOs.Applicant;
using Microsoft.IdentityModel.Tokens;
using QuestPDF;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace Infrastructure.ExternalServices.PDF;

public class PdfService : IPdfService
{
    public PdfService()
    {
        Settings.License = LicenseType.Community;
    }

    public async Task<byte[]> GenerateProfileInPdf(ApplicantProfileDto profile)
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(11));

                page.Header()
                    .Text("Scholarship Application")
                    .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        // Personal Information
                        x.Item().Text("Personal Information").FontSize(14).SemiBold();
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Name:");
                            table.Cell().Text($"{profile.FirstName} {profile.LastName}");

                            table.Cell().Text("Date of Birth:");
                            table.Cell().Text($"{profile.BirthDate?.ToString("yyyy-MM-dd") ?? "N/A"}");

                            table.Cell().Text("Address:");
                            table.Cell().Text($"{profile.Applicant?.Address ?? "N/A"}");

                            table.Cell().Text("Email:");
                            table.Cell().Text($"{profile.Applicant?.Email}");

                            table.Cell().Text("Phone:");
                            table.Cell().Text($"{profile.Applicant?.PhoneNumber}");
                        });

                        // Academic Information
                        x.Item().Text("Academic Information").FontSize(14).SemiBold();
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(150);
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Current School:");
                            table.Cell().Text("University of Example");

                            table.Cell().Text("Major:");
                            table.Cell().Text("Computer Science");

                            table.Cell().Text("Expected Graduation:");
                            table.Cell().Text("May 2024");

                            table.Cell().Text("GPA:");
                            table.Cell().Text("3.8/4.0");
                        });

                        // Achievements
                        x.Item().Text("Achievements").FontSize(14).SemiBold();
                        if (profile.Achievements.IsNullOrEmpty())
                        {
                            x.Item().Text("N/A");
                        }
                        else
                        {
                            x.Item().Column(column =>
                            {
                                foreach (var achievement in profile.Achievements)
                                {
                                    column.Item().Text($"• {achievement.Name}");
                                }
                            });
                        }


                        // Extracurricular Activities
                        x.Item().Text("Extracurricular Activities").FontSize(14).SemiBold();
                        x.Item().Column(column =>
                        {
                            column.Item().Text("• Student Government Association - Vice President");
                            column.Item().Text("• Coding Club - Founder and President");
                            column.Item().Text("• Volunteer at Local Food Bank - 100+ hours");
                        });

                        // Awards and Honors
                        x.Item().Text("Awards and Honors").FontSize(14).SemiBold();
                        x.Item().Column(column =>
                        {
                            column.Item().Text("• Dean's List - All Semesters");
                            column.Item().Text("• First Place, University Hackathon 2022");
                            column.Item().Text("• National Merit Scholar");
                        });

                        // Personal Statement
                        x.Item().Text("Personal Statement").FontSize(14).SemiBold();
                        x.Item().Text(Placeholders.LoremIpsum());

                        // References
                        x.Item().Text("References").FontSize(14).SemiBold();
                        x.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.ConstantColumn(100);
                                columns.RelativeColumn();
                            });

                            table.Cell().Text("Name:");
                            table.Cell().Text("Dr. Jane Smith");

                            table.Cell().Text("Position:");
                            table.Cell().Text("Professor of Computer Science");

                            table.Cell().Text("Contact:");
                            table.Cell().Text("jsmith@university.edu | (987) 654-3210");
                        });
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        });

        return document.GeneratePdf();
    }
}