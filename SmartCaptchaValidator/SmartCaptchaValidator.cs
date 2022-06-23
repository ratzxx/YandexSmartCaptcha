using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace SmartCaptchaValidator
{
    public class SmartCaptchaValidator : ISmartCaptchaValidator
    {
        private readonly HttpClient _client;
        private readonly SmartCaptchaOptions _options;

        public SmartCaptchaValidator(IOptionsSnapshot<SmartCaptchaOptions> options,
            IHttpClientFactory clientFactory)
        {
            this._options = options.Value;
            this._client = clientFactory.CreateClient("SmartCaptcha");
            this._client.BaseAddress = new Uri(this._options.VerifyBaseUrl);
        }

        public async Task<SmartCaptchaVerifyResponse> Verify(SmartCaptchaVerifyRequest request)
        {
            HttpResponseMessage httpResponseMessage =
                await this._client.GetAsync(
                    $"validate?secret={this._options.Secret}&token={request.Token}&ip={request.Ip}");

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                return new SmartCaptchaVerifyResponse()
                {
                    Status = "false",
                    Message = httpResponseMessage.StatusCode.ToString()
                };
            }

            var response = await httpResponseMessage.Content
                .ReadAsStringAsync();
            return JsonSerializer.Deserialize<SmartCaptchaVerifyResponse>(response, new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}