namespace TransferCenterWeb.Models;

public static class Constant
{
    public static class Session
    {
        public static string IsAuthenticated = "isAuthenticated";
        public static string UserId = "userId";
        public static string Email = "email";
        public static string Role = "role";
    }

    public static class Log
    {
        public static class Error
        {
            public static string InvalidLoginAttempt = "Invalid login attempt.";
        }
    }

    public static class ViewPath
    {
        public const string ModalActionResult = "~/Views/Shared/_ModalActionResult.cshtml";
    }

    public static class Status
    {
       public static class Message{
        public const string Created = "Successfully Created !!";
        public const string Updated = "Successfully Updated !!";
        public const string Deleted = "Successfully Deleted !!";
        }
 public static class Code {
        public const int Success = 200;
        public const int Error = 500;
        public const int NotFound = 404;
 }

           }
}