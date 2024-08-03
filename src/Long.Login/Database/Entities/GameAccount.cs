using Org.BouncyCastle.Utilities.Encoders;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Long.Login.Database.Entities
{
    [Table("conquer_account")]
    public class GameAccount
    {
        [Key]
        public virtual int Id { get; set; }
        public virtual string UserName { get; set; }
        public virtual string Password { get; set; }
        public virtual string Salt { get; set; }
        public virtual int AuthorityId { get; set; }
        public virtual int Flag { get; set; }
        public virtual string IpAddress { get; set; }
        public virtual string MacAddress { get; set; }
        public virtual Guid ParentId { get; set; }
        public virtual DateTime Created { get; set; }
        public virtual DateTime? Modified { get; set; }
        public virtual DateTime? Deleted { get; set; }

        public virtual GameAccountAuthority Authority { get; set; }

        /// <summary>
        ///     Validates the user's inputted password, which has been decrypted from the client
        ///     request decode method, and is ready to be hashed and compared with the SHA-1
        ///     hash in the database.
        /// </summary>
        /// <param name="input">Inputted password from the client's request</param>
        /// <param name="hash">Hashed password in the database</param>
        /// <param name="salt">Salt for the hashed password in the database.</param>
        /// <returns>Returns true if the password is correct.</returns>
        public static bool CheckPassword(string input, string hash, string salt)
        {
            return HashPassword(input, salt).Equals(hash);
        }

        public static string HashPassword(string password, string salt)
        {
            byte[] inputHashed = SHA256.HashData(Encoding.ASCII.GetBytes(password + salt));
            string final = Hex.ToHexString(inputHashed);
            return final;
        }

        public static string GenerateSalt()
        {
            const string upperS = "QWERTYUIOPASDFGHJKLZXCVBNM";
            const string lowerS = "qwertyuioplkjhgfdsazxcvbnm";
            const string numberS = "1236547890";
            const string poolS = upperS + lowerS + numberS;
            const int sizeI = 36;

            var output = new StringBuilder();
            for (var i = 0; i < sizeI; i++)
                output.Append(poolS[RandomNumberGenerator.GetInt32(int.MaxValue) % poolS.Length]);
            return output.ToString();
        }
    }
}
