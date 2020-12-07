using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUM.DAL.Security
{
    public class SecurityProvider
    {
        public static string Encrypt(string password)
        {
            string encryptedPassword = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            encryptedPassword = Convert.ToBase64String(encode);
            return encryptedPassword;
        }
        public static string Decrypt(string encryptedPassword)
        {
            string decryptedPassword = string.Empty;
            UTF8Encoding encodePassword = new UTF8Encoding();
            Decoder Decode = encodePassword.GetDecoder();
            byte[] encocodedByte = Convert.FromBase64String(encryptedPassword);
            int charCount = Decode.GetCharCount(encocodedByte, 0, encocodedByte.Length);
            char[] decodedChar = new char[charCount];
            Decode.GetChars(encocodedByte, 0, encocodedByte.Length, decodedChar, 0);
            decryptedPassword = new String(decodedChar);
            return decryptedPassword;
        }
    }
}
