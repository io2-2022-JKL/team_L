using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using Azure.Storage.Blobs;
using System.IO;

namespace VaccinationSystem.Services
{
    public class VaccinationCertificateGenerator : ICertificateGenerator
    {
        private BlobContainerClient container;
        public VaccinationCertificateGenerator()
        {
            var storage = new BlobServiceClient("DefaultEndpointsProtocol=https;AccountName=vaccinationsystemstorage;AccountKey=66vN88zKSolLXKw5Om5liSBM5iROdOO/BXoxa2wsQT8TVZnXgV9wmd+BuGjxsPg47QRBqNEJ3C7i+AStyuY9rg==;EndpointSuffix=core.windows.net");
            container = storage.GetBlobContainerClient("certificates");

        }
        public async Task<string> Generate(string patientName, DateTime dateOfBirth, string pesel, string vcName, string vcAddress, string vaccine, int dose, string batch)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            PdfDocument pdf = new PdfDocument();
            PdfPage page = pdf.AddPage();
            XGraphics g = XGraphics.FromPdfPage(page);

            DrawTitle(g);
            DrawPatientInformations(g, patientName, dateOfBirth, pesel);
            DrawVaccinationInformations(g, vcName, vcAddress, vaccine, dose, batch);

            string fileName = patientName + "_" + pesel + "_" + dose.ToString() + ".pdf";
            var client = container.GetBlobClient(fileName);
            using (var ms = new MemoryStream())
            {
                pdf.Save(ms);
                await client.UploadAsync(ms, true);
            }

            g.Dispose();

            return client.Uri.ToString();
        }

        private void DrawTitle(XGraphics g)
        {
            g.DrawString("Vacciantion Certificate", new XFont("Arial", 30, XFontStyle.Bold), XBrushes.Black, new XPoint(140, 70));
            g.DrawLine(XPens.DarkRed, new XPoint(70, 100), new XPoint(530, 100));
        }

        private void DrawPatientInformations(XGraphics g, string patientName, DateTime dateOfBirth, string pesel)
        {
            g.DrawString("Patient Informations", new XFont("Arial", 20, XFontStyle.Bold), XBrushes.Black, new XPoint(210, 150));

            g.DrawString("Name", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 200));
            g.DrawString(patientName, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 220));

            g.DrawString("Date of birth", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 260));
            g.DrawString(dateOfBirth.ToString("dd-MM-yyyy"), new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 280));

            g.DrawString("PESEL", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 320));
            g.DrawString(pesel, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 340));

            g.DrawLine(XPens.DarkRed, new XPoint(70, 370), new XPoint(530, 370));
        }

        private void DrawVaccinationInformations(XGraphics g, string vcName, string vcAddress, string vaccine, int dose, string batch)
        {
            g.DrawString("Vaccination Informations", new XFont("Arial", 20, XFontStyle.Bold), XBrushes.Black, new XPoint(180, 420));

            g.DrawString("Vaccination center name", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 470));
            g.DrawString(vcName, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 490));

            g.DrawString("Vaccination center adress", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 530));
            g.DrawString(vcAddress, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 550));

            g.DrawString("Vaccine name", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 590));
            g.DrawString(vaccine, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 610));

            g.DrawString("Dose number", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 650));
            g.DrawString(dose.ToString(), new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 670));

            g.DrawString("Batch number", new XFont("Arial", 14, XFontStyle.Bold), XBrushes.Black, new XPoint(70, 710));
            g.DrawString(batch, new XFont("Arial", 14), XBrushes.Black, new XPoint(70, 730));
        }
    }
}
