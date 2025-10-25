using DinkToPdf;
using DinkToPdf.Contracts;

namespace TransferCenterHelper.Utility;

public class PDFExporter
{
    private readonly IConverter _converter;

    public PDFExporter(IConverter converter)
    {
        _converter = converter;
    }

    /// <summary>
    /// Converts HTML content to a PDF document
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert</param>
    /// <param name="title">Optional title for the PDF document</param>
    /// <returns>Byte array containing the PDF document</returns>
    public byte[] ConvertHtmlToPdf(string htmlContent, string? title = null)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 20, Bottom = 20, Left = 20, Right = 20 }
            },
            Objects = {
                new ObjectSettings
                {
                    PagesCount = true,
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" },
                    HeaderSettings = {
                        FontSize = 12,
                        Right = title,
                        Line = true,
                        Spacing = 2.812
                    },
                    FooterSettings = {
                        FontSize = 10,
                        Right = "Page [page] of [toPage]",
                        Line = true,
                        Spacing = 2.812
                    }
                }
            }
        };

        return _converter.Convert(doc);
    }

    /// <summary>
    /// Converts HTML content to a PDF document and saves it to a file
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert</param>
    /// <param name="outputPath">Path where the PDF file should be saved</param>
    /// <param name="title">Optional title for the PDF document</param>
    public void ConvertHtmlToPdfFile(string htmlContent, string outputPath, string? title = null)
    {
        var pdfBytes = ConvertHtmlToPdf(htmlContent, title);
        File.WriteAllBytes(outputPath, pdfBytes);
    }

    /// <summary>
    /// Creates a PDF document with custom settings
    /// </summary>
    /// <param name="htmlContent">The HTML content to convert</param>
    /// <param name="settings">Custom PDF settings</param>
    /// <returns>Byte array containing the PDF document</returns>
    public byte[] CreatePdfWithCustomSettings(string htmlContent, Action<HtmlToPdfDocument> settings)
    {
        var doc = new HtmlToPdfDocument()
        {
            GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4
            },
            Objects = {
                new ObjectSettings
                {
                    HtmlContent = htmlContent,
                    WebSettings = { DefaultEncoding = "utf-8" }
                }
            }
        };

        settings(doc);
        return _converter.Convert(doc);
    }
}
