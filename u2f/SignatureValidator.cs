using System;
using System.Linq;

namespace u2f
{
    internal class SignatureValidator
    {
        internal void Validate(U2fResponse<U2fSignResponseData> response, string origin, byte[] userPublicKey, byte[] keyHandle, string challenge)
        {
            if (response.Type != "u2f_sign_response"
                || !response.ResponseData.KeyHandle.SequenceEqual(keyHandle)
                || response.ResponseData.ClientData.Typ != "navigator.id.getAssertion"
                || response.ResponseData.ClientData.Challenge != challenge
                || response.ResponseData.ClientData.Origin != origin
                || response.ResponseData.ClientData.CId_PubKey != ""
                || response.ResponseData.SignatureData.UserPresenceByte != 1
                || response.ResponseData.SignatureData.Counter != 0 //TODO: another one to force it being an argument
                )
            {
                throw new ApplicationException();
            }

            EllipticCurve.LoadPublicKey(userPublicKey).ThrowIfSignatureNotOkay(response.ResponseData.SignatureData.Signature,
                Hash.String(origin),
                new byte[] { response.ResponseData.SignatureData.UserPresenceByte },
                new byte[] { 0, 0, 0, 0 }, //TODO: counter
                Hash.Array(response.ResponseData.ClientData.Raw));
        }
    }
}
