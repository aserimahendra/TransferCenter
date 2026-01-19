namespace TransferCenterHelper;

public interface IPdfExporter
{
    byte[] ConvertHtmlToPdf(string htmlContent, string? title = null, string? baseUrl = null);
    void ConvertHtmlToPdfFile(string htmlContent, string outputPath, string? title = null);
}
