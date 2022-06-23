using System.Threading.Tasks;

namespace SmartCaptchaValidator
{
    public interface ISmartCaptchaValidator
    {
        Task<SmartCaptchaVerifyResponse> Verify(SmartCaptchaVerifyRequest request);
    }
}