namespace TransferCenterWeb.Models;

public class PaginationModel
{
    public string Action { get; set; } = string.Empty;
    public string? Controller { get; set; }
    public string? Area { get; set; }
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get; set; } = 10;
    public int TotalItems { get; set; }
    public int MaxPageLinks { get; set; } = 5;
    public IDictionary<string, string>? RouteValues { get; set; }
    public string PageQueryKey { get; set; } = "page";
    public string PageSizeQueryKey { get; set; } = "pageSize";
    public bool RenderIfSinglePage { get; set; }
    public int TotalPages => PageSize <= 0 ? 0 : (int)Math.Ceiling(TotalItems / (double)PageSize);
}
