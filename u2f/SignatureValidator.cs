using System;

namespace u2f
{
    internal class SignatureValidator
    {
        internal void Validate(U2fResponse<U2fSignResponseData> response, string origin, byte[] userPublicKey, byte[] keyHandle, string challenge)
        {
            if (response.Type != "u2f_sign_response"
                //TODO: KeyHandle
                || response.ResponseData.ClientData.Typ != "navigator.id.getAssertion"
                || response.ResponseData.ClientData.Challenge != challenge
                || response.ResponseData.ClientData.Origin != origin
                || response.ResponseData.ClientData.CId_PubKey != ""
                //TODO: SignatureData
                )
            {
                throw new ApplicationException();
            }
        }
    }
}
