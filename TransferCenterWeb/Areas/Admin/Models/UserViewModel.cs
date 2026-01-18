using TransferCenterWeb.Models;

namespace TransferCenterWeb.Areas.Admin.Models;

public class UserViewModel
{
    public int TotalCount {set;get;}
    public required List<User> Users {set;get;}
}