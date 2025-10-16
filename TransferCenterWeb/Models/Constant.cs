namespace TransferCenterWeb.Models
{
    public static class Constant
    {
        public static class Session
        {
            public static string IsAuthenticated = "isAuthenticated";
            public static string UserId = "userId";
            public static string Email = "email";
        }

        public static class Log
        {
            public static class Error
            {
                public static string InvalidLoginAttempt = "Invalid login attempt.";
            }
        }        
    }
}
