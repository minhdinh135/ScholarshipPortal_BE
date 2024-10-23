using Domain.Entities;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System.IO;
using Application.Interfaces.IServices;

namespace Infrastructure.ExternalServices.ExportPDF
{
	public class PdfExportService : IPdfExportService
	{
		public byte[] ExportProfileToPdf(ApplicantProfile profile)
		{
			using (var memoryStream = new MemoryStream())
			{
				using (var writer = new PdfWriter(memoryStream))
				{
					using (var pdf = new PdfDocument(writer))
					{
						var document = new Document(pdf);

						document.Add(new Paragraph("Curriculum Vitae")
							.SetFontSize(30)
							.SetBold()
							.SetTextAlignment(TextAlignment.CENTER)
							.SetMarginBottom(20));

						document.Add(new Paragraph("Personal Information")
							.SetFontSize(20)
							.SetBold()
							.SetMarginTop(20));

						document.Add(new Paragraph($"First Name: {profile.FirstName}"));
						document.Add(new Paragraph($"Last Name: {profile.LastName}"));
						document.Add(new Paragraph($"Birth Date: {profile.BirthDate?.ToString("yyyy-MM-dd") ?? "N/A"}"));
						document.Add(new Paragraph($"Gender: {profile.Gender}"));
						document.Add(new Paragraph($"Nationality: {profile.Nationality}"));
						document.Add(new Paragraph($"Ethnicity: {profile.Ethnicity}"));

						//document.Add(new Paragraph().SetMarginTop(10));

						//document.Add(new Paragraph("Work Experience")
						//	.SetFontSize(20)
						//	.SetBold()
						//	.SetMarginTop(20));

						//foreach (var experience in profile.WorkExperiences)
						//{
						//	document.Add(new Paragraph($"Position: {experience.Position}")
						//		.SetBold());
						//	document.Add(new Paragraph($"Company: {experience.Company}"));
						//	document.Add(new Paragraph($"Duration: {experience.StartDate.ToString("yyyy-MM-dd")} - {experience.EndDate.ToString("yyyy-MM-dd")}"));
						//	document.Add(new Paragraph($"Description: {experience.Description}"));
						//	document.Add(new Paragraph().SetMarginTop(5));
						//}


						document.Close();
					}
				}

				return memoryStream.ToArray();
			}
		}
	}
}
