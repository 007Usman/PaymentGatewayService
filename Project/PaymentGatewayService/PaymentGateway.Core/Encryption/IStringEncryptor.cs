namespace PaymentGatway.Core.Encryption
{
	public interface IStringEncryptor
	{
		string Decrypt(string cipherText);
		string Encrypt(string plainText);
	}
}