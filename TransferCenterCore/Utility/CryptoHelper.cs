using System;
using System.Security.Cryptography;
using System.Text;

namespace TransferCenterCore.Utility;

public static class CryptoHelper
{
    // Encrypt using AES-GCM (AEAD). Result: base64 of [nonce|ciphertext|tag]
    public static string Encrypt(string plaintext, byte[] key)
    {
        if (plaintext == null) throw new ArgumentNullException(nameof(plaintext));
        if (key == null) throw new ArgumentNullException(nameof(key));

        byte[] nonce = RandomNumberGenerator.GetBytes(12); // 96-bit nonce for GCM
        byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
        byte[] cipherBytes = new byte[plainBytes.Length];
        byte[] tag = new byte[16]; // 128-bit tag

    using (var aesGcm = new AesGcm(key, tag.Length))
        {
            aesGcm.Encrypt(nonce, plainBytes, cipherBytes, tag);
        }

        byte[] combined = new byte[nonce.Length + cipherBytes.Length + tag.Length];
        Buffer.BlockCopy(nonce, 0, combined, 0, nonce.Length);
        Buffer.BlockCopy(cipherBytes, 0, combined, nonce.Length, cipherBytes.Length);
        Buffer.BlockCopy(tag, 0, combined, nonce.Length + cipherBytes.Length, tag.Length);
        return Convert.ToBase64String(combined);
    }

    public static string Decrypt(string base64Cipher, byte[] key)
    {
        if (string.IsNullOrWhiteSpace(base64Cipher)) throw new ArgumentNullException(nameof(base64Cipher));
        if (key == null) throw new ArgumentNullException(nameof(key));

        byte[] combined = Convert.FromBase64String(base64Cipher);
        if (combined.Length < 12 + 16)
            throw new ArgumentException("Cipher text is invalid.", nameof(base64Cipher));

        byte[] nonce = new byte[12];
        byte[] tag = new byte[16];
        int ctLen = combined.Length - nonce.Length - tag.Length;
        byte[] cipherBytes = new byte[ctLen];

        Buffer.BlockCopy(combined, 0, nonce, 0, nonce.Length);
        Buffer.BlockCopy(combined, nonce.Length, cipherBytes, 0, ctLen);
        Buffer.BlockCopy(combined, nonce.Length + ctLen, tag, 0, tag.Length);

        byte[] plainBytes = new byte[ctLen];
    using (var aesGcm = new AesGcm(key, tag.Length))
        {
            aesGcm.Decrypt(nonce, cipherBytes, tag, plainBytes);
        }
        return Encoding.UTF8.GetString(plainBytes);
    }

    // Accept key as base64 or any string and normalize to 32-byte key via SHA256 if needed.
    public static byte[] NormalizeKey(string keyOrBase64)
    {
        if (string.IsNullOrWhiteSpace(keyOrBase64))
            throw new InvalidOperationException("Encryption key not configured.");

        try
        {
            var raw = Convert.FromBase64String(keyOrBase64);
            if (raw.Length == 16 || raw.Length == 24 || raw.Length == 32) return raw;
            // If decoded but unexpected size, hash to 32 bytes
            return SHA256.HashData(raw);
        }
        catch
        {
            // Not base64, derive 32-byte key from string
            return SHA256.HashData(Encoding.UTF8.GetBytes(keyOrBase64));
        }
    }

    // Deterministic HMAC-SHA256 digest for password matching (not reversible)
    public static string ComputeDeterministicDigest(string plaintext, byte[] key)
    {
        if (plaintext == null) throw new ArgumentNullException(nameof(plaintext));
        using var hmac = new HMACSHA256(key);
        var bytes = Encoding.UTF8.GetBytes(plaintext);
        var hash = hmac.ComputeHash(bytes);
        // Use Base64 for compactness; length is 44 chars with padding
        return Convert.ToBase64String(hash);
    }

    public static bool VerifyDeterministicDigest(string plaintext, string storedDigest, byte[] key)
    {
        if (string.IsNullOrEmpty(storedDigest)) return false;
        var computed = ComputeDeterministicDigest(plaintext, key);
        // constant time comparison
        return CryptographicOperations.FixedTimeEquals(Convert.FromBase64String(computed), Convert.FromBase64String(storedDigest));
    }
}
