using Org.BouncyCastle.X509;
using System;
using System.Text;

namespace u2f
{
    internal class RegistrationValidator
    {
        private static readonly string foo = @"-----BEGIN CERTIFICATE-----
MIIDHjCCAgagAwIBAgIEG0BT9zANBgkqhkiG9w0BAQsFADAuMSwwKgYDVQQDEyNZ
dWJpY28gVTJGIFJvb3QgQ0EgU2VyaWFsIDQ1NzIwMDYzMTAgFw0xNDA4MDEwMDAw
MDBaGA8yMDUwMDkwNDAwMDAwMFowLjEsMCoGA1UEAxMjWXViaWNvIFUyRiBSb290
IENBIFNlcmlhbCA0NTcyMDA2MzEwggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEK
AoIBAQC/jwYuhBVlqaiYWEMsrWFisgJ+PtM91eSrpI4TK7U53mwCIawSDHy8vUmk
5N2KAj9abvT9NP5SMS1hQi3usxoYGonXQgfO6ZXyUA9a+KAkqdFnBnlyugSeCOep
8EdZFfsaRFtMjkwz5Gcz2Py4vIYvCdMHPtwaz0bVuzneueIEz6TnQjE63Rdt2zbw
nebwTG5ZybeWSwbzy+BJ34ZHcUhPAY89yJQXuE0IzMZFcEBbPNRbWECRKgjq//qT
9nmDOFVlSRCt2wiqPSzluwn+v+suQEBsUjTGMEd25tKXXTkNW21wIWbxeSyUoTXw
LvGS6xlwQSgNpk2qXYwf8iXg7VWZAgMBAAGjQjBAMB0GA1UdDgQWBBQgIvz0bNGJ
hjgpToksyKpP9xv9oDAPBgNVHRMECDAGAQH/AgEAMA4GA1UdDwEB/wQEAwIBBjAN
BgkqhkiG9w0BAQsFAAOCAQEAjvjuOMDSa+JXFCLyBKsycXtBVZsJ4Ue3LbaEsPY4
MYN/hIQ5ZM5p7EjfcnMG4CtYkNsfNHc0AhBLdq45rnT87q/6O3vUEtNMafbhU6kt
hX7Y+9XFN9NpmYxr+ekVY5xOxi8h9JDIgoMP4VB1uS0aunL1IGqrNooL9mmFnL2k
LVVee6/VR6C5+KSTCMCWppMuJIZII2v9o4dkoZ8Y7QRjQlLfYzd3qGtKbw7xaF1U
sG/5xUb/Btwb2X2g4InpiB/yt/3CpQXpiWX/K4mBvUKiGn05ZsqeY1gx4g0xLBqc
U9psmyPzK+Vsgw2jeRQ5JlKDyqE0hebfC1tvFu0CCrJFcw==
-----END CERTIFICATE-----"
;        private static readonly X509Certificate Yubico = new X509CertificateParser().ReadCertificate(Encoding.ASCII.GetBytes(foo));

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

            //TODO: move yubico to a different file, better name for foo, etc...
            //TODO: make this extension method, also check date of yubico, and its self-sig is correct
            response.ResponseData.RegistrationData.AttestationCertificate.CheckValidity();
            response.ResponseData.RegistrationData.AttestationCertificate.Verify(Yubico.GetPublicKey());

            // TODO: check signature
        }
    }
}
