using System;

namespace u2f
{
    internal class RegistrationValidator
    {
        internal void Validate(U2fResponse<U2fRegisterResponseData> response, string origin, string challenge)
        {
            if (response.Type != "u2f_register_response"
                || response.ResponseData.Version != "U2F_V2"
                || response.ResponseData.AppId != origin
                || response.ResponseData.Challenge != challenge
                || response.ResponseData.ClientData.Typ != "navigator.id.finishEnrollment"
                || response.ResponseData.ClientData.Challenge != challenge
                || response.ResponseData.ClientData.Origin != origin
                || response.ResponseData.ClientData.CId_PubKey != ""
                || response.ResponseData.RegistrationData.ReservedByte != 5
                || response.ResponseData.RegistrationData.KeyHandle.Length != 64 //NOTE: 64 is characterisation value; strictly speaking it only needs entropy
                )
            {
                throw new ApplicationException();
            }

            var attestationCertificate = response.ResponseData.RegistrationData.AttestationCertificate;

            attestationCertificate.ThrowIfChainNotOkay(RootCertificates.Yubico);


            attestationCertificate.GetPublicKey().ThrowIfSignatureNotOkay(response.ResponseData.RegistrationData.Signature,
                new byte[] { 0 },
                Hash.String(origin),
                Hash.Array(response.ResponseData.ClientData.Raw),
                response.ResponseData.RegistrationData.KeyHandle,
                response.ResponseData.RegistrationData.UserPublicKey);
        }
    }
}
