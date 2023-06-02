using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace QuizCreator.Model
{
    public class Cryptography
    {
        public static void EncryptFile(string inputFile, string outputFile, string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("SomeSaltValue");
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                {
                    using (RijndaelManaged aes = new RijndaelManaged())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;

                        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);

                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (CryptoStream cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;

                            while ((bytesRead = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                cryptoStream.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }

        public static void EncryptFile(string inputFile, string password)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            EncryptFile(inputFile, tempFile, password);
            System.IO.File.Delete(inputFile);
            System.IO.File.Move(tempFile, inputFile);
        }

        public static void DecryptFile(string inputFile, string outputFile, string password)
        {
            byte[] salt = Encoding.UTF8.GetBytes("SomeSaltValue");
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            using (FileStream fsInput = new FileStream(inputFile, FileMode.Open))
            {
                using (FileStream fsOutput = new FileStream(outputFile, FileMode.Create))
                {
                    using (RijndaelManaged aes = new RijndaelManaged())
                    {
                        aes.KeySize = 256;
                        aes.BlockSize = 128;

                        Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(passwordBytes, salt, 1000);
                        aes.Key = key.GetBytes(aes.KeySize / 8);
                        aes.IV = key.GetBytes(aes.BlockSize / 8);

                        aes.Mode = CipherMode.CBC;
                        aes.Padding = PaddingMode.PKCS7;

                        using (CryptoStream cryptoStream = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;

                            while ((bytesRead = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                            {
                                fsOutput.Write(buffer, 0, bytesRead);
                            }
                        }
                    }
                }
            }
        }

        public static void DecryptFile(string inputFile, string password)
        {
            string tempFile = System.IO.Path.GetTempFileName();
            DecryptFile(inputFile, tempFile, password);
            System.IO.File.Delete(inputFile);
            System.IO.File.Move(tempFile, inputFile);
        }

    }
}
