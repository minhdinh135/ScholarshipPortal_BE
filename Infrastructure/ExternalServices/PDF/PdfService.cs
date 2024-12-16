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

	public async Task<byte[]> GenerateScholarshipContractPdf(
	string applicantName,
	string scholarshipAmount,
	string scholarshipProviderName,
	DateTime deadline)
	{
		var document = Document.Create(container =>
		{
			container.Page(page =>
			{
				page.Size(PageSizes.A4);
				page.Margin(2, Unit.Centimetre);
				page.PageColor(Colors.White);

				// Thêm hình nền
				page.Background()
			   .PaddingTop(4, Unit.Centimetre) 
			   .PaddingLeft(0)                
			   .PaddingRight(0)            
			   .PaddingBottom(0)            
			   .Image("SSAPblur.jpg", ImageScaling.FitArea);

				page.Content().Column(column =>
				{
					column.Spacing(20);

					column.Item().Text("SCHOLARSHIP CONTRACT")
						.Bold().FontSize(24).AlignCenter().Underline();

					column.Item().Text("This Scholarship Agreement (the 'Agreement') is made between the following parties:")
						.FontSize(12);
					column.Item().Text($"\u2022 The Scholarship Funder: {scholarshipProviderName}.")
						.FontSize(12);
					column.Item().Text($"\u2022 The Scholarship Recipient: {applicantName}, hereinafter referred to as the 'Student'.")
						.FontSize(12);
					column.Item().Text("The purpose of this Agreement is to set the terms and conditions for the scholarship awarded to the Student for their academic year.")
						.FontSize(12);

					column.Spacing(20);

					column.Item().Text("1. SCHOLARSHIP AMOUNT").Bold().FontSize(14);
					column.Item().Text($"The Scholarship Fund Provider agrees to provide the Student with a scholarship amount of {scholarshipAmount} for the current academic year.")
						.FontSize(12);

					column.Spacing(20);

					column.Item().Text("2. FUND PAYMENT COMMITMENT").Bold().FontSize(14);
					column.Item().Text("The Scholarship Fund Provider agrees to disburse the total scholarship amount in a timely manner, ensuring full payment of tuition and other eligible fees directly to the institution or any other entity designated by the Student. Payments shall be made before the start of each semester.")
						.FontSize(12);

					column.Spacing(20);

					column.Item().Text("3. ACADEMIC REQUIREMENTS").Bold().FontSize(14);
					column.Item().Text("The Student must maintain a minimum GPA of 3.0 each semester. Should the GPA fall below this threshold, the Student may be placed on probation or the scholarship may be revoked.")
						.FontSize(12);

					column.Spacing(20);

					column.Item().Text("4. TERMINATION OF AGREEMENT").Bold().FontSize(14);
					column.Item().Text("This Agreement may be terminated under the following circumstances:")
						.FontSize(12);
					column.Item().Text("\u2022 The Student withdraws from or transfers out of the institution.")
						.FontSize(12);
					column.Item().Text("\u2022 The Student fails to comply with the terms of this Agreement.")
						.FontSize(12);

					column.Spacing(30);

					column.Item().Text("SIGNATURES").Bold().FontSize(14);
					column.Item().Table(table =>
					{
						table.ColumnsDefinition(columns =>
						{
							columns.RelativeColumn();
							columns.RelativeColumn();
						});

						table.Cell().Text("Scholarship Funder:").FontSize(12);
						table.Cell().Text(scholarshipProviderName).FontSize(12);

						table.Cell().Text("Contract creation date:").FontSize(12);
						table.Cell().Text(DateTime.Now.ToString("MM-dd-yyyy")).FontSize(12);

						table.Cell().Text("Student:").FontSize(12);
						table.Cell().Text(applicantName).FontSize(12);

						table.Cell().Text("Deadline:").FontSize(12);
						table.Cell().Text(deadline.ToString("MM-dd-yyyy")).FontSize(12);
					});

					column.Spacing(30);
					column.Item().AlignRight().Column(signatureColumn =>
					{
						signatureColumn.Spacing(5);
						signatureColumn.Item().Text("Representative of A (Funder)").FontSize(12).AlignRight();
						signatureColumn.Item().PaddingBottom(4, Unit.Centimetre);
						signatureColumn.Item().Text("Signature:").FontSize(12).AlignRight();
						signatureColumn.Item().Text("______________________").FontSize(12).AlignRight();
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
