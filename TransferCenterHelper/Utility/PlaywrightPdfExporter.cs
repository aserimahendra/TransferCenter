using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;

namespace TransferCenterHelper.Utility;

public sealed class PlaywrightPdfExporter : IPdfExporter
{
    public byte[] ConvertHtmlToPdf(string htmlContent, string? title = null, string? baseUrl = null)
    {
        return ConvertWithPlaywright(htmlContent, title, baseUrl).GetAwaiter().GetResult();
    }

    public void ConvertHtmlToPdfFile(string htmlContent, string outputPath, string? title = null)
    {
        var bytes = ConvertHtmlToPdf(htmlContent, title);
        File.WriteAllBytes(outputPath, bytes);
    }
    private static async Task<byte[]> ConvertWithPlaywright(string htmlContent, string? title, string? baseUrl)
    {
        using var playwright = await Microsoft.Playwright.Playwright.CreateAsync();
        // Launch a single-use headless Chromium instance
        await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = true
        });

        var context = await browser.NewContextAsync(new BrowserNewContextOptions
        {
            ViewportSize = null // let Chromium decide based on content
        });

        var page = await context.NewPageAsync();
        await page.EmulateMediaAsync(new PageEmulateMediaOptions { Media = Media.Screen, ColorScheme = ColorScheme.Light });

        var html = htmlContent ?? string.Empty;
        if (!string.IsNullOrWhiteSpace(baseUrl))
        {
            html = InjectBaseHref(html, baseUrl!);
        }

        await page.SetContentAsync(html, new PageSetContentOptions { WaitUntil = WaitUntilState.NetworkIdle });

        // Build PDF options
        var pdfOptions = new PagePdfOptions
        {
            PrintBackground = true,
            PreferCSSPageSize = true
        };
        // Defaults
        pdfOptions.Format = "A4";

        // Header/Footer mapping via provided title
        if (!string.IsNullOrWhiteSpace(title))
        {
            // If title provided explicitly, use as header right side
            pdfOptions.DisplayHeaderFooter = true;
            pdfOptions.HeaderTemplate = BuildHeaderTemplate(title);
        }

        var data = await page.PdfAsync(pdfOptions);
        return data;
    }

    private static string ToPx(double? mm) => $"{(mm ?? 0)}mm";

    private static string BuildHeaderTemplate(string text)
    {
        // Chromium templates use a small HTML; replace page counters if present
        text = (text ?? string.Empty)
            .Replace("[page]", "<span class=\"pageNumber\"></span>")
            .Replace("[toPage]", "<span class=\"totalPages\"></span>");

        return "" +
               "<div style=\"font-size:10px;width:100%;padding:0 20px;display:flex;justify-content:flex-end;\">" +
               $"<div>{System.Net.WebUtility.HtmlEncode(text)}</div>" +
               "</div>";
    }

    private static string BuildFooterTemplate(string text)
    {
        text = (text ?? string.Empty)
            .Replace("[page]", "<span class=\"pageNumber\"></span>")
            .Replace("[toPage]", "<span class=\"totalPages\"></span>");

        return "" +
               "<div style=\"font-size:10px;width:100%;padding:0 20px;display:flex;justify-content:flex-end;\">" +
               $"<div>{System.Net.WebUtility.HtmlEncode(text)}</div>" +
               "</div>";
    }

    private static string InjectBaseHref(string html, string baseUrl)
    {
        try
        {
            var idx = html.IndexOf("<head", StringComparison.OrdinalIgnoreCase);
            if (idx >= 0)
            {
                var headStartClose = html.IndexOf('>', idx);
                if (headStartClose > idx)
                {
                    var insertPos = headStartClose + 1;
                    var baseTag = $"<base href=\"{baseUrl.TrimEnd('/')}/\">";
                    return html.Substring(0, insertPos) + baseTag + html.Substring(insertPos);
                }
            }
            // No head tag; prepend a simple head
            return $"<head><base href=\"{baseUrl.TrimEnd('/')}/\"></head>" + html;
        }
        catch
        {
            return html; // best effort
        }
    }
}
