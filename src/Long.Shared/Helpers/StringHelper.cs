using System.Security.Cryptography;
using System.Text;

namespace Long.Shared.Helpers
{
    public static class StringHelper
    {
        public static string RandomString(int length)
        {
            const string upperS = "QWERTYUIOPASDFGHJKLZXCVBNM";
            const string lowerS = "qwertyuioplkjhgfdsazxcvbnm";
            const string numberS = "1236547890";
            const string poolS = upperS + lowerS + numberS;
            var output = new StringBuilder();
            for (var i = 0; i < length; i++)
                output.Append(poolS[RandomNumberGenerator.GetInt32(int.MaxValue) % poolS.Length]);
            return output.ToString();
        }
    }
}
