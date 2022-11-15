namespace In66.Helper;

/// <summary>
/// 安全助手
/// </summary>
public static class SecurityExtentions
{
    /// <summary>
    /// get MD5 hashed string
    /// </summary>
    /// <param name="sourceString">原字符串</param>
    /// <param name="isLower">加密后的字符串是否为小写</param>
    /// <returns>加密后字符串</returns>
    public static string MD5(this ISecurity _, string sourceString, bool isLower = false)
    {
        if (string.IsNullOrEmpty(sourceString))
        {
            return "";
        }
        return InfraHelper.Hash.GetHashedString(HashType.MD5, sourceString, isLower);
    }


    #region SecurityCommon
    public static byte[] ComputeSha256Hash(string rawData)
    {
        // Create a SHA256   
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // ComputeHash - returns byte array  
            return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        }
    }

    public static string ToHex(byte[] data)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < data.Length; i++)
        {
            sb.Append(data[i].ToString("x2", CultureInfo.InvariantCulture));
        }

        return sb.ToString();
    }
    #endregion

    #region Rsa
    /// <summary>
    /// RSA的加密函数  string
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="plaintext">明文</param>
    /// <returns></returns>
    public static string Encrypt(string publicKey, string plaintext)
    {
        return Encrypt(publicKey, Encoding.UTF8.GetBytes(plaintext));
    }
    /// <summary>
    /// RSA的加密函数  string
    /// </summary>
    /// <param name="publicKey">公钥</param>
    /// <param name="plainbytes">明文字节数组</param>
    /// <returns></returns>
    public static string Encrypt(string publicKey, byte[] plainbytes)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            var bufferSize = (rsa.KeySize / 8 - 11);
            byte[] buffer = new byte[bufferSize]; //待加密块
            using (MemoryStream msInput = new MemoryStream(plainbytes))
            {
                using (MemoryStream msOutput = new MemoryStream())
                {
                    int readLen;
                    while ((readLen = msInput.Read(buffer, 0, bufferSize)) > 0)
                    {
                        byte[] dataToEnc = new byte[readLen];
                        Array.Copy(buffer, 0, dataToEnc, 0, readLen);
                        byte[] encData = rsa.Encrypt(dataToEnc, false);
                        msOutput.Write(encData, 0, encData.Length);
                    }
                    byte[] result = msOutput.ToArray();
                    rsa.Clear();
                    return Convert.ToBase64String(result);
                }
            }
        }
    }
    /// <summary>
    /// RSA的解密函数  stirng
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="ciphertext">密文字符串</param>
    /// <returns></returns>
    public static string Decrypt(string privateKey, string ciphertext)
    {
        return Decrypt(privateKey, Convert.FromBase64String(ciphertext));
    }
    /// <summary>
    /// RSA的解密函数  byte
    /// </summary>
    /// <param name="privateKey">私钥</param>
    /// <param name="cipherbytes">密文字节数组</param>
    /// <returns></returns>
    public static string Decrypt(string privateKey, byte[] cipherbytes)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            int keySize = rsa.KeySize / 8;
            if (cipherbytes.Length <= keySize)
                return Encoding.UTF8.GetString(rsa.Decrypt(cipherbytes, false));
            byte[] buffer = new byte[keySize];
            using (MemoryStream msInput = new MemoryStream(cipherbytes))
            {
                using (MemoryStream msOutput = new MemoryStream())
                {
                    int readLen;
                    while ((readLen = msInput.Read(buffer, 0, keySize)) > 0)
                    {
                        byte[] dataToDec = new byte[readLen];
                        Array.Copy(buffer, 0, dataToDec, 0, readLen);
                        byte[] decData = rsa.Decrypt(dataToDec, false);
                        msOutput.Write(decData, 0, decData.Length);
                    }
                    byte[] result = msOutput.ToArray();
                    rsa.Clear();
                    return Encoding.UTF8.GetString(result);
                }
            }
        }
    }
    /// <summary>
    /// 生成签名
    /// </summary>
    /// <param name="privateKey"></param>
    /// <param name="encrypt"></param>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static string CreateSignature(string privateKey, string encrypt)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(privateKey);
            byte[] dataBytes = Encoding.UTF8.GetBytes(encrypt);
            byte[] signatureBytes = rsa.SignData(dataBytes, "SHA256");
            return Convert.ToBase64String(signatureBytes);
        }
    }
    /// <summary>
    /// 校验签名
    /// </summary>
    /// <param name="publicKey"></param>
    /// <param name="encrypt"></param>
    /// <param name="signature"></param>
    /// <returns></returns>
    public static bool ValidateSignature(string publicKey, string encrypt, string signature)
    {
        using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
        {
            rsa.FromXmlString(publicKey);
            RSAPKCS1SignatureDeformatter rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
            //指定解密的时候HASH算法为SHA256
            rsaDeformatter.SetHashAlgorithm("SHA256");

            byte[] deformatterData = Convert.FromBase64String(signature);
            using (HashAlgorithm sha256 = HashAlgorithm.Create("SHA256"))
            {
                byte[] hashData = sha256.ComputeHash(Encoding.UTF8.GetBytes(encrypt));
                return rsaDeformatter.VerifySignature(hashData, deformatterData);
            }
        }
    }
    #endregion

    /// <summary>
    /// C#使用私钥加签
    /// </summary>
    /// <param name="data"></param>
    /// <param name="privateKeyCSharp"></param>
    /// <param name="hashAlgorithm"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static String RSASignCSharp(String data, String privateKeyCSharp, String hashAlgorithm = "SHA256", String encoding = "UTF-8")
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        rsa.FromXmlString(privateKeyCSharp); //加载私钥   
        var dataBytes = Encoding.GetEncoding(encoding).GetBytes(data);
        var HashbyteSignature = rsa.SignData(dataBytes, hashAlgorithm);
        return Convert.ToBase64String(HashbyteSignature);
    }
    /// <summary>
    /// C#使用公钥验签
    /// </summary>
    /// <param name="data"></param>
    /// <param name="publicKeyCSharp"></param>
    /// <param name="signature"></param>
    /// <param name="hashAlgorithm"></param>
    /// <param name="encoding"></param>
    /// <returns></returns>
    public static bool VerifyCSharp(String data, String publicKeyCSharp, String signature, String hashAlgorithm = "SHA256", String encoding = "UTF-8")
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        //导入公钥，准备验证签名
        rsa.FromXmlString(publicKeyCSharp);
        //返回数据验证结果
        Byte[] Data = Encoding.GetEncoding(encoding).GetBytes(data);
        Byte[] rgbSignature = Convert.FromBase64String(signature);

        return rsa.VerifyData(Data, hashAlgorithm, rgbSignature);
    }
}