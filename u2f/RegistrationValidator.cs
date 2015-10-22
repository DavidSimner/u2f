using System;

namespace u2f
{
    internal class RegistrationValidator
    {
        internal void Validate(U2fRegisterResponse response, string origin, string challenge)
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

            //TODO: make this extension method, also check date of yubico, and its self-sig is correct
            response.ResponseData.RegistrationData.AttestationCertificate.CheckValidity();
            response.ResponseData.RegistrationData.AttestationCertificate.Verify(RootCertificates.Yubico.GetPublicKey());

            // TODO: check signature
        }
    }
}
