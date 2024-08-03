namespace Long.Login.Managers
{
    public class LoginStatisticManager
    {
        public static int LoginCount;
        public static int SuccessLoginCount;
        public static int ErrorLoginCount;

        public static void IncreaseLogin()
        {
            Interlocked.Increment(ref LoginCount);
        }

        public static void IncreaseSuccessLogin()
        {
            Interlocked.Increment(ref SuccessLoginCount);
        }

        public static void IncreaseErrorLogin()
        {
            Interlocked.Increment(ref ErrorLoginCount);
        }

        public static void Reset()
        {
            Interlocked.Exchange(ref LoginCount, 0);
        }

        public static string ToTitleString()
        {
            return $"Login: {LoginCount}/5min, Suc: {SuccessLoginCount}/5min, Err: {ErrorLoginCount}/5min";
        }
    }
}
