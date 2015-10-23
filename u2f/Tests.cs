using Newtonsoft.Json;
using NUnit.Framework;
using u2f.DTOs;

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
            var input = JsonConvert.DeserializeObject<U2fResponse<U2fRegisterResponseData>>(json, Helpers.JsonSerializerSettings);

            new RegistrationValidator().Validate(input, "https://www.papaya.me.uk", "AAAA");
        }

        [Test]
        public void TheFirstValidSignResponseIsParsedAsValid()
        {
            const string json = @"{
    ""type"": ""u2f_sign_response"",
    ""responseData"": {
        ""keyHandle"": ""ybvpL6-qA0xABdbtOhx17_EhSpR1v_iC_tXmIwUWTGOzzEjxOItOM9rDuxx_S7qwjxg4uLjCNAgvEqCoQTUPKg"",
        ""clientData"": ""eyJ0eXAiOiJuYXZpZ2F0b3IuaWQuZ2V0QXNzZXJ0aW9uIiwiY2hhbGxlbmdlIjoiQkJCQiIsIm9yaWdpbiI6Imh0dHBzOi8vd3d3LnBhcGF5YS5tZS51ayIsImNpZF9wdWJrZXkiOiIifQ"",
        ""signatureData"": ""AQAAAAAwRgIhAKdaFbjBhcTvr1e3nnoADBlscqpumEHMYUoZjSRzXFCPAiEA9nY7k4b1xQYYs4whrPMnStvikF69Vd8xB6rcOOxMLR4""
    }
}";
            var input = JsonConvert.DeserializeObject<U2fResponse<U2fSignResponseData>>(json, Helpers.JsonSerializerSettings);

            new SignatureValidator().Validate(input, "https://www.papaya.me.uk", new byte[] { 4, 135, 68, 251, 130, 52, 232, 251, 203, 76, 15, 195, 166, 111, 50, 169, 66, 81, 0, 34, 251, 151, 226, 26, 114, 207, 47, 3, 169, 76, 151, 66, 22, 23, 180, 245, 20, 78, 15, 228, 116, 204, 126, 89, 51, 22, 207, 60, 18, 149, 173, 97, 33, 76, 51, 133, 52, 9, 70, 108, 170, 25, 128, 215, 249 }, new byte[] { 201, 187, 233, 47, 175, 170, 3, 76, 64, 5, 214, 237, 58, 28, 117, 239, 241, 33, 74, 148, 117, 191, 248, 130, 254, 213, 230, 35, 5, 22, 76, 99, 179, 204, 72, 241, 56, 139, 78, 51, 218, 195, 187, 28, 127, 75, 186, 176, 143, 24, 56, 184, 184, 194, 52, 8, 47, 18, 160, 168, 65, 53, 15, 42 }, "BBBB", 0);
        }

        [Test]
        public void TheSecondValidSignResponseIsParsedAsValid()
        {
            const string json = @"{
    ""type"": ""u2f_sign_response"",
    ""responseData"": {
        ""keyHandle"": ""ybvpL6-qA0xABdbtOhx17_EhSpR1v_iC_tXmIwUWTGOzzEjxOItOM9rDuxx_S7qwjxg4uLjCNAgvEqCoQTUPKg"",
        ""clientData"": ""eyJ0eXAiOiJuYXZpZ2F0b3IuaWQuZ2V0QXNzZXJ0aW9uIiwiY2hhbGxlbmdlIjoiQ0NDQyIsIm9yaWdpbiI6Imh0dHBzOi8vd3d3LnBhcGF5YS5tZS51ayIsImNpZF9wdWJrZXkiOiIifQ"",
        ""signatureData"": ""AQAAAAEwRQIhAPnt8C7pJjMZVevYGBKVaL-_1BewLIQlRgmlN4qtRviFAiAIY-iKhQQv4NmjjHY0zRbI_4BWwSTD1b2pLSbxIWX8KQ""
    }
}";
            var input = JsonConvert.DeserializeObject<U2fResponse<U2fSignResponseData>>(json, Helpers.JsonSerializerSettings);

            new SignatureValidator().Validate(input, "https://www.papaya.me.uk", new byte[] { 4, 135, 68, 251, 130, 52, 232, 251, 203, 76, 15, 195, 166, 111, 50, 169, 66, 81, 0, 34, 251, 151, 226, 26, 114, 207, 47, 3, 169, 76, 151, 66, 22, 23, 180, 245, 20, 78, 15, 228, 116, 204, 126, 89, 51, 22, 207, 60, 18, 149, 173, 97, 33, 76, 51, 133, 52, 9, 70, 108, 170, 25, 128, 215, 249 }, new byte[] { 201, 187, 233, 47, 175, 170, 3, 76, 64, 5, 214, 237, 58, 28, 117, 239, 241, 33, 74, 148, 117, 191, 248, 130, 254, 213, 230, 35, 5, 22, 76, 99, 179, 204, 72, 241, 56, 139, 78, 51, 218, 195, 187, 28, 127, 75, 186, 176, 143, 24, 56, 184, 184, 194, 52, 8, 47, 18, 160, 168, 65, 53, 15, 42 }, "CCCC", 1);
        }
    }
}
