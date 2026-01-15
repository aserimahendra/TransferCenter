namespace TransferCenterWeb.Extensions;

public static class ExportValidationExtensions
{
    /// <summary>
    /// Validates export date range parameters.
    /// Both dates are mandatory and the range cannot exceed 31 days.
    /// </summary>
    /// <param name="fromDate">From date (inclusive)</param>
    /// <param name="toDate">To date (inclusive)</param>
    /// <returns>Tuple with (isValid, errorMessage)</returns>
    public static (bool isValid, string errorMessage) ValidateExportDateRange(DateTime? fromDate, DateTime? toDate)
    {
        // Validation: Both dates are mandatory
        if (!fromDate.HasValue || !toDate.HasValue)
        {
            return (false, "Both 'From Date' and 'To Date' are mandatory for export.");
        }

        DateTime from = fromDate.Value.Date;
        DateTime to = toDate.Value.Date;

        // Validation: From date cannot be greater than To date
        if (from > to)
        {
            return (false, "'From Date' cannot be greater than 'To Date'.");
        }

        // Validation: Date range should not exceed 31 days
        var dateDifference = (to - from).TotalDays;
        if (dateDifference > 30)
        {
            return (false, "Date range cannot exceed 31 days. Please select a smaller date range and retry.");
        }

        return (true, string.Empty);
    }

    /// <summary>
    /// Validates export date range parameters.
    /// Both dates are mandatory and the range cannot exceed 31 days.
    /// </summary>
    /// <param name="fromDate">From date (inclusive)</param>
    /// <param name="toDate">To date (inclusive)</param>
    /// <returns>Tuple with (isValid, errorMessage)</returns>
    public static (bool isValid, string errorMessage) ValidateFilterDateRange(DateTime? fromDate, DateTime? toDate)
    {

        // Validation: Both dates are mandatory
        if (fromDate.HasValue || toDate.HasValue)
        {
            DateTime from = fromDate.Value.Date;
            DateTime to = toDate.Value.Date;

            // Validation: From date cannot be greater than To date
            if (from > to)
            {
                return (false, "'From Date' cannot be greater than 'To Date'.");
            }

            // Validation: Date range should not exceed 31 days
            var dateDifference = (to - from).TotalDays;
            if (dateDifference > 30)
            {
                return (false, "Date range cannot exceed 31 days. Please select a smaller date range and retry.");
            }
        }

        return (true, string.Empty);
    }
}
