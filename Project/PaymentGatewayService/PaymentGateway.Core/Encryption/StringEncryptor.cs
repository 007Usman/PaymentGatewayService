using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PaymentGatway.Core.Encryption
{
	/// <summary>
	/// This class will successfuly Encrypt and Decrypt while application is running
	/// Stopping the application will loose secret key used to encryption and decryption
	/// in-memory key generation is a temperory solution whilst testing locally
	/// </summary>
	public class StringEncryptor : IStringEncryptor
	{
		#region Properties

		private readonly ICryptoTransform _encryptor;
		private readonly ICryptoTransform _decryptor;

		#endregion

		#region Constructor 

		public StringEncryptor()
		{
			using var aes = new AesManaged();
			aes.GenerateKey();                      /// In production replace this with real key and double wrap with KMS or KeyVault 
			aes.GenerateIV();                       /// In production replace this with real key and double wrap with KMS or KeyVault 
			aes.KeySize = 256;
			aes.BlockSize = 128;
			aes.Mode = CipherMode.CBC;
			aes.Padding = PaddingMode.PKCS7;
			_encryptor = aes.CreateEncryptor();
			_decryptor = aes.CreateDecryptor();
		}

		#endregion


		/// <summary>
		/// Call to Encrypt data
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns></returns>
		public string Encrypt(string plainText)
		{

			plainText = $"{Guid.NewGuid().ToString("D")}@{plainText}";
			var saltedBytes = Encoding.UTF8.GetBytes(plainText);
			byte[] cipherBytes = null;

			using (MemoryStream memoryStream = new MemoryStream())
			{
				using CryptoStream cryptoStream = new CryptoStream(memoryStream, _encryptor, CryptoStreamMode.Write);

				cryptoStream.Write(saltedBytes, 0, saltedBytes.Length);
				cryptoStream.FlushFinalBlock();
				cipherBytes = memoryStream.ToArray();
			}
			return Convert.ToBase64String(cipherBytes);

		}

		/// <summary>
		/// Call to decrypt data
		/// </summary>
		/// <param name="cipherText"></param>
		/// <returns></returns>
		public string Decrypt(string cipherText)
		{
			var cipherBytes = Convert.FromBase64String(cipherText);
			var plainText = string.Empty;

			using (MemoryStream memoryStream = new MemoryStream(200))
			{
				using CryptoStream cryptoStream = new CryptoStream(memoryStream, _decryptor, CryptoStreamMode.Write);

				cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
				cryptoStream.FlushFinalBlock();

				plainText = Encoding.UTF8.GetString(memoryStream.ToArray());
			}

			var saltEndPos = plainText.IndexOf("@");
			return plainText.Substring(saltEndPos + 1);

		}
	}
}

