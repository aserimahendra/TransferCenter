using System.Collections.Generic;
using System.Linq;
using TransferCenterCore.Context;
using WebUser = TransferCenterWeb.Models.User;

namespace TransferCenterWeb.Translators;

public static class UserTranslator
{
    // Web model -> Core model
    public static TransferCenterCore.Models.User ToCoreModel(this WebUser source)
    {
        if (source == null)
        {
            return new TransferCenterCore.Models.User();
        }

        return new TransferCenterCore.Models.User
        {
            UserId = source.UserId,
            FirstName = source.FirstName,
            LastName = source.LastName,
            EmailId = source.EmailId,
            Password = source.Password,
            DomainID = source.DomainID,
            LoginId = source.LoginId,
            CreatedOn = source.CreatedOn,
            Role = source.Role,
            IsActive = source.IsActive,
            CreatedBy = CallContextScope.Current?.EmailId ?? String.Empty
        };
    }

    // Core model -> Web model
    public static WebUser ToWebModel(this TransferCenterCore.Models.User source)
    {
        if (source == null)
        {
            return new WebUser();
        }

        return new WebUser
        {
            UserId = source.UserId,
            FirstName = source.FirstName,
            LastName = source.LastName,
            EmailId = source.EmailId,
            Password = source.Password,
            DomainID = source.DomainID,
            LoginId = source.LoginId,
            CreatedOn = source.CreatedOn,
            Role = source.Role,
            IsActive = source.IsActive,
            CreatedBy = source.CreatedBy
            
        };
    }

    // Web collection -> Core collection
    public static IEnumerable<TransferCenterCore.Models.User> ToCoreModel(this IEnumerable<WebUser> sources)
    {
        return sources?.Select(s => s.ToCoreModel()) ?? Enumerable.Empty<TransferCenterCore.Models.User>();
    }

    public static List<TransferCenterCore.Models.User> ToCoreModel(this List<WebUser> sources)
    {
        if (sources == null) return new List<TransferCenterCore.Models.User>();
        return sources.Select(s => s.ToCoreModel()).ToList();
    }

    // Core collection -> Web collection
    public static IEnumerable<WebUser> ToWebModel(this IEnumerable<TransferCenterCore.Models.User> sources)
    {
        return sources?.Select(s => s.ToWebModel()) ?? Enumerable.Empty<WebUser>();
    }

    public static List<WebUser> ToWebModel(this List<TransferCenterCore.Models.User> sources)
    {
        if (sources == null) return new List<WebUser>();
        return sources.Select(s => s.ToWebModel()).ToList();
    }
}