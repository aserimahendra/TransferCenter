using System.Collections.Generic;
using System.Linq;

namespace TransferCenterWeb.Models;

public class ModalActionResult
{
    public int StatusCode { get; }
    public bool IsSuccess { get; }
    public string Message { get; }
    public List<string> Warnings { get; }
    public List<string> Errors { get; }

    public ModalActionResult(
        string message,
        int statusCode = 200,
        bool isSuccess = true,
        IEnumerable<string>? warnings = null,
        IEnumerable<string>? errors = null)
    {
        Message = NormalizeMessage(message);
        StatusCode = statusCode > 0 ? statusCode : (isSuccess ? 200 : 500);
        IsSuccess = isSuccess;
        Warnings = FilterMessages(warnings);
        Errors = FilterMessages(errors);

        if (!IsSuccess && StatusCode >= 500)
        {
            Message = string.IsNullOrWhiteSpace(Message)
                ? "Internal server error."
                : Message;

            if (!Errors.Any())
            {
                Errors.Add("Internal server error.");
            }

            if (!Warnings.Any())
            {
                Warnings.Add("Please contact support if the problem persists.");
            }
        }
    }

    private static string NormalizeMessage(string message)
    {
        return string.IsNullOrWhiteSpace(message)
            ? string.Empty
            : message.Trim();
    }

    private static List<string> FilterMessages(IEnumerable<string>? source)
    {
        return source?
            .Where(value => !string.IsNullOrWhiteSpace(value))
            .Select(value => value.Trim())
            .ToList()
            ?? new List<string>();
    }
}
