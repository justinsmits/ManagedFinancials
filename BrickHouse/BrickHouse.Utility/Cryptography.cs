using System;
using System.Security.Cryptography;
using System.Text;

namespace BrickHouse.Utility
{
	public static class Cryptography
	{
		public static byte[] CreateSaltBytes()
		{
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] buff = new byte[10];
			rng.GetBytes(buff);
			return buff;
		}

		public static string ComputeHash(string plainText, HashAlgorithm hashAlgorithm, byte[] saltBytes)
		{
			// Convert plain text into a byte array.
			byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

			// Allocate array, which will hold plain text and salt.
			byte[] plainTextWithSaltBytes =
							new byte[plainTextBytes.Length + saltBytes.Length];

			// Copy plain text bytes into resulting array.
			for (int i = 0; i < plainTextBytes.Length; i++)
				plainTextWithSaltBytes[i] = plainTextBytes[i];

			// Append salt bytes to the resulting array.
			for (int i = 0; i < saltBytes.Length; i++)
				plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

			// Because we support multiple hashing algorithms, we must define
			// hash object as a common (abstract) base class. We will specify the
			// actual hashing algorithm class later during object creation.
			System.Security.Cryptography.HashAlgorithm hash;

			// Initialize appropriate hashing algorithm class.
			switch (hashAlgorithm)
			{
				case HashAlgorithm.SHA1:
					hash = new SHA1Managed();
					break;

				case HashAlgorithm.SHA256:
					hash = new SHA256Managed();
					break;

				case HashAlgorithm.SHA384:
					hash = new SHA384Managed();
					break;

				case HashAlgorithm.SHA512:
					hash = new SHA512Managed();
					break;

				default:
					hash = new MD5CryptoServiceProvider();
					break;
			}

			// Compute hash value of our plain text with appended salt.
			byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

			// Create array which will hold hash and original salt bytes.
			byte[] hashWithSaltBytes = new byte[hashBytes.Length + saltBytes.Length];

			// Copy hash bytes into resulting array.
			for (int i = 0; i < hashBytes.Length; i++)
				hashWithSaltBytes[i] = hashBytes[i];

			// Append salt bytes to the result.
			for (int i = 0; i < saltBytes.Length; i++)
				hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

			// Convert result into a base64-encoded string.
			string hashValue = Convert.ToBase64String(hashWithSaltBytes);

			// Return the result.
			return hashValue;
		}

		public enum HashAlgorithm
		{
			SHA1 = 0,
			SHA256 = 1,
			SHA384 = 2,
			SHA512 = 3,
			MD5Hash = 4
		}
	}
}
