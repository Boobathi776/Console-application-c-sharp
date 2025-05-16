using System;
using PdfSharp.Drawing;
using PdfSharp;
using PdfSharp.Pdf;
namespace Tasks.Practice;

public class PdfFileCreation
{
    public void PdfCreate()
    {
        try
        {
            //TimeOnly.TryParse("09:42:00",out TimeOnly morningTime);
            //DateTime now = DateTime.Now;
            //TimeOnly val = TimeOnly.FromDateTime(now);
            //TimeSpan span = val  - morningTime;
            //Console.WriteLine(span.ToString());

            string folderPath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice\PdfFileHandling\";
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, "PdfSharpExample.pdf");
            PdfDocument document = new PdfDocument();
            document.Info.Title = "Created with PdfSharpCore";
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Times New Roman", 20 ,XFontStyleEx.Bold);
            XFont font1 = new XFont("Arial" ,18,XFontStyleEx.Italic);
            gfx.DrawString("Hello from PdfSharpCore!", font, XBrushes.DarkBlue,
                new XRect(0, 0, page.Width, page.Height),
                XStringFormats.TopLeft);
            gfx.DrawString("Welcome to the magix show", font, XBrushes.Black, new XRect(2, 2, page.Width, page.Height), XStringFormat.Center);


            PdfPage page2 = document.AddPage();
            XGraphics gfx1 = XGraphics.FromPdfPage(page2);
            gfx1.DrawString("this is the first line of the second page", font1, XBrushes.DarkGreen, 10, 15);
            string imagePath = @"D:\Azure-Assignment\C#-Tasks\C#-Tasks\Practice\PdfFileHandling\download1.jpg";
            XImage image = XImage.FromFile(imagePath);
            gfx1.DrawImage(image, 50, 100,200,200);
            document.Save(filePath);
            Console.WriteLine("PDF created at: " + filePath);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}