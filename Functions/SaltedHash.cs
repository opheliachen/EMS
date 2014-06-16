using System;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace EMSSystem.Functions
{
    public class SaltedHash
    {
        #region 加密字串
        private Byte[] Salt
        {
            get
            {
                return new UTF8Encoding(false).GetBytes(ConfigurationManager.AppSettings["Salt"]);
            }
        }

        private string UltraKey
        {
            get
            {
                return ConfigurationManager.AppSettings["UltraKey"];
            }
        }
        #endregion

        #region 金錀加密後傳回加密字串
       // public string EncryptDerivedKey(String SrcString, String Password, Byte[] Salt)
        protected string EncryptDerivedKey(String SrcString)
        {
            try
            {
                PasswordDeriveBytes pdb = new PasswordDeriveBytes(UltraKey, Salt);

                byte[] iv = new byte[] { 0xA0, 0x16, 0xBC, 0xF2, 0x08, 0x3C, 0x55, 0x68 };
                //byte[] key = pdb.CryptDeriveKey("RC2", "SHA1", 128, iv);
                byte[] key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, iv);

                // Encrypt the data.
                TripleDES encAlg = TripleDES.Create();
                encAlg.Key = key;
                encAlg.IV = new byte[] { 0x06, 0xA2, 0xCC, 0x53, 0x2B, 0x33, 0x28, 0x2F };


                MemoryStream encryptionStream = new MemoryStream();
                CryptoStream encrypt = new CryptoStream(encryptionStream, encAlg.CreateEncryptor(), CryptoStreamMode.Write);


                byte[] utfD1 = new System.Text.UTF8Encoding(false).GetBytes(SrcString);

                encrypt.Write(utfD1, 0, utfD1.Length);
                encrypt.FlushFinalBlock();
                encrypt.Close();
                byte[] edata1 = encryptionStream.ToArray();
                pdb.Reset();

                // 以Base-64編碼傳回
                return Convert.ToBase64String(edata1);

            }
            catch (Exception EX)
            {
                throw EX;
            }
        }
        #endregion

        #region 金錀解密後傳回字串
        //public string DecryptDerivedKey(String SrcString, String Password, Byte[] Salt)
        protected string DecryptDerivedKey(String SrcString)
        {
            try
            {
                Byte[] edata1 = Convert.FromBase64String(SrcString);


                PasswordDeriveBytes pdb = new PasswordDeriveBytes(UltraKey, Salt);

                byte[] iv = new byte[] { 0xA0, 0x16, 0xBC, 0xF2, 0x08, 0x3C, 0x55, 0x68 };
                //byte[] key = pdb.CryptDeriveKey("RC2", "SHA1", 128, iv);
                byte[] key = pdb.CryptDeriveKey("TripleDES", "SHA1", 192, iv);


                TripleDES decAlg = TripleDES.Create();
                decAlg.Key = key;
                decAlg.IV = new byte[] { 0x06, 0xA2, 0xCC, 0x53, 0x2B, 0x33, 0x28, 0x2F };

                MemoryStream decryptionStreamBacking = new MemoryStream();
                CryptoStream decrypt = new CryptoStream(decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
                decrypt.Write(edata1, 0, edata1.Length);
                decrypt.Flush();
                decrypt.Close();
                pdb.Reset();
                string data2 = new UTF8Encoding(false).GetString(decryptionStreamBacking.ToArray());

                return data2;
            }
            catch (Exception EX)
            {
                return SrcString;
                //throw EX;
            }
        }
        #endregion
    }
       
}