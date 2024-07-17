namespace HC.Application.Encryption;

public interface IEncryptor
{
    string Encrypt(string text);
    string Decrypt(string encryptedText);
}