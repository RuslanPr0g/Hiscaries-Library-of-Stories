using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using HC.Application.Encryption;
using HC.Application.Options;
using Microsoft.Extensions.Options;

namespace HC.Infrastructure.Encryption;

public class Encryptor : IEncryptor
{
    private readonly EncryptionOptions _encryptionConfig;
    private readonly SecureString _password;

    public Encryptor(IOptions<EncryptionOptions> config)
    {
        _encryptionConfig = config.Value;
        _password = GetPassword();
    }

    public string Encrypt(string text)
    {
        return Encrypt(text, GetDefaultSalt());
    }

    public string Decrypt(string encryptedText)
    {
        return Decrypt(encryptedText, GetDefaultSalt());
    }

    public string Encrypt(string text, string salt)
    {
        if (text == null) return null;

        RijndaelManaged rijndaelCipher;
        byte[] textData;
        ICryptoTransform encryptor;

        using (rijndaelCipher = new RijndaelManaged())
        {
            PasswordDeriveBytes secretKey = GetSecretKey(salt);

            // First we need to turn the input strings into a byte array.
            textData = Encoding.Unicode.GetBytes(text);

            // Create a encryptor from the existing secretKey bytes.
            // We use 32 bytes for the secret key. The default Rijndael 
            // key length is 256 bit (32 bytes) and then 16 bytes for the 
            // Initialization Vector (IV). The default Rijndael IV length is 
            // 128 bit (16 bytes).
            encryptor = rijndaelCipher.CreateEncryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
        }

        MemoryStream memoryStream;
        byte[] encryptedData;

        // Create a MemoryStream that is going to hold the encrypted bytes:
        using (memoryStream = new MemoryStream())
        {
            // Create a CryptoStream through which we are going to be processing 
            // our data. CryptoStreamMode.Write means that we are going to be 
            // writing data to the stream and the output will be written in the 
            // MemoryStream we have provided.
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
            {
                // Start the encryption process.
                cryptoStream.Write(textData, 0, textData.Length);

                // Finish encrypting.
                cryptoStream.FlushFinalBlock();

                // Convert our encrypted data from a memoryStream into a byte array.
                encryptedData = memoryStream.ToArray();

                // Close both streams.
                memoryStream.Close();
                cryptoStream.Close();
            }
        }

        // Convert encrypted data into a base64-encoded string.
        // A common mistake would be to use an Encoding class for that.
        // It does not work, because not all byte values can be
        // represented by characters. We are going to be using Base64 encoding.
        // That is designed exactly for what we are trying to do.
        string encryptedText = Convert.ToBase64String(encryptedData);

        // Return encrypted string.
        return encryptedText;
    }

    public string Decrypt(string encryptedText, string salt)
    {
        if (encryptedText is null) return null;

        RijndaelManaged rijndaelCipher;
        byte[] encryptedData;
        ICryptoTransform decryptor;

        using (rijndaelCipher = new RijndaelManaged())
        {
            PasswordDeriveBytes secretKey = GetSecretKey(salt);

            // First we need to turn the input strings into a byte array.
            encryptedData = Convert.FromBase64String(encryptedText);

            // Create a decryptor from the existing SecretKey bytes.
            decryptor = rijndaelCipher.CreateDecryptor(secretKey.GetBytes(32), secretKey.GetBytes(16));
        }

        MemoryStream memoryStream;
        byte[] unencryptedData;
        int decryptedDataLength;

        using (memoryStream = new MemoryStream(encryptedData))
        {
            // Create a CryptoStream. Always use Read mode for decryption.
            using (CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
            {
                // Since at this point we don't know what the size of decrypted data
                // will be, allocate the buffer long enough to hold EncryptedData;
                // DecryptedData is never longer than EncryptedData.
                unencryptedData = new byte[encryptedData.Length];

                // Start decrypting.
                try
                {
                    decryptedDataLength = cryptoStream.Read(unencryptedData, 0, unencryptedData.Length);
                }
                catch
                {
                    throw new CryptographicException("Unable to decrypt string");
                }

                cryptoStream.Close();
                memoryStream.Close();
            }
        }

        // Convert decrypted data into a string.
        string decryptedText = Encoding.Unicode.GetString(unencryptedData, 0, decryptedDataLength);

        // Return decrypted string.  
        return decryptedText;
    }

    private PasswordDeriveBytes GetSecretKey(string salt)
    {
        // We are using salt to make it harder to guess our key
        // using a dictionary attack.
        byte[] encodedSalt = Encoding.ASCII.GetBytes(salt);

        IntPtr valuePointer = IntPtr.Zero;
        try
        {
            // The Secret Key will be generated from the specified
            // password and salt.
            valuePointer = Marshal.SecureStringToGlobalAllocUnicode(_password);
            return new PasswordDeriveBytes(Marshal.PtrToStringUni(valuePointer), encodedSalt);
        }
        finally
        {
            Marshal.ZeroFreeGlobalAllocUnicode(valuePointer);
        }
    }

    private string GetDefaultSalt()
    {
        return _password.Length.ToString(CultureInfo.InvariantCulture);
    }

    private SecureString GetPassword()
    {
        SecureString password = new SecureString();

        foreach (char c in _encryptionConfig.Secret)
            password.AppendChar(c);

        //password.AppendChar((char)80);
        //password.AppendChar((char)97);
        //password.AppendChar((char)115);
        //password.AppendChar((char)115);
        //password.AppendChar((char)119);
        //password.AppendChar((char)111);
        //password.AppendChar((char)114);
        //password.AppendChar((char)100);
        //password.AppendChar((char)49);

        return password;
    }
}