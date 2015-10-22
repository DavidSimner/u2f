using Newtonsoft.Json;
using NUnit.Framework;

namespace u2f
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void ValidRegisterResponseIsParsedAsValid()
        {
            const string json = @"{
    ""type"": ""u2f_register_response"",
    ""responseData"": {
        ""version"": ""U2F_V2"",
        ""appId"": ""https://www.papaya.me.uk"",
        ""challenge"": ""AAAA"",
        ""clientData"": ""eyJ0eXAiOiJuYXZpZ2F0b3IuaWQuZmluaXNoRW5yb2xsbWVudCIsImNoYWxsZW5nZSI6IkFBQUEiLCJvcmlnaW4iOiJodHRwczovL3d3dy5wYXBheWEubWUudWsiLCJjaWRfcHVia2V5IjoiIn0"",
        ""registrationData"": ""BQSHRPuCNOj7y0wPw6ZvMqlCUQAi-5fiGnLPLwOpTJdCFhe09RROD-R0zH5ZMxbPPBKVrWEhTDOFNAlGbKoZgNf5QMm76S-vqgNMQAXW7Tocde_xIUqUdb_4gv7V5iMFFkxjs8xI8TiLTjPaw7scf0u6sI8YOLi4wjQILxKgqEE1DyowggItMIIBF6ADAgECAgQFtgV5MAsGCSqGSIb3DQEBCzAuMSwwKgYDVQQDEyNZdWJpY28gVTJGIFJvb3QgQ0EgU2VyaWFsIDQ1NzIwMDYzMTAgFw0xNDA4MDEwMDAwMDBaGA8yMDUwMDkwNDAwMDAwMFowKDEmMCQGA1UEAwwdWXViaWNvIFUyRiBFRSBTZXJpYWwgOTU4MTUwMzMwWTATBgcqhkjOPQIBBggqhkjOPQMBBwNCAAT9uN6zoe1w62NsBm62AGmWpflw_LXbiPw7MF1B5ZZvDBtUuFL-8KCQftF_O__CnU0yG5z4qEos6qA4yr011ZjeoyYwJDAiBgkrBgEEAYLECgIEFTEuMy42LjEuNC4xLjQxNDgyLjEuMTALBgkqhkiG9w0BAQsDggEBAH7T-2zMJSAT-C8hjCo32mAx0g5_MIHa_K6xKPx_myM5FL-2TWE18XziIfp2T0U-8Sc6jOlllWRCuy8eR0g_c33LyYtYU3f-9QsnDgKJ-IQ28a3PSbJiHuXjAt9VW5q3QnLgafkYFJs97E8SIosQwPiN42r1inS7RCuFrgBTZL2mcCBY_B8th5tTARHqYOhsY_F_pZRMyD8KommEiz7jiKbAnmsFlT_LuPR-g6J-AHKmPDKtZIZOkm1xEvoZl_eDllb7syvo94idDwFFUZonr92ORrBMpCkNhUC2NLiGFh51iMhimdzdZDXRZ4o6bwp0gpxN0_cMNSTR3fFteK3SG2QwRgIhAPDjlSeXy_qcN-A0GlSaNvX7yPUgrOs_jmMvpHkQlOa4AiEAjHk4_2LQJQ-jWnc4IAEjkpZRdTXnyCmIb6Q72dSWh00""
    }
}";
            var input = JsonConvert.DeserializeObject<U2fRegisterResponse>(json, Helpers.JsonSerializerSettings);

            new RegistrationValidator().Validate(input, "https://www.papaya.me.uk", "AAAA");
        }
    }
}
