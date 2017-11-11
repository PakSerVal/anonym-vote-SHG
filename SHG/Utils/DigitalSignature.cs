using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities.Encoders;
using System.Text;


namespace SHG.Utils
{
    public class DigitalSignature
    {
        private RsaKeyParameters MakeKey(String modulusHexString, String exponentHexString, bool isPrivateKey)
        {
            var modulus = new Org.BouncyCastle.Math.BigInteger(modulusHexString, 16);
            var exponent = new Org.BouncyCastle.Math.BigInteger(exponentHexString, 16);

            return new RsaKeyParameters(isPrivateKey, modulus, exponent);
        }

        public bool Verify(String data, String signatureHex, String publicModulusHexString, String publicExponentHexString)
        {
            string expectedSignature = GetSignatureStringFromHex(signatureHex);
            /* Make the key */
            RsaKeyParameters key = MakeKey(publicModulusHexString, publicExponentHexString, false);

            /* Init alg */
            ISigner signer = SignerUtilities.GetSigner("SHA1withRSA");

            /* Populate key */
            signer.Init(false, key);

            /* Get the signature into bytes */
            var expectedSig = Convert.FromBase64String(expectedSignature);

            /* Get the bytes to be signed from the string */
            var msgBytes = Encoding.UTF8.GetBytes(data);

            /* Calculate the signature and see if it matches */
            signer.BlockUpdate(msgBytes, 0, msgBytes.Length);
            return signer.VerifySignature(expectedSig);
        }

        private string GetSignatureStringFromHex(string SignatureHex)
        {
            byte[] sig = StringToByteArray(SignatureHex);
            return Convert.ToBase64String(sig);

        }
        private byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }
    }
}
