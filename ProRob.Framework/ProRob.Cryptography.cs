﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using ProRob.Extensions;
using ProRob.Conversion;

//https://docs.microsoft.com/it-it/dotnet/api/system.security.cryptography.rsacryptoserviceprovider?view=netframework-4.8

namespace ProRob.Security
{
    public interface ICryptor
    {
        // TODO: Get / Set Key (diversi tipi di strutture dati)

        List<byte[]> Encrypt(in List<byte[]> dataToEncrypt);
        List<byte[]> Decrypt(in List<byte[]> dataToDecrypt);
        byte[] Encrypt(in byte[] dataToEncrypt);
        byte[] Decrypt(in byte[] dataToDecrypt);
    }

    public static class Cryptografy
    {
        public struct AESKeys
        {
            public byte[] key;
            public byte[] iv;

            public AESKeys(byte[] key, byte[] iv)
            {
                this.key = key;
                this.iv = iv;
            }
        }

        public class AES : ICryptor
        {
            public enum AESKeySize : int { KeySize128 = 128, KeySize256 = 256 }
            public bool Enabled { get; internal set; }

            private AesManaged aes;
            private const CipherMode Mode = CipherMode.CBC;
            private ICryptoTransform encryptor;
            private ICryptoTransform decryptor;

            public AES(AESKeys aesKeys)
            : this(aesKeys.key, aesKeys.iv)
            {

            }

            public AES(byte[] key, byte[] iv)
            {
                aes = new AesManaged();
                aes.Mode = Mode;
                encryptor = aes.CreateEncryptor(key, iv);
                decryptor = aes.CreateDecryptor(key, iv);
                Enabled = true;
                //Console.WriteLine("AES->Constructor [DONE]");
            }

            public List<byte[]> Encrypt(in List<byte[]> dataToEncrypt)
            {
                var dataEncrypted = new List<byte[]>();
                foreach (var data in dataToEncrypt)
                {
                    dataEncrypted.Add(Encrypt(data));
                }
                return dataEncrypted;
            }

            public List<byte[]> Decrypt(in List<byte[]> dataToDecrypt)
            {
                var dataDecrypted = new List<byte[]>();
                foreach (var data in dataToDecrypt)
                {
                    dataDecrypted.Add(Decrypt(data));
                }
                return dataDecrypted;
            }

            public byte[] Encrypt(in byte[] dataToEncrypt)
            {
                byte[] dataCrypted;

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(dataToEncrypt, 0, dataToEncrypt.Length);
                    }

                    dataCrypted = ms.ToArray();
                }

                return dataCrypted;
            }

            public byte[] Decrypt(in byte[] dataToDecrypt)
            {
                byte[] dataDecrypted = new byte[dataToDecrypt.Count()];
                int nBytes = 0;
                using (MemoryStream ms = new MemoryStream(dataToDecrypt))
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        nBytes = cs.Read(dataDecrypted, 0, dataDecrypted.Length);
                    }
                }

                return dataDecrypted.Take(nBytes).ToArray();
            }


            public static (byte[], byte[]) CreateKeys(AESKeySize KeySize = AESKeySize.KeySize256)
            {
                AesManaged genKey;

                genKey = new AesManaged();
                genKey.Mode = CipherMode.CBC;
                genKey.KeySize = (int)KeySize;
                genKey.GenerateKey();
                genKey.GenerateIV();

                var key = genKey.Key;
                var iv = genKey.IV;

                AESKeys aesKeys = new AESKeys(key, iv);

                return (key, iv);
            }
        }

        public class RSA
        {
            //OAEP padding is only available on Microsoft Windows XP or later.  
            public bool DoOAEPPAdding = true;
            public int MaxNumberOfBytesToBeEncrypted { get; internal set; }
            public int KeySize { get; internal set; }

            private RSACryptoServiceProvider rsa;

            public RSA(int keySize, RSAParameters rsaParameters)
            {
                rsa = new RSACryptoServiceProvider();
                rsa.ImportParameters(rsaParameters);
                KeySize = keySize;
                MaxNumberOfBytesToBeEncrypted = (KeySize / 8) - 11;
            }


            public string Encrypt(string text)
            {

                var base64 = System.Convert.ToBase64String(Encrypt(UTF8Encoding.UTF8.GetBytes(text)));

                return base64;
            }


            public string Decrypt(string base64)
            {

                return UTF8Encoding.UTF8.GetString(Decrypt(System.Convert.FromBase64String(base64)));
            }

            public byte[] Encrypt(byte[] dataToEncrypt)
            {
                var dataIn = dataToEncrypt.ToList();
                var dataOut = new List<byte[]>();
                var N = MaxNumberOfBytesToBeEncrypted;
                var n = dataIn.Count();
                int i = 0;

                if (n > N)
                {
                    while (i * N < n)
                    {
                        var buffer = dataIn.Skip(i * N).Take(N).ToArray();
                        dataOut.Add(rsa.Encrypt(buffer, DoOAEPPAdding));
                        i++;
                    }
                }

                dataOut.Add(rsa.Encrypt(dataIn.Skip((i - 1) * N).Take(n - (i - 1) * N).ToArray(), DoOAEPPAdding));

                return dataOut.SelectMany(x => x).ToArray();
            }


            public byte[] Decrypt(byte[] dataBlockToDecrypt)
            {
                var dataIn = dataBlockToDecrypt.ToList();
                var dataOut = new List<byte[]>();

                var N = KeySize / 8;
                var n = dataIn.Count();
                int i = 0;

                if (n > N)
                {
                    while (i * N < n)
                    {
                        var buffer = dataIn.Skip(i * N).Take(N).ToArray();
                        dataOut.Add(rsa.Decrypt(buffer, DoOAEPPAdding));
                        i++;
                    }
                }

                dataOut.Add(rsa.Decrypt(dataIn.Skip((i - 1) * N).Take(n - (i - 1) * N).ToArray(), DoOAEPPAdding));

                return dataOut.SelectMany(x => x).ToArray();
            }

            public static void CreateKeys(int keySize, out RSAParameters publicKey, out RSAParameters privateKey)
            {
                RSACryptoServiceProvider rsaKeys = new RSACryptoServiceProvider(keySize);

                publicKey = rsaKeys.ExportParameters(false);
                privateKey = rsaKeys.ExportParameters(true);
            }
        }

    }
}


/* [OK]
         public byte[] Encrypt(byte[] dataToEncrypt)
         {
             var dataIn = dataToEncrypt.ToList();
             var dataOut = new List<byte[]>();

             var N = MaxNumberOfBytesToBeEncrypted;
             var n = dataIn.Count();
             int i = 0;

             if (n > N)
             {
                 while (i * N < n)
                 {
                     var buffer = dataIn.Skip(i * N).Take(N).ToArray();
                     dataOut.Add(rsaPrivateProvider.Encrypt(buffer, DoOAEPPAdding));
                     i++;
                 }
             }

             dataOut.Add(rsaPrivateProvider.Encrypt(dataIn.Skip((i-1) * N).Take(n - (i-1) * N).ToArray(),DoOAEPPAdding));

             return dataOut.SelectMany(x => x).ToArray();
         }


         public byte[] Decrypt(byte[] dataBlockToDecrypt)
         {
             var dataIn = dataBlockToDecrypt.ToList();
             var dataOut = new List<byte[]>();
             dataOut.Capacity = dataIn.Count / MaxNumberOfBytesToBeEncrypted * (KeySize / 8);

             var N = KeySize / 8;
             var n = dataIn.Count();
             int i = 0;

             if (n > N)
             {
                 while (i * N < n)
                 {
                     var buffer = dataIn.Skip(i * N).Take(N).ToArray();
                     dataOut.Add(rsaPublicProvider.Decrypt(buffer, DoOAEPPAdding));
                     i++;
                 }
             }

             dataOut.Add(rsaPublicProvider.Decrypt(dataIn.Skip((i-1) * N).Take(n - (i-1) * N).ToArray(), DoOAEPPAdding));

             return dataOut.SelectMany(x => x).ToArray();
         }
    */

/*public class PemKeyUtils
{
    //    const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
    //    const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
    //    const String pempubheader = "-----BEGIN PUBLIC KEY-----";
    //    const String pempubfooter = "-----END PUBLIC KEY-----";
    //    const String pemp8header = "-----BEGIN PRIVATE KEY-----";
    //    const String pemp8footer = "-----END PRIVATE KEY-----";
    //    const String pemp8encheader = "-----BEGIN ENCRYPTED PRIVATE KEY-----";
    //    const String pemp8encfooter = "-----END ENCRYPTED PRIVATE KEY-----";

    //    static bool verbose = false;

    //    public static RSACryptoServiceProvider GetRSAProviderFromPemFile(String pemfile)
    //    {
    //        bool isPrivateKeyFile = true;
    //        string pemstr = File.ReadAllText(pemfile).Trim();
    //        if (pemstr.StartsWith(pempubheader) && pemstr.EndsWith(pempubfooter))
    //            isPrivateKeyFile = false;

    //        byte[] pemkey;
    //        if (isPrivateKeyFile)
    //            pemkey = DecodeOpenSSLPrivateKey(pemstr);
    //        else
    //            pemkey = DecodeOpenSSLPublicKey(pemstr);

    //        if (pemkey == null)
    //            return null;

    //        if (isPrivateKeyFile)
    //            return DecodeRSAPrivateKey(pemkey);
    //        else
    //            return DecodeX509PublicKey(pemkey);

    //    }



    //    //--------   Get the binary RSA PUBLIC key   --------
    //    static byte[] DecodeOpenSSLPublicKey(String instr)
    //    {
    //        const String pempubheader = "-----BEGIN PUBLIC KEY-----";
    //        const String pempubfooter = "-----END PUBLIC KEY-----";
    //        String pemstr = instr.Trim();
    //        byte[] binkey;
    //        if (!pemstr.StartsWith(pempubheader) || !pemstr.EndsWith(pempubfooter))
    //            return null;
    //        StringBuilder sb = new StringBuilder(pemstr);
    //        sb.Replace(pempubheader, "");  //remove headers/footers, if present
    //        sb.Replace(pempubfooter, "");

    //        String pubstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

    //        try
    //        {
    //            binkey = Convert.FromBase64String(pubstr);
    //        }
    //        catch (System.FormatException)
    //        {       //if can't b64 decode, data is not valid
    //            return null;
    //        }
    //        return binkey;
    //    }

    //    static RSACryptoServiceProvider DecodeX509PublicKey(byte[] x509Key)
    //    {
    //        // encoded OID sequence for  PKCS #1 rsaEncryption szOID_RSA_RSA = "1.2.840.113549.1.1.1"
    //        byte[] seqOid = { 0x30, 0x0D, 0x06, 0x09, 0x2A, 0x86, 0x48, 0x86, 0xF7, 0x0D, 0x01, 0x01, 0x01, 0x05, 0x00 };
    //        // ---------  Set up stream to read the asn.1 encoded SubjectPublicKeyInfo blob  ------
    //        using (var mem = new MemoryStream(x509Key))
    //        {
    //            using (var binr = new BinaryReader(mem))    //wrap Memory Stream with BinaryReader for easy reading
    //            {
    //                try
    //                {
    //                    var twobytes = binr.ReadUInt16();
    //                    switch (twobytes)
    //                    {
    //                        case 0x8130:
    //                            binr.ReadByte();    //advance 1 byte
    //                            break;
    //                        case 0x8230:
    //                            binr.ReadInt16();   //advance 2 bytes
    //                            break;
    //                        default:
    //                            return null;
    //                    }

    //                    var seq = binr.ReadBytes(15);
    //                    if (!CompareBytearrays(seq, seqOid))  //make sure Sequence for OID is correct
    //                        return null;

    //                    twobytes = binr.ReadUInt16();
    //                    if (twobytes == 0x8103) //data read as little endian order (actual data order for Bit String is 03 81)
    //                        binr.ReadByte();    //advance 1 byte
    //                    else if (twobytes == 0x8203)
    //                        binr.ReadInt16();   //advance 2 bytes
    //                    else
    //                        return null;

    //                    var bt = binr.ReadByte();
    //                    if (bt != 0x00)     //expect null byte next
    //                        return null;

    //                    twobytes = binr.ReadUInt16();
    //                    if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
    //                        binr.ReadByte();    //advance 1 byte
    //                    else if (twobytes == 0x8230)
    //                        binr.ReadInt16();   //advance 2 bytes
    //                    else
    //                        return null;

    //                    twobytes = binr.ReadUInt16();
    //                    byte lowbyte = 0x00;
    //                    byte highbyte = 0x00;

    //                    if (twobytes == 0x8102) //data read as little endian order (actual data order for Integer is 02 81)
    //                        lowbyte = binr.ReadByte();  // read next bytes which is bytes in modulus
    //                    else if (twobytes == 0x8202)
    //                    {
    //                        highbyte = binr.ReadByte(); //advance 2 bytes
    //                        lowbyte = binr.ReadByte();
    //                    }
    //                    else
    //                        return null;
    //                    byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };   //reverse byte order since asn.1 key uses big endian order
    //                    int modsize = BitConverter.ToInt32(modint, 0);

    //                    byte firstbyte = binr.ReadByte();
    //                    binr.BaseStream.Seek(-1, SeekOrigin.Current);

    //                    if (firstbyte == 0x00)
    //                    {   //if first byte (highest order) of modulus is zero, don't include it
    //                        binr.ReadByte();    //skip this null byte
    //                        modsize -= 1;   //reduce modulus buffer size by 1
    //                    }

    //                    byte[] modulus = binr.ReadBytes(modsize); //read the modulus bytes

    //                    if (binr.ReadByte() != 0x02)            //expect an Integer for the exponent data
    //                        return null;
    //                    int expbytes = binr.ReadByte();        // should only need one byte for actual exponent data (for all useful values)
    //                    byte[] exponent = binr.ReadBytes(expbytes);

    //                    // We don't really need to print anything but if we insist to...
    //                    //showBytes("\nExponent", exponent);
    //                    //showBytes("\nModulus", modulus);

    //                    // ------- create RSACryptoServiceProvider instance and initialize with public key -----
    //                    RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
    //                    RSAParameters rsaKeyInfo = new RSAParameters
    //                    {
    //                        Modulus = modulus,
    //                        Exponent = exponent
    //                    };
    //                    rsa.ImportParameters(rsaKeyInfo);
    //                    return rsa;
    //                }
    //                catch (Exception)
    //                {
    //                    return null;
    //                }
    //            }
    //        }
    //    }

    //    //------- Parses binary ans.1 RSA private key; returns RSACryptoServiceProvider  ---
    //    static RSACryptoServiceProvider DecodeRSAPrivateKey(byte[] privkey)
    //    {
    //        byte[] MODULUS, E, D, P, Q, DP, DQ, IQ;

    //        // ---------  Set up stream to decode the asn.1 encoded RSA private key  ------
    //        MemoryStream mem = new MemoryStream(privkey);
    //        BinaryReader binr = new BinaryReader(mem);    //wrap Memory Stream with BinaryReader for easy reading
    //        byte bt = 0;
    //        ushort twobytes = 0;
    //        int elems = 0;
    //        try
    //        {
    //            twobytes = binr.ReadUInt16();
    //            if (twobytes == 0x8130) //data read as little endian order (actual data order for Sequence is 30 81)
    //                binr.ReadByte();    //advance 1 byte
    //            else if (twobytes == 0x8230)
    //                binr.ReadInt16();   //advance 2 bytes
    //            else
    //                return null;

    //            twobytes = binr.ReadUInt16();
    //            if (twobytes != 0x0102) //version number
    //                return null;
    //            bt = binr.ReadByte();
    //            if (bt != 0x00)
    //                return null;


    //            //------  all private key components are Integer sequences ----
    //            elems = GetIntegerSize(binr);
    //            MODULUS = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            E = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            D = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            P = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            Q = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            DP = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            DQ = binr.ReadBytes(elems);

    //            elems = GetIntegerSize(binr);
    //            IQ = binr.ReadBytes(elems);

    //            Console.WriteLine("showing components ..");
    //            if (verbose)
    //            {
    //                showBytes("\nModulus", MODULUS);
    //                showBytes("\nExponent", E);
    //                showBytes("\nD", D);
    //                showBytes("\nP", P);
    //                showBytes("\nQ", Q);
    //                showBytes("\nDP", DP);
    //                showBytes("\nDQ", DQ);
    //                showBytes("\nIQ", IQ);
    //            }

    //            // ------- create RSACryptoServiceProvider instance and initialize with public key -----
    //            RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
    //            RSAParameters RSAparams = new RSAParameters();
    //            RSAparams.Modulus = MODULUS;
    //            RSAparams.Exponent = E;
    //            RSAparams.D = D;
    //            RSAparams.P = P;
    //            RSAparams.Q = Q;
    //            RSAparams.DP = DP;
    //            RSAparams.DQ = DQ;
    //            RSAparams.InverseQ = IQ;
    //            RSA.ImportParameters(RSAparams);
    //            return RSA;
    //        }
    //        catch (Exception)
    //        {
    //            return null;
    //        }
    //        finally { binr.Close(); }
    //    }

    //    private static int GetIntegerSize(BinaryReader binr)
    //    {
    //        byte bt = 0;
    //        byte lowbyte = 0x00;
    //        byte highbyte = 0x00;
    //        int count = 0;
    //        bt = binr.ReadByte();
    //        if (bt != 0x02)     //expect integer
    //            return 0;
    //        bt = binr.ReadByte();

    //        if (bt == 0x81)
    //            count = binr.ReadByte();    // data size in next byte
    //        else
    //            if (bt == 0x82)
    //        {
    //            highbyte = binr.ReadByte(); // data size in next 2 bytes
    //            lowbyte = binr.ReadByte();
    //            byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
    //            count = BitConverter.ToInt32(modint, 0);
    //        }
    //        else
    //        {
    //            count = bt;     // we already have the data size
    //        }



    //        while (binr.ReadByte() == 0x00)
    //        {   //remove high order zeros in data
    //            count -= 1;
    //        }
    //        binr.BaseStream.Seek(-1, SeekOrigin.Current);     //last ReadByte wasn't a removed zero, so back up a byte
    //        return count;
    //    }

    //    //-----  Get the binary RSA PRIVATE key, decrypting if necessary ----
    //    static byte[] DecodeOpenSSLPrivateKey(String instr)
    //    {
    //        const String pemprivheader = "-----BEGIN RSA PRIVATE KEY-----";
    //        const String pemprivfooter = "-----END RSA PRIVATE KEY-----";
    //        String pemstr = instr.Trim();
    //        byte[] binkey;
    //        if (!pemstr.StartsWith(pemprivheader) || !pemstr.EndsWith(pemprivfooter))
    //            return null;

    //        StringBuilder sb = new StringBuilder(pemstr);
    //        sb.Replace(pemprivheader, "");  //remove headers/footers, if present
    //        sb.Replace(pemprivfooter, "");

    //        String pvkstr = sb.ToString().Trim();   //get string after removing leading/trailing whitespace

    //        try
    //        {        // if there are no PEM encryption info lines, this is an UNencrypted PEM private key
    //            binkey = Convert.FromBase64String(pvkstr);
    //            return binkey;
    //        }
    //        catch (System.FormatException)
    //        {       //if can't b64 decode, it must be an encrypted private key
    //                //Console.WriteLine("Not an unencrypted OpenSSL PEM private key");  
    //        }

    //        StringReader str = new StringReader(pvkstr);

    //        //-------- read PEM encryption info. lines and extract salt -----
    //        if (!str.ReadLine().StartsWith("Proc-Type: 4,ENCRYPTED"))
    //            return null;
    //        String saltline = str.ReadLine();
    //        if (!saltline.StartsWith("DEK-Info: DES-EDE3-CBC,"))
    //            return null;
    //        String saltstr = saltline.Substring(saltline.IndexOf(",") + 1).Trim();
    //        byte[] salt = new byte[saltstr.Length / 2];
    //        for (int i = 0; i < salt.Length; i++)
    //            salt[i] = Convert.ToByte(saltstr.Substring(i * 2, 2), 16);
    //        if (!(str.ReadLine() == ""))
    //            return null;

    //        //------ remaining b64 data is encrypted RSA key ----
    //        String encryptedstr = str.ReadToEnd();

    //        try
    //        {   //should have b64 encrypted RSA key now
    //            binkey = Convert.FromBase64String(encryptedstr);
    //        }
    //        catch (System.FormatException)
    //        {  // bad b64 data.
    //            return null;
    //        }

    //        //------ Get the 3DES 24 byte key using PDK used by OpenSSL ----

    //        SecureString despswd = GetSecPswd("Enter password to derive 3DES key==>");
    //        //Console.Write("\nEnter password to derive 3DES key: ");
    //        //String pswd = Console.ReadLine();
    //        byte[] deskey = GetOpenSSL3deskey(salt, despswd, 1, 2);    // count=1 (for OpenSSL implementation); 2 iterations to get at least 24 bytes
    //        if (deskey == null)
    //            return null;
    //        //showBytes("3DES key", deskey) ;

    //        //------ Decrypt the encrypted 3des-encrypted RSA private key ------
    //        byte[] rsakey = DecryptKey(binkey, deskey, salt); //OpenSSL uses salt value in PEM header also as 3DES IV
    //        if (rsakey != null)
    //            return rsakey;  //we have a decrypted RSA private key
    //        else
    //        {
    //            Console.WriteLine("Failed to decrypt RSA private key; probably wrong password.");
    //            return null;
    //        }
    //    }


    //    // ----- Decrypt the 3DES encrypted RSA private key ----------

    //    static byte[] DecryptKey(byte[] cipherData, byte[] desKey, byte[] IV)
    //    {
    //        MemoryStream memst = new MemoryStream();
    //        TripleDES alg = TripleDES.Create();
    //        alg.Key = desKey;
    //        alg.IV = IV;
    //        try
    //        {
    //            CryptoStream cs = new CryptoStream(memst, alg.CreateDecryptor(), CryptoStreamMode.Write);
    //            cs.Write(cipherData, 0, cipherData.Length);
    //            cs.Close();
    //        }
    //        catch (Exception exc)
    //        {
    //            Console.WriteLine(exc.Message);
    //            return null;
    //        }
    //        byte[] decryptedData = memst.ToArray();
    //        return decryptedData;
    //    }

    //    //-----   OpenSSL PBKD uses only one hash cycle (count); miter is number of iterations required to build sufficient bytes ---
    //    static byte[] GetOpenSSL3deskey(byte[] salt, SecureString secpswd, int count, int miter)
    //    {
    //        IntPtr unmanagedPswd = IntPtr.Zero;
    //        int HASHLENGTH = 16;    //MD5 bytes
    //        byte[] keymaterial = new byte[HASHLENGTH * miter];     //to store contatenated Mi hashed results


    //        byte[] psbytes = new byte[secpswd.Length];
    //        unmanagedPswd = Marshal.SecureStringToGlobalAllocAnsi(secpswd);
    //        Marshal.Copy(unmanagedPswd, psbytes, 0, psbytes.Length);
    //        Marshal.ZeroFreeGlobalAllocAnsi(unmanagedPswd);

    //        //UTF8Encoding utf8 = new UTF8Encoding();
    //        //byte[] psbytes = utf8.GetBytes(pswd);

    //        // --- contatenate salt and pswd bytes into fixed data array ---
    //        byte[] data00 = new byte[psbytes.Length + salt.Length];
    //        Array.Copy(psbytes, data00, psbytes.Length);      //copy the pswd bytes
    //        Array.Copy(salt, 0, data00, psbytes.Length, salt.Length); //concatenate the salt bytes

    //        // ---- do multi-hashing and contatenate results  D1, D2 ...  into keymaterial bytes ----
    //        MD5 md5 = new MD5CryptoServiceProvider();
    //        byte[] result = null;
    //        byte[] hashtarget = new byte[HASHLENGTH + data00.Length];   //fixed length initial hashtarget

    //        for (int j = 0; j < miter; j++)
    //        {
    //            // ----  Now hash consecutively for count times ------
    //            if (j == 0)
    //                result = data00;    //initialize 
    //            else
    //            {
    //                Array.Copy(result, hashtarget, result.Length);
    //                Array.Copy(data00, 0, hashtarget, result.Length, data00.Length);
    //                result = hashtarget;
    //                //Console.WriteLine("Updated new initial hash target:") ;
    //                //showBytes(result) ;
    //            }

    //            for (int i = 0; i < count; i++)
    //                result = md5.ComputeHash(result);
    //            Array.Copy(result, 0, keymaterial, j * HASHLENGTH, result.Length);  //contatenate to keymaterial
    //        }
    //        //showBytes("Final key material", keymaterial);
    //        byte[] deskey = new byte[24];
    //        Array.Copy(keymaterial, deskey, deskey.Length);

    //        Array.Clear(psbytes, 0, psbytes.Length);
    //        Array.Clear(data00, 0, data00.Length);
    //        Array.Clear(result, 0, result.Length);
    //        Array.Clear(hashtarget, 0, hashtarget.Length);
    //        Array.Clear(keymaterial, 0, keymaterial.Length);

    //        return deskey;
    //    }

    //    static SecureString GetSecPswd(String prompt)
    //    {
    //        SecureString password = new SecureString();

    //        Console.ForegroundColor = ConsoleColor.Gray;
    //        Console.Write(prompt);
    //        Console.ForegroundColor = ConsoleColor.Magenta;

    //        while (true)
    //        {
    //            ConsoleKeyInfo cki = Console.ReadKey(true);
    //            if (cki.Key == ConsoleKey.Enter)
    //            {
    //                Console.ForegroundColor = ConsoleColor.Gray;
    //                Console.WriteLine("");
    //                return password;
    //            }
    //            else if (cki.Key == ConsoleKey.Backspace)
    //            {
    //                // remove the last asterisk from the screen...
    //                if (password.Length > 0)
    //                {
    //                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    //                    Console.Write(" ");
    //                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
    //                    password.RemoveAt(password.Length - 1);
    //                }
    //            }
    //            else if (cki.Key == ConsoleKey.Escape)
    //            {
    //                Console.ForegroundColor = ConsoleColor.Gray;
    //                Console.WriteLine("");
    //                return password;
    //            }
    //            else if (Char.IsLetterOrDigit(cki.KeyChar) || Char.IsSymbol(cki.KeyChar))
    //            {
    //                if (password.Length < 20)
    //                {
    //                    password.AppendChar(cki.KeyChar);
    //                    Console.Write("*");
    //                }
    //                else
    //                {
    //                    Console.Beep();
    //                }
    //            }
    //            else
    //            {
    //                Console.Beep();
    //            }
    //        }
    //    }

    //    static bool CompareBytearrays(byte[] a, byte[] b)
    //    {
    //        if (a.Length != b.Length)
    //            return false;
    //        int i = 0;
    //        foreach (byte c in a)
    //        {
    //            if (c != b[i])
    //                return false;
    //            i++;
    //        }
    //        return true;
    //    }

    //    static void showBytes(String info, byte[] data)
    //    {
    //        Console.WriteLine("{0}  [{1} bytes]", info, data.Length);
    //        for (int i = 1; i <= data.Length; i++)
    //        {
    //            Console.Write("{0:X2}  ", data[i - 1]);
    //            if (i % 16 == 0)
    //                Console.WriteLine();
    //        }
    //        Console.WriteLine("\n\n");
    //    }
}*/
