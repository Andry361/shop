using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Services.Logic.Extensions
{
  public static class StringExtensions
  {
    public static bool IsValidEmail(this string email)
    {
      if (string.IsNullOrWhiteSpace(email)) return false;

      return Regex.Match(email, "^([\\w-]+(?:\\.[\\w-]+)*)@((?:[\\w-]+\\.)*\\w[\\w-]{0,66})\\.([a-z]{2,6}(?:\\.[a-z]{2})?)$").Success;
    }

    /// <summary>
    /// Делает первую букву заглавной
    /// </summary>
    public static string Capitalize(this string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return "";

      return value.First().ToString().ToUpper() + (value.Length > 1 ? value.Substring(1) : "");
    }

    /// <summary>
    /// Делает первую букву маленькой
    /// </summary>
    public static string Decapitalize(this string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return "";

      return value.First().ToString().ToLower() + (value.Length > 1 ? value.Substring(1) : "");
    }
    public static string GetHashString(this string s)
    {
      // Create a new instance of the MD5CryptoServiceProvider object.
      MD5 md5Hasher = MD5.Create();

      // Convert the input string to a byte array and compute the hash.
      byte[] data = md5Hasher.ComputeHash(Encoding.Default.GetBytes(s));

      // Create a new Stringbuilder to collect the bytes
      // and create a string.
      StringBuilder sBuilder = new StringBuilder();

      // Loop through each byte of the hashed data 
      // and format each one as a hexadecimal string.
      for (int i = 0; i < data.Length; i++)
      {
        sBuilder.Append(data[i].ToString("x2"));
      }

      // Return the hexadecimal string.
      return sBuilder.ToString();
    }

    public static string Localize(this string s)
    {
      return Strings.GetString(s);
    }

    public static string ToDigitsOnly(this string input, IEnumerable<char> allowAdditionalChars = null)
    {
      //L.Debug("");
      //L.Debug("Enter method: input={0}", input);
      try
      {
        if (input == null)
        {
          //L.Error("input is null");
          throw new ArgumentNullException("input");
        }
        return new string(input.Where(x => char.IsDigit(x)
                                            || allowAdditionalChars != null && allowAdditionalChars.Contains(x)).ToArray());
      }
      catch (Exception ex)
      {
        //L.Exception(ex, "General exception");
        throw;
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }

    public static string ReplaceSpacesToSingle(this string input)
    {
      //L.Debug("");
      //L.Debug("Enter method: input={0}", input);
      try
      {
        if (input == null) return null;

        input = Regex.Replace(input, @"\s+", " ").Trim();

        return input;
      }
      catch (Exception ex)
      {
        //L.Exception(ex, "General exception");
        throw;
      }
      finally
      {
        //L.Debug("Leave method");
        //L.Debug("");
      }
    }

    /// <param name="stringToReplaceSpecialChars">Строка, на которую будут заменены специальные символы</param>
    public static string RemoveSpecialChars(this string input,
                                            bool replaceDoubleSpaces = true,
                                            bool trim = true,
                                            string stringToReplaceSpecialChars = null,
                                            bool removeSpaces = false)
    {
      if (string.IsNullOrWhiteSpace(input)) return "";
      if (stringToReplaceSpecialChars == null) stringToReplaceSpecialChars = "";

      var specialCharsList = new List<string> { ".", ",", ";", ":", "'", "!", "&", "?","#","@","/",
                           "\\","\"","|","{","}","(",")","}","%","^","*","-","_",
                           "+","=","[","]","~","`","<",">","$"};
      if (removeSpaces)
      {
        specialCharsList.Add(" ");
      }

      specialCharsList.ForEach(x => input = input.Replace(x, stringToReplaceSpecialChars));

      if (replaceDoubleSpaces)
      {
        input = Regex.Replace(input, "\\s{1,}", " ");
      }
      if (trim)
      {
        input = input.Trim();
      }

      return input;
    }

    /// <summary>
    /// Заменяет 'ё' на 'е', 'й' на 'и' с учётом регистра
    /// </summary>
    public static string NormalizeYoYeI(this string input)
    {
      //L.Debug("Enter method:");
      try
      {
        if (input == null)
        {
          //L.Error("input is null");
          throw new ArgumentNullException("input");
        }

        return input.Replace('ё', 'е').Replace('Ё', 'Е')
                    .Replace('й', 'и').Replace('Й', 'И');
      }
      catch (Exception ex)
      {
        //L.Exception(ex, "General exception");
        throw;
      }
      finally
      {
        //L.Debug("Leave method");
      }
    }

    /// <summary>
    /// Конвертирует строку из формата %u0420%u0430%u0441%u0448%u0438%u0440%u0435%u043d%u043d%u0430%u044f
    /// в обычный
    /// </summary>
    /// <param name="input">Строка в формате %u0420%u0430%u0441%u0448%u0438%u0440%u0435%u043d%u043d%u0430%u044f</param>
    /// <returns></returns>
    public static string FromPercentUEscaped(this string input)
    {
      var output = new StringBuilder();
      var accum = "";
      var charNumber = 0;

      for (int i = 0; i < input.Count(); i++)
      {
        var c = input[i];
        accum += input[i].ToString();

        if (charNumber == 0 && c == '%')
        {
          charNumber++;
        }
        else if (charNumber == 1 && c.ToString().ToLower() == "u")
        {
          charNumber++;
        }
        else if (charNumber >= 2 && charNumber <= 5
                      && (Char.IsDigit(c) || "abcdefABCDEF".Any(x => x == c)))
        {
          charNumber++;
          if (charNumber == 6)
          {
            var code = accum.Replace("%", "").Replace("U", "").Replace("u", "");
            accum = Char.ConvertFromUtf32(int.Parse(code, System.Globalization.NumberStyles.HexNumber));
            charNumber = 0;
            output.Append(accum);
            accum = "";
          }
        }
        else
        {
          charNumber = 0;
          output.Append(accum);
          accum = "";
        }
      }

      return output.ToString();
    }
    public static decimal? ToDecimalCultureIndependent(this string value)
    {
      if (string.IsNullOrWhiteSpace(value)) return null;

      decimal d;
      if (!decimal.TryParse(value.Replace(',', '.'), NumberStyles.Any, new CultureInfo("en-US"), out d)) return null;

      return d;
    }

    public static DateTime? ToDateTimeCultureIndependent(this string value, DateTime? @default = null)
    {
      if (string.IsNullOrWhiteSpace(value)) return @default;

      var cultureInfo = new System.Globalization.CultureInfo("en-US");
      if (value.Contains("."))
      {
        cultureInfo = new System.Globalization.CultureInfo("ru-RU");
      }

      DateTime dt;

      if (value.All(x => Char.IsDigit(x)))
      {
        dt = new DateTime(1970, 01, 01).AddMilliseconds(long.Parse(value));
      }
      else if (!DateTime.TryParse(value, cultureInfo, DateTimeStyles.None, out dt))
      {
        return @default;
      }

      return dt;
    }

    public static string Md5(this string input)
    {
      byte[] hash = Encoding.ASCII.GetBytes(input);
      var md5 = new MD5CryptoServiceProvider();
      byte[] hashenc = md5.ComputeHash(hash);
      string result = "";
      foreach (var b in hashenc)
      {
        result += b.ToString("x2");
      }

      return result;
    }

    public static string EncryptAES(this string input, string privateKey)
    {
      if (string.IsNullOrEmpty(privateKey))
        throw new ArgumentNullException("privateKey");

      string outStr = null;                       // Encrypted string to return
      RijndaelManaged aesAlg = null;              // RijndaelManaged object used to encrypt the data.

      try
      {
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(privateKey, GetSalt());

        aesAlg = new RijndaelManaged();
        aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);

        ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using (MemoryStream msEncrypt = new MemoryStream())
        {
          msEncrypt.Write(BitConverter.GetBytes(aesAlg.IV.Length), 0, sizeof(int));
          msEncrypt.Write(aesAlg.IV, 0, aesAlg.IV.Length);
          using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
          {
            using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
            {
              swEncrypt.Write(input);
            }
          }
          outStr = Convert.ToBase64String(msEncrypt.ToArray());
        }
      }
      finally
      {
        if (aesAlg != null)
          aesAlg.Clear();
      }

      return outStr;
    }

    public static string DecryptAES(this string cipher, string privateKey)
    {
      if (string.IsNullOrEmpty(cipher))
        throw new ArgumentNullException("cipher");
      if (string.IsNullOrEmpty(privateKey))
        throw new ArgumentNullException("privateKey");

      // Declare the RijndaelManaged object
      // used to decrypt the data.
      RijndaelManaged aesAlg = null;

      // Declare the string used to hold
      // the decrypted text.
      string plaintext = null;

      try
      {
        // generate the key from the shared secret and the salt
        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(privateKey, GetSalt());

        // Create the streams used for decryption.                
        byte[] bytes = Convert.FromBase64String(cipher);
        using (MemoryStream msDecrypt = new MemoryStream(bytes))
        {
          // Create a RijndaelManaged object
          // with the specified key and IV.
          aesAlg = new RijndaelManaged();
          aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
          // Get the initialization vector from the encrypted stream
          aesAlg.IV = ReadByteArray(msDecrypt);
          // Create a decrytor to perform the stream transform.
          ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
          using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
          {
            using (StreamReader srDecrypt = new StreamReader(csDecrypt))

              // Read the decrypted bytes from the decrypting stream
              // and place them in a string.
              plaintext = srDecrypt.ReadToEnd();
          }
        }
      }
      finally
      {
        // Clear the RijndaelManaged object.
        if (aesAlg != null)
          aesAlg.Clear();
      }

      return plaintext;
    }

    private static byte[] GetSalt()
    {
      return Encoding.ASCII.GetBytes("ccl88f5ydp6dzgu5");
    }

    private static byte[] ReadByteArray(Stream s)
    {
      byte[] rawLength = new byte[sizeof(int)];
      if (s.Read(rawLength, 0, rawLength.Length) != rawLength.Length)
      {
        throw new SystemException("Stream did not contain properly formatted byte array");
      }

      byte[] buffer = new byte[BitConverter.ToInt32(rawLength, 0)];
      if (s.Read(buffer, 0, buffer.Length) != buffer.Length)
      {
        throw new SystemException("Did not read byte array properly");
      }

      return buffer;
    }
  }
}
