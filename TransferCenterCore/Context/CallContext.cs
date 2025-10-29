using System;
using System.Collections.Generic;

namespace TransferCenterCore.Context;

public sealed class CallContext
{
    public static CallContext Empty { get; } = new();

    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? DisplayName { get; set; }
    public string? EmailId { get; set; }
    public string? CorrelationId { get; set; }
    public string? IpAddress { get; set; }
    public IList<string> Roles { get; } = new List<string>();
    public IDictionary<string, string> Items { get; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    public void SetItem(string key, string? value)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(value))
        {
            Items.Remove(key);
            return;
        }

        Items[key] = value;
    }

    public string? GetItem(string key)
    {
        if (string.IsNullOrWhiteSpace(key))
        {
            return null;
        }

        return Items.TryGetValue(key, out var value) ? value : null;
    }
}
