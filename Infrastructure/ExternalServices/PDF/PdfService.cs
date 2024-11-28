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

    public async Task<byte[]> GenerateProfileInPdf(ApplicantProfileDetails profile)
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
                    .Text("Applicant Resume")
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

                            table.Cell().Text("Gender:");
                            table.Cell().Text($"{profile.Gender}");

                            table.Cell().Text("Date of Birth:");
                            table.Cell().Text($"{profile.Birthdate.ToString("yyyy-MM-dd") ?? "N/A"}");

                            table.Cell().Text("Address:");
                            table.Cell().Text($"{profile.Address ?? "N/A"}");

                            table.Cell().Text("Email:");
                            table.Cell().Text($"{profile.Email}");

                            table.Cell().Text("Phone:");
                            table.Cell().Text($"{profile.Phone}");

                            table.Cell().Text("Nationality:");
                            table.Cell().Text($"{profile.Nationality}");

                            table.Cell().Text("Phone:");
                            table.Cell().Text($"{profile.Ethnicity}");
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

                            table.Cell().Text("Major");
                            table.Cell().Text($"{profile.Major}");

                            table.Cell().Text("Current School:");
                            table.Cell().Text($"{profile.School}");

                            table.Cell().Text("GPA:");
                            table.Cell().Text($"{profile.Gpa}/4.0");
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
                                    column.Item().Text($"• {achievement}");
                                }
                            });
                        }


                        x.Item().Text("Experience").FontSize(14).SemiBold();
                        if (profile.Experience.IsNullOrEmpty())
                        {
                            x.Item().Text("N/A");
                        }
                        else
                        {
                            x.Item().Column(column =>
                            {
                                foreach (var experience in profile.Experience)
                                {
                                    column.Item().Text($"• {experience}");
                                }
                            });
                        }

                        x.Item().Text("Skills").FontSize(14).SemiBold();
                        if (profile.Skills.IsNullOrEmpty())
                        {
                            x.Item().Text("N/A");
                        }
                        else
                        {
                            x.Item().Column(column =>
                            {
                                foreach (var skill in profile.Skills)
                                {
                                    column.Item().Text($"• {skill}");
                                }
                            });
                        }

                        x.Item().Text("Certificates").FontSize(14).SemiBold();
                        if (profile.Certificates.IsNullOrEmpty())
                        {
                            x.Item().Text("N/A");
                        }
                        else
                        {
                            x.Item().Column(column =>
                            {
                                foreach (var certificate in profile.Certificates)
                                {
                                    column.Item().Text($"• {certificate}");
                                }
                            });
                        }
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