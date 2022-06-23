# YandexSmartCaptcha
Yandex SmartCaptcha позволяет определить, кем является пользователь системы: человеком или компьютером

# ConfigureOptions
```c#
private void ConfigureOptions(ServiceConfigurationContext context)
{
    context.Services.Configure<SmartCaptchaOptions>(context.Services.GetConfiguration()
        .GetSection("SmartCaptcha"));
}
```

# appsettings
```json
...
"SmartCaptcha": {
  "VerifyBaseUrl": "https://captcha-api.yandex.ru",
  "Secret": "serverKey"
}
```

# Using
```c#
var captchaResponse = await _smartCaptchaValidator.Verify(new SmartCaptchaVerifyRequest()
{
    Token = input.Captcha,
    Ip = _webClientInfoProvider.ClientIpAddress
});
if (captchaResponse.Status != "ok")
{
    throw new UserFriendlyException("Каптча");
}
```
