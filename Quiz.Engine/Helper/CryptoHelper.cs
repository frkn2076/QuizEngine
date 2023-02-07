using System.Security.Cryptography;
using System.Text;

namespace Quiz.Engine.Helper;

public static class CryptoHelper
{
    public static string EncryptPassword(string password)
    {
        var md5 = MD5.Create();
        md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));
        var result = md5.Hash.ToList();

        var strBuilder = new StringBuilder();
        result.ForEach(x => strBuilder.Append(x.ToString("x2")));
        return strBuilder.ToString();
    }
}
