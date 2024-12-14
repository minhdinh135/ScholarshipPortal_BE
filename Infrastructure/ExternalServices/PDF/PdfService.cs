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
    
    public async Task<byte[]> GenerateScholarshipContractPdf()
    {
        var document = Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(12));

                // Title and Content
                page.Content().Column(column =>
                {
                    column.Spacing(20);

                    // Title
                    column.Item().Text("SCHOLARSHIP CONTRACT")
                        .Bold().FontSize(24).AlignCenter().Underline();

                    // Institution Name
                    column.Item().Text("Tri Do Scholarship Fund")
                        .Bold().FontSize(18).AlignCenter();

                    // Introduction Section
                    column.Item().Text("This Scholarship Agreement (the 'Agreement') is made between the following parties:")
                        .FontSize(12);
                    column.Item().Text("• The Scholarship Funder: Tri Do Scholarship Fund.")
                        .FontSize(12);
                    column.Item().Text("• The Scholarship Recipient: [Student Name], hereinafter referred to as the 'Student'.")
                        .FontSize(12);
                    column.Item().Text("The purpose of this Agreement is to set the terms and conditions for the scholarship awarded to the Student for their academic year.")
                        .FontSize(12);
                    
                    column.Spacing(20);

                    // Scholarship Amount Section
                    column.Item().Text("1. SCHOLARSHIP AMOUNT").Bold().FontSize(14);
                    column.Item().Text("The Scholarship Fund Provider agrees to provide the Student with a scholarship amount of $100,000 for the current academic year.")
                        .FontSize(12);
                    column.Item().Text("The scholarship amount will be equally distributed across the Fall and Spring semesters of the academic year.")
                        .FontSize(12);

                    column.Spacing(20);

                    // Fund Payment Commitment Section
                    column.Item().Text("2. FUND PAYMENT COMMITMENT").Bold().FontSize(14);
                    column.Item().Text("The Scholarship Fund Provider agrees to disburse the total scholarship amount in a timely manner, ensuring full payment of tuition and other eligible fees directly to the institution or any other entity designated by the Student. Payments shall be made before the start of each semester.")
                        .FontSize(12);
                    column.Item().Text("Failure to fulfill this payment commitment will result in the scholarship being revoked and the Student will not be eligible for the scholarship until payments are brought up to date.")
                        .FontSize(12);

                    column.Spacing(20);

                    // Academic Requirements Section
                    column.Item().Text("3. ACADEMIC REQUIREMENTS").Bold().FontSize(14);
                    column.Item().Text("The Student must maintain a minimum GPA of 3.0 each semester. Should the GPA fall below this threshold, the Student may be placed on probation or the scholarship may be revoked.")
                        .FontSize(12);
                    column.Item().Text("The Scholarship Fund Provider reserves the right to review the academic performance of the Student at the end of each semester.")
                        .FontSize(12);

                    column.Spacing(20);

                    // Termination Section
                    column.Item().Text("4. TERMINATION OF AGREEMENT").Bold().FontSize(14);
                    column.Item().Text("This Agreement may be terminated under the following circumstances:")
                        .FontSize(12);
                    column.Item().Text("• The Student withdraws from or transfers out of the institution.")
                        .FontSize(12);
                    column.Item().Text("• The Student fails to comply with the terms of this Agreement.")
                        .FontSize(12);
                    column.Item().Text("• The Student's academic performance fails to meet the required standards.")
                        .FontSize(12);

                    column.Spacing(20);

                    // Compliance Section
                    column.Item().Text("5. COMPLIANCE WITH LAWS AND REGULATIONS").Bold().FontSize(14);
                    column.Item().Text("The Student agrees to abide by all the rules, regulations, and policies of the institution. Failure to comply with these terms may result in termination of the scholarship.")
                        .FontSize(12);

                    column.Spacing(20);

                    // General Provisions Section
                    column.Item().Text("6. GENERAL PROVISIONS").Bold().FontSize(14);
                    column.Item().Text("This Agreement is governed by the laws of [Jurisdiction]. Any disputes arising from this Agreement will be resolved in accordance with the applicable laws of the jurisdiction.")
                        .FontSize(12);
                    column.Item().Text("This Agreement constitutes the entire understanding between the parties and supersedes all prior negotiations, representations, or agreements, whether written or oral.")
                        .FontSize(12);

                    column.Spacing(30);

                    // Signature Section
                    column.Item().Text("SIGNATURES").Bold().FontSize(14);
                    column.Item().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Cell().Text("Scholarship Funder:").FontSize(12);
                        table.Cell().Text("[Scholarship Provider Representative Name]").FontSize(12);

                        table.Cell().Text("Signature:").FontSize(12);
                        table.Cell().Text("[Signature]").FontSize(12);

                        table.Cell().Text("Date:").FontSize(12);
                        table.Cell().Text($"{DateTime.UtcNow:yyyy-MM-dd}").FontSize(12);

                        table.Cell().Text("Student:").FontSize(12);
                        table.Cell().Text("[Student Name]").FontSize(12);

                        table.Cell().Text("Signature:").FontSize(12);
                        table.Cell().Text("[Signature]").FontSize(12);

                        table.Cell().Text("Date:").FontSize(12);
                        table.Cell().Text($"{DateTime.UtcNow:yyyy-MM-dd}").FontSize(12);
                    });

                    column.Spacing(20);

                });

                // Footer
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
